using System;
using Foundation;

namespace JKMIOSApp
{
    /// <summary>
    /// Class Name      : Extensions.
    /// Author          : Hiren Patel
    /// Creation Date   : 22 JAN 2018
    /// Purpose         : To add exetion method for IOS
    /// Revision        :
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Method Name     : DateTimeToNSDate.
        /// Author          : Hiren Patel
        /// Creation Date   : 22 JAN 2018
        /// Purpose         : To add exetion method for DateTime to convert into NSDate
        /// Revision        :
        /// </summary>
        /// <returns>The time to NSD ate.</returns>
        /// <param name="date">Date.</param>
        public static NSDate DateTimeToNSDate(this DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
                date = DateTime.SpecifyKind(date, DateTimeKind.Local);
            return (NSDate)date;
        }

        /// <summary>
        /// Method Name     : DateTimeToNSDate.
        /// Author          : Hiren Patel
        /// Creation Date   : 22 JAN 2018
        /// Purpose         : To add exetion method for NSdate time to convert into DateTime
        /// Revision        :
        /// </summary>
        /// <returns>The time to NSD ate.</returns>
        /// <param name="date">Date.</param>
        public static DateTime NSDateToDateTime(this NSDate date)
        {
            return ((DateTime)date).ToLocalTime();
        }
    }
}
