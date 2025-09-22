using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreWebAPIs.Services
{
    public class GenesysAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public GenesysAuthService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var clientId = _config["Genesys:ClientId"];
            var clientSecret = _config["Genesys:ClientSecret"];
            var region = _config["Genesys:Region"] ?? "mypurecloud.ie";

            var authUrl = $"https://login.{region}/oauth/token";

            var authString = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

            var request = new HttpRequestMessage(HttpMethod.Post, authUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authString);
            request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            return doc.RootElement.GetProperty("access_token").GetString();
        }
    }
}
