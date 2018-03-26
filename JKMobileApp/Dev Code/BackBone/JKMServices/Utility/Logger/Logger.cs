using System;

namespace Utility.Logger
{
    public class Logger : ILogger
    {
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ILoggerStackTrace loggerStackTrace;

        public Logger(ILoggerStackTrace loggerStackTrace)
        {
            this.loggerStackTrace = loggerStackTrace;
        }
        public void Error(object message, Exception exception = null)
        {
            SetLoggerConfig();
            if (exception == null)
            {
                log.Error(message);
            }
            else
            {
                log.Error(message, exception);
            }
        }

        public void Info(object message, Exception exception = null)
        {
            SetLoggerConfig();
            if (exception == null)
            {
                log.Info(message);
            }
            else
            {
                log.Info(message, exception);
            }
        }

        public void Warn(object message, Exception exception = null)
        {
            SetLoggerConfig();
            if (exception == null)
            {
                log.Warn(message);
            }
            else
            {
                log.Warn(message, exception);
            }
        }

        public void SetLoggerConfig()
        {
            var stackTrace = loggerStackTrace.GetStackTrace();
            SetLoggerProperty("Method", stackTrace.GetFrame(3).GetMethod().Name);
            SetLoggerProperty("ClassName", Convert.ToString(stackTrace.GetFrame(3).GetMethod().DeclaringType));
        }

        public void SetLoggerProperty(string propertyName, string propertyValue)
        {
            log4net.LogicalThreadContext.Properties[propertyName] = propertyValue;
        }
    }
}
