using System.Diagnostics;

namespace Utility.Logger
{
    public interface ILoggerStackTrace
    {
        StackTrace GetStackTrace();
    }
}