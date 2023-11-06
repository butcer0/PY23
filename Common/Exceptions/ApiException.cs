using System.Data;
using System.Net;

namespace PY23.Common.Exceptions
{
    public class ApiException: DataException
    {
        public HttpStatusCode StatusCode { get; }
        public string ContentType { get; } = "text/plain";

        public ApiException(HttpStatusCode statusCode, string message, string contentType = "text/plain")
            : base(message)
        {
            StatusCode = statusCode;
            ContentType = contentType;
        }
    }
}
