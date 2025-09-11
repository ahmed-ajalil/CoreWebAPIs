using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class SixFsrPax
{
    public decimal? Dcspaxid { get; set; }

    public string? Cabincode { get; set; }

    public string? Secondarytype { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Title { get; set; }

    public string? Freetext { get; set; }

    public string? FileId { get; set; }

    public DateTime? DateSent { get; set; }

    public decimal? FltSeqNr { get; set; }
}
