using System.Reflection.Metadata.Ecma335;

namespace CoreWebAPIs.Models
{
    public class ClassOfServiceModel
    {

        public int Id { get; set; }

        public string?  ClassName{ get; set; }

        public virtual ICollection<FareTypeModel>? FareTypes { get; set; }

    }
}
