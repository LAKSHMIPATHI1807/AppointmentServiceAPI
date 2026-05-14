using AppointmentServiceAPI.DTOs.External; 

namespace AppointmentServiceAPI.Services.ExternalModels
{
    public interface IDoctorServiceClient
    {
        Task<DoctorLookupDto> GetDoctorByIdAsync(int id);
    }
}
