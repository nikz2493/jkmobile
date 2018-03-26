using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.Common;
using JKMPCL.Model;
using System.Linq;
using JKMPCL.Services;
using System;
using System.Threading.Tasks;

namespace JKMAndroidApp.activity
{
    /// <summary>
    /// Method Name     : CreatePasswordActivity
    /// Author          : Sanket Prajapati
    /// Creation Date   : 23 Jan 2018
    /// Purpose         : Activity for CreatePassword page
    /// Revision        : 
    /// </summary>
    [Activity(Label = "CreatePasswordActivity", Theme = "@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = SoftInput.AdjustResize)]
    public class CreatePasswordActivity : Activity
    {
        Button btnContinue;
        TextView tvCreatePasswordErrorMsg;
        TextView tvVerifyyPasswordErrorMsg;
        TextView tvCreatePasswordLabel;
        TextView tvVerifictionCodeLabel;
        EditText txtCreatepassword;
        EditText txtVerifypassword;
        ProgressDialog progressDialog;
        Login login;
        AlertDialog.Builder dialogue;
        AlertDialog alert;
        private ISharedPreferences sharedPreference;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            login = new Login();
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreatePassword);
            //Find Control
            UIReference();
            //Apply Font
            ApplyFont();
            btnContinue.Click += async delegate 
            {
                await PopulateDataAsync();
            };
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
            //Find Control
            btnContinue = FindViewById<Button>(Resource.Id.btnContinue);
            txtCreatepassword = FindViewById<EditText>(Resource.Id.txtcreatepassword);
            txtVerifypassword = FindViewById<EditText>(Resource.Id.txtVerifypassword);
            tvCreatePasswordErrorMsg = FindViewById<TextView>(Resource.Id.tvCreatePasswordErrorMsg);
            tvVerifyyPasswordErrorMsg = FindViewById<TextView>(Resource.Id.tvVerifyyPasswordErrorMsg);
            tvCreatePasswordLabel = FindViewById<TextView>(Resource.Id.tvCreatePasswordLabel);
            tvVerifictionCodeLabel = FindViewById<TextView>(Resource.Id.tvVerifictionCodeLabel);
            sharedPreference = PreferenceManager.GetDefaultSharedPreferences(this);

            txtCreatepassword.LongClickable = false;
            txtVerifypassword.LongClickable = false;

            tvCreatePasswordErrorMsg.Visibility = Android.Views.ViewStates.Gone;
            tvVerifyyPasswordErrorMsg.Visibility = Android.Views.ViewStates.Gone;
        }

        /// <summary>
        /// Event Name      : BtnContinue_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Move For Prvivacy Policy Activity in Application
        /// Revision        : 
        /// </summary>    
        private async Task PopulateDataAsync()
        {
            string errorMessage = string.Empty;
            try
            {
                progressDialog = UIHelper.SetProgressDailoge(this);
                ServiceResponse serviceResponse = await login.CreatePassword(txtCreatepassword.Text, txtVerifypassword.Text);
                if (serviceResponse != null)
                {
                    if (string.IsNullOrEmpty(serviceResponse.Message))
                    {
                        dialogue = new AlertDialog.Builder(new ContextThemeWrapper(this, Resource.Style.AlertDialogCustom));
                        alert = dialogue.Create();
                        alert.SetMessage(StringResource.msgPasswordSuccessfully);
                        alert.SetButton(StringResource.msgOK, async (c, ev) =>
                        {
                            alert.Dispose();
                            await SetRedirectActivityAsync(serviceResponse);
                        });
                        alert.Show();
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
                    tvVerifyyPasswordErrorMsg.Visibility = Android.Views.ViewStates.Visible;
                    tvVerifyyPasswordErrorMsg.Text = errorMessage;
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
        /// Event Name      : OnBackPressed
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : backup button avoid verification code screen
        /// Revision        : 
        /// </summary>
        public override void OnBackPressed()
        {
            alert = dialogue.Create();
            alert.SetMessage(StringResource.msgBackButtonDisable);
            alert.SetButton(StringResource.msgOK, (c, ev) =>
            {
                alert.Dispose();
            });

            alert.Show();
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
            UIHelper.SetTextViewFont(tvCreatePasswordLabel, (int)UIHelper.LinotteFont.LinotteRegular, Assets);
            UIHelper.SetTextViewFont(tvVerifictionCodeLabel, (int)UIHelper.LinotteFont.LinotteRegular, Assets);
            UIHelper.SetTextViewFont(tvCreatePasswordErrorMsg, (int)UIHelper.LinotteFont.LinotteRegular, Assets);
            UIHelper.SetTextViewFont(tvVerifyyPasswordErrorMsg, (int)UIHelper.LinotteFont.LinotteRegular, Assets);
            UIHelper.SetButtonFont(btnContinue, (int)UIHelper.LinotteFont.LinotteSemiBold, Assets);
        }

    }
}