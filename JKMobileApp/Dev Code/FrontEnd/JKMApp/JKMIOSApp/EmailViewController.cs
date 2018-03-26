using System;
using UIKit;
using JKMPCL.Services;
using JKMPCL.Model;
using System.Threading.Tasks;
using JKMPCL.Common;

namespace JKMIOSApp
{
    /// <summary>
    /// Controller Name : EmailViewController
    /// Author          : Hiren Patel
    /// Creation Date   : 29 Dec 2017
    /// Purpose         : To display login screen for enter emailid.
    /// Revision        : 
    /// </summary>
    public partial class EmailViewController : BaseViewController
    {

        public EmailViewController(IntPtr handle) : base(handle)
        {

        }

        public override void ViewDidLoad()
        {
            UIHelper.SetLabelFont(lblEnterYourEmailID);
            UIHelper.SetTextFieldFont(txtEmailID);
            UIHelper.SetButtonFont(btnContinue);
            UIHelper.DismissKeyboardOnBackgroundTap(this);
            UIHelper.DismissKeyboardOnUITextField(txtEmailID);
            UIHelper.SetMaximumUiTextFieldLength(txtEmailID, AppConstant.MAXIMUM_EMAIL_LENGTH);

            btnContinue.TouchUpInside += BtnContinue_TouchUpInside;
        }
        public async override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            if (!String.IsNullOrEmpty(UIHelper.GetCustomerIDFromCache()) && !String.IsNullOrWhiteSpace(UIHelper.GetCustomerIDFromCache()))
            {
                LoadingOverlay objectLoadingScreen = UIHelper.ShowLoadingScreen(View);
                await UIHelper.BindingDataAsync();
                RedirectToOtherScreen();
                objectLoadingScreen.Hide();
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
                if (DTOConsumer.dtoMoveData != null && DTOConsumer.dtoEstimateData != null)
                {
                    if (DTOConsumer.dtoEstimateData.Count > 1)
                    {
                        PerformSegue("emailToEstimateList", this);
                    }
                    else
                    {
                        PerformSegue("emailToEstimateReview", this);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(UtilityPCL.LoginCustomerData.CustomerId))
                    {
                        PerformSegue("emailToDashboard", this);
                    }
                }
            }
        }

        /// <summary>
        /// Event Name     : BtnContinue_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : To call login services
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private async void BtnContinue_TouchUpInside(object sender, EventArgs e)
        {
            await CallLoginService();
        }

        /// <summary>
        /// Method Name     : CallLoginService
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : To calls login services
        /// Revision        : 
        /// </summary>
        private async Task CallLoginService()
        {
            string errorMessage = string.Empty;
            try
            {
                LoadingOverlay objectLoadingScreen = UIHelper.ShowLoadingScreen(View);
                ServiceResponse serviceResponse = await loginServices.GetCustomerLogin(txtEmailID.Text);
                objectLoadingScreen.Hide();
                if (string.IsNullOrEmpty(serviceResponse.Message))
                {
                    if (serviceResponse.Status)
                    {
                        UIHelper.RedirectToViewController(sorunceViewController: this, destinationViewControllerName: JKMEnum.UIViewControllerID.EnterPasswordView);
                    }
                    else
                    {
                        UIHelper.RedirectToViewController(sorunceViewController: this, destinationViewControllerName: JKMEnum.UIViewControllerID.VerificationCodeView);
                    }
                }
                else
                {
                    errorMessage = serviceResponse.Message;
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
        /// Method Name     : ShouldAutorotate
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : To prevent the screen for landscap
        /// Revision        : 
        /// </summary>
        /// <returns><c>true</c>, if autorotate was shoulded, <c>false</c> otherwise.</returns>
        public override bool ShouldAutorotate()
        {
            return false;

        }

        /// <summary>
        /// Method Name     : GetSupportedInterfaceOrientations
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Gets the supported interface orientations.
        /// Revision        : 
        /// Gets the supported interface orientations.
        /// </summary>
        /// <returns>The supported interface orientations.</returns>
        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
        {
            return UIInterfaceOrientationMask.Portrait;
        }
    }
}