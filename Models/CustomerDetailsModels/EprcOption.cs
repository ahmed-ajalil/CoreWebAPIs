using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class EprcOption
{
    public decimal Opt { get; set; }

    public string? Keywords { get; set; }

    public string? Dty { get; set; }

    public decimal? Dien { get; set; }

    public string? Descoptions { get; set; }
}
