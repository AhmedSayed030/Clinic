using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicDataBusinessLayer.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogServerError(this ILogger logger, ServerErrorMessage errorMessage, string operationName)
        {
            logger.LogError(errorMessage.Exception, "An error occurred while {OperationName}: {ErrorMessage}", operationName, errorMessage.Exception.Message);
        }
    }
}
