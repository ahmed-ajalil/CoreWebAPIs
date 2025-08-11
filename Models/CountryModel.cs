using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebAPIs.Models
{
    public class CountryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Country { get; set; }

        public virtual ZoneModel? Zone { get; set; }

        public virtual ICollection<AirportCodeModel>? Airports { get; set; }
    }
}
