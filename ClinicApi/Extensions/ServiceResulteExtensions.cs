namespace ClinicApi.Extensions;

public static class ServiceResultExtensions
{
    private static int GetStatusCode(ErrorMessageBaseStatus errorStatus)
    {
        return errorStatus switch
        {
            ErrorMessageBaseStatus.NotFound => StatusCodes.Status404NotFound,
            ErrorMessageBaseStatus.BadData => StatusCodes.Status400BadRequest,
            ErrorMessageBaseStatus.ServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
    }
    private static object ErrorMessageToObject(ErrorMessageBase error)
    {
        var status = GetStatusCode(error.Status);

        return new
        {
            error = error.ErrorMessage,
            title = error.ErrorTitle,
            status
        };
    }

    private static ObjectResult GenerateErrorResult(ErrorMessageBase error)
    {
        var errorObject = ErrorMessageToObject(error);
        var statusCode = GetStatusCode(error.Status);
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