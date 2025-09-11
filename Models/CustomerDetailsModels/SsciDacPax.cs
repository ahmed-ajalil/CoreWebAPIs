using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class SsciDacPax
{
    public string? Seatno { get; set; }

    public string? Gender { get; set; }

    public string? Paxname { get; set; }

    public string? Country { get; set; }

    public string? Ppno { get; set; }

    public DateTime? Fltdate { get; set; }

    public decimal? Fltseqnr { get; set; }

    public DateTime? Dob { get; set; }
}
