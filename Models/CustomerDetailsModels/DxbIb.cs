using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class DxbIb
{
    public decimal Fltseqnr { get; set; }

    public decimal? Inj { get; set; }

    public decimal? Iny { get; set; }

    public string? Dest { get; set; }

    public DateTime? Adddate { get; set; }
}
