using AppointmentServiceAPI.DTOs.External;

namespace AppointmentServiceAPI.Services.ExternalModels
{
    public interface IPatientServiceClient
    {
        Task <PatientLookupDto> GetPatientByIdAsync(int id);
    }
}
