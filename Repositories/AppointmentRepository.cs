using AppointmentServiceAPI.Data;
using AppointmentServiceAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppointmentServiceAPI.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppointmentDbContext _dbcontext;
        public AppointmentRepository(AppointmentDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task CreateAppointmentAsync(Appointment appointment)
        {
            _dbcontext.Appointments.Add(appointment);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return await _dbcontext.Appointments.ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            var appointment =await _dbcontext.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return null;
            }
            return appointment;
        }

        public async Task<List<Appointment>> GetAppointmentsByDoctorId(int id)
        {
            var appointments = await _dbcontext.Appointments.
                Where(x => x.DoctorId == id).ToListAsync();
            return appointments;
        }

        public async Task<List<Appointment>> GetAppointmentsByPatientId(int id)
        {
            var appointments = await _dbcontext.Appointments.
                Where (x => x.PatientId == id).ToListAsync();
            return appointments;
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            _dbcontext.Appointments.Update(appointment);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
