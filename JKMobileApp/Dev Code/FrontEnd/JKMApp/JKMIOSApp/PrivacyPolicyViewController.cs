using Foundation;
using System;
using JKMPCL.Model;
using System.Threading.Tasks;
using JKMPCL.Services;
using JKMPCL.Common;

namespace JKMIOSApp
{
    /// <summary>
    /// Controller Name : PrivacyPolicyViewController
    /// Author          : Hiren Patel
    /// Creation Date   : 16 JAN 2018
    /// Purpose         : To display privacy policy screen
    /// Revision        : 
    /// </summary>
    public partial class PrivacyPolicyViewController : BaseViewController
    {
        public PrivacyPolicyViewController(IntPtr handle) : base(handle)
        {
        }
        public override void ViewDidLoad()
        {
            UIHelper.SetButtonFont(btnDisagree);
            UIHelper.SetButtonFont(btnAgree);
            btnDisagree.TouchUpInside += BtnDisagree_TouchUpInside;
            btnAgree.TouchUpInside += BtnAgree_TouchUpInside;
            LoadPDFFile();
        }

        /// <summary>
        /// Method Name     : LoadPDFFile
        /// Author          : Hiren Patel
        /// Creation Date   : 13 Dec 2017
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

        /// <summary>
        /// Event Name      : BtnAgree_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 13 Dec 2017
        /// Purpose         : Calls the privacy policy service to agree privacy policy
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private async void BtnAgree_TouchUpInside(object sender, EventArgs e)
        {
           await CallPrivacyPolicyService(true);
        }

        /// <summary>
        /// Event Name      : BtnAgree_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 13 Dec 2017
        /// Purpose         : Calls the privacy policy service to disagree privacy policy
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void BtnDisagree_TouchUpInside(object sender, EventArgs e)
        {
            UIHelper.CallingScreenContactUs = JKMEnum.UIViewControllerID.PrivacyPolicyView;
            UIHelper.RedirectToViewController(sorunceViewController: this, destinationViewControllerName: JKMEnum.UIViewControllerID.ContactusView);
        }

        /// <summary>
        /// Event Name      : CallPrivacyPolicyService
        /// Author          : Hiren Patel
        /// Creation Date   : 13 Dec 2017
        /// Purpose         : Calls the privacy policy service to agree or disagree privacy policy
        /// Revision        : 
        /// </summary>
        /// <summary>
        /// Calls the privacy policy service.
        /// </summary>
        /// <param name="IsAgree">If set to <c>true</c> is agree.</param>
        private async Task CallPrivacyPolicyService(bool IsAgree)
        {
            
            string errorMessage = string.Empty;
            try
            {
                ServiceResponse serviceResponse = await loginServices.PrivacyPolicyService(IsAgree);
                if (serviceResponse != null)
                {
                    if (string.IsNullOrEmpty(serviceResponse.Message))
                    {
                        if (serviceResponse.Status)
                        {
                            UIHelper.SetLoggedInCustomerIDInCache();
                            LoadingOverlay objectLoadingScreen = UIHelper.ShowLoadingScreen(View);
                            await UIHelper.BindingDataAsync();
                            RedirectToOtherScreen();
                            objectLoadingScreen.Hide();
                          
                        }
                        else
                        {
                            UIHelper.CallingScreenContactUs = JKMEnum.UIViewControllerID.PrivacyPolicyView;
                            UIHelper.RedirectToViewController(sorunceViewController: this, destinationViewControllerName: JKMEnum.UIViewControllerID.ContactusView);
                        }
                    }
                    else
                    {
                        errorMessage = serviceResponse.Message;
                    }
                }
            }
            catch (Exception error)
            {
                errorMessage = error.Message;
            }
            finally
            {
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    UIHelper.ShowAlertMessage(this, errorMessage);
                }
            }
        }

        /// <summary>
        /// Event Name      : RedirectToOtherScreen
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : To redirect other screen
        /// Revision        : 
        /// </summary>
        private void RedirectToOtherScreen()
        {
            if (UtilityPCL.LoginCustomerData != null)
            {
                if (DTOConsumer.dtoMoveData != null)
                {
                    if (DTOConsumer.dtoEstimateData != null)
                    {
                        if (DTOConsumer.dtoEstimateData.Count > 1)
                        {
                            UIHelper.RedirectToViewController(sorunceViewController: this, destinationViewControllerName: JKMEnum.UIViewControllerID.EstimateListView);
                        }
                        else
                        {
                            UIHelper.RedirectToViewController(sorunceViewController: this, destinationViewControllerName: JKMEnum.UIViewControllerID.EstimateReviewView);
                        }
                    }
                    else
                    {

                        PerformSegue("privacyToDashboard",this);
                    }
                }
                else
                {
                    PerformSegue("privacyToDashboard", this);
                }
            }
        }
    }
}