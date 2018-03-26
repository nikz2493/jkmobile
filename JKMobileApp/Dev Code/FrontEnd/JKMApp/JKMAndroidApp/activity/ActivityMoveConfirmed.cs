
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.Common;
using JKMPCL.Model;
using JKMPCL.Services;
using JKMPCL.Services.Estimate;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JKMAndroidApp.activity
{
    [Activity(Label = "", Theme = "@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ActivityMoveConfirmed : Activity
    {
        private int count = 1;
        private int iCount;
        private double progress;
        private ProgressBar progressBar;
        private FrameLayout frameTen;
        private TextView tvTitle, tvCount;
        private TextView tvCongo, tvmsg1, tvmsg2, tvmsg3, tvmsg4, tvmsg5,
                       tvDashbord, tvDepositCollected, tvDespositAmmount,
                       tvTransactionId, tvDisplayTransactionId;
        private ImageView imageViewOne, imageViewTwo, imageViewThree, imageViewFour, imageViewFive, imageViewSix, imageViewSeven, imageViewEight, imageViewNine, imageViewTen;
        private ImageView imageViewCompletedOne, imageViewCompletedTwo, imageViewCompletedThree, imageViewCompletedFour, imageViewCompletedFive, imageViewCompletedSix,
                            imageViewCompletedSeven, imageViewCompletedEight, imageViewCompletedNine, imageViewCompletedTen;
        private View viewOne, viewTwo, viewThree, viewFour, viewFive, viewSix, viewSeven, viewEight, viewNine;
        private EstimateModel estimateModel;
        private LinearLayout linearLayoutTransaction;
        private MoveDataModel dtoMoveData;
        private ImageButton btnBack;
        private ProgressDialog progressDialog;
        private ImageView Imgthumb;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LayoutActivityMoveConfirmed);
            estimateModel = DTOConsumer.dtoEstimateData.FirstOrDefault(rc => rc.MoveNumber == UIHelper.SelectedMoveNumber);
            dtoMoveData = DTOConsumer.dtoMoveData;
            UIReference();
            DisplayCount();
            ApplyFont();
        }


        // <summary>
        /// Method Name     : UIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         :  Use for Apply font 
        /// Revision        : 
        /// </summary>
        public void ApplyFont()
        {
            UIHelper.SetTextViewFont(tvTitle, (int)UIHelper.LinotteFont.LinotteBold, Assets);
            UIHelper.SetTextViewFont(tvCount, (int)UIHelper.LinotteFont.LinotteBold, Assets);
            UIHelper.SetTextViewFont(tvCongo, (int)UIHelper.LinotteFont.LinotteBold, Assets);
            UIHelper.SetTextViewFont(tvmsg1, (int)UIHelper.LinotteFont.LinotteSemiBold, Assets);
            UIHelper.SetTextViewFont(tvmsg2, (int)UIHelper.LinotteFont.LinotteSemiBold, Assets);
            UIHelper.SetTextViewFont(tvmsg3, (int)UIHelper.LinotteFont.LinotteSemiBold, Assets);
            UIHelper.SetTextViewFont(tvmsg4, (int)UIHelper.LinotteFont.LinotteSemiBold, Assets);
            UIHelper.SetTextViewFont(tvmsg5, (int)UIHelper.LinotteFont.LinotteSemiBold,Assets);
            UIHelper.SetTextViewFont(tvDepositCollected, (int)UIHelper.LinotteFont.LinotteSemiBold, Assets);
            UIHelper.SetTextViewFont(tvDespositAmmount, (int)UIHelper.LinotteFont.LinotteSemiBold, Assets);
            UIHelper.SetTextViewFont(tvDashbord, (int)UIHelper.LinotteFont.LinotteSemiBold,Assets);
        }

        // <summary>
        /// Method Name     : UIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : For find all control  
        /// Revision        : 
        /// </summary>
        private void UIReference()
        {
            FindProgressbarAndTextView();
            FindImageView();
            FindImageViewCompleted();
            FindView();
            UIMoveconfirmReference();
            imageViewOne.Selected = true;
        }


        /// Method Name     : UIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 12 Dec 2017
        /// Purpose         : Find all control
        /// Revision        : 
        /// </summary>
        private void UIMoveconfirmReference()
        {
            tvCongo = FindViewById<TextView>(Resource.Id.tvCongo);
            tvmsg1 =FindViewById<TextView>(Resource.Id.tvmsg1);
            tvmsg2 = FindViewById<TextView>(Resource.Id.tvmsg2);
            tvmsg3 =FindViewById<TextView>(Resource.Id.tvmsg3);
            tvmsg4 = FindViewById<TextView>(Resource.Id.tvmsg4);
            tvmsg5 =FindViewById<TextView>(Resource.Id.tvmsg5);
            tvDepositCollected =FindViewById<TextView>(Resource.Id.tvDepositCollected);
            tvDespositAmmount =FindViewById<TextView>(Resource.Id.tvDespositAmmount);
            tvDashbord = FindViewById<TextView>(Resource.Id.tvDashbord);
            btnBack = FindViewById<ImageButton>(Resource.Id.btnBack);
            linearLayoutTransaction = FindViewById<LinearLayout>(Resource.Id.linearLayoutTransaction);
            tvTransactionId =FindViewById<TextView>(Resource.Id.tvTransactionId);
            tvDisplayTransactionId = FindViewById<TextView>(Resource.Id.tvDisplayTransactionId);
            Imgthumb = FindViewById<ImageView>(Resource.Id.Imgthumb);
            btnBack.Click += async delegate
            {
                await GoToDesboardAsync();
            };
            tvDashbord.Click += async delegate
            {
                await GoToDesboardAsync();
            };
        }

        /// Method Name     : GoToDesboardAsync
        /// Author          : Sanket Prajapati
        /// Creation Date   : 12 Dec 2017
        /// Purpose         :Use for Update Estimed 
        /// Revision        : 
        /// </summary>
        private async Task GoToDesboardAsync()
        {
            Estimate estimate;
            estimate = new Estimate();
            string retMessage = string.Empty;
            try
            {
                progressDialog = UIHelper.SetProgressDailoge(this);
                if (estimateModel.IsAddressEdited || estimateModel.IsServiceDate || estimateModel.IsWhatMatterMostEdited || estimateModel.IsValuationEdited)
                {
                    APIResponse<EstimateModel> aPIResponse = await estimate.PutEstimateData(estimateModel, estimateModel.MoveNumber);
                    if (aPIResponse.STATUS)
                    {
                        await UpdateMoveDataAsync();
                    }
                    else
                    {
                        AlertMessage(aPIResponse.Message);
                    }
                }
                else
                {
                    await UpdateMoveDataAsync();
                }
            }
            catch (Exception error)
            {
                retMessage = error.Message;
                progressDialog.Dismiss();
            }
            finally
            {
                progressDialog.Dismiss();
                if (!string.IsNullOrEmpty(retMessage))
                {
                    AlertMessage(retMessage);
                }
            }
        }

        /// Method Name     : UpdateMoveDataAsync
        /// Author          : Sanket Prajapati
        /// Creation Date   : 12 Dec 2017
        /// Purpose         : Use for Update move data 
        /// Revision        : 
        /// </summary>
        public async Task UpdateMoveDataAsync()
        {
            Move move;
            move = new Move();
            dtoMoveData.StatusReason = GetMoveStatusReason(estimateModel);
            APIResponse<MoveDataModel> aPIResponse = await move.PutMoveData(dtoMoveData, estimateModel.MoveNumber);
            if (aPIResponse.STATUS)
            {
                AlertMessage(StringResource.msgEstimateUpdate);
                await DTOConsumer.BindMoveDataAsync();
                StartActivity(new Intent(this, typeof(MainActivity)));
            }
            else
            {
                AlertMessage(aPIResponse.Message);
            }
        }

        /// <summary>
        /// Method Name     : GetMoveStatusReason
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Use for Set move StatusReason 
        /// Revision        : 
        /// </summary>
        public string GetMoveStatusReason(EstimateModel estimateModel)
        {
            if (estimateModel.PaymentStatus)
            {
                return "100000000";
            }
            else
            {
                return "148050000"; //needs overrides
            }
        }

        /// <summary>
        /// Method Name      : AlertMessage
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Use for move back fragment
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


        // <summary>
        /// Method Name     : FindImageView
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : For find all imageview Completed control  
        /// Revision        : 
        /// </summary>
        private void FindImageViewCompleted()
        {
            imageViewCompletedOne = FindViewById<ImageView>(Resource.Id.imageViewCompletedOne);
            imageViewCompletedTwo = FindViewById<ImageView>(Resource.Id.imageViewCompletedTwo);
            imageViewCompletedThree = FindViewById<ImageView>(Resource.Id.imageViewCompletedThree);
            imageViewCompletedFour = FindViewById<ImageView>(Resource.Id.imageViewCompletedFour);
            imageViewCompletedFive = FindViewById<ImageView>(Resource.Id.imageViewCompletedFive);
            imageViewCompletedSix = FindViewById<ImageView>(Resource.Id.imageViewCompletedSix);
            imageViewCompletedSeven = FindViewById<ImageView>(Resource.Id.imageViewCompletedSeven);
            imageViewCompletedEight = FindViewById<ImageView>(Resource.Id.imageViewCompletedEight);
            imageViewCompletedNine = FindViewById<ImageView>(Resource.Id.imageViewCompletedNine);
            imageViewCompletedTen = FindViewById<ImageView>(Resource.Id.imageViewCompletedTen);
        }

        // <summary>
        /// Method Name     : FindImageView
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : For find all imageview control  
        /// Revision        : 
        /// </summary>
        private void FindImageView()
        {
            imageViewOne = FindViewById<ImageView>(Resource.Id.imageViewOne);
            imageViewTwo = FindViewById<ImageView>(Resource.Id.imageViewTwo);
            imageViewThree = FindViewById<ImageView>(Resource.Id.imageViewThree);
            imageViewFour = FindViewById<ImageView>(Resource.Id.imageViewFour);
            imageViewFive = FindViewById<ImageView>(Resource.Id.imageViewFive);
            imageViewSix = FindViewById<ImageView>(Resource.Id.imageViewSix);
            imageViewSeven = FindViewById<ImageView>(Resource.Id.imageViewSeven);
            imageViewEight = FindViewById<ImageView>(Resource.Id.imageViewEight);
            imageViewNine = FindViewById<ImageView>(Resource.Id.imageViewNine);
            imageViewTen = FindViewById<ImageView>(Resource.Id.imageViewTen);
        }

        // <summary>
        /// Method Name     : FindImageView
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : For find all view control  
        /// Revision        : 
        /// </summary>
        private void FindView()
        {
            viewOne = FindViewById<View>(Resource.Id.viewOne);
            viewTwo = FindViewById<View>(Resource.Id.viewTwo);
            viewThree = FindViewById<View>(Resource.Id.viewThree);
            viewFour = FindViewById<View>(Resource.Id.viewFour);
            viewFive = FindViewById<View>(Resource.Id.viewFive);
            viewSix = FindViewById<View>(Resource.Id.viewSix);
            viewSeven = FindViewById<View>(Resource.Id.viewSeven);
            viewEight = FindViewById<View>(Resource.Id.viewEight);
            viewNine = FindViewById<View>(Resource.Id.viewNine);
            frameTen = FindViewById<FrameLayout>(Resource.Id.frameTen);
        }

        // <summary>
        /// Method Name     : FindImageView
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : For find textview and progress control  
        /// Revision        : 
        /// </summary>
        private void FindProgressbarAndTextView()
        {
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            progressBar.ProgressDrawable = Resources.GetDrawable(Resource.Drawable.progressbar);
            progress = 100;
            progressBar.Progress = (int)progress;
            tvTitle = FindViewById<TextView>(Resource.Id.textViewTitle);
            tvCount = FindViewById<TextView>(Resource.Id.textViewCount);
            tvTitle.Text = StringResource.wizMoveConfirmed;
        
        }

        private void DisplayCount()
        {
            estimateModel = DTOConsumer.dtoEstimateData.FirstOrDefault(rc => rc.MoveNumber == UIHelper.SelectedMoveNumber);
            if (estimateModel != null && !estimateModel.IsDepositPaid)
            {
                viewNine.Visibility = ViewStates.Visible;
                frameTen.Visibility = ViewStates.Visible;
                iCount = 10;
                tvCount.Text = "10";
                wizMoveConfirmedWitDeposit();
            }
            else
            {
                viewNine.Visibility = ViewStates.Gone;
                frameTen.Visibility = ViewStates.Gone;
                tvCount.Text = "09";
                iCount = 9;
                wizMoveConfirmed();
            }
            //viewNine.Visibility = ViewStates.Gone;
            //frameTen.Visibility = ViewStates.Gone;
            //tvCount.Text = "9";
            //iCount = 9;
            //wizMoveConfirmed();
        }

        // <summary>
        /// Method Name     : wizMoveConfirmed
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Set image Selector for MoveConfirmed wizard
        /// Revision        : 
        /// </summary>
        private void wizMoveConfirmed()
        {
            ResetImageSelector();
            imageViewNine.Selected = true;
            imageViewCompletedOne.Visibility = ViewStates.Visible;
            imageViewCompletedTwo.Visibility = ViewStates.Visible;
            imageViewCompletedThree.Visibility = ViewStates.Visible;
            imageViewCompletedFour.Visibility = ViewStates.Visible;
            imageViewCompletedFive.Visibility = ViewStates.Visible;
            imageViewCompletedSix.Visibility = ViewStates.Visible;
            imageViewCompletedSeven.Visibility = ViewStates.Visible;
            imageViewCompletedEight.Visibility = ViewStates.Visible;
            viewOne.Selected = true;
            viewTwo.Selected = true;
            viewThree.Selected = true;
            viewFour.Selected = true;
            viewFive.Selected = true;
            viewSix.Selected = true;
            viewSeven.Selected = true;
            viewEight.Selected = true;
        }

        // <summary>
        /// Method Name     : wizMoveConfirmed
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Set image Selector for MoveConfirmed wizard
        /// Revision        : 
        /// </summary>
        private void wizMoveConfirmedWitDeposit()
        {
            ResetImageSelector();
            imageViewTen.Selected = true;
            imageViewCompletedOne.Visibility = ViewStates.Visible;
            imageViewCompletedTwo.Visibility = ViewStates.Visible;
            imageViewCompletedThree.Visibility = ViewStates.Visible;
            imageViewCompletedFour.Visibility = ViewStates.Visible;
            imageViewCompletedFive.Visibility = ViewStates.Visible;
            imageViewCompletedSix.Visibility = ViewStates.Visible;
            imageViewCompletedSeven.Visibility = ViewStates.Visible;
            imageViewCompletedEight.Visibility = ViewStates.Visible;
            imageViewCompletedNine.Visibility = ViewStates.Visible;

            viewOne.Selected = true;
            viewTwo.Selected = true;
            viewThree.Selected = true;
            viewFour.Selected = true;
            viewFive.Selected = true;
            viewSix.Selected = true;
            viewSeven.Selected = true;
            viewEight.Selected = true;
            viewNine.Selected = true;

        }

        // <summary>
        /// Method Name     : ResetImageSelector
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : Reset image Selector
        /// Revision        : 
        /// </summary>
        private void ResetImageSelector()
        {
            imageViewOne.Selected = false;
            imageViewTwo.Selected = false;
            imageViewThree.Selected = false;
            imageViewFour.Selected = false;
            imageViewFive.Selected = false;
            imageViewSix.Selected = false;
            imageViewSeven.Selected = false;
            imageViewEight.Selected = false;
            imageViewNine.Selected = false;

            imageViewCompletedOne.Visibility = ViewStates.Gone;
            imageViewCompletedTwo.Visibility = ViewStates.Gone;
            imageViewCompletedThree.Visibility = ViewStates.Gone;
            imageViewCompletedFour.Visibility = ViewStates.Gone;
            imageViewCompletedFive.Visibility = ViewStates.Gone;
            imageViewCompletedSix.Visibility = ViewStates.Gone;
            imageViewCompletedSeven.Visibility = ViewStates.Gone;
            imageViewCompletedEight.Visibility = ViewStates.Gone;
            imageViewCompletedNine.Visibility = ViewStates.Gone;
        }
    }
}