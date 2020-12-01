using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFoundry.CloudController.Common
{
    public static class Log
    {
        private static Action<ILogger, string, Exception> _informationAction
            = LoggerMessage.Define<string>(LogLevel.Information, new EventId(101, "Information"), "{Param1}");
        private static Action<ILogger, string, Exception> _errorAction =
            LoggerMessage.Define<string>(LogLevel.Error, new EventId(500, "Error"), "{param1}");
        public static void LogCCInformation(this ILogger logger, string information)
        {
            _informationAction(logger, information, null);
        }
        public static void LogCCError(this ILogger logger, string addtionalInfo, Exception ex)
        {
            _errorAction(logger, addtionalInfo, ex);
        }

    }
}
