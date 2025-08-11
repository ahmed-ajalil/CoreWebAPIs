using System.Text.Json.Serialization;

namespace CoreWebAPIs.Models
{
    public class TripSearchResponse
    {
        public object? SearchQuery { get; set; }
        public List<ReservationDetailResponse> Results { get; set; } = new();
    }
}
