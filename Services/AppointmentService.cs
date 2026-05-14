using AppointmentServiceAPI.DTOs;
using AppointmentServiceAPI.Entities;
using AppointmentServiceAPI.Repositories;
using AutoMapper;
using AppointmentServiceAPI.Services.ExternalModels;

namespace AppointmentServiceAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDoctorServiceClient _doctorServiceClient;
        private readonly IPatientServiceClient _patientServiceClient;

        public AppointmentService(IAppointmentRepository repository, IMapper mapper, IDoctorServiceClient doctorServiceClient, IPatientServiceClient patientServiceClient)
        {
            _repository = repository;
            _mapper = mapper;
            _doctorServiceClient = doctorServiceClient;
            _patientServiceClient = patientServiceClient;
        }

        public async Task BookAppointmentAsync(CreateAppointmentDto appointmentDto)
        {
            var doctor = await _doctorServiceClient.GetDoctorByIdAsync(appointmentDto.DoctorId);
            if (doctor == null)
            {
                throw new Exception("Invalid doctor ID");
            }

            var patient = await _patientServiceClient.GetPatientByIdAsync(appointmentDto.PatientId);
            if (patient == null)
            {
                throw new Exception("Invalid patient ID");
            }

            var existing = await _repository.GetAllAppointmentsAsync();

            if (existing.Any(a =>
                a.DoctorId == appointmentDto.DoctorId &&
                a.AppointmentDate == appointmentDto.AppointmentDate.Date &&
                a.Status == AppointmentStatus.Booked))
            {
                throw new Exception("Doctor already has an appointment at this time");
            }

            var appointment = _mapper.Map<Appointment>(appointmentDto);
            appointment.Status = AppointmentStatus.Booked;
            await _repository.CreateAppointmentAsync(appointment);
        }

        public async Task CancelAsync(int id)
        {
            var appointment = await _repository.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                throw new Exception("Appointment not found");
            }
            if (appointment.Status == AppointmentStatus.Completed)
            {
                throw new Exception("Cannot cancel a completed appointment");
            }
            appointment.Status = AppointmentStatus.Cancelled;
            await _repository.UpdateAppointmentAsync(appointment);
        }

        public async Task CompleteAsync(int id)
        {
            var appointment = await _repository.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                throw new Exception("Appointment not found");
            }
            if (appointment.Status == AppointmentStatus.Cancelled)
            {
                throw new Exception("Cannot complete a cancelled appointment");
            }
            appointment.Status = AppointmentStatus.Completed;
            await _repository.UpdateAppointmentAsync(appointment);
        }

        public async Task<List<ReadAppointmentDto>> GetAllAppointmentsAsync()
        {
            var appointments = await _repository.GetAllAppointmentsAsync();
            return _mapper.Map<List<ReadAppointmentDto>>(appointments);
        }

        public async Task<ReadAppointmentDto> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _repository.GetAppointmentByIdAsync(id);

            if (appointment == null)
            {
                return null;
            }
            return _mapper.Map<ReadAppointmentDto>(appointment);
        }

        public async Task<List<ReadAppointmentDto>> GetAppointmentsByDoctorIdAsync(int id)
        {
            var appointments = await _repository.GetAppointmentsByDoctorId(id);
            return _mapper.Map<List<ReadAppointmentDto>>(appointments);
        }

        public async Task<List<ReadAppointmentDto>> GetAppointmentsByPatientIdAsync(int id)
        {
            var appointments = await _repository.GetAppointmentsByPatientId(id);
            return _mapper.Map<List<ReadAppointmentDto>>(appointments);
        }

        public async Task UpdateAppointmentAsync(int id, UpdateAppointmentDto appointmentDto)
        {
            var appointment = await _repository.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                throw new Exception("Appointment not found");
            }
            if (appointment.Status == AppointmentStatus.Completed || appointment.Status == AppointmentStatus.Cancelled)
            {
                throw new Exception("Cannot update a completed or cancelled appointment");
            }
            _mapper.Map(appointmentDto, appointment);
            await _repository.UpdateAppointmentAsync(appointment);
        }
    }
}
