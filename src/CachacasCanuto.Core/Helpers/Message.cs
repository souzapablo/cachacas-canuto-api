using System.Net;

namespace CachacasCanuto.Core.Helpers
{
    public class ErrorMessage
    {
        public ErrorMessage(HttpStatusCode status, string message)
        {
            Status = status;
            Message = message;
        }

        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
    }
}
