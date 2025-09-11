using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class AdmNoshow
{
    public string? Docnum { get; set; }

    public string? Cardocnum { get; set; }

    public DateTime? Issuedate { get; set; }

    public string? Agentcode { get; set; }

    public string? Pnr { get; set; }

    public string? Paxname { get; set; }

    public string? Cls { get; set; }

    public string? Arrival { get; set; }

    public string? Origin { get; set; }

    public string? Destination { get; set; }

    public string? Carrier { get; set; }

    public string? Flightno { get; set; }

    public DateTime? Flightdate { get; set; }

    public decimal? Fltseqnr { get; set; }
}
