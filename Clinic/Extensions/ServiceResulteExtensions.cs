using ClinicDataBusinessLayer.Services.ServiceResults;
using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ErrorMessages.Status;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApi.Extensions
{
    public static class ServiceResultExtensions
    {
        private static object ErrorMessageToObject(ErrorMessageBase error)
        {
            var status = error.Status switch
            {
                ErrorMessageStatus.NotFound => StatusCodes.Status404NotFound,
                ErrorMessageStatus.BadData => StatusCodes.Status400BadRequest,
                ErrorMessageStatus.ServerError => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };

            return new
            {
                error = error.ErrorMessage,
                title = error.ErrorTitle,
                status
            };
        }

        private static IActionResult GenerateErrorResult(ErrorMessageBase error)
        {
            var errorObject = ErrorMessageToObject(error);
            var statusCode = (int)errorObject.GetType().GetProperty("status")?.GetValue(errorObject)!;
            return new ObjectResult(errorObject) { StatusCode = statusCode };
        }

        public static IActionResult MapToActionResult(this IServiceResult serviceResult)
        {
            return serviceResult.Status switch
            {
                ServiceResultStatus.Success => new ObjectResult(null) { StatusCode = StatusCodes.Status200OK },
                ServiceResultStatus.NotFound or ServiceResultStatus.BadData or ServiceResultStatus.ServerError =>
                    GenerateErrorResult(serviceResult.Error!),
                _ => throw new NotImplementedException(),
            };
        }

        public static IActionResult MapToActionResult<T>(this IServiceResult<T> serviceResult)
        {
            return serviceResult.Status switch
            {
                ServiceResultStatus.Success => new ObjectResult(serviceResult.Data) { StatusCode = StatusCodes.Status200OK },
                ServiceResultStatus.NotFound or ServiceResultStatus.BadData or ServiceResultStatus.ServerError =>
                    GenerateErrorResult(serviceResult.Error!),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
