using System.ComponentModel.DataAnnotations;

namespace CoreWebAPIs.Models
{
    public class PhoneNumberRequest
    {
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "RegionCode must be a 2-letter ISO code.")]
        public string RegionCode { get; set; } = "BH"; 
    }
}
