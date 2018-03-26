using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.Common;
using JKMPCL.Model;
using JKMPCL.Services;
using System;

namespace JKMAndroidApp.activity
{
    [Activity(Label = "VerificationCodeActivity", Theme = "@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = SoftInput.AdjustResize)]
    public class VerificationCodeActivity : AppCompatActivity
    {

        EditText txtVerficationDigit1;
        EditText txtVerficationDigit2;
        EditText txtVerficationDigit3;
        EditText txtVerficationDigit4;
        EditText txtVerficationDigit5;
        EditText txtVerficationDigit6;
        View viewDigit1;
        View viewDigit2;
        View viewDigit3;
        View viewDigit4;
        View viewDigit5;
        View viewDigit6;
        LinearLayout lnLayVerificationCode;
        Button btnContinue;
        TextView tvback;
        TextView tvErrorMsg;
        TextView tvVerifictionCodeLabel;
        ImageButton btnBack;
        Login objLogin;
        ProgressDialog progressDialog;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            objLogin = new Login();
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.VerificationLayout);
            // Create your application here
            FindTextBox();
            // Find Edittext Control
            UIReference();

            tvErrorMsg.Visibility = Android.Views.ViewStates.Gone;
            //Set Font
            ApplyFont();
            // Click event
            btnContinue.Click += BtnContinue_Click;
            btnBack.Click += BtnBack_Click;
            tvback.Click += BtnBack_Click;

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
            FindEditTextAndViews();
            // set Edittext Control LongClicable false
            txtVerficationDigit1.LongClickable = false;
            txtVerficationDigit2.LongClickable = false;
            txtVerficationDigit3.LongClickable = false;
            txtVerficationDigit4.LongClickable = false;
            txtVerficationDigit5.LongClickable = false;
            txtVerficationDigit6.LongClickable = false;

            tvErrorMsg = FindViewById<TextView>(Resource.Id.tvErrorMsg);
            tvback = FindViewById<TextView>(Resource.Id.tvback);
            tvVerifictionCodeLabel = FindViewById<TextView>(Resource.Id.tvVerifictionCodeLabel);
            btnBack = FindViewById<ImageButton>(Resource.Id.btnBack);
            btnContinue = FindViewById<Button>(Resource.Id.btnContinue);
        }

        // <summary>
        /// Event Name      : UIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : For find Edittext control  
        /// Revision        : 
        /// </summary>
        private void FindEditTextAndViews()
        {
            txtVerficationDigit1 = FindViewById<EditText>(Resource.Id.txtVerficationDigit1);
            txtVerficationDigit2 = FindViewById<EditText>(Resource.Id.txtVerficationDigit2);
            txtVerficationDigit3 = FindViewById<EditText>(Resource.Id.txtVerficationDigit3);
            txtVerficationDigit4 = FindViewById<EditText>(Resource.Id.txtVerficationDigit4);
            txtVerficationDigit5 = FindViewById<EditText>(Resource.Id.txtVerficationDigit5);
            txtVerficationDigit6 = FindViewById<EditText>(Resource.Id.txtVerficationDigit6);

            // Find View 
            viewDigit1 = FindViewById<View>(Resource.Id.viewDigit1);
            viewDigit2 = FindViewById<View>(Resource.Id.viewDigit2);
            viewDigit3 = FindViewById<View>(Resource.Id.viewDigit3);
            viewDigit4 = FindViewById<View>(Resource.Id.viewDigit4);
            viewDigit5 = FindViewById<View>(Resource.Id.viewDigit5);
            viewDigit6 = FindViewById<View>(Resource.Id.viewDigit6);
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
            UIHelper.SetTextViewFont(tvVerifictionCodeLabel, (int)UIHelper.LinotteFont.LinotteRegular, Assets);
            UIHelper.SetTextViewFont(tvback, (int)UIHelper.LinotteFont.LinotteSemiBold, Assets);
            UIHelper.SetTextViewFont(tvErrorMsg, (int)UIHelper.LinotteFont.LinotteRegular, Assets);
            UIHelper.SetButtonFont(btnContinue, (int)UIHelper.LinotteFont.LinotteSemiBold, Assets);
        }

        /// <summary>
        /// Event Name      : BtnBack_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Move For Back page in Application
        /// Revision        : 
        /// </summary>
        private void BtnBack_Click(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(LoginActivity)));
        }

        /// <summary>
        /// Event Name      : BtnContinue_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : Move For Next page in Application
        /// Revision        : 
        /// </summary>
        private void BtnContinue_Click(object sender, EventArgs e)
        {
            SetEditTextFocus();
            string errorMessage = string.Empty;
            try
            {
                progressDialog = UIHelper.SetProgressDailoge(this);
                ServiceResponse serviceResponse = objLogin.GetVerifyCode(GetVerificationCodeValue());
                if (serviceResponse != null)
                {
                    if (string.IsNullOrEmpty(serviceResponse.Message))
                    {
                        if (serviceResponse.Status)
                        {
                            StartActivity(new Intent(this, typeof(CreatePasswordActivity)));
                        }
                    }
                    else
                    {
                        errorMessage = serviceResponse.Message;
                    }
                }
            }
            catch (System.Exception error)
            {
                errorMessage = error.Message;
            }
            finally
            {
                progressDialog.Dismiss();
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    tvErrorMsg.Visibility = Android.Views.ViewStates.Visible;
                    tvErrorMsg.Text = errorMessage;
                }
            }
        }

        /// <summary>
        /// Method Name     : OnKeyDown
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : find delete key and remove digit
        /// Revision        : 
        /// </summary>
        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Del)
            {
                SetEditTextBackFocus();
            }
            return base.OnKeyDown(keyCode, e);
        }

        /// <summary>
        /// Method Name     : OnKeyDown
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         :  Set edittext focus and enter value 
        /// Revision        : 
        /// </summary>
        public override bool OnKeyUp(Keycode keyCode, KeyEvent e)
        {
            if (e.KeyCode == Keycode.Num0 || e.KeyCode == Keycode.Num1 || e.KeyCode == Keycode.Num2 || e.KeyCode == Keycode.Num3
                || e.KeyCode == Keycode.Num4 || e.KeyCode == Keycode.Num5 || e.KeyCode == Keycode.Num6 || e.KeyCode == Keycode.Num7 ||
                 e.KeyCode == Keycode.Num8 || e.KeyCode == Keycode.Num9)
            {
                SetEditTextFocus();
                SetFilleditTtxtColor();
            }
            return base.OnKeyUp(keyCode, e);
        }

        /// <summary>
        /// Method Name     : FindTextBox
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : find EdittextBox  
        /// Revision        : 
        /// </summary>
        public void FindTextBox()
        {
            lnLayVerificationCode = FindViewById<LinearLayout>(Resource.Id.lnLayVerficationCode);
            for (int i = 0; i < lnLayVerificationCode.ChildCount; i++)
            {
                View v = lnLayVerificationCode.GetChildAt(i);
                EditText edittextvalue = (EditText)FindViewById(v.Id);
                UIHelper.SetEditTextFont(edittextvalue, (int)UIHelper.LinotteFont.LinotteBold, Assets);
            }
        }

        /// <summary>
        /// Method Name     : SetEditTextFocus
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : set empty edittext focus
        /// Revision        : 
        /// </summary>
        public void SetEditTextFocus()
        {
            if (string.IsNullOrEmpty(txtVerficationDigit1.Text))
            {
                txtVerficationDigit1.RequestFocus();
            }
            else if (string.IsNullOrEmpty(txtVerficationDigit2.Text))
            {
                txtVerficationDigit2.RequestFocus();
            }
            else if (string.IsNullOrEmpty(txtVerficationDigit3.Text))
            {
                txtVerficationDigit3.RequestFocus();
            }
            else if (string.IsNullOrEmpty(txtVerficationDigit4.Text))
            {
                txtVerficationDigit4.RequestFocus();
            }
            else if (string.IsNullOrEmpty(txtVerficationDigit5.Text))
            {
                txtVerficationDigit5.RequestFocus();
            }
            else if (string.IsNullOrEmpty(txtVerficationDigit6.Text))
            {
                txtVerficationDigit6.RequestFocus();
            }
        }

        /// <summary>
        /// Method Name     : SetEditTextFocus
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         :  set color when input time
        /// Revision        : 
        /// </summary>
        public void SetFilleditTtxtColor()
        {
            if (txtVerficationDigit6.Text.Length == 1)
            {
                viewDigit6.SetBackgroundColor(Android.Graphics.Color.Red);
            }
            if (txtVerficationDigit5.Text.Length == 1)
            {
                viewDigit5.SetBackgroundColor(Android.Graphics.Color.Red);
            }
            if (txtVerficationDigit4.Text.Length == 1)
            {
                viewDigit4.SetBackgroundColor(Android.Graphics.Color.Red);
            }
            if (txtVerficationDigit3.Text.Length == 1)
            {
                viewDigit3.SetBackgroundColor(Android.Graphics.Color.Red);
            }
            if (txtVerficationDigit2.Text.Length == 1)
            {
                viewDigit2.SetBackgroundColor(Android.Graphics.Color.Red);
            }
            if (txtVerficationDigit1.Text.Length == 1)
            {
                viewDigit1.SetBackgroundColor(Android.Graphics.Color.Red);
            }
        }

        /// <summary>
        /// Method Name     : SetEditTextFocus
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : set Back edittext focus and use for  delete digite code
        /// Revision        : 
        /// </summary>
        public void SetEditTextBackFocus()
        {
            if (txtVerficationDigit6.Text.Length == 0)
            {
                txtVerficationDigit5.RequestFocus();
                viewDigit6.SetBackgroundColor(Android.Graphics.Color.Gray);
            }
            if (txtVerficationDigit5.Text.Length == 0)
            {
                txtVerficationDigit4.RequestFocus();
                viewDigit5.SetBackgroundColor(Android.Graphics.Color.Gray);
            }
            if (txtVerficationDigit4.Text.Length == 0)
            {
                txtVerficationDigit3.RequestFocus();
                viewDigit4.SetBackgroundColor(Android.Graphics.Color.Gray);
            }
            if (txtVerficationDigit3.Text.Length == 0)
            {
                txtVerficationDigit2.RequestFocus();
                viewDigit3.SetBackgroundColor(Android.Graphics.Color.Gray);
            }
            if (txtVerficationDigit2.Text.Length == 0)
            {
                txtVerficationDigit1.RequestFocus();
                viewDigit2.SetBackgroundColor(Android.Graphics.Color.Gray);
            }
            if (txtVerficationDigit1.Text.Length == 0)
            {
                txtVerficationDigit1.RequestFocus();
                viewDigit1.SetBackgroundColor(Android.Graphics.Color.Gray);
            }
        }

        /// <summary>
        /// Method Name     : GetVerificationCodeValue
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : find EdittextBox  
        /// Revision        : Marge all editText value and make verification code or Check Validation (null or not)
        /// </summary>
        public int? GetVerificationCodeValue()
        {
            string strEditTextValue = string.Empty;
            if (!string.IsNullOrEmpty(txtVerficationDigit1.Text))
            {
                strEditTextValue += txtVerficationDigit1.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtVerficationDigit2.Text))
            {
                strEditTextValue += txtVerficationDigit2.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtVerficationDigit3.Text))
            {
                strEditTextValue += txtVerficationDigit3.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtVerficationDigit4.Text))
            {
                strEditTextValue += txtVerficationDigit4.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtVerficationDigit5.Text))
            {
                strEditTextValue += txtVerficationDigit5.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtVerficationDigit6.Text))
            {
                strEditTextValue += txtVerficationDigit6.Text.Trim();
            }
            if (string.IsNullOrEmpty(strEditTextValue))
            {
                return null;
            }
            else
            {
                return int.Parse(strEditTextValue);
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
            // Not Required
        }
    }
}