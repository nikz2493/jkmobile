// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using JKMPCL.Common;
using JKMPCL.Model;
using JKMPCL.Services;
using UIKit;

namespace JKMIOSApp
{
	public partial class ViewMyDocumentController : UIViewController
	{
		public ViewMyDocumentController (IntPtr handle) : base (handle)
		{
		}
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitilizeIntarface();
            btnBack.TouchUpInside += BtnBack_TouchUpInside;
            PopulateData();
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
            UIHelper.SetDefaultWizardScrollViewBorderProperty(scrollViewDocumentContainer);
        }

        /// <summary>
        /// Event Name      : BtnBack_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To redirec to back screen
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnBack_TouchUpInside(object sender, EventArgs e)
        {
            DismissViewControllerAsync(true);
        }

        /// <summary>
        /// Method Name     : PopulateData
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : fill My document file  
        /// Revision        : 
        /// </summary>
        public void PopulateData()
        {
            uiVebViewDocument.LoadRequest(new NSUrlRequest(new NSUrl(AppConstant.PRIVACY_POLICY_TERMS_DOCUMENT_URL, true)));
            uiVebViewDocument.ScalesPageToFit = true;
            if (UtilityPCL.selectedDocumentModel != null)
            {
                lblDocumentTitle.Text = UtilityPCL.selectedDocumentModel.DocumentTitle;
            }
        }
	}
}