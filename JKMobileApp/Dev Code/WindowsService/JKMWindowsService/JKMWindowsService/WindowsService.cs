using JKMWindowsService.Utility.Log;
using System;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;

namespace JKMWindowsService
{
    public partial class WindowsService : ServiceBase, IWindowsService
    {
        private Timer Scheduler;
        private readonly MoveManager.IMoveDetails moveDetails;
        private readonly AlertManager.IBookYourMove bookYourMove;
        private readonly AlertManager.IPreMoveConfirmationNotifications preMoveConfirmationNotifications;
        private readonly AlertManager.IBeginningOfDayOfServiceCheckIn beginningOfDayOfServiceCheckIn;
        private readonly AlertManager.IEndOfServiceCheckIn endOfServiceCheckIn;
        private readonly AlertManager.IFinalPaymentMade finalPaymentMade;
        private readonly IResourceManagerFactory resourceManager;
        private readonly ILogger logger;

        /// <summary>
        /// Constructor Name     : LogService
        /// Author               : Pratik Soni
        /// Creation Date        : 14 Feb 2017
        /// </summary>
        public WindowsService(MoveManager.IMoveDetails moveDetails,
                              AlertManager.IBookYourMove bookYourMove,
                              AlertManager.IPreMoveConfirmationNotifications preMoveConfirmationNotifications,
                              AlertManager.IBeginningOfDayOfServiceCheckIn beginningOfDayOfServiceCheckIn,
                              AlertManager.IEndOfServiceCheckIn endOfServiceCheckIn,
                              AlertManager.IFinalPaymentMade finalPaymentMade,
                              IResourceManagerFactory resourceManager,
                              ILogger logger)
        {
            InitializeComponent();
            this.moveDetails = moveDetails;
            this.bookYourMove = bookYourMove;
            this.preMoveConfirmationNotifications = preMoveConfirmationNotifications;
            this.beginningOfDayOfServiceCheckIn = beginningOfDayOfServiceCheckIn;
            this.endOfServiceCheckIn = endOfServiceCheckIn;
            this.finalPaymentMade = finalPaymentMade;
            this.resourceManager = resourceManager;
            this.logger = logger;
        }

        /// <summary>
        /// Method Name     : TestStartupAndStop
        /// Author          : Ranjana Singh
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : This method is used for debugging the service and then it will be scheduled.
        /// Revision        : 
        /// </summary>
        /// <param name="args"></param>
        public void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            this.OnStop();
        }

        /// <summary>
        /// Method Name     : OnStart
        /// Author          : Ranjana Singh
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : For Starting the service and then it will be scheduled.
        /// Revision        : 
        /// </summary>
        protected override void OnStart(string[] args)
        {
            logger.Info(resourceManager.GetString("ServiceStartLog").Replace("@Time", DateTime.Now.ToString(resourceManager.GetString("DateFormat"))));
            this.ScheduleService();
        }

        /// <summary>
        /// Method Name     : OnStop
        /// Author          : Ranjana Singh
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : For Stoping the service and then scheduler will be disposed.
        /// Revision        : 
        /// </summary>
        protected override void OnStop()
        {
            logger.Info(resourceManager.GetString("ServiceStopLog").Replace("@Time", DateTime.Now.ToString(resourceManager.GetString("DateFormat"))));
            this.Scheduler.Dispose();
        }

        /// <summary>
        /// Method Name     : SchedulerCallback
        /// Author          : Pratik Soni
        /// Creation Date   : 14 Feb 2017
        /// Purpose         : Main entry point for scheduler
        /// Revision        : 
        /// </summary>
        public void SchedulerCallback(object e)
        {
            ScheduleService();
            bookYourMove.SendAlerts();                      // For move booked status notifications
            preMoveConfirmationNotifications.SendAlerts();  // For Pre Move Confirmation notifications
            beginningOfDayOfServiceCheckIn.SendAlerts();    // For beginning of day of service notifications
            endOfServiceCheckIn.SendAlerts();               // For end of service check-in notifications
            finalPaymentMade.SendAlerts();                  // For final payment mode notifications
        }

        /// <summary>
        /// Method Name     : ScheduleService
        /// Author          : Ranjana Singh
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : For scheduling service based on different Modes.
        /// Revision        : 
        /// </summary>
        public void ScheduleService()
        {
            DateTime scheduledTime;
            string serviceMode, mode;
            int intervalMinutes, dueTime;

            try
            {
                Scheduler = new Timer(new TimerCallback(SchedulerCallback));

                mode = ConfigurationManager.AppSettings["Mode"].ToUpper();
                serviceMode = resourceManager.GetString("ServiceMode");

                logger.Info(serviceMode.Replace("@mode", mode.ToString()) + " {0}");

                //Set the Default Time.
                scheduledTime = DateTime.MinValue;

                if (mode == resourceManager.GetString("DailyMode"))
                {
                    //Get the Scheduled Time from AppSettings.
                    scheduledTime = DateTime.Parse(ConfigurationManager.AppSettings["ScheduledTime"]);
                    if (DateTime.Now > scheduledTime)
                    {
                        //If Scheduled Time is passed set Schedule for the next day.
                        scheduledTime = scheduledTime.AddDays(1);
                    }
                }

                if (mode.ToUpper() == resourceManager.GetString("IntervalMode"))
                {
                    //Get the Interval in Minutes from AppSettings.
                    intervalMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalMinutes"]);

                    //Set the Scheduled Time by adding the Interval to Current Time.
                    scheduledTime = DateTime.Now.AddMinutes(intervalMinutes);

                    if (DateTime.Now > scheduledTime)
                    {
                        //If Scheduled Time is passed set Schedule for the next Interval.
                        scheduledTime = scheduledTime.AddMinutes(intervalMinutes);
                    }
                }

                TimeSpan timeSpan = scheduledTime.Subtract(DateTime.Now);
                string schedule = string.Format("{0} day(s) {1} hour(s) {2} minute(s) {3} seconds(s)", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

                logger.Info(resourceManager.GetString("ScheduleTime").Replace("@schedule", schedule).Replace("@Time", DateTime.Now.ToString(resourceManager.GetString("DateFormat"))));

                //Get the difference in Minutes between the Scheduled and Current Time.
                dueTime = Convert.ToInt32(timeSpan.TotalMilliseconds);

                //Change the Timer's Due Time.
                Scheduler.Change(0, 0);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ex.StackTrace);
            }
        }
    }
}
