using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class MfcFlight
{
    public int? FltSeqNr { get; set; }

    public DateTime? FltDate { get; set; }

    public string? Origin { get; set; }

    public string? Destination { get; set; }

    public decimal? Male { get; set; }

    public decimal? Female { get; set; }

    public decimal? Child { get; set; }

    public decimal? Infant { get; set; }
}
