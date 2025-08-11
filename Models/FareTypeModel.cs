using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace CoreWebAPIs.Models
{
    public class FareTypeModel
    {

        public int Id { get; set; }

        public string? FareTypeName { get; set; }

        public ClassOfServiceModel? ClassOfService{ get; set; }

        public virtual ICollection<RouteModel>? Routes { get; set; }
    }

    public class RouteModel
    {
        public int Id { get; set; }

        public string? RouteName { get; set; }

        public int  Weight{ get; set; }

        public virtual FareTypeModel? FareType { get; set; }

        public bool IsSensitiveDestination { get; set; }
    }
}
