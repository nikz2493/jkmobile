using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.Common;
using JKMPCL.Model;
using JKMPCL.Services;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JKMAndroidApp.activity
{
    [Activity(Label = "MyAccountActivity", Theme = "@style/MyTheme", NoHistory = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = SoftInput.AdjustResize)]
    public class MyAccountActivity : Activity
    {

        TextView tvTitlemyAccount, tvDisplayName, tvName, tvPhone, tvRegisteredEmail, tvDisplayEmail, tvMethodOfContent, tvSendAlerts, tvlogout;
        EditText txtphone;
        RadioButton radioButtonSMS, radioButtonEmail, radioButtonYes, radioButtonNo;
        Button btnClose;
        LinearLayout linearLayoutLogout;

        private ISharedPreferences sharedPreference;
        bool isEdit = false;
        ProgressDialog progressDialog;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            sharedPreference = PreferenceManager.GetDefaultSharedPreferences(this);
            SetContentView(Resource.Layout.LayoutFragmentMyAccount);
            UIReference();
            UIClickEvent();
            ApplyFont();
            PopulateData();
        }


        /// <summary>
        /// Method Name     : ApplyFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 20 feb 2018
        /// Purpose         : Apply font all control   
        /// Revision        : 
        /// </summary>
        public void ApplyFont()
        {
            UIHelper.SetTextViewFont(tvTitlemyAccount, (int)UIHelper.LinotteFont.LinotteSemiBold, this.Assets);
            UIHelper.SetTextViewFont(tvDisplayName, (int)UIHelper.LinotteFont.LinotteSemiBold, this.Assets);
            UIHelper.SetTextViewFont(tvPhone, (int)UIHelper.LinotteFont.LinotteSemiBold, this.Assets);
            UIHelper.SetTextViewFont(tvName, (int)UIHelper.LinotteFont.LinotteSemiBold, this.Assets);
            UIHelper.SetTextViewFont(tvRegisteredEmail, (int)UIHelper.LinotteFont.LinotteSemiBold, this.Assets);
            UIHelper.SetTextViewFont(tvDisplayEmail, (int)UIHelper.LinotteFont.LinotteSemiBold, this.Assets);
            UIHelper.SetTextViewFont(tvlogout, (int)UIHelper.LinotteFont.LinotteSemiBold, this.Assets);
            UIHelper.SetTextViewFont(tvMethodOfContent, (int)UIHelper.LinotteFont.LinotteSemiBold, this.Assets);
            UIHelper.SetTextViewFont(tvSendAlerts, (int)UIHelper.LinotteFont.LinotteSemiBold, this.Assets);
            UIHelper.SetEditTextFont(txtphone, (int)UIHelper.LinotteFont.LinotteSemiBold, this.Assets);
            UIHelper.SetButtonFont(btnClose, (int)UIHelper.LinotteFont.LinotteSemiBold, this.Assets);
        }

        /// Method Name     : UIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 20 feb 2018
        /// Purpose         : Find all control   
        /// Revision        : 
        /// </summary>
        private void UIReference()
        {
            tvTitlemyAccount = FindViewById<TextView>(Resource.Id.tvTitlemyAccount);
            tvDisplayName = FindViewById<TextView>(Resource.Id.tvDisplayName);
            tvName = FindViewById<TextView>(Resource.Id.tvName);
            tvPhone = FindViewById<TextView>(Resource.Id.tvPhone);
            tvRegisteredEmail = FindViewById<TextView>(Resource.Id.tvRegisteredEmail);
            tvDisplayEmail = FindViewById<TextView>(Resource.Id.tvDisplayEmail);
            tvMethodOfContent = FindViewById<TextView>(Resource.Id.tvMethodOfContent);
            tvlogout = FindViewById<TextView>(Resource.Id.tvlogout);
            tvSendAlerts = FindViewById<TextView>(Resource.Id.tvSendAlerts);
            txtphone = FindViewById<EditText>(Resource.Id.txtphone);
            btnClose = FindViewById<Button>(Resource.Id.btnClose);
            radioButtonSMS = FindViewById<RadioButton>(Resource.Id.radioButtonSMS);
            radioButtonEmail = FindViewById<RadioButton>(Resource.Id.radioButtonEmail);
            radioButtonYes = FindViewById<RadioButton>(Resource.Id.radiobuttonYes);
            radioButtonNo = FindViewById<RadioButton>(Resource.Id.radiobuttonNo);
            linearLayoutLogout = FindViewById<LinearLayout>(Resource.Id.linearLayoutLogout);
            radioButtonEmail.Checked = false;
            radioButtonYes.Checked = false;
        }

        /// <summary>
        /// Method Name     : UIClickEvent
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Set Click delegate for all control
        /// Revision        : By Jitendra Garg On 24 Feb 2018 To Fix Bug 1445
        /// </summary>
        private void UIClickEvent()
        {
            radioButtonSMS.CheckedChange += RadioButtonSMS_CheckedChange;
            radioButtonEmail.CheckedChange += RadioButtonEmail_CheckedChange;
            radioButtonYes.CheckedChange += RadiobuttonYes_CheckedChange;
            radioButtonNo.CheckedChange += RadiobuttonNo_CheckedChange;
            linearLayoutLogout.Click += LinearLayoutLogout_Click;
            btnClose.Click += BtnClose_ClickAsync;
            txtphone.TextChanged += Txtphone_TextChanged;

            //Add a format watcher for phone number.
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
            {
                txtphone.AddTextChangedListener(new PhoneNumberFormattingTextWatcher("US"));
               
            }
            else
            {
                txtphone.AddTextChangedListener(new PhoneNumberFormattingTextWatcher());
            }
        }

        /// <summary>
        /// Event Name      : Txtphone_TextChanged
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Set button name
        /// Revision        : 
        /// </summary>
        private void Txtphone_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(UtilityPCL.LoginCustomerData.Phone) && txtphone.Text != UtilityPCL.LoginCustomerData.Phone)
            {
                isEdit = true;
                btnClose.Text = "Save";
            }
        }

        /// <summary>
        /// Event Name      : IsPhoneNumberValid
        /// Author          : Jitendra Garg
        /// Creation Date   : 24 Feb 2018
        /// Purpose         : Validates phone number for US format and all zeros
        /// Revision        : 
        /// </summary>
        private bool IsPhoneNumberValid(string numberToValidate)
        {
            //Get the regex for validation
            Regex phoneNumberFormat = new Regex(StringResource.phoneNumberFormatRegex);
            //Validate number
            Match numberFormatMatch = phoneNumberFormat.Match(numberToValidate);

            if (numberFormatMatch.Success)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Event Name      : StripCharacters
        /// Author          : Jitendra Garg
        /// Creation Date   : 27 Feb 2018
        /// Purpose         : Strips unnecessary characters in the phone number
        /// Revision        : 
        /// </summary>
        private string StripCharacters(string numberToStrip)
        {
            //Strip extra characters in the number
            numberToStrip = Regex.Replace(numberToStrip, StringResource.phoneNumberStripCharacterRegex, string.Empty);
            //Strip the initial ISD code if it was entered.
            if (numberToStrip.Length == 11 && numberToStrip.StartsWith("1"))
            {
                numberToStrip = numberToStrip.Substring(1);
            }

            return numberToStrip;
        }

        /// <summary>
        /// Event Name      : BtnClose_ClickAsync
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Use for update Myaccound details
        /// Revision        : By Jitendra Garg On 24 Feb 2018 For Bug 1447
        /// </summary>
        private async void BtnClose_ClickAsync(object sender, EventArgs e)
        {
            if (isEdit)
            {
                btnClose.Text = "Save";
                if (IsPhoneNumberValid(StripCharacters(txtphone.Text)))
                {
                    await EditDataAsync();
                }
                else
                {
                    AlertMessage(StringResource.msgPhoneNumberInvalid);
                }
            }
            else
            {
                btnClose.Text = "Okay";
                StartActivity(new Intent(this, typeof(MainActivity)));
            }
        }

        /// <summary>
        /// Event Name      : LinearLayoutLogout_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 20 feb 2018
        /// Purpose         : Using for logout
        /// Revision        : 
        /// </summary>
        private void LinearLayoutLogout_Click(object sender, EventArgs e)
        {
            Android.App.AlertDialog.Builder dialogue;
            Android.App.AlertDialog alert;
            dialogue = new Android.App.AlertDialog.Builder(new ContextThemeWrapper(this, Resource.Style.AlertDialogCustom));
            alert = dialogue.Create();
            alert.SetMessage(StringResource.msgLogout);
            alert.SetButton(StringResource.msgYes, (c, ev) =>
            {
                alert.Dispose();
                SetLogout();
            });
            alert.SetButton2(StringResource.msgNo, (c, ev) => { });
            alert.Show();
           
        }

        /// <summary>
        /// Mathod Name     : SetLogout()
        /// Author          : Sanket Prajapati
        /// Creation Date   : 20 feb 2018
        /// Purpose         : Using for logout
        /// Revision        : 
        /// </summary>
        private void SetLogout()
        {
            string customerId;
            customerId = sharedPreference.GetString(StringResource.keyCustomerID, string.Empty);
            if (!string.IsNullOrEmpty(customerId))
            {
                Android.Support.V4.App.ActivityCompat.FinishAffinity(this);
                sharedPreference.Edit().PutString(StringResource.keyCustomerID, null).Apply();
                UtilityPCL.LoginCustomerData = null;
                StartActivity(new Intent(this, typeof(LoginActivity)));
            }
        }

        /// <summary>
        /// Event Name      : LinearLayoutLogout_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 20 feb 2018
        /// Purpose         : Using for check on radiobutton
        /// Revision        : 
        /// </summary>
        private void RadiobuttonNo_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            isEdit = true;
            btnClose.Text = "Save";
            if (radioButtonNo.Checked)
            {
                radioButtonYes.Checked = false;
            }
            else
            {
                radioButtonYes.Checked = true;
            }
        }

        /// <summary>
        /// Event Name      : LinearLayoutLogout_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 20 feb 2018
        /// Purpose         : Using for check on radiobutton
        /// Revision        : 
        /// </summary>
        private void RadiobuttonYes_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            isEdit = true;
            btnClose.Text = "Save";
            if (radioButtonYes.Checked)
            {
                radioButtonNo.Checked = false;
            }
            else
            {
                radioButtonNo.Checked = true;
            }
        }

        /// <summary>
        /// Event Name      : LinearLayoutLogout_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 20 feb 2018
        /// Purpose         : Using for check on radiobutton
        /// Revision        : 
        /// </summary>
        private void RadioButtonEmail_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            isEdit = true;
            btnClose.Text = "Save";           
            if (radioButtonEmail.Checked)
            {
                radioButtonSMS.Checked = false;
            }
            else
            {
                radioButtonSMS.Checked = true;
            }
        }

        /// <summary>
        /// Event Name      : LinearLayoutLogout_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 20 feb 2018
        /// Purpose         : Using for check on radiobutton
        /// Revision        : 
        /// </summary>
        private void RadioButtonSMS_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {

            if (radioButtonSMS.Checked)
            {
                radioButtonEmail.Checked = false;
            }
            else
            {
                radioButtonEmail.Checked = true;
            }
        }

        /// <summary>
        /// Method Name     : PopulateData
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : fill myaccount Data  
        /// Revision        : 
        /// </summary>
        public void PopulateData()
        {

            if (UtilityPCL.LoginCustomerData != null)
            {
                txtphone.Text = UtilityPCL.LoginCustomerData.Phone;
                tvDisplayEmail.Text = UtilityPCL.LoginCustomerData.EmailId;
                tvDisplayName.Text = UtilityPCL.LoginCustomerData.CustomerFullName;
                if (!string.IsNullOrEmpty(UtilityPCL.LoginCustomerData.PreferredContact))
                {
                    if (UtilityPCL.LoginCustomerData.PreferredContact == "2")
                    {
                        radioButtonEmail.Checked = true;
                        radioButtonSMS.Checked = false;
                    }
                    else if (UtilityPCL.LoginCustomerData.PreferredContact == "3")
                    {
                        radioButtonEmail.Checked = false;
                        radioButtonSMS.Checked = true;
                    }
                }

                if (UtilityPCL.LoginCustomerData.ReceiveNotifications)
                {
                    radioButtonYes.Checked = true;
                    radioButtonNo.Checked = false;
                }
                else
                {
                    radioButtonYes.Checked = false;
                    radioButtonNo.Checked = true;
                }
                btnClose.Text = "Okay";
                isEdit = false;
            }
        }

        /// <summary>
        /// Method Name     : EditData
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : edit Estimate Data  
        /// Revision        : 
        /// </summary>
        public async Task EditDataAsync()
        {
            MyAccountValidateService myAccountValidateService = new MyAccountValidateService();
            if (!string.IsNullOrEmpty(txtphone.Text))
            {
                PrivacyPolicyModel privacyPolicyModel = new PrivacyPolicyModel() { CustomerId = UtilityPCL.LoginCustomerData.CustomerId,
                    Phone = StripCharacters(txtphone.Text), PreferredContact = (radioButtonEmail.Checked ? "2" : "3"),
                    ReceiveNotifications = (radioButtonYes.Checked) };

                string message = myAccountValidateService.ValidatePrivacyPolicyModel(privacyPolicyModel);
                if (String.IsNullOrEmpty(message))
                {
                    await CallMyAccountService(privacyPolicyModel);
                }
                else
                {
                    AlertMessage(message);
                }
            }
            else
            {
                AlertMessage(StringResource.msgPhoneNoIsRequired);
            }
        }

        /// <summary>
        /// Method Name     : AlertMessage
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Dispaly alert message
        /// Revision        : 
        /// </summary>
        public void AlertMessage(String StrErrorMessage)
        {
            Android.App.AlertDialog.Builder dialogue;
            Android.App.AlertDialog alert;
            dialogue = new Android.App.AlertDialog.Builder(new ContextThemeWrapper(this, Resource.Style.AlertDialogCustom));
            alert = dialogue.Create();
            alert.SetMessage(StrErrorMessage);
            alert.SetButton(StringResource.msgOK, (c, ev) =>
            {
                alert.Dispose();
            });
            alert.Show();
        }

        /// <summary>
        /// Event Name      : CallMyAccountService
        /// Author          : Sanket Prajapati
        /// Creation Date   : 13 Dec 2017
        /// Purpose         : Calls my account service to update account data
        /// Revision        : 
        /// </summary>
        /// <param name="privacyPolicyModel">Privacy policy model.</param>
        private async Task CallMyAccountService(PrivacyPolicyModel privacyPolicyModel)
        {
            APIResponse<PrivacyPolicyModel> serviceResponse=null;
            string errorMessage = string.Empty;
            try
            {
                MyAccount myAccountService = new MyAccount();
                progressDialog = UIHelper.SetProgressDailoge(this);
                serviceResponse = await myAccountService.PutMyAccountDetails(privacyPolicyModel);
                errorMessage = serviceResponse.Message;
            }
            catch (Exception error)
            {
                errorMessage = error.Message;
            }
            finally
            {
                progressDialog.Dismiss();
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    Android.App.AlertDialog.Builder dialogue;
                    Android.App.AlertDialog alert;
                    dialogue = new Android.App.AlertDialog.Builder(new ContextThemeWrapper(this, Resource.Style.AlertDialogCustom));
                    alert = dialogue.Create();
                    alert.SetMessage(errorMessage);
                    alert.SetButton(StringResource.msgOK, (c, ev) =>
                    {
                        if (serviceResponse.STATUS)
                        {
                            StartActivity(new Intent(this, typeof(MainActivity)));
                        }
                        alert.Dispose();
                    });
                    alert.Show();
                }
            }
        }

        /// <summary>
        /// Event Name      : OnBackPressed
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : backup button avoid Login screen
        /// Revision        : 
        /// </summary>
        public override void OnBackPressed()
        {
            AlertMessage(StringResource.msgBackButtonDisable);
        }

    }
}