using System.Net;
using System.Reflection.PortableExecutable;

namespace CoreWebAPIs.Models
{
    public class ApiResponseModel<T>
    {

        public HttpStatusCode StatusCode{ get; set; }

        public string? Message { get; set; }

        public string? Error { get; set; }

        public T? Data { get; set; }

    }


    public class DbSaveStatusModel
    {

        public bool IsSaveSuccessfully { get; set; }

        public string Error { get; set; }

    }
}
