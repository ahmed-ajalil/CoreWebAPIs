using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class AcsiDxbManifest
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

    public DateTime? IssueDate { get; set; }
}
