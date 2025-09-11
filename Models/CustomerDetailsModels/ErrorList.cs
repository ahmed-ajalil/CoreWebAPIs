using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class ErrorList
{
    public DateTime? Fdate { get; set; }

    public string? Fltnum { get; set; }

    public string? Brd { get; set; }

    public string? Off { get; set; }

    public string? Class { get; set; }
}
