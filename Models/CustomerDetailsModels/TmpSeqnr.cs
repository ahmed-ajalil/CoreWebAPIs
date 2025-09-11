using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class TmpSeqnr
{
    public decimal? Fltseqnr { get; set; }

    public string? Fltnr { get; set; }

    public string? Org { get; set; }

    public string? Dest { get; set; }

    public DateTime? Fltdate { get; set; }
}
