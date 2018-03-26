using System;
using EventKit;

namespace JKMIOSApp
{
    /// <summary>
    /// Class Name      : App.
	/// Author          : Hiren Patel
	/// Creation Date   : 22 JAN 2017
	/// Purpose         : For adding Add to calendar functionality in alert list page
    /// Revision        : 
    /// </summary>
    public class App
    {
        private static App current;

        /// <summary>
        /// Constructor Name      : App.
        /// Author          : Hiren Patel
        /// Creation Date   : 22 JAN 2017
        /// Purpose         : To get current app
        /// Revision        : 
        /// </summary>
        /// <value>The current.</value>
        public static App Current
        {
            get { return current; }
        }

        protected EKEventStore eventStore;

        /// <summary>
        /// Constructor Name      : App.
        /// Author          : Hiren Patel
        /// Creation Date   : 22 JAN 2017
        /// Purpose         : To gets event store.
        /// Revision        : 
        /// </summary>
        /// <value>The event store.</value>
        public EKEventStore EventStore
        {
            get { return eventStore; }
        }

        static App()
        {
            current = new App();
        }
        protected App()
        {
            eventStore = new EKEventStore();
        }
    }
}