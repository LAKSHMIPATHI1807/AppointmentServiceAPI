using AppointmentServiceAPI.Entities;

namespace AppointmentServiceAPI.Repositories
{
    public interface IAppointmentRepository
    {
        Task CreateAppointmentAsync(Appointment appointment);
        Task <List<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetAppointmentByIdAsync(int id);
        Task UpdateAppointmentAsync(Appointment appointment);

        Task<List<Appointment>> GetAppointmentsByPatientId(int id);
        Task <List<Appointment>> GetAppointmentsByDoctorId(int id);
    }
}
