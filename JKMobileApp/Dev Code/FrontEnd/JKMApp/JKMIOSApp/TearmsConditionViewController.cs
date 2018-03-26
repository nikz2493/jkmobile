using System;
using Foundation;
using UIKit;

namespace JKMIOSApp
{
    /// <summary>
    /// Controller Name : TearmsConditionViewController
    /// Author          : Hiren Patel
    /// Creation Date   : 16 JAN 2018
    /// Purpose         : To display terms and condition page screen as app shell screen
    /// Revision        : 
    /// </summary>
    public partial class TearmsConditionViewController : UIViewController
	{
		public TearmsConditionViewController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitilizeIntarface();
            LoadPDFFile();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
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

        /// <summary>
        /// Event Name      : BtnContactUs_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : To redirect contactus page
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnContactUs_TouchUpInside(object sender, EventArgs e)
        {
            PerformSegue("termsToContactus", this);
        }

        /// <summary>
        /// Event Name      : BtnAlert_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : To redirect notification
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnAlert_TouchUpInside(object sender, EventArgs e)
        {
            PerformSegue("termsToNotification", this);
        }

        /// <summary>
        /// Method Name     : LoadPDFFile
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Feb 2018
        /// Purpose         : Loads the PDFF ile.
        /// Revision        : 
        /// </summary>
        private void LoadPDFFile()
        {
            webViewPDF.Layer.MasksToBounds = true;
            webViewPDF.Layer.CornerRadius = 8.0f;
            webViewPDF.Layer.BorderWidth = 2;
            webViewPDF.Layer.BorderColor = new CoreGraphics.CGColor(0.5f, 0.5f);
            webViewPDF.LoadRequest(new NSUrlRequest(new NSUrl(AppConstant.PRIVACY_POLICY_TERMS_DOCUMENT_URL, true)));
            webViewPDF.ScalesPageToFit = true;
        }
	}
}
