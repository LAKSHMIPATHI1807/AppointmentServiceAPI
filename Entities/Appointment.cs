using AppointmentServiceAPI.DTOs.External;
//using DoctorServiceAPI.Entities;
//using PatientServiceAPI.Entities;
using System.Numerics;

namespace AppointmentServiceAPI.Entities
{
    public class Appointment
    {
        public int Id {  get; set; }

        
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; }
        //public Patient Patient { get; set; }
        //public Doctor Doctor { get; set; }
    }
}

public enum AppointmentStatus
{
    Booked = 1,
    Cancelled = 2,
    Completed = 3
}