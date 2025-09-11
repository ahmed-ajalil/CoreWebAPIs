using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class Mybidt1
{
    public DateTime? TxDate { get; set; }

    public string? IataNumber { get; set; }

    public string? OfficeId { get; set; }

    public string? OfficeName { get; set; }

    public string? RegionName { get; set; }

    public string? CountryName { get; set; }

    public string? DistributionChannel { get; set; }

    public decimal? AddedBookings { get; set; }

    public decimal? CancelledBookings { get; set; }

    public decimal? NetBookings { get; set; }

    public decimal? TotalAda { get; set; }
}
