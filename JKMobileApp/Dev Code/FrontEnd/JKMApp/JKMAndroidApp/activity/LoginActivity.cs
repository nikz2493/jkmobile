using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using JKMAndroidApp.Common;
using JKMPCL.Model;
using JKMPCL.Services;
using System.Threading.Tasks;

namespace JKMAndroidApp.activity
{
    [Activity(Label = "LoginActivity", NoHistory = true, Theme = "@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait,
        WindowSoftInputMode = SoftInput.AdjustResize)]
    public class LoginActivity : AppCompatActivity
    {
        Button btnlogin;
        EditText txtemailid;
        TextView tvErrorMsg;
        TextView tvEnterEmailid;
        Login login;
        ProgressDialog progressDialog;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            login = new Login();
            SetContentView(Resource.Layout.Login);
            UIReference();
            ApplyFont();
            ///Set Click Event for Login button
            btnlogin.Click += Btnlogin_Click;
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
            btnlogin = FindViewById<Button>(Resource.Id.btnContinue);
            txtemailid = FindViewById<EditText>(Resource.Id.txtemailid);
            tvErrorMsg = FindViewById<TextView>(Resource.Id.tvErrorMsg);
            tvEnterEmailid = FindViewById<TextView>(Resource.Id.tvEnterEmailid);
            tvErrorMsg.Visibility = Android.Views.ViewStates.Gone;
        }

        // <summary>
        /// Event Name      : Btnlogin_ClickAsync
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : For Customer Login    
        /// Revision        : 
        /// </summary>
        private async void Btnlogin_Click(object sender, EventArgs e)
        {
            await CallLoginService();
        }

        /// <summary>
        /// Calls the login service.
        /// </summary>
        private async Task CallLoginService()
        {
            string retMessage = string.Empty;
            try
            {
                progressDialog = UIHelper.SetProgressDailoge(this);
                ServiceResponse serviceResponse = await login.GetCustomerLogin(txtemailid.Text);
                if (serviceResponse != null)
                {
                    if (string.IsNullOrEmpty(serviceResponse.Message))
                    {
                        SetRedirectActivity(serviceResponse);
                    }
                    else
                    {
                        retMessage = serviceResponse.Message;
                        progressDialog.Dismiss();
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
                    tvErrorMsg.Visibility = ViewStates.Visible;
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
        private void SetRedirectActivity(ServiceResponse serviceResponse)
        {
            if (serviceResponse.Status)
            {
                StartActivity(new Intent(this, typeof(CustomerPasswordActivity)));
                progressDialog.Dismiss();
            }
            else
            {
                StartActivity(new Intent(this, typeof(VerificationCodeActivity)));
                progressDialog.Dismiss();
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
            UIHelper.SetTextViewFont(tvEnterEmailid, (int)UIHelper.LinotteFont.LinotteRegular, Assets);
            UIHelper.SetEditTextFont(txtemailid, (int)UIHelper.LinotteFont.LinotteRegular, Assets);
            UIHelper.SetButtonFont(btnlogin, (int)UIHelper.LinotteFont.LinotteSemiBold, Assets);
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
            Android.App.AlertDialog.Builder dialogue;
            Android.App.AlertDialog alert;
            dialogue = new Android.App.AlertDialog.Builder(new ContextThemeWrapper(this, Resource.Style.AlertDialogCustom));
            alert = dialogue.Create();
            alert.SetMessage(StringResource.msgApplicationExit);
            alert.SetButton(StringResource.msgYes, (c, ev) =>
            {
                alert.Dispose();
                Android.Support.V4.App.ActivityCompat.FinishAffinity(this);
            });
            alert.SetButton2(StringResource.msgNo, (c, ev) => { });
            alert.Show();
        }
    }
}