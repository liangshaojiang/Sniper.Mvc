using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniper.Mapping.SysLog;

namespace Sniper.Services.SysLog
{
    public static class LoggingExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Debug(this ISysLogService logger, string message, Exception exception = null)
        {
            FilteredLog(logger, EnumLevel.Debug, message, exception);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Information(this ISysLogService logger, string message, Exception exception = null)
        {
            FilteredLog(logger, EnumLevel.Information, message, exception);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Warning(this ISysLogService logger, string message, Exception exception = null)
        {
            FilteredLog(logger, EnumLevel.Warning, message, exception);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Error(this ISysLogService logger, string message, Exception exception = null)
        {
            FilteredLog(logger, EnumLevel.Error, message, exception);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Fatal(this ISysLogService logger, string message, Exception exception = null)
        {
            FilteredLog(logger, EnumLevel.Fatal, message, exception);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        private static void FilteredLog(ISysLogService logger, EnumLevel level, string message, Exception exception = null)
        {
            try
            {
                if (exception is System.Threading.ThreadAbortException)
                    return;

                string fullMessage = string.Empty;
                if (exception != null && exception.InnerException != null)
                    exception = exception.InnerException;
                if (exception != null && exception.InnerException != null)
                    exception = exception.InnerException;
                if (exception != null)
                    fullMessage = exception.Message;
                logger.InsertLog(level, message, fullMessage);
            }
            catch (Exception)
            {
                 //log4net
            }
        }
    }
}
