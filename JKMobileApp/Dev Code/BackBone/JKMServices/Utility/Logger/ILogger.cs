using System;

namespace Utility.Logger
{
    public interface ILogger
    {
        void Error(object message, Exception exception = null);

        void Info(object message, Exception exception = null);

        void Warn(object message, Exception exception = null);

        void SetLoggerConfig();
        void SetLoggerProperty(string propertyName, string propertyValue);
    }
}