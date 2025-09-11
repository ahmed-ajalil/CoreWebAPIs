using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class SixPtc
{
    public decimal? FltSeqNr { get; set; }

    public decimal? Dcsflightlegid { get; set; }

    public decimal? Operatingflightnumber { get; set; }

    public string? Operatingcarrier { get; set; }

    public string? Origin { get; set; }

    public string? Destination { get; set; }

    public DateTime? Scheduleddeparturedatetime { get; set; }

    public decimal? Stafftravelcount { get; set; }

    public string? FileId { get; set; }

    public DateTime? DateSent { get; set; }
}
