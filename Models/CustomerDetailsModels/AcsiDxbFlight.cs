using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class AcsiDxbFlight
{
    public string? Fltno { get; set; }

    public DateTime? Fltdate { get; set; }

    public string? Org { get; set; }

    public string? Dest { get; set; }

    public decimal? Totalpax { get; set; }

    public decimal? Male { get; set; }

    public string? Female { get; set; }

    public decimal? Inf { get; set; }

    public DateTime? Adddate { get; set; }
}
