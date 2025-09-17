using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreWebAPIs.Services
{
    public class FlightStatusService
    {
        private readonly HttpClient _httpClient;

        public FlightStatusService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FlightStatusResponse?> GetFlightStatusAsync(string flightNumber, string flightDate)
        {
            var url = $"https://gfflightstatus.azurewebsites.net/api/flightStatus/{flightNumber}/{flightDate}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<FlightStatusResponse>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }

    public class FlightStatusResponse
    {
        public string FlightNumber { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public string ScheduledDeparture { get; set; }
        public string ActualDeparture { get; set; }
        public string ScheduledArrival { get; set; }
        public string ActualArrival { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
    }
}
