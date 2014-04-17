using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace GSA.Search.WebApi.Logging
{
    /// <summary>
    /// Encapsulates all handling of logging for the application
    /// </summary>
    public static class LogManager
    {
        private static ILog Logger { get { return log4net.LogManager.GetLogger("WebApiUsage"); } }

        public static void LogInfo(object message, Exception ex = null)
        {
            Logger.Info(message, ex);
        }

        public static void LogDebug(object message, Exception ex = null)
        {
            Logger.Debug(message, ex);
        }

        public static void LogWarn(object message, Exception ex = null)
        {
            Logger.Warn(message, ex);
        }

        public static void LogError(object message, Exception ex = null)
        {
            Logger.Error(message, ex);
        }

        public static void LogFatal(object message, Exception ex = null)
        {
            Logger.Fatal(message, ex);
        }

    }
}