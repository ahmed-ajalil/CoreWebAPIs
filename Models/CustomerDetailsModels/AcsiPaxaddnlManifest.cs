using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class AcsiPaxaddnlManifest
{
    public int? FltSeqNr { get; set; }

    public string? Destination { get; set; }

    public DateTime? Adddate { get; set; }

    public string? Pnr { get; set; }

    public DateTime? Issuedt { get; set; }

    public string? Paxdoc { get; set; }
}
