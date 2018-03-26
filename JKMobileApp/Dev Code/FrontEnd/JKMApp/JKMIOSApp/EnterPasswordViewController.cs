using JKMPCL.Model;
using System;
using System.Threading.Tasks;
using JKMPCL.Services;
using JKMPCL.Common;

namespace JKMIOSApp
{
    /// <summary>
    /// Controller Name : EnterPasswordViewController
    /// Author          : Hiren Patel
    /// Creation Date   : 29 Dec 2017
    /// Purpose         : To display login screen for enter password.
    /// Revision        : 
    /// </summary>
    public partial class EnterPasswordViewController : BaseViewController
    {
        public EnterPasswordViewController(IntPtr handle) : base(handle)
        {
        }
        public override void ViewDidLoad()
        {
            UIHelper.SetLabelFont(lblEnterYourPassword);
            UIHelper.SetTextFieldFont(txtPassword);
            UIHelper.SetButtonFont(btnContinue);
            UIHelper.SetButtonFont(btnForgotPassoword);
            UIHelper.DismissKeyboardOnBackgroundTap(this);
            UIHelper.DismissKeyboardOnUITextField(txtPassword);
            UIHelper.SetUiTextFieldAsPassword(txtPassword, AppConstant.MAXIMUM_PASSWORD_LENGTH);
            btnForgotPassoword.TouchUpInside += BtnForgotPassoword_TouchUpInside;
            btnContinue.TouchUpInside += BtnContinue_TouchUpInside;
        }

        /// <summary>
        /// Event Name      : BtnForgotPassoword_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Buttons the forgot passoword touch up inside.
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Arguments</param>
        private async void BtnForgotPassoword_TouchUpInside(object sender, EventArgs e)
        {
            string errorMessage = string.Empty;
            try
            {
                var objectLoadingScreen = UIHelper.ShowLoadingScreen(View);
                errorMessage = await loginServices.GetVerificationData();
                objectLoadingScreen.Hide();
                if (string.IsNullOrEmpty(errorMessage))
                {
                    UIHelper.RedirectToViewController(sorunceViewController: this, destinationViewControllerName: JKMEnum.UIViewControllerID.VerificationCodeView);
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
        /// Event Name      : BtnContinue_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : To get customer profile data and check validate password
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Arguments</param>
        private async void BtnContinue_TouchUpInside(object sender, EventArgs e)
        {
            string errorMessage = string.Empty;
            try
            {
                LoadingOverlay objectLoadingScreen = UIHelper.ShowLoadingScreen(View);
                ServiceResponse serviceResponse = await loginServices.GetPassword(txtPassword.Text);
                objectLoadingScreen.Hide();
                if (string.IsNullOrEmpty(serviceResponse.Message))
                {
                    if (serviceResponse.Status)
                    {
                        UIHelper.SetLoggedInCustomerIDInCache();
                        objectLoadingScreen = UIHelper.ShowLoadingScreen(View);
                        await UIHelper.BindingDataAsync();
                        RedirectToOtherScreen();
                        objectLoadingScreen.Hide();
                    }
                    else
                    {
                      UIHelper.RedirectToViewController(sorunceViewController: this, destinationViewControllerName: JKMEnum.UIViewControllerID.PrivacyPolicyView);
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
                    if ( DTOConsumer.dtoEstimateData != null)
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

                        PerformSegue("dashboard",this);
                    }
                }
                else
                {
                       PerformSegue("dashboard", this);
                }
            }
        }
    }
}


