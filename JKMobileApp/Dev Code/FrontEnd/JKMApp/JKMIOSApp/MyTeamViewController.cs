using System;
using Foundation;
using UIKit;

namespace JKMIOSApp
{
    /// <summary>
    /// Controller Name : MyTeamViewController
    /// Author          : Hiren Patel
    /// Creation Date   : 16 JAN 2018
    /// Purpose         : To display my team page screen as app shell screen
    /// Revision        : 
    /// </summary>
    public partial class MyTeamViewController : UIViewController
	{
		public MyTeamViewController (IntPtr handle) : base (handle)
		{
		}
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitilizeIntarface();
        }

        /// <summary>
        /// Method Name     : InitilizeIntarface
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To Initilizes the intarface.
        /// Revision        : 
        /// </summary>
        public void InitilizeIntarface()
        {
            // InitilizeIntarface
            btnAlert.TouchUpInside += BtnAlert_TouchUpInside;
            btnContactUs.TouchUpInside += BtnContactUs_TouchUpInside;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.NavigationBarHidden = true;
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        /// <summary>
        /// Event Name      : BtnContactUs_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To redirect contactus page
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnContactUs_TouchUpInside(object sender, EventArgs e)
        {
            PerformSegue("myteamToContactus", this);
        }

        /// <summary>
        /// Event Name      : BtnAlert_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To redirect notification
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnAlert_TouchUpInside(object sender, EventArgs e)
        {
            PerformSegue("notification", this);
        }

	}
}
