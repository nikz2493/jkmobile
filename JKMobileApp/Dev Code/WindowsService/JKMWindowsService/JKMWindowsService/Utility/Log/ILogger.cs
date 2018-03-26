using System;

namespace JKMWindowsService.Utility.Log
{
    public interface ILogger
    {
        void Error(object message);
        void Error(object message, Exception exception);
        void Info(object message);
        void Info(object message, Exception exception);
        void SetLoggerConfig();
        void SetLoggerProperty(string propertyName, string propertyValue);
        void Warn(object message);
        void Warn(object message, Exception exception);
    }
}