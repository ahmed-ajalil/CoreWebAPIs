using System.ComponentModel.DataAnnotations;

namespace CoreWebAPIs.Models
{
    public partial class otpGatesdep
    {
        public int Id { get; set; }
        public DateTime? FltDt { get; set; }
        public string FltNr { get; set; }
        public string Dep { get; set; }
        public string Arr { get; set; }
        public string Gat { get; set; }
        public DateTime? Bstpln { get; set; }
        public DateTime? Gclpln { get; set; }
    }
}
