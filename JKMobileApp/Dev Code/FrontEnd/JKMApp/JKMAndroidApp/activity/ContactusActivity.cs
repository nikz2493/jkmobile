using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.Common;
using JKMPCL.Model;
using JKMPCL.Services;
using System;
using System.Threading.Tasks;

namespace JKMAndroidApp.activity
{
    [Activity(Label = "ContactusActivity", Theme = "@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = SoftInput.AdjustResize)]
    public class ContactusActivity : Activity
    {
        TextView tvTitleMsg;
        TextView tvQuestionLabel;
        TextView tvSend;
        TextView tvPhoneInfo;
        TextView tvEmailInfo;
        TextView tvMoveCoordinaterLabel;
        TextView tvOrLabel;
        TextView tvSendEamilAtLabel;
        EditText txtQuestion;
        ImageButton btnsend;
        ImageButton imgBtnPhoneinfo;
        ImageButton imgBtnEamilinfo;
        Button btnClose;

        ProgressDialog progressDialog;
        AlertDialog.Builder dialogue;
        AlertDialog alert;
        Move move;
       
        string fullName;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            move = new Move();
            // Create your application here
            SetContentView(Resource.Layout.Contactus);

            //Find control
            UIReference();

            btnClose.Click += BtnClose_Click;
            btnsend.Click += TvSend_Click;
            tvSend.Click += TvSend_Click;

            imgBtnPhoneinfo.Click += TvPhoneInfo_Click;
            tvPhoneInfo.Click += TvPhoneInfo_Click;

            imgBtnEamilinfo.Click += TvEmailInfo_Click;
            tvEmailInfo.Click += TvEmailInfo_Click;

            await CallGetContactMoveService();
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
            tvQuestionLabel = FindViewById<TextView>(Resource.Id.tvQuestionLabel);
            txtQuestion = FindViewById<EditText>(Resource.Id.txtQuestion);
            tvSend = FindViewById<TextView>(Resource.Id.tvSend);
            btnsend = FindViewById<ImageButton>(Resource.Id.btnsend);
            btnClose = FindViewById<Button>(Resource.Id.btnClose);
            tvPhoneInfo = FindViewById<TextView>(Resource.Id.tvPhoneInfo);
            tvEmailInfo = FindViewById<TextView>(Resource.Id.tvEmailInfo);
            tvMoveCoordinaterLabel = FindViewById<TextView>(Resource.Id.tvMoveCoordinaterLabel);
            tvOrLabel = FindViewById<TextView>(Resource.Id.tvOrLabel);
            tvSendEamilAtLabel = FindViewById<TextView>(Resource.Id.tvSendEamilatLabel);
            imgBtnPhoneinfo = FindViewById<ImageButton>(Resource.Id.imgbtnPhoneinfo);
            imgBtnEamilinfo = FindViewById<ImageButton>(Resource.Id.imgbtnEamilinfo);
        }


        /// <summary>
        /// Event Name      : TvPhoneInfo_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Open call window when user tab Phoneinfo label
        /// Revision        : 
        /// </summary>
        private void TvPhoneInfo_Click(object sender, EventArgs e)
        {
            string strPhoneNo = tvPhoneInfo.Text;
            var uri = Android.Net.Uri.Parse("tel:" + strPhoneNo.ToString());
            var intent = new Intent(Intent.ActionDial, uri);
            StartActivity(intent);
        }

        /// <summary>
        /// Event Name      : TvEmailInfo_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Open Email window when user tab Emailinfo label
        /// Revision        : 
        /// </summary>
        private void TvEmailInfo_Click(object sender, EventArgs e)
        {
            var email = new Intent(Android.Content.Intent.ActionSendto);
            email.SetData(Android.Net.Uri.Parse("mailto:"));
            email.PutExtra(Android.Content.Intent.ExtraEmail, new string[]
               {
                   tvEmailInfo.Text
              });
            if (!string.IsNullOrEmpty(UtilityPCL.LoginCustomerData.CustomerFullName))
            {
                email.PutExtra(Intent.ExtraSubject, string.Format(StringResource.msgContactusSubject, UtilityPCL.LoginCustomerData.CustomerFullName));
            }
            else
            {
                email.PutExtra(Intent.ExtraSubject, string.Format(StringResource.msgContactusSubject, "Customer"));
            }
            this.StartActivityForResult(email, 101);
        }

        /// <summary>
        /// Event Name      : TvSend_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Open Email window when user tab Emailinfo label
        /// Revision        : 
        /// </summary>
        private void TvSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtQuestion.Text.Trim()))
            {
               AlertMessage(StringResource.msgQuestionSendSuccefully);
            }
            else
            {
                AlertMessage(StringResource.msgEnterquestion);
            }
        }

        /// <summary>
        /// Method Name     : CallGetContactMoveService
        /// Author          : Hiren Patel
        /// Creation Date   : 5 Jan 2018
        /// Purpose         : To calls get contact move services
        /// Revision        : 
        /// </summary>
        private async Task CallGetContactMoveService()
        {
            try
            {
                progressDialog = UIHelper.SetProgressDailoge(this);
                APIResponse<GetContactListForMoveResponse> serviceResponse = await move.GetContactListForMove();
                if (serviceResponse.STATUS)
                {
                    if (serviceResponse.DATA is null)
                    {
                        //Defult value display
                    }
                    else
                    {
                        tvEmailInfo.Text = serviceResponse.DATA.internalemailaddress.ToLower();
                        string strPhone = serviceResponse.DATA.address1_telephone1;
                        if (!string.IsNullOrEmpty(strPhone))
                        {
                            tvPhoneInfo.Text = strPhone;
                        }
                        fullName = serviceResponse.DATA.fullname;
                    }
                }
            }
            finally
            {
                progressDialog.Dismiss();
            }
        }
        /// <summary>
        /// Method Name     : AlertMessage
        /// Author          : Sanket prajapati
        /// Creation Date   : 5 Jan 2018
        /// Purpose         : Show alert message
        /// Revision        : 
        /// </summary>
        public void AlertMessage(String StrErrorMessage)
        {

            dialogue = new AlertDialog.Builder(new ContextThemeWrapper(this, Resource.Style.AlertDialogCustom));
            alert = dialogue.Create();
            alert.SetMessage(StrErrorMessage);
            alert.SetButton(StringResource.msgOK, (c, ev) =>
            {
                if (!string.IsNullOrEmpty(txtQuestion.Text.Trim())) { txtQuestion.Text = string.Empty; }
                alert.Dispose();
            });
            alert.Show();
        }

        /// <summary>
        /// Event Name      : BtnContinue_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Finish this activity
        /// Revision        : 
        /// </summary>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Finish();
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
            UIHelper.SetTextViewFont(tvQuestionLabel, (int)UIHelper.LinotteFont.LinotteRegular, Assets);
            UIHelper.SetEditTextFont(txtQuestion, (int)UIHelper.LinotteFont.LinotteRegular, Assets);
            UIHelper.SetTextViewFont(tvSend, (int)UIHelper.LinotteFont.LinotteSemiBold, Assets);
            UIHelper.SetButtonFont(btnClose, (int)UIHelper.LinotteFont.LinotteSemiBold, Assets);

            UIHelper.SetTextViewFont(tvPhoneInfo, (int)UIHelper.LinotteFont.LinotteBold, Assets);
            UIHelper.SetTextViewFont(tvEmailInfo, (int)UIHelper.LinotteFont.LinotteBold, Assets);
            UIHelper.SetTextViewFont(tvMoveCoordinaterLabel, (int)UIHelper.LinotteFont.LinotteRegular, Assets);
            UIHelper.SetTextViewFont(tvOrLabel, (int)UIHelper.LinotteFont.LinotteBold, Assets);
            UIHelper.SetTextViewFont(tvSendEamilAtLabel, (int)UIHelper.LinotteFont.LinotteRegular, Assets);
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