namespace CoreWebAPIs.Models
{
    public class RetrieveCustomerDetailsRequest
    {
        public string CompanyCode { get; set; }
        public string CustomerType { get; set; }
        public string MobileNumber { get; set; }
        public string PageNumber { get; set; }
        public string AbsoluteIndex { get; set; }
        public int PageSize { get; set; }
        public TxnHeader TxnHeader { get; set; }
    }

    public class TxnHeader
    {
        public string UserName { get; set; }
        public string ChannelUserCode { get; set; }
    }

}
