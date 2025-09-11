using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class PnrFinder
{
    public int? FltSeqNr { get; set; }

    public string? TktNr { get; set; }

    public string? Pnr { get; set; }

    public string? MCls { get; set; }
}
