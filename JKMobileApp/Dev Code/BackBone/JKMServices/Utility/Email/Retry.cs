using System;
using System.Collections.Generic;

namespace Utility.Email
{
    public class Retry : IRetry
    {
        public T Do<T>(Func<T> action, TimeSpan retryInterval, int maxAttempt = 3)
        {
            bool status = false;
            T obj = default(T);
            var exceptions = new List<Exception>();
            for (int attempted = 1; attempted <= maxAttempt; attempted++)
            {
                //if status is true, there is no need to attempt sending email again
                if (!status)
                {
                    try
                    {
                        if (attempted > 1)
                        {
                            System.Threading.Thread.Sleep(retryInterval);
                        }

                        obj = action();
                        status = true;
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                    }
                }
            }
            if (status)
            {
                return obj;
            }
            else
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
