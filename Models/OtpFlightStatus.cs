using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models;

public partial class OtpFlightStatus
{
    public int Id { get; set; }

    public string FltSeqNr { get; set; } = null!;

    public DateTime FltDt { get; set; }

    public string FltNr { get; set; } = null!;

    public int LegSeqNr { get; set; }

    public string? OpSuffix { get; set; }

    public DateTime SchDepDt { get; set; }

    public DateTime PubSchDepDt { get; set; }

    public DateTime SchArvDt { get; set; }

    public DateTime PubSchArvDt { get; set; }

    public DateTime? ActualOffblocks { get; set; }

    public DateTime? ActualOnblocks { get; set; }

    public DateTime? ActualAirborne { get; set; }

    public DateTime? ActualLanding { get; set; }

    public DateTime? LatestDepDt { get; set; }

    public DateTime? LatestArvDt { get; set; }

    public string? SchDepArpCd { get; set; }

    public string? SchArvArpCd { get; set; }

    public string? LatestDepArpCd { get; set; }

    public string? LatestArvArpCd { get; set; }

    public string? SchEqpCd { get; set; }

    public string? LatestEqpCd { get; set; }

    public string? LatestEqpCdType { get; set; }

    public string? SchTailNr { get; set; }

    public string? LatestTailNr { get; set; }

    public string? CnclCd { get; set; }

    public string? LocalRemarks { get; set; }

    public string? CancelledTime { get; set; }

    public string? SvcType { get; set; }

    public string? FltManipCode { get; set; }

    public string? LegStatus { get; set; }

    public string CarrierCode { get; set; } = null!;

    public DateTime EstDepDt { get; set; }

    public DateTime EstArvDt { get; set; }

    public DateTime? LastUpdated { get; set; }

    public string? PreStartStatus { get; set; }

    public string? PreEndStatus { get; set; }

    public string? GrdReturn { get; set; }

    public string? DepGates { get; set; }
}
