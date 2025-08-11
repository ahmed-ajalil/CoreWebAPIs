using System.Text.Json.Serialization;

namespace CoreWebAPIs.Models
{
    public class FfpSearchRequest
    {
    //    [JsonPropertyName("programId")]
    //    public string ProgramId { get; set; } = string.Empty;

        [JsonPropertyName("membershipId")]
        public string MembershipId { get; set; } = string.Empty;

        //[JsonPropertyName("airlineCode")]
        //public string AirlineCode { get; set; } = string.Empty;
    }
}
