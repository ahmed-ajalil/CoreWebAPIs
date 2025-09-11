namespace CoreWebAPIs.Models
{
    public class PaxFlightDetail
    {
        public string? SEX { get; set; }
        public string? PAX_NAME { get; set; }
        public decimal? FLT_SEQ_NR { get; set; }   // NUMBER → decimal?
        public string? PAX_CLASS { get; set; }
        public string? STATUS { get; set; }
        public string? PAX_TYPE { get; set; }
        public string? PNR { get; set; }
        public string? PAXNAT { get; set; }
        public string? BOARDED { get; set; }
        public string? FFPNUM { get; set; }
        public string? FLT_NR { get; set; }
        public string? ALN_CD { get; set; }
        public decimal? LEG_SEQ_NR { get; set; }   // NUMBER → decimal?
        public string? ACTUAL_DEP_ARP_CD { get; set; }
        public string? DEP_STATION_NAME { get; set; }
        public string? ACTUAL_ARV_ARP_CD { get; set; }
        public string? ARV_STATION_NAME { get; set; }

        public DateTime? SCH_DEP_DT { get; set; }
        public DateTime? SCH_ARV_DT { get; set; }
        public DateTime? PUB_DEP_DT { get; set; }
        public DateTime? PUB_ARV_DT { get; set; }
        public DateTime? ACTUAL_DEP_DT { get; set; }
        public DateTime? ACTUAL_ARV_DT { get; set; }

        public string? CNCL_CD { get; set; }
        public string? DISRUPTED_FLAG { get; set; }
        public string? FFP_FLAG { get; set; }
        public DateTime? DEP_DT { get; set; }
        public string? SEAT_NO { get; set; }
        public string? DOC_NUMBER { get; set; }
        public string? M_CLS { get; set; }
        public string? MANIFEST_TKT_NR { get; set; }
        public string? DESTINATION { get; set; }
        public DateTime? MANIFEST_DATE { get; set; }
        public DateTime? DOB { get; set; }
        public string? PAXDOC { get; set; }
        public string? PAXBOARDPOINT { get; set; }
        public string? PASSENGER_PHONE { get; set; }
        public string? PASSENGER_EMAIL { get; set; }
    }
}
