using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class AcsiInvolUpgrade
{
    public int? FltSeqNr { get; set; }

    public string? PaxName { get; set; }

    public string? Port { get; set; }

    public string? Class { get; set; }

    public string? Seat { get; set; }

    public string? Type { get; set; }

    public string? RLoc { get; set; }

    public string? Vcr { get; set; }

    public string? EdCode { get; set; }

    public DateTime? AddDate { get; set; }
}
