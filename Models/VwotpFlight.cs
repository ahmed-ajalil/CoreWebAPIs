namespace CoreWebAPIs.Models
{
    public partial class VwotpFlight
    {
        //public int? FltSeqNr { get; set; }

        public DateTime? FltDt { get; set; }

        public string? FltNr { get; set; }

        public string? AlnCd { get; set; }

        public int? LegSeqNr { get; set; }

        public string? ActualDepArpCd { get; set; }

        public string? ActualArvArpCd { get; set; }

        public string? ActualDivArpCd { get; set; }

        public string? ArvArpCd { get; set; }

        public string? DepArpCd { get; set; }

        public DateTime? SchDepDt { get; set; }

        public DateTime? SchArvDt { get; set; }

        public DateTime? PubDepDt { get; set; }

        public DateTime? PubArvDt { get; set; }

        public DateTime? ActualDepDt { get; set; }

        public DateTime? ActualArvDt { get; set; }

        public string? LatestTailNr { get; set; }

        public string? LatestEqpCd { get; set; }

        public string? ScheduledEqpCd { get; set; }

        public string? CnclCd { get; set; }

        public string? DlyCd1 { get; set; }

        public string? DlyCd2 { get; set; }

        public string? DlyCd3 { get; set; }

        public string? DlyCd4 { get; set; }

        public string? DlyCd5 { get; set; }

        public decimal? DlyTm1 { get; set; }

        public decimal? DlyTm2 { get; set; }

        public decimal? DlyTm3 { get; set; }

        public decimal? DlyTm4 { get; set; }

        public decimal? DlyTm5 { get; set; }

        public string? DlyRemark1 { get; set; }

        public string? DlyRemark2 { get; set; }

        public string? DlyRemark3 { get; set; }

        public string? DlyRemark4 { get; set; }

        public string? DlyRemark5 { get; set; }

        public byte? TaxiInTm { get; set; }

        public byte? TaxiOutTm { get; set; }

        public DateTime? ActualOffblocks { get; set; }

        public DateTime? ActualAirborne { get; set; }

        public DateTime? ActualLanding { get; set; }

        public DateTime? ActualOnblocks { get; set; }

        public DateTime? MvaActualOffblocks { get; set; }

        public DateTime? MvaActualAirborne { get; set; }

        public DateTime? MvaActualLanding { get; set; }

        public DateTime? MvaActualOnblocks { get; set; }

        public int? TotFltTm { get; set; }

        public string? FromSkdSys { get; set; }

        public string? FltManipCode { get; set; }

        public string? DivSuffix { get; set; }

        public string? OpSuffix { get; set; }

        public DateTime? CrewDepDt { get; set; }

        public string? DlyCd6 { get; set; }

        public string? DlyCd7 { get; set; }

        public string? DlyCd8 { get; set; }

        public string? DlyCd9 { get; set; }

        public string? DlyCd10 { get; set; }

        public decimal? DlyTm6 { get; set; }

        public decimal? DlyTm7 { get; set; }

        public decimal? DlyTm8 { get; set; }

        public decimal? DlyTm9 { get; set; }

        public decimal? DlyTm10 { get; set; }

        public string? DlyRemark6 { get; set; }

        public string? DlyRemark7 { get; set; }

        public string? DlyRemark8 { get; set; }

        public string? DlyRemark9 { get; set; }

        public string? DlyRemark10 { get; set; }

        public string? Depregion { get; set; }

        public string? Arvregion { get; set; }

        public string? OpFrtg { get; set; }

        public string? Fltcount { get; set; }

        public decimal? Depdelay { get; set; }

        public decimal? Arvdelay { get; set; }

        public decimal? Dep15delay { get; set; }

        public decimal? Dep15delaycount { get; set; }

        public decimal? Arv15delay { get; set; }

        public decimal? Arv15delaycount { get; set; }

        public DateTime? Fltdtlocal { get; set; }
    }
}
