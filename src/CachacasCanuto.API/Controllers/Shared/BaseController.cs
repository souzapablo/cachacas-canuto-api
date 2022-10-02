using CachacasCanuto.Application.Common.Extensions;
using CachacasCanuto.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CachacasCanuto.API.Controllers.Shared
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult CustomResponse(object? result)
        {
            var messageHandler = HttpContext is not null ? HttpContext.RequestServices.GetService<IMessageHandler>() : default;

            if (result is null && messageHandler?.HasMessage == true)
            {
                var error = messageHandler.Messages.First();
                return Response(error.Status, error.Message);
            }

            return Response(HttpStatusCode.OK, result);
        }

        protected new JsonResult Response(HttpStatusCode statusCode, object? data, string? errorMessage)
        {

            CustomResponse result;
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                var success = statusCode.IsSuccess();

                if (data != null)
                    result = new CustomResponse(statusCode, success, data);
                else
                    result = new CustomResponse(statusCode, success);
            }
            else
            {
                var errors = new List<string>();

                if (!string.IsNullOrWhiteSpace(errorMessage))
                    errors.Add(errorMessage);

                result = new CustomResponse(statusCode, false, errors);
            }
            return new JsonResult(result) { StatusCode = (int)result.StatusCode };
        }

        protected new JsonResult Response(HttpStatusCode statusCode, object? result) =>
            Response(statusCode, result, null);

        protected new JsonResult Response(HttpStatusCode statusCode, string errorMessage) =>
            Response(statusCode, null, errorMessage);
    }
}
