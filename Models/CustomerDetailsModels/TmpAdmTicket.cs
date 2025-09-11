using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class TmpAdmTicket
{
    public string? Docnum { get; set; }

    public string? Cardocnum { get; set; }

    public string? Issuedate { get; set; }

    public string? Agentcode { get; set; }

    public string? Pnr { get; set; }

    public string? Cls { get; set; }

    public string? Arrival { get; set; }

    public string? FltNr { get; set; }

    public string? FltDate { get; set; }

    public decimal? Fltseqnr { get; set; }
}
