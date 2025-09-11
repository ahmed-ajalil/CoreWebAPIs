using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class VAcsiPaxManifest
{
    public string? SeatNo { get; set; }

    public string? Sex { get; set; }

    public string? Name { get; set; }

    public string? DocNumber { get; set; }

    public int? FltSeqNr { get; set; }

    public string? MCls { get; set; }

    public string? CCls { get; set; }

    public string? TktNr { get; set; }

    public string? Status { get; set; }

    public string? Destination { get; set; }

    public DateTime? Adddate { get; set; }

    public string? PaxType { get; set; }

    public DateTime? Dob { get; set; }

    public string? Pnr { get; set; }

    public string? Docissuedt { get; set; }

    public string? Paxdoc { get; set; }

    public string? Splsrvc { get; set; }

    public string? Splcode { get; set; }

    public decimal? Bagwt { get; set; }

    public decimal? Bagcount { get; set; }

    public string? Filename { get; set; }

    public string? Paxboardpoint { get; set; }

    public string? Paxnat { get; set; }
}
