using System.Net;

namespace CachacasCanuto.API.Controllers.Shared
{
    public class CustomResponse
    {
        public HttpStatusCode StatusCode { get; private set; }
        public bool Success { get; private set; }
        public object Data { get; private set; } = null!;
        public IEnumerable<string> Errors { get; private set; } = null!;

        public CustomResponse(HttpStatusCode statusCode, bool success)
        {
            StatusCode = statusCode;
            Success = success;
        }

        public CustomResponse(HttpStatusCode statusCode, bool success, object data) : this(statusCode, success) =>
            Data = data;

        public CustomResponse(HttpStatusCode statusCode, bool success, IEnumerable<string> errors) : this(statusCode, success) =>
            Errors = errors;

        public CustomResponse(HttpStatusCode statusCode, bool success, object data, IEnumerable<string> errors) : this(statusCode, success, data) =>
            Errors = errors;
    }
}
