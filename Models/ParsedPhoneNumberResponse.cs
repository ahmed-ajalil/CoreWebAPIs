namespace CoreWebAPIs.Models
{
    public class ParsedPhoneNumberResponse
    {
        public bool IsValid { get; set; }
        public int? CountryCode { get; set; }
        public ulong? NationalNumber { get; set; }
        public string? E164Format { get; set; } // The standard international format, e.g., +97339123456
        public string? OriginalNumber { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
