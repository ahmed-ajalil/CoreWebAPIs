using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreWebAPIs.Services
{
    public class GenesysCallbackService
    {
        private readonly HttpClient _http;

        public GenesysCallbackService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> CreateCallback(string queueId, string name, string phone, string callerIdName, string token)
        {
            var body = new
            {
                queueId = queueId,
                callbackUserName = name,
                callbackNumbers = new[] { phone },
                callerId = phone,
                callerIdName = callerIdName
            };

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await _http.PostAsync("https://api.mypurecloud.ie/api/v2/conversations/callbacks", content);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
