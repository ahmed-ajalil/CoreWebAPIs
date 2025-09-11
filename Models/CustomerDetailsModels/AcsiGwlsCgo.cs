using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class AcsiGwlsCgo
{
    public int? FltSeqNr { get; set; }

    public string? Destination { get; set; }

    public decimal? Cargo { get; set; }

    public decimal? Mail { get; set; }

    public short? BagNos { get; set; }

    public int? BagWeight { get; set; }

    public string? Filler { get; set; }
}
