using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class VCheckinReport
{
    public string? Doctype { get; set; }

    public string? Paxnat { get; set; }

    public string? DocNumber { get; set; }

    public string? Isscntry { get; set; }

    public string? Name { get; set; }

    public DateTime? Dob { get; set; }

    public string? Sex { get; set; }

    public string? Placissue { get; set; }

    public int? FltSeqNr { get; set; }

    public string? CCls { get; set; }

    public string? Expdt { get; set; }
}
