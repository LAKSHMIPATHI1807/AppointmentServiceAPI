using System.Net.Http.Json;
using AppointmentServiceAPI.DTOs.External;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AppointmentServiceAPI.Services.ExternalModels
{
    public class DoctorServiceClient : IDoctorServiceClient
    {
        private readonly HttpClient _httpClient;
        public DoctorServiceClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("DoctorService");
        }

        public async Task<DoctorLookupDto> GetDoctorByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"GetDoctorById/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Doctor Service Error: {response.StatusCode} - {error}");
            }
            return await response.Content.ReadFromJsonAsync<DoctorLookupDto>();
        }
    }
}
