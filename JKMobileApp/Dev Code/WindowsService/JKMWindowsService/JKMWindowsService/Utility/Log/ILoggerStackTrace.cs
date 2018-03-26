using System.Diagnostics;

namespace JKMWindowsService.Utility.Log
{
    public interface ILoggerStackTrace
    {
        StackTrace GetStackTrace();
    }
}