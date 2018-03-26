using System.Diagnostics;

namespace JKMWindowsService.Utility.Log
{
    public class LoggerStackTrace : ILoggerStackTrace
    {
        public StackTrace GetStackTrace()
        {
            return new StackTrace();
        }
    }
}
