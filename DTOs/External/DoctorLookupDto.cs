using System.ComponentModel.DataAnnotations;

namespace AppointmentServiceAPI.DTOs.External
{
    public class DoctorLookupDto
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Speicalization { get; set; }
    }
}
