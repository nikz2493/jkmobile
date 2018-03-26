using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.Common;
using JKMAndroidApp.activity;
using JKMPCL.Model;
using JKMPCL.Services.Estimate;
using JKMPCL.Services;
using System.Threading.Tasks;

namespace JKMAndroidApp.fragment
{
    /// <summary>
    /// Method Name     : FragmentMoveConfirmed
    /// Author          : Sanket Prajapati
    /// Creation Date   : 23 Jan 2018
    /// Purpose         : Fragement for moveconfirmed page
    /// Revision        : 
    /// </summary>
    public class FragmentMoveConfirmed : Android.Support.V4.App.Fragment
    {
        View view;
        private TextView tvCongo, tvmsg1, tvmsg2, tvmsg3, tvmsg4, tvmsg5, 
                         tvDashbord, tvDepositCollected, tvDespositAmmount, 
                         tvTransactionId, tvDisplayTransactionId;

        private EstimateModel estimateModel;
        private LinearLayout linearLayoutTransaction;
        private Estimate estimate;
        private MoveDataModel dtoMoveData;
        private ImageButton btnBack;
        private ProgressDialog progressDialog;
        private ImageView Imgthumb;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.LayoutFragmentMoveConfirmed, container, false);
            estimateModel = DTOConsumer.dtoEstimateData.FirstOrDefault(rc => rc.MoveNumber == UIHelper.SelectedMoveNumber);
            dtoMoveData = DTOConsumer.dtoMoveData;
            estimate = new Estimate();
            UIReference();
            ApplyFont();
            view.Invalidate();
            return view;
        }

        public override void OnResume()
        {
            UIReference();
            PopulateData();
            SetMessage();
            base.OnResume();
        }

        /// Method Name     : UIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 12 Dec 2017
        /// Purpose         : Find all control
        /// Revision        : 
        /// </summary>
        private void UIReference()
        {
            tvCongo = view.FindViewById<TextView>(Resource.Id.tvCongo);
            tvmsg1 = view.FindViewById<TextView>(Resource.Id.tvmsg1);
            tvmsg2 = view.FindViewById<TextView>(Resource.Id.tvmsg2);
            tvmsg3 = view.FindViewById<TextView>(Resource.Id.tvmsg3);
            tvmsg4 = view.FindViewById<TextView>(Resource.Id.tvmsg4);
            tvmsg5 = view.FindViewById<TextView>(Resource.Id.tvmsg5);
            tvDepositCollected = view.FindViewById<TextView>(Resource.Id.tvDepositCollected);
            tvDespositAmmount = view.FindViewById<TextView>(Resource.Id.tvDespositAmmount);
            tvDashbord = view.FindViewById<TextView>(Resource.Id.tvDashbord);
            btnBack = view.FindViewById<ImageButton>(Resource.Id.btnBack);
            linearLayoutTransaction= view.FindViewById<LinearLayout>(Resource.Id.linearLayoutTransaction);
            tvTransactionId= view.FindViewById<TextView>(Resource.Id.tvTransactionId);
            tvDisplayTransactionId= view.FindViewById<TextView>(Resource.Id.tvDisplayTransactionId);
            Imgthumb= view.FindViewById<ImageView>(Resource.Id.Imgthumb);
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
            string retMessage = string.Empty;
            try
            {
                progressDialog = UIHelper.SetProgressDailoge(Activity);
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
                StartActivity(new Intent(Activity, typeof(MainActivity)));
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
            dialogue = new Android.App.AlertDialog.Builder(new ContextThemeWrapper(Activity, Resource.Style.AlertDialogCustom));
            alert = dialogue.Create();
            alert.SetMessage(StrErrorMessage);
            alert.SetButton(StringResource.msgOK, (c, ev) =>
            {
                alert.Dispose();
            });
            alert.Show();
        }

        /// Method Name     : ApplyFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Apply font all control   
        /// Revision        : 
        /// </summary>
        public void ApplyFont()
        {
            UIHelper.SetTextViewFont(tvCongo, (int)UIHelper.LinotteFont.LinotteBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvmsg1, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvmsg2, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvmsg3, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvmsg4, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvmsg5, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDepositCollected, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDespositAmmount, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDashbord, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
        }

        /// <summary>
        /// Method Name     : PopulateData
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : fill Estimate Data 
        /// Revision        : 
        /// </summary>
        public void PopulateData()
        {
            if (estimateModel != null && string.IsNullOrEmpty(estimateModel.message))
            {
                tvDespositAmmount.Text =UtilityPCL.CurrencyFormat(estimateModel.Deposit);
                estimateModel.Deposit= UtilityPCL.RemoveCurrencyFormat(estimateModel.Deposit);
                estimateModel.EstimatedLineHaul= UtilityPCL.RemoveCurrencyFormat(estimateModel.EstimatedLineHaul);
                if(estimateModel.PaymentStatus)
                {
                    linearLayoutTransaction.Visibility = ViewStates.Visible;
                    tvDisplayTransactionId.Text = estimateModel.TransactionId;
                }
                else
                {
                    linearLayoutTransaction.Visibility = ViewStates.Gone;
                    tvDisplayTransactionId.Text =string.Empty;
                }
               
            }
        }

        public  void SetMessage()
        {
            if (!estimateModel.PaymentStatus)
            {
                Imgthumb.Visibility = ViewStates.Gone;
                tvCongo.Text = "Sorry! Your payment was not successful.";
                tvCongo.SetTextSize( Android.Util.ComplexUnitType.Sp ,14);
                tvmsg1.Text = "Don't worry though. You can go to dashboard,";
                tvmsg2.Text = "and still save the move details. Our sales";
                tvmsg3.Text = "representative will contact you shortly,  ";
                tvmsg4.Text = "and help with the booking process.";
                tvmsg5.Visibility = ViewStates.Invisible;
            }
            else if(estimateModel.IsDepositPaidByCheck)
            {
                Imgthumb.Visibility = ViewStates.Invisible;
                tvCongo.Visibility = ViewStates.Invisible;
                tvmsg1.Text = "We have received your payment request";
                tvmsg2.Text ="with a check. Please contact our sales";
                tvmsg3.Text = "representative for payment status. You will find ";
                tvmsg4.Text = "he information about the move after payment process";
                tvmsg5.Text = "will completed successfully.";
            }
        }
    }
}