using AppointmentServiceAPI.Entities;

namespace AppointmentServiceAPI.DTOs
{
    public class ReadAppointmentDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
