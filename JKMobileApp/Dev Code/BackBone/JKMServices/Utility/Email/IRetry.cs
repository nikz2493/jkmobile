using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Email
{
    public interface IRetry
    {
        T Do<T>(Func<T> action, TimeSpan retryInterval, int maxAttempt = 3);
    }
}
