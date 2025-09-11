using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class VFltDatum
{
    public string? PaxName { get; set; }

    public decimal? FltSeqNr { get; set; }

    public DateTime? Adddate { get; set; }

    public string? FltNr { get; set; }

    public string? AlnCd { get; set; }

    public string? Isscntry { get; set; }

    public string? ActualDepArpCd { get; set; }

    public string? ActualArvArpCd { get; set; }

    public DateTime? SchDepDt { get; set; }

    public DateTime? SchArvDt { get; set; }

    public string? PassengerPhone { get; set; }
}
