using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class EprcStaff
{
    public string Staffno { get; set; } = null!;

    public decimal? Opt { get; set; }

    public string? Station { get; set; }
}
