using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace CoreWebAPIs.Models
{
    public class AirportCodeModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }

        public string City{ get; set; }

        public string Code { get; set; }

        public string AirportName { get; set; }

        public virtual CountryModel Country { get; set; }
        public virtual ZoneModel Zone { get; set; }

    }
}
