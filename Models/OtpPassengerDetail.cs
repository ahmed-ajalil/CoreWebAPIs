using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models;

public partial class OtpPassengerDetails
{
    public long FlightSequenceNumber { get; set; }

    public int? Total { get; set; }

    public int? First { get; set; }

    public int? Business { get; set; }

    public int? Coach { get; set; }

    public int? Adult { get; set; }

    public int? Child { get; set; }

    public int? Infant { get; set; }

    public string? SeatConfig { get; set; }

    public string? YSeatConfig { get; set; }

    public string? JSeatConfig { get; set; }

    public int? BookedJ { get; set; }

    public int? BookedY { get; set; }

    public virtual OtpFlightInfo? FlightSequenceNumberNavigation { get; set; } // Make this property nullable
}