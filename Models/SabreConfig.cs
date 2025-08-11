namespace CoreWebAPIs.Models
{
    public class SabreConfig
    {
        public string ApiUrl { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PCC { get; set; } = string.Empty; // PseudoCityCode
        public string TripSearchCPAId { get; set; } = string.Empty;



        public string CertApiUrl { get; set; } = string.Empty;
        public string CertUsername { get; set; } = string.Empty;
        public string CertPassword { get; set; } = string.Empty;
        public string CertPCC { get; set; } = string.Empty; // PseudoCityCode
        public string CertTripSearchCPAId { get; set; } = string.Empty;
    }
}
