using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class SixFsrfbSegment
{
    /// <summary>
    /// flight/segment
    /// </summary>
    public decimal? SegmentStatus { get; set; }

    /// <summary>
    /// flight/segment/segmentCabin/segmentLink/bookingNameItem
    /// </summary>
    public string? CabinCode { get; set; }

    /// <summary>
    /// flight/segment
    /// </summary>
    public string? Destination { get; set; }

    /// <summary>
    /// flight/segment/segmentCabin/segmentLink/bookingNameItem
    /// </summary>
    public decimal? ItemStatus { get; set; }

    /// <summary>
    /// flight/segment/segmentCabin/segmentLink/bookingNameItem
    /// </summary>
    public string? OperatingBookingClass { get; set; }

    /// <summary>
    /// flight/segment
    /// </summary>
    public string? Origin { get; set; }

    /// <summary>
    /// flight/segment/segmentCabin/segmentLink/bookingNameItem/bookingName
    /// </summary>
    public string? RawName { get; set; }

    /// <summary>
    /// flight/segment/segmentCabin/segmentLink/bookingNameItem/bookingName/serviceLine
    /// </summary>
    public decimal? ServiceLineId { get; set; }

    /// <summary>
    /// flight/segment/segmentCabin/segmentLink/bookingNameItem/bookingName/serviceLine
    /// </summary>
    public DateTime? AddingDate { get; set; }

    /// <summary>
    /// flight/segment/segmentCabin/segmentLink/bookingNameItem/bookingName/serviceLine
    /// </summary>
    public string? FreeText { get; set; }

    /// <summary>
    /// flight/segment/segmentCabin/segmentLink/bookingNameItem/bookingName/serviceLine
    /// </summary>
    public string? SecondaryType { get; set; }

    /// <summary>
    /// flight/segment/segmentCabin/segmentLink/bookingNameItem/bookingName/serviceLine
    /// </summary>
    public decimal? ServiceLineState { get; set; }

    /// <summary>
    /// flight/segment/segmentCabin/segmentLink/bookingNameItem/bookingName/serviceLine
    /// </summary>
    public string? ServiceLineTypeCode { get; set; }

    /// <summary>
    /// flight/segment
    /// </summary>
    public DateTime? DepartureDate { get; set; }

    public string? FileId { get; set; }

    public DateTime? DateSent { get; set; }
}
