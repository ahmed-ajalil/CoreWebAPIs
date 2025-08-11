namespace CoreWebAPIs.Models
{
    public partial class VwotpQregularity
    {
        public string? RegDesc { get; set; }

        public string? FltNr { get; set; }

        public DateTime? PubDepDt { get; set; }

        public string? Route { get; set; }

        public string? Comments { get; set; }

        public decimal? FltSeqNr { get; set; }

        public string? RegFltsRegCd { get; set; }

        public string? RegFltsComments { get; set; }

        public string? LatestEqpCd { get; set; }

        public string? LatestTailNr { get; set; }

        public decimal? ActualPax { get; set; }

        public decimal? BookPax { get; set; }

        public string? CnclDate { get; set; }

        public string? FPax { get; set; }

        public string? JPax { get; set; }

        public string? YPax { get; set; }

        public DateTime? Loaddate { get; set; }
    }
}
