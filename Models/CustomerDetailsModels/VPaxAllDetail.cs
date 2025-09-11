using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class VPaxAllDetail
{
    public string? Sex { get; set; }

    public string? PaxName { get; set; }

    public decimal? FltSeqNr { get; set; }

    public string? PaxClass { get; set; }

    public string? Status { get; set; }

    public string? PaxType { get; set; }

    public string? Pnr { get; set; }

    public string? Paxnat { get; set; }

    public string? Boarded { get; set; }

    public string? Ffpnum { get; set; }

    public string? FltNr { get; set; }

    public string? AlnCd { get; set; }

    public decimal? LegSeqNr { get; set; }

    public string? ActualDepArpCd { get; set; }

    public string? DepStationName { get; set; }

    public string? ActualArvArpCd { get; set; }

    public string? ArvStationName { get; set; }

    public DateTime? SchDepDt { get; set; }

    public DateTime? SchArvDt { get; set; }

    public DateTime? PubDepDt { get; set; }

    public DateTime? PubArvDt { get; set; }

    public DateTime? ActualDepDt { get; set; }

    public DateTime? ActualArvDt { get; set; }

    public string? CnclCd { get; set; }

    public string? DisruptedFlag { get; set; }

    public string? FfpFlag { get; set; }

    public DateTime? DepDt { get; set; }

    public string? SeatNo { get; set; }

    public string? DocNumber { get; set; }

    public string? MCls { get; set; }

    public string? ManifestTktNr { get; set; }

    public string? Destination { get; set; }

    public DateTime? ManifestDate { get; set; }

    public DateTime? Dob { get; set; }

    public string? Paxdoc { get; set; }

    public string? Paxboardpoint { get; set; }

    public string? PassengerPhone { get; set; }

    public string? PassengerEmail { get; set; }
}
