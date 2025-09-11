using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace CoreWebAPIs.Services
{
    public class LoyaltyApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public LoyaltyApiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;

            // Basic Auth setup
            var user = _config["LoyaltyApi:User"];
            var pass = _config["LoyaltyApi:Password"];
            var byteArray = Encoding.ASCII.GetBytes($"{user}:{pass}");
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            // iFly channel headers
            _httpClient.DefaultRequestHeaders.Add("x-auth-token", _config["LoyaltyApi:Token"]);
            _httpClient.DefaultRequestHeaders.Add("x-auth-channel", _config["LoyaltyApi:Channel"]);
        }

        public async Task<Dictionary<string, object>> GetAccountSummaryAsync(string membershipNumber)
        {
            // Full API endpoint
            var url = $"{_config["LoyaltyApi:BaseUrl"]}/iflyloyalty/api/member-retrieval/v50/rest/AccountSummaryService/retrieveAccountSummary";

            // Build the body payload
            var requestBody = new
            {
                companyCode = "GF",
                membershipNumber = membershipNumber,
                programCode = "FF"
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                return new Dictionary<string, object>
                {
                    { "error", true },
                    { "rawBody", await response.Content.ReadAsStringAsync() }
                };
            }

            var rawJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Dictionary<string, object>>(rawJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
    }
}
