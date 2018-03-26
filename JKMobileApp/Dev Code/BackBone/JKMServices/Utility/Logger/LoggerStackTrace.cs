using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Logger
{
    public class LoggerStackTrace : ILoggerStackTrace
    {
        public StackTrace GetStackTrace()
        {
            return new StackTrace();
        }
    }
}
