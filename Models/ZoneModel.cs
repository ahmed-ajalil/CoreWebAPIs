using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata.Ecma335;

namespace CoreWebAPIs.Models
{
    public class ZoneModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  Id { get; set; }

        public string? ZoneName { get; set; }

        public virtual ICollection<CountryModel>?  Countries{ get; set; }

        public virtual ICollection<AirportCodeModel>? Airports { get; set; }

    }
}
