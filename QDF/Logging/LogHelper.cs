using System;
using log4net;

namespace QDF.Logging
{
    public class LogHelper
    {
        /// <summary>
        /// A reference to the logger.
        /// </summary>
        private static readonly ILog Logger;

        /// <summary>
        /// Static Constructor.
        /// </summary>
        static LogHelper()
        {
            Logger = LogManager.GetLogger("QDF.Logger");
        }

        /// <summary>
        /// Log Exception.
        /// </summary>
        /// <param name="ex">Exception</param>
        public static void LogException(Exception ex)
        {
            LogException(Logger, ex);
        }

        /// <summary>
        ///  Log Exception
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="ex">Exception</param>
        public static void LogException(ILog logger, Exception ex)
        {
            logger.Error(ex.ToString(), ex);
        }

        /// <summary>
        /// Log Information
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            Logger.Info(message);
        }

        /// <summary>
        /// Log Debug information
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message)
        {
            Logger.Debug(message);
        }
    }
}