using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class AcsiGcdDetail
{
    public int? FltSeqNr { get; set; }

    public string? PaxName { get; set; }

    public string? Category { get; set; }

    public string? Class { get; set; }

    public string? RLoc { get; set; }

    public string? Port { get; set; }
}
