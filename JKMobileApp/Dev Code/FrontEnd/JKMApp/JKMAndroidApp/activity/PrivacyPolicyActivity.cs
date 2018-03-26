using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Support.V7.App;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using JKMAndroidApp.Common;
using JKMPCL.Model;
using JKMPCL.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JKMAndroidApp.activity
{
    [Activity(Label = "PrivacyPolicyActivity", Theme = "@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = SoftInput.AdjustResize)]
    public class PrivacyPolicyActivity : AppCompatActivity
    {
        ProgressDialog progressDialog;
        TextView tvTitleMsg;
        Button btnAgree;
        Button btnDisagee;
        Login login;
        Android.App.AlertDialog.Builder dialog;
        Android.App.AlertDialog alert;
        private ISharedPreferences sharedPreference;
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            login = new Login();
            sharedPreference = PreferenceManager.GetDefaultSharedPreferences(this);
            // Create your application here
            SetContentView(Resource.Layout.PrivacyPolicy);
            //Find Control
            UIReference();
            btnDisagee.Click += BtnDisagee_Click;
            btnAgree.Click += BtnAgree_Click;
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
            tvTitleMsg = FindViewById<TextView>(Resource.Id.tvTitleMsg);
            btnAgree = FindViewById<Button>(Resource.Id.btnAgree);
            btnDisagee = FindViewById<Button>(Resource.Id.btnDisagree);
            WebView webview; webview = FindViewById<WebView>(Resource.Id.webView1);
            webview.LoadUrl(StringResource.PrivacyPolicyUrl);
        }

        /// <summary>
        /// Event Name      : BtnDisagee_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Open deshboard screen
        /// Revision        : 
        /// </summary>
        private async void BtnDisagee_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ContactusActivity));
            intent.PutExtra(StringResource.msgFromActvity, StringResource.msgPrivacyPolicyActivity);
            StartActivity(intent);
        }

        /// <summary>
        /// Event Name      : BtnAgree_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Open contactus screen
        /// Revision        : 
        /// </summary>
        private async void BtnAgree_Click(object sender, EventArgs e)
        {
            await CallPrivacyPolicyService(true);
        }

        /// <summary>
        /// Method Name     : CallPrivacyPolicyService
        /// Author          : Sanket Prajapati
        /// Creation Date   : 14 Dec 2017
        /// Purpose         : check agree or disagree & move next activity   
        /// Revision        : 
        /// </summary>
        private async Task CallPrivacyPolicyService(bool IsAgree)
        {
            string errorMessage = string.Empty;
            try
            {
                progressDialog = UIHelper.SetProgressDailoge(this);
                ServiceResponse serviceResponse = await login.PrivacyPolicyService(IsAgree);
                if (serviceResponse != null)
                {
                    if (string.IsNullOrEmpty(serviceResponse.Message))
                    {
                       await SetRedirectActivityAsync(serviceResponse, IsAgree);
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
                progressDialog.Dismiss();
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    AlertMessage(errorMessage);
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
        private async Task SetRedirectActivityAsync(ServiceResponse serviceResponse,bool IsAgree)
        {
            if (serviceResponse.Status)
            {
                SetSharedPreference();
                if (IsAgree)
                {
                    await DTOConsumer.BindMoveDataAsync();
                    SetMoveActivity();
                }
            }
            else
            {
                Intent intent = new Intent(this, typeof(ContactusActivity));
                intent.PutExtra(StringResource.msgFromActvity, StringResource.msgPrivacyPolicyActivity);
                StartActivity(intent);
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
        /// Method Name      : SetSharedPreference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Set Customer Id in Shared perfence
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
        /// Method Name     : AlertMessage
        /// Author          : Sanket Prajapati
        /// Creation Date   : 14 Dec 2017
        /// Purpose         : Show Alert message   
        /// Revision        : 
        /// </summary>
        public void AlertMessage(String StrErrorMessage)
        {
            dialog = new Android.App.AlertDialog.Builder(new ContextThemeWrapper(this, Resource.Style.AlertDialogCustom));
            alert = dialog.Create();
            alert.SetMessage(StrErrorMessage);
            alert.SetButton(StringResource.msgOK, (c, ev) =>
            {
                alert.Dispose();
            });
            alert.Show();
        }

        /// <summary>
        /// Method Name     : ApplyFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 14 Dec 2017
        /// Purpose         : Apply font all control   
        /// Revision        : 
        /// </summary>
        public void ApplyFont()
        {
            UIHelper.SetTextViewFont(tvTitleMsg, (int)UIHelper.LinotteFont.LinotteBold, Assets);
            UIHelper.SetButtonFont(btnAgree, (int)UIHelper.LinotteFont.LinotteSemiBold, Assets);
            UIHelper.SetButtonFont(btnDisagee, (int)UIHelper.LinotteFont.LinotteSemiBold, Assets);
        }

        /// <summary>
        /// Event Name      : OnBackPressed
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : backup button avoid Create password and customer password screen
        /// Revision        : 
        /// </summary>
        public override void OnBackPressed()
        {
            dialog = new Android.App.AlertDialog.Builder(new ContextThemeWrapper(this, Resource.Style.AlertDialogCustom));
            alert = dialog.Create();
            alert.SetMessage(StringResource.msgBackButtonDisable);
            alert.SetButton(StringResource.msgOK, (c, ev) =>
            {
                alert.Dispose();
            });
            alert.Show();
        }
    }
}