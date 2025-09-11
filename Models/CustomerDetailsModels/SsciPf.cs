using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class SsciPf
{
    public int? FltSeqNr { get; set; }

    public string? PaxName { get; set; }

    public string? Category { get; set; }

    public string? Cls { get; set; }

    public string? Arrival { get; set; }

    public string? FfpNo { get; set; }

    public string? Rloc { get; set; }
}
