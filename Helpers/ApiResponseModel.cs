using System.Net;

namespace CoreWebAPIs.Helpers
{
    public class ApiResponseModel
    {
        public HttpStatusCode StatusCode { get; set; }

        public string? Message { get; set; }

        public List<ApiResponseError>? Errors { get; set; }

        public dynamic? Data { get; set; }
    }


    public class ApiResponseError
    {
        public string? ErrorMessag { get; set; }
    }
}
