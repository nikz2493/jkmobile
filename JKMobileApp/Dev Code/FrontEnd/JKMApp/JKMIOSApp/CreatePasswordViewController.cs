using JKMPCL.Model;
using System;
using UIKit;
using System.Threading.Tasks;
using JKMPCL.Common;
using JKMPCL.Services;

namespace JKMIOSApp
{
    /// <summary>
    /// Controller Name : CreatePasswordViewController
    /// Author          : Hiren Patel
    /// Creation Date   : 29 Dec 2017
    /// Purpose         : To display create passowrd page screen
    /// Revision        : 
    /// </summary>
    public partial class CreatePasswordViewController : BaseViewController
    {
        public CreatePasswordViewController(IntPtr handle) : base(handle)
        {
        }
        public override void ViewDidLoad()
        {
            UIHelper.SetLabelFont(lblEnterYourPassword);
            UIHelper.SetLabelFont(lblVerifyPassword);

            UIHelper.SetTextFieldFont(txtPassword);
            UIHelper.SetTextFieldFont(txtVerifyPassword);

            UIHelper.SetButtonFont(btnContinue);

            txtPassword.EndEditing(true);
            txtVerifyPassword.EndEditing(true);

            UIHelper.DismissKeyboardOnBackgroundTap(this);
            UIHelper.DismissKeyboardOnUITextField(txtPassword);
            UIHelper.DismissKeyboardOnUITextField(txtVerifyPassword);

            UIHelper.SetUiTextFieldAsPassword(txtPassword, AppConstant.MAXIMUM_PASSWORD_LENGTH);
            UIHelper.SetUiTextFieldAsPassword(txtVerifyPassword, AppConstant.MAXIMUM_PASSWORD_LENGTH);

            btnContinue.TouchUpInside += BtnContinue_TouchUpInside;
        }

        /// <summary>
        /// Event Name     : BtnContinue_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : To call create pasword services
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
                ServiceResponse serviceResponse = await loginServices.CreatePassword(txtPassword.Text, txtVerifyPassword.Text);
                objectLoadingScreen.Hide();
                if (serviceResponse != null)
                {
                    if (string.IsNullOrEmpty(serviceResponse.Message))
                    {
                        await UIHelper.ShowMessageWithOKConfirm(string.Empty, AppConstant.CREATED_PASSWORD_MESSAGE, AppConstant.ALERT_OK_BUTTON_TEXT);
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

                        PerformSegue("createPassowrdToDashBaord",this);
                    }
                }
                else
                {
                       PerformSegue("createPassowrdToDashBaord", this);
                }
            }
        }

    }
}