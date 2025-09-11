using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class ResCatBooked
{
    public int FltSeqNr { get; set; }

    public string Class { get; set; } = null!;

    public string? BoardingPoint { get; set; }

    public byte? Capacity { get; set; }

    public byte? Booked { get; set; }

    public byte? Pad { get; set; }

    public string? Dest { get; set; }

    public DateTime? FltDtGmt { get; set; }

    public DateTime? AddDate { get; set; }
}
