using System.Text.Json.Serialization;

namespace CoreWebAPIs.Models
{
    public class PhoneSearchRequest
    {
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; } = string.Empty;

        //[JsonPropertyName("airlineCode")]
        //public string AirlineCode { get; set; } = string.Empty;
    }
}
