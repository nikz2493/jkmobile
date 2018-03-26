using System;
using System.Linq;
using Foundation;
using JKMPCL.Common;
using JKMPCL.Model;
using UIKit;

namespace JKMIOSApp
{
    /// <summary>
    /// Controller Name : ServicesViewController
    /// Author          : Hiren Patel
    /// Creation Date   : 16 JAN 2018
    /// Purpose         : To display services page screen as estimate wizard step
    /// Revision        : 
    /// </summary>
	public partial class ServicesViewController : UIViewController
    {
        private EstimateModel estimateModel;

        public ServicesViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitilizeIntarface();
            SetContactUsPageLink();
            PopulateData();

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            btnBack.TouchUpInside += BtnBack_TouchUpInside;
            btnConfirm.TouchUpInside += BtnConfirm_TouchUpInside;
            btnViewEstimate.TouchUpInside += BtnViewEstimate_TouchUpInside;
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
            UIHelper.SetDefaultEstimateButtonProperty(btnViewEstimate);
        }

        /// <summary>
        /// Event Name     : BtnBack_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To redirec to back screen
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnBack_TouchUpInside(object sender, EventArgs e)
        {
            PerformSegue("servicesToEstimateReveiw", this);
        }

        /// <summary>
        /// Event Name      : BtnConfirm_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To confirmed services and redirec to services date screen.
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnConfirm_TouchUpInside(object sender, EventArgs e)
        {
            PerformSegue("servicesToServiceDates", this);
        }

        /// <summary>
        /// Event Name      : BtnViewEstimate_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To view or download estimate pdf file
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnViewEstimate_TouchUpInside(object sender, EventArgs e)
        {
            UIHelper.CallingScreen = JKMEnum.UIViewControllerID.ServicesView;
            UIHelper.RedirectToViewController(this, JKMEnum.UIViewControllerID.ViewEstimateReviewView);
        }

        /// <summary>
        /// Method Name     : SetContactUsPageLink
        /// Author          : Hiren Patel
        /// Creation Date   : 3 Dec 2017
        /// Purpose         : Sets the phone open property.
        /// Revision        : 
        /// </summary>
        private void SetContactUsPageLink()
        {
            UITapGestureRecognizer lblWantToUpdateYourServicesTap = new UITapGestureRecognizer(() =>
            {

                UIHelper.CallingScreenContactUs = JKMEnum.UIViewControllerID.ServicesView;
                PerformSegue("serviceToContactUs", this);
            });

            UITapGestureRecognizer lblPleaseConatctSalesTap = new UITapGestureRecognizer(() =>
            {
                UIHelper.CallingScreenContactUs = JKMEnum.UIViewControllerID.ServicesView;
                PerformSegue("serviceToContactUs", this);
            });

            lblWantToUpdateYourServices.AddGestureRecognizer(lblWantToUpdateYourServicesTap);
            lblWantToUpdateYourServices.UserInteractionEnabled = true;

            lblPleaseConatctSales.AddGestureRecognizer(lblPleaseConatctSalesTap);
            lblPleaseConatctSales.UserInteractionEnabled = true;
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
          
            estimateModel = DTOConsumer.GetSelectedEstimate();
            if (estimateModel != null && estimateModel.MyServices != null && string.IsNullOrEmpty(estimateModel.message))
            {
                
                UIHelper.BindMyServiceData(estimateModel.MyServices, scrollviewServices, View, true);
                UIHelper.SetDefaultWizardScrollViewBorderProperty(scrollviewServices);
             
            }

            UIHelper.CreateWizardHeader(2, viewHeader, estimateModel);
        }
    }
}

