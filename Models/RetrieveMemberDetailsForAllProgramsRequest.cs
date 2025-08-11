namespace CoreWebAPIs.Models
{
    public class RetrieveMemberDetailsForAllProgramsRequest
    {
        public string CompanyCode { get; set; }
        public string CustomerNumber { get; set; }
        public RCTxnHeader TxnHeader { get; set; }
    }
}
