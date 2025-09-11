using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class CuCdgSncf
{
    public string FltNo { get; set; } = null!;

    public DateTime FltDate { get; set; }

    public string TktNo { get; set; } = null!;

    public string? IssueDate { get; set; }

    public string? Pnr { get; set; }

    public string? PaxName { get; set; }

    public string? SncfFltNo { get; set; }

    public string? SncfFltDate { get; set; }

    public string? SncfOrigin { get; set; }

    public string? SncfDestination { get; set; }

    public string? SncfFareBasis { get; set; }

    public string? SncfStatus { get; set; }

    public string? SncfClass { get; set; }
}
