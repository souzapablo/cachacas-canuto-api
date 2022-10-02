using System.Net;

namespace CachacasCanuto.Application.Common.Extensions
{
    public static class HttpExtensions
    {
        public static bool IsSuccess(this HttpStatusCode statusCode) =>
            new HttpResponseMessage(statusCode).IsSuccessStatusCode;
    }
}
