using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class SsciPending
{
    public string? FltNr { get; set; }

    public string? ActualDepArpCd { get; set; }

    public int? FltSeqNr { get; set; }

    public string? ActualArvArpCd { get; set; }
}
