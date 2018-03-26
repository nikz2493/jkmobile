using System;
using Foundation;
using JKMPCL.Common;
using JKMPCL.Model;
using UIKit;

namespace JKMIOSApp
{
    /// <summary>
    /// Controller Name : VitalInformationViewController
    /// Author          : Hiren Patel
    /// Creation Date   : 23 JAN 2018
    /// Purpose         : To display vital information page screen as estimate wizard step
    /// Revision        : 
    /// </summary>
    public partial class VitalInformationViewController : UIViewController
    {
        private EstimateModel estimateModel;

        public VitalInformationViewController(IntPtr handle) : base(handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitilizeIntarface();
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
            UIHelper.SetDefaultWizardScrollViewBorderProperty(scrollViewPDFContainer);
            UIHelper.SetDefaultEstimateButtonProperty(btnViewRightsResponsibilities);

            btnBack.TouchUpInside += BtnBack_TouchUpInside;
            btnInformationReceived.TouchUpInside += BtnInformationReceived_TouchUpInside;
            btnViewRightsResponsibilities.TouchUpInside += BtnViewRightsResponsibilities_TouchUpInside;
        }

        /// <summary>
        /// Event Name      : BtnBack_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 23 Dec 2017
        /// Purpose         : To redirec to back screen as valuation.
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnBack_TouchUpInside(object sender, EventArgs e)
        {
            PerformSegue("vitalInformationToValuation", this);
        }

        /// <summary>
        /// Event Name      : BtnInformationReceived_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To confirmed vital information and redirec to acknowledgement screen.
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnInformationReceived_TouchUpInside(object sender, EventArgs e)
        {
            PerformSegue("vitalInformationToAcknowledgement", this);
        }

        /// <summary>
        /// Event Name      : BtnViewRightsResponsibilities_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To view or download rights and responsibilities pdf file.
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnViewRightsResponsibilities_TouchUpInside(object sender, EventArgs e)
        {
            UIHelper.CallingScreen = JKMEnum.UIViewControllerID.VitalInformationView;
            UIHelper.RedirectToViewController(this, JKMEnum.UIViewControllerID.ViewEstimateReviewView);
        }

        /// <summary>
        /// Method Name     : PopulateData
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : fill Estimate Data  
        /// Revision        : 
        /// </summary>
        public void PopulateData()
        {
            webViewPDF.LoadRequest(new NSUrlRequest(new NSUrl(AppConstant.VITAL_INFORMATION_DOCUMENT_URL, true)));
            webViewPDF.ScalesPageToFit = true;
           
            if (DTOConsumer.dtoEstimateData != null)
            {
                estimateModel = DTOConsumer.GetSelectedEstimate();
            }
            UIHelper.CreateWizardHeader(7, viewHeader, estimateModel);
        }
    }
}
