using System.ComponentModel.DataAnnotations;

namespace AppointmentServiceAPI.DTOs.External
{
    public class PatientLookupDto
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
    }
}
