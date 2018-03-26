// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using JKMPCL.Common;
using JKMPCL.Model;
using JKMPCL.Services;
using UIKit;
namespace JKMIOSApp
{
    /// <summary>
    /// Controller Name : MyAccountViewController
    /// Author          : Hiren Patel
    /// Creation Date   : 16 JAN 2018
    /// Purpose         : To display my account page screen as app shell screen
    /// Revision        : 
    /// </summary>
    public partial class MyAccountViewController : UIViewController
    {
        public readonly MyAccount myAccountService;
        public readonly MyAccountValidateService myAccountValidateService;

        public MyAccountViewController(IntPtr handle) : base(handle)
        {
            myAccountService = new MyAccount();
            myAccountValidateService = new MyAccountValidateService();
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitilizeIntarface();
            UIHelper.DismissKeyboardOnBackgroundTap(this);
            UIHelper.DismissKeyboardOnUITextField(txtPhone);

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
            btnLogout.TouchUpInside += BtnLogout_TouchUpInside;
            btnEmail.TouchUpInside += BtnEmail_TouchUpInside;
            btnSMS.TouchUpInside += BtnSMS_TouchUpInside;
            btnSendMeAlertsYes.TouchUpInside += BtnSendMeAlertsYes_TouchUpInside;
            btnSendMeAlertsNo.TouchUpInside += BtnSendMeAlertsNo_TouchUpInside;
            btnOkay.TouchUpInside += BtnOkay_TouchUpInside;
            txtPhone.EditingChanged += TxtPhone_EditingChanged;
            UIHelper.SetUiTextFieldAsNumberOnly(txtPhone, 15);

            btnEmail.Tag = 0;
            btnSMS.Tag = 0;
            btnSendMeAlertsYes.Tag = 1;
            btnSendMeAlertsNo.Tag = 0;
            btnOkay.Tag = 0;

            UIHelper.SetDefaultEstimateButtonProperty(btnLogout);

            lblName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            lblRegisteredEmail.Text = string.Empty;
        }




        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.NavigationBarHidden = true;
            this.TabBarController.TabBar.Hidden = true;
            PopulateData();
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        /// <summary>
        /// Event Name      : BtnLogout_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To logout from application and redirect to email screen
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private async void BtnLogout_TouchUpInside(object sender, EventArgs e)
        {
            int buttonIndex = await UIHelper.ShowMessageWithOKConfirm(string.Empty, AppConstant.MYACCOUNT_LOGOUT_CONFIRM_TEXT, AppConstant.CONIRM_YES_BUTTON_TEXT, AppConstant.CONIRM_NO_BUTTON_TEXT);

            if (buttonIndex == 0 && !string.IsNullOrEmpty(UIHelper.GetCustomerIDFromCache()))
            {
                UIHelper.SaveCustomerIDInCache(string.Empty);
                UtilityPCL.LoginCustomerData.CustomerId = string.Empty;
                UtilityPCL.LoginCustomerData = null;
                UIHelper.RedirectToViewController(this, JKMEnum.UIViewControllerID.EmailView);
            }

        }

        /// <summary>
        /// Event Name      : BtnEmail_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Feb 2018
        /// Purpose         : To set preferred method of contact as email
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnEmail_TouchUpInside(object sender, EventArgs e)
        {
            if (btnEmail.Tag == 0)
            {
                btnEmail.Tag = 1;
                btnSMS.Tag = 0;
                SetButtonImage(btnEmail);
                SetButtonImage(btnSMS);
                ChangeOkButtonText();
            }

        }

        /// <summary>
        /// Event Name      : BtnSMS_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Feb 2018
        /// Purpose         : To set preferred method of contact as SMS
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnSMS_TouchUpInside(object sender, EventArgs e)
        {
            if (btnSMS.Tag == 0)
            {
                btnSMS.Tag = 1;
                btnEmail.Tag = 0;
                SetButtonImage(btnEmail);
                SetButtonImage(btnSMS);
                ChangeOkButtonText();
            }

        }

        /// <summary>
        /// Event Name      : BtnSendMeAlertsYes_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Feb 2018
        /// Purpose         : To get alert notification when app is close
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnSendMeAlertsYes_TouchUpInside(object sender, EventArgs e)
        {
            if (btnSendMeAlertsYes.Tag == 0)
            {
                btnSendMeAlertsYes.Tag = 1;
                btnSendMeAlertsNo.Tag = 0;
                SetButtonImage(btnSendMeAlertsYes);
                SetButtonImage(btnSendMeAlertsNo);
                ChangeOkButtonText();
            }
        }

        /// <summary>
        /// Event Name      : BtnSendMeAlertsNo_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Feb 2018
        /// Purpose         : To set no alrets notifications when app is close
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnSendMeAlertsNo_TouchUpInside(object sender, EventArgs e)
        {
            if (btnSendMeAlertsNo.Tag == 0)
            {
                btnSendMeAlertsNo.Tag = 1;
                btnSendMeAlertsYes.Tag = 0;
                SetButtonImage(btnSendMeAlertsYes);
                SetButtonImage(btnSendMeAlertsNo);
                ChangeOkButtonText();
            }
        }

        /// <summary>
        /// Methode Name    : SetButtonImage
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Feb 2018
        /// Purpose         : Texts the card number editing changed.
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void TxtPhone_EditingChanged(object sender, EventArgs e)
        {
            ChangeOkButtonText();

            txtPhone.Text = FormatPhoneNumber(txtPhone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", ""), false);

        }


        /// <summary>
        /// Methode Name    : FormatPhoneNumber
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Feb 2018
        /// Purpose         : Convert simple phone number to US formate.
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private string FormatPhoneNumber(string simpleNumber, bool deleteLastChar)
        {
            //https://stackoverflow.com/questions/1246439/uitextfield-for-phone-number
            //https://stackoverflow.com/questions/188510/how-to-format-a-string-as-a-telephone-number-in-c-sharp
            if (simpleNumber.Length == 0)
            {
                return "";
            }
            //// check if the number is to long
            if (simpleNumber.Length > 10)
            {
                // remove last extra chars.
                simpleNumber = simpleNumber.Substring(0, 10);
            }

            //// format the number.. if it's less then 7 digits.. then use this regex.
            if (simpleNumber.Length == 7)
            {
                simpleNumber = Regex.Replace(simpleNumber, @"(\d{3})(\d{3})", "($1) $2");

            }
            if (simpleNumber.Length == 10)
            {
                simpleNumber = Regex.Replace(simpleNumber, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
            }

            return simpleNumber;

        }

        /// <summary>
        /// Methode Name      : SetButtonImage
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Feb 2018
        /// Purpose         : Sets the button image.
        /// Revision        : 
        /// </summary>
        /// <param name="uiButton">User interface button.</param>
        private void SetButtonImage(UIButton uiButton)
        {
            if (uiButton.Tag == 1)
            {
                uiButton.SetImage(UIImage.FromFile(AppConstant.ESTIMATE_SELECETD_IMAGE_URL), UIControlState.Normal);
            }
            else
            {
                uiButton.SetImage(UIImage.FromFile(AppConstant.ESTIMATE_UNSELECETD_IMAGE_URL), UIControlState.Normal);
            }
        }

        /// <summary>
        /// Methode Name    : ChangeOkButtonText
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Feb 2018
        /// Purpose         : Changes the ok button text as Save or Okay
        /// Revision        : 
        /// </summary>
        private void ChangeOkButtonText()
        {
            btnOkay.SetTitle(AppConstant.MYACCOUNT_SAVE_BUTTON_TEXT, UIControlState.Normal);
            btnOkay.Tag = 1;
        }

        /// <summary>
        /// Event Name      : BtnOkay_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Feb 2018
        /// Purpose         : To set no alrets notifications when app is close
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private async void BtnOkay_TouchUpInside(object sender, EventArgs e)
        {


            if (btnOkay.Tag == 0)
            {
                this.TabBarController.SelectedIndex = 0;
                this.TabBarController.TabBar.Hidden = false;
            }
            else
            {
                PrivacyPolicyModel privacyPolicyModel =
                    new PrivacyPolicyModel()
                    {
                        CustomerId = UtilityPCL.LoginCustomerData.CustomerId,
                        Phone = txtPhone.Text,
                        PreferredContact = (btnEmail.Tag == 1 ? JKMEnum.PreferredContact.Email.GetStringValue() : JKMEnum.PreferredContact.SMS.GetStringValue()),
                        ReceiveNotifications = (btnSendMeAlertsYes.Tag == 1)
                    };

                string message = myAccountValidateService.ValidatePrivacyPolicyModel(privacyPolicyModel);
                if (String.IsNullOrEmpty(message))
                {
                    await CallMyAccountService(privacyPolicyModel);
                }
                else
                {
                    UIHelper.ShowAlertMessage(this, message);
                }

            }

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
            if (UtilityPCL.LoginCustomerData != null)
            {

                lblName.Text = UtilityPCL.LoginCustomerData.CustomerFullName;
                lblRegisteredEmail.Text = UtilityPCL.LoginCustomerData.EmailId;
                txtPhone.Text = UtilityPCL.LoginCustomerData.Phone;
                if (!string.IsNullOrEmpty(UtilityPCL.LoginCustomerData.PreferredContact))
                {
                    if (UtilityPCL.LoginCustomerData.PreferredContact == JKMEnum.PreferredContact.Email.GetStringValue())
                    {
                        btnEmail.Tag = 1;
                        btnSMS.Tag = 0;
                    }
                    else if (UtilityPCL.LoginCustomerData.PreferredContact == JKMEnum.PreferredContact.SMS.GetStringValue())
                    {
                        btnEmail.Tag = 0;
                        btnSMS.Tag = 1;
                    }
                }

                if (UtilityPCL.LoginCustomerData.ReceiveNotifications)
                {
                    btnSendMeAlertsYes.Tag = 1;
                    btnSendMeAlertsNo.Tag = 0;
                }
                else
                {
                    btnSendMeAlertsYes.Tag = 0;
                    btnSendMeAlertsNo.Tag = 1;
                }

                SetButtonImage(btnEmail);
                SetButtonImage(btnSMS);
                SetButtonImage(btnSendMeAlertsYes);
                SetButtonImage(btnSendMeAlertsNo);
            }
        }

        /// <summary>
        /// Event Name      : CallMyAccountService
        /// Author          : Hiren Patel
        /// Creation Date   : 13 Dec 2017
        /// Purpose         : Calls my account service to update account data
        /// Revision        : 
        /// </summary>
        /// <param name="privacyPolicyModel">Privacy policy model.</param>
        private async Task CallMyAccountService(PrivacyPolicyModel privacyPolicyModel)
        {
            string errorMessage = string.Empty;
            LoadingOverlay objectLoadingScreen = UIHelper.ShowLoadingScreen(View);
            try
            {

                APIResponse<PrivacyPolicyModel> serviceResponse = await myAccountService.PutMyAccountDetails(privacyPolicyModel);

                if (serviceResponse.STATUS)
                {
                    await UIHelper.ShowMessageWithOKConfirm(string.Empty, serviceResponse.Message, AppConstant.ALERT_OK_BUTTON_TEXT);
                    btnOkay.Tag = 0;
                    btnOkay.SetTitle(AppConstant.MYACCOUNT_OK_BUTTON_TEXT, UIControlState.Normal);
                    this.TabBarController.SelectedIndex = 0;
                    this.TabBarController.TabBar.Hidden = false;
                    await DTOConsumer.GetCustomerProfileData();
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
                objectLoadingScreen.Hide();
            }
        }
    }
}