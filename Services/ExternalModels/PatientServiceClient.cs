using System.Net.Http.Json;
using AppointmentServiceAPI.DTOs.External;

namespace AppointmentServiceAPI.Services.ExternalModels
{
    public class PatientServiceClient : IPatientServiceClient
    {
        private readonly HttpClient _httpClient;
        public PatientServiceClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("PatientService");
        }

        public async Task<PatientLookupDto> GetPatientByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"GetPatientById/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"PatientServce Error: {response.StatusCode} - {error}");
            }
            return await response.Content.ReadFromJsonAsync<PatientLookupDto>();
        }
    }
}
