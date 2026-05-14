using AppointmentServiceAPI.DTOs;

namespace AppointmentServiceAPI.Services
{
    public interface IAppointmentService
    {
        Task BookAppointmentAsync(CreateAppointmentDto appointment);
        Task<List<ReadAppointmentDto>> GetAllAppointmentsAsync();
        Task <ReadAppointmentDto> GetAppointmentByIdAsync(int id);
        Task UpdateAppointmentAsync(int id, UpdateAppointmentDto appointment);
        Task CancelAsync(int id);
        Task CompleteAsync(int id);
        Task <List<ReadAppointmentDto>> GetAppointmentsByPatientIdAsync(int id);

        Task <List<ReadAppointmentDto>> GetAppointmentsByDoctorIdAsync(int id);
    }
}
