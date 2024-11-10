namespace ClinicDataBusinessLayer.Extensions;

public static class LoggerExtensions
{
    public static void LogServerError(this ILogger logger, ServerError error, string operationName)
    {
        logger.LogError(error.Exception,
            "An error occurred while {OperationName}: {ErrorMessage}",
            operationName,
            error.Exception.Message
        );
    }
}