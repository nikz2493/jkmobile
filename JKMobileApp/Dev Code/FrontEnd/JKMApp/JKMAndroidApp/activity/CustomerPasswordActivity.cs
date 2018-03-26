using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.Common;
using JKMPCL.Model;
using JKMPCL.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JKMAndroidApp.activity
{
    [Activity(Label = "CustomerPasswordActivity", Theme = "@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = SoftInput.AdjustResize, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class CustomerPasswordActivity : Activity
    {
        AlertDialog.Builder dialogue;
        AlertDialog alert;
        Button btnContinue;
        Button btnforgetpassword;
        TextView tvErrorMsg;
        TextView tvEnterPasswordLabel;
        EditText txtCustomerPassword;
        Login login;
        ProgressDialog progressDialog;
        private ISharedPreferences sharedPreference;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            login = new Login();
            sharedPreference = PreferenceManager.GetDefaultSharedPreferences(this);
           
            // Create your application here
            SetContentView(Resource.Layout.CustomerPassword);
          
            //Find Control
            UIReference();
           
            //Set font
            ApplyFont();
            tvErrorMsg.Visibility = Android.Views.ViewStates.Gone;
            btnContinue.Click += BtnContinue_ClickAsync;
            btnforgetpassword.Click += Btnforgetpassword_ClickAsync;
        }

        // <summary>
        /// Event Name      : UIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : For find all control  
        /// Revision        : 
        /// </summary>
        private void UIReference()
        {
            btnContinue = FindViewById<Button>(Resource.Id.btnContinue);
            btnforgetpassword = FindViewById<Button>(Resource.Id.btnforgetpassword);
            txtCustomerPassword = FindViewById<EditText>(Resource.Id.txtCustomerpassword);
            txtCustomerPassword.LongClickable = false;
            tvErrorMsg = FindViewById<TextView>(Resource.Id.tvErrorMsg);
            tvEnterPasswordLabel = FindViewById<TextView>(Resource.Id.tvEnterPasswordLabel);
        }

        /// <summary>
        /// Event Name      : Btnforgetpassword_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Move For VerificationCode page in Application
        /// Revision        : 
        /// </summary>      
        private async void Btnforgetpassword_ClickAsync(object sender, EventArgs e)
        {
            string errorMessage = await login.GetVerificationData();
            if (string.IsNullOrEmpty(errorMessage))
            {
                StartActivity(new Intent(this, typeof(VerificationCodeActivity)));
            }
            else
            {
                tvErrorMsg.Visibility = Android.Views.ViewStates.Visible;
                tvErrorMsg.Text = errorMessage;
            }
        }

        /// <summary>
        /// Event Name      : BtnContinue_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Move For Next page in Application
        /// Revision        : 
        /// </summary>
        private async void BtnContinue_ClickAsync(object sender, EventArgs e)
        {
            string retMessage = string.Empty;
            try
            {
                progressDialog= UIHelper.SetProgressDailoge(this);
                ServiceResponse serviceResponse = await login.GetPassword(txtCustomerPassword.Text);
                if (serviceResponse != null)
                {
                    if (string.IsNullOrEmpty(serviceResponse.Message))
                    {
                       await SetRedirectActivityAsync(serviceResponse);
                    }
                    else
                    {
                        retMessage = serviceResponse.Message;
                    }
                }
            }
            catch (Exception error)
            {
                retMessage = error.Message;
            }
            finally
            {
                progressDialog.Dismiss();
                if (!string.IsNullOrEmpty(retMessage))
                {
                    tvErrorMsg.Visibility = Android.Views.ViewStates.Visible;
                    tvErrorMsg.Text = retMessage;
                }
            }
        }

        /// <summary>
        /// Method Name     : SetRedirectActivity
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         :To Redirect Activity 
        /// Revision        : 
        /// </summary>
        private async Task SetRedirectActivityAsync(ServiceResponse serviceResponse)
        {
            if (serviceResponse.Status)
            {
                SetSharedPreference();
                await DTOConsumer.BindMoveDataAsync();
                SetMoveActivity();
            }
            else
            {
                StartActivity(new Intent(this, typeof(PrivacyPolicyActivity)));
            }
        }

        ///// <summary>
        ///// Event Name      : BindgDataAsync
        ///// Author          : Sanket Prajapati
        ///// Creation Date   : 23 jan 2018
        ///// Purpose         : Use for set condition ways flow  
        ///// Revision        : 
        ///// </summary>
        private void SetMoveActivity()
        {
            if (DTOConsumer.dtoEstimateData != null && DTOConsumer.dtoEstimateData.Count != 0)
            {
                if (DTOConsumer.dtoEstimateData.Count == 1)
                {
                    UIHelper.SelectedMoveNumber = DTOConsumer.dtoEstimateData.FirstOrDefault().MoveNumber;
                    Intent intent = new Intent(this, typeof(ActivityEstimateViewPager));
                    StartActivity(intent);
                }
                else
                {
                    UIHelper.SelectedMoveNumber = string.Empty;
                    Intent intent = new Intent(this, typeof(MultipleEstimatedActivity));
                    StartActivity(intent);
                }
            }
            else
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            }
        }

        /// <summary>
        /// Method      : OnBackPressed
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : backup button avoid Login screen
        /// Revision        : 
        /// </summary>
        public override void OnBackPressed()
        {
            dialogue = new AlertDialog.Builder(new ContextThemeWrapper(this, Resource.Style.AlertDialogCustom));
            alert = dialogue.Create();
            alert.SetMessage(StringResource.msgBackButtonDisable);
            alert.SetButton(StringResource.msgOK, (c, ev) =>
            {
                alert.Dispose();
            });
            alert.Show();
        }

        /// <summary>
        /// Method Name      : SetSharedPreference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : set Customer Id in Shared perfence
        /// Revision        : 
        /// </summary>
        private void SetSharedPreference()
        {
            string customerId;
            customerId = sharedPreference.GetString(StringResource.keyCustomerID, string.Empty);
            if (customerId != UtilityPCL.LoginCustomerData.CustomerId)
            {
                sharedPreference.Edit().PutString(StringResource.keyCustomerID, UtilityPCL.LoginCustomerData.CustomerId).Apply();
                sharedPreference.Edit().PutString(StringResource.keyLastLoginDate, DateTime.Now.ToString()).Apply();
                sharedPreference.Edit().PutBoolean(UtilityPCL.LoginCustomerData.CustomerId, true).Apply();
            }
        }

        /// <summary>
        /// Method Name     : ApplyFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Apply font all control   
        /// Revision        : 
        /// </summary>
        public void ApplyFont()
        {
            UIHelper.SetTextViewFont(tvEnterPasswordLabel, (int)UIHelper.LinotteFont.LinotteRegular, Assets);
            UIHelper.SetTextViewFont(tvErrorMsg, (int)UIHelper.LinotteFont.LinotteRegular, Assets);
            UIHelper.SetButtonFont(btnContinue, (int)UIHelper.LinotteFont.LinotteSemiBold, Assets);
            UIHelper.SetButtonFont(btnforgetpassword, (int)UIHelper.LinotteFont.LinotteSemiBold, Assets);
        }
    }
}