using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class AcsiGwl
{
    public int? FltSeqNr { get; set; }

    public DateTime? FltDate { get; set; }

    public string? FltNr { get; set; }

    public string? Origin { get; set; }

    public string? Destination { get; set; }

    public int? TotTfcLoad { get; set; }

    public int? DryOprWeight { get; set; }

    public int? ZerFulWtAct { get; set; }

    public int? TakOffFuel { get; set; }

    public int? TakOffWtAct { get; set; }

    public int? TripFul { get; set; }

    public int? LndgWtAct { get; set; }

    public int? TxiOutFul { get; set; }

    public decimal? Doi { get; set; }

    public decimal? Lizfw { get; set; }

    public decimal? Litow { get; set; }

    public decimal? Maczfw { get; set; }

    public decimal? Mactow { get; set; }

    public string? Soc { get; set; }
}
