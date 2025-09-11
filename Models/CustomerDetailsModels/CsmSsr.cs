using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class CsmSsr
{
    public int? FltSeqNr { get; set; }

    public DateTime? FltDate { get; set; }

    public string? FltNr { get; set; }

    public string? Origin { get; set; }

    public string? Destination { get; set; }

    public string? PaxName { get; set; }

    public string? BookingClass { get; set; }

    public string? ReqType { get; set; }
}
