using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static JKMPCL.Services.UtilityPCL;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.Common;
using Android.Support.V4.View;
using JKMPCL.Model;
using JKMPCL.Services;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JKMPCL.Services.Payment;
using JKMAndroidApp.adapter;
using System.Net;
using Android.Text;
using static JKMAndroidApp.adapter.PaymentControlAdapter;
using JKMPCL.Services.Estimate;

namespace JKMAndroidApp.fragment
{
    /// <summary>
    /// Method Name     : FragmentPayment
    /// Author          : Sanket Prajapati
    /// Creation Date   : 23 Jan 2018
    /// Purpose         : Fragement for Payment page
    /// Revision        : 
    /// </summary>
    public class FragmentPayment : Android.Support.V4.App.Fragment, IAdapterTextViewClickListener, IAdapterTextChangeListener
    {
        View view;
       

        TextView tvTotalCost, tvDisplayTotalCost, tvTotalPaid, tvDateDisplayTotalPaid, tvDepositCollected,
                 tvDisplayDepositCollected, tvTotalDue, tvDisplayTotalDue, tvClose, tvAddOtherCard;
        // Final Payment Control
        TextView tvAmount, tvCVV, tvExpYear, tvExpMonth,
                 tvNameofCardHolder, tvCardNumber, textViewMakePayment;
        EditText txtAmount, txtNameofCardHolder, txtCardNumber, txtCVV;
        Spinner spinnerExpMonth, spinnerExpYear;
        ImageView imgCard;
        RelativeLayout RelativeLayoutFianlPaymentControl;
        FrameLayout framlayEnable;
        LinearLayout linearLayoutCenter;
        private EstimateValidateServices estimateValidateServices;

        /// Payment Successfully control
        TextView tvPaymentSubmittedtitle, tvSuccessfullmsg1, tvSuccessfullmsg2, tvTransectionId, tvDisplayTransectionId, tvAmountPaid, tvDisaplyAmountPaid;
        ImageView imgPaymentSuccessfull;

        ///Payment fail control
        TextView tvPaymentfailtitle, tvFailmsg1, tvFailmsg2, tvFailTransectionId, tvDisplayFailTransectionId;
        ImageView imgPaymentFailed;

        LinearLayout linearLayoutClose, linearLayoutAddOtherCard;
        ViewPager paymentViewPager;

        CardType cardType;
        List<MonthYearModel> monthList, yearList;
        List<LayoutModel> layoutModelList;
        PaymentModel paymentModel;
       
        Payment payment;
        AlertDialog.Builder dialogue;
        AlertDialog alert;
        ProgressDialog progressDialog;

        readonly int FinalPaymentlId = Resource.Layout.FinalPaymentControl;
        readonly int PaymentSuccesfullylId = Resource.Layout.LayoutPaymentSuccessfull;
        readonly int PaymentFaillId = Resource.Layout.LayoutPaymentFail;
        private bool isFormatCardNumber = true;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.LayoutFragmentFinalPayment, container, false);
            estimateValidateServices = new EstimateValidateServices();
            layoutModelList = LayoutModelList();
            UIReferences();
            UIClickEvents();
            monthList = BindMonthList();
            yearList = BindYearList();
            BindSpinerMonth();
            BindSpinerYear();
            PopulateData();
            BindPaymentControlAdapter();
            ApplyFont();
            framlayEnable.Visibility = ViewStates.Visible;
            framlayEnable.Clickable = true;
            return view;
        }

        /// Method Name     : BindSpinerMonth
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use fort bind month   
        /// Revision        : 
        /// </summary>
        private void BindSpinerMonth()
        {
            List<string> valutionList = MonthList();
            if (valutionList.Count > 0)
            {
                var monthAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerItem, valutionList);
                monthAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerExpMonth.Adapter = monthAdapter;
                string currentMonth = System.DateTime.Now.Month.ToString(StringResource.formate00);
                MonthYearModel monthYearModel = monthList.FirstOrDefault(rc => rc.Month == currentMonth);
                if (monthYearModel.Monthindex == 0) { spinnerExpMonth.SetSelection(monthYearModel.Monthindex); } else spinnerExpMonth.SetSelection(monthYearModel.Monthindex - 1);
            }
        }

        /// Method Name     : BindSpiner
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use fort bind year  
        /// Revision        : 
        /// </summary>
        private void BindSpinerYear()
        {
            List<string> valutionList = YearList();
            if (valutionList.Count > 0)
            {
                var yearAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerItem, valutionList);
                yearAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerExpYear.Adapter = yearAdapter;
            }
        }

        /// <summary>
        /// Method Name     : ApplyFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Feb 2018
        /// Purpose         : Apply font all control   
        /// Revision        : 
        /// </summary>
        public void ApplyFont()
        {
            ApplyFontFinalLayout();
            ApplyFontPayment();
            ApplyFontPaymentSuccesfully();
            ApplyFontPaymentFail();
        }

        /// </summary>
        /// Method Name     : UIReferences
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Feb 2018
        /// Purpose         : Finds Control  
        /// Revision        : 
        /// </summary>
        private void UIReferences()
        {
            UIReferencesFinalLayout();
            UIReferencesForPayment();
            UIReferencesForPaymentFail();
            UIReferencesForPaymentSuccesfully();
        }

        /// </summary>
        /// Method Name     : UIReferences
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Feb 2018
        /// Purpose         : Finds Control  
        /// Revision        : 
        /// </summary>
        private void UIReferencesFinalLayout()
        {
            tvTotalCost = view.FindViewById<TextView>(Resource.Id.tvTotalCost);
            tvDisplayTotalCost = view.FindViewById<TextView>(Resource.Id.tvDisplayTotalCost);
            tvTotalPaid = view.FindViewById<TextView>(Resource.Id.tvTotalPaid);
            tvDateDisplayTotalPaid = view.FindViewById<TextView>(Resource.Id.tvDateDisplayTotalPaid);
            tvDepositCollected = view.FindViewById<TextView>(Resource.Id.tvDepositCollected);
            tvDisplayDepositCollected = view.FindViewById<TextView>(Resource.Id.tvDisplayDepositCollected);
            tvTotalDue = view.FindViewById<TextView>(Resource.Id.tvTotalDue);
            tvDisplayTotalDue = view.FindViewById<TextView>(Resource.Id.tvDisplayTotalDue);
            tvClose = view.FindViewById<TextView>(Resource.Id.tvClose);
            tvAddOtherCard = view.FindViewById<TextView>(Resource.Id.tvAddOtherCard);
            linearLayoutClose = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutClose);
            linearLayoutAddOtherCard = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutAddOtherCard);
            paymentViewPager = view.FindViewById<ViewPager>(Resource.Id.paymentViewPager);
            framlayEnable = view.FindViewById<FrameLayout>(Resource.Id.framlayEnable);
            linearLayoutCenter = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutCenter);
        }

        /// </summary>
        /// Method Name     : UIReferencesForPayment
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Feb 2018
        /// Purpose         : Finds  Payment fail layout Control    
        /// Revision        : 
        /// </summary>
        private void UIReferencesForPayment()
        {
            ViewGroup viewPayment = (ViewGroup)paymentViewPager.RootView;
            LayoutInflater inflater = LayoutInflater.From(Activity);
            ViewGroup FinaPaymentlayout = (ViewGroup)inflater.Inflate(FinalPaymentlId, viewPayment, false);
            RelativeLayoutFianlPaymentControl = FinaPaymentlayout.FindViewById<RelativeLayout>(Resource.Id.RelativeLayoutFianlPaymentControl);
           
            tvAmount = FinaPaymentlayout.FindViewById<TextView>(Resource.Id.tvAmount);
            tvCVV = FinaPaymentlayout.FindViewById<TextView>(Resource.Id.tvCVV);
            tvExpYear = FinaPaymentlayout.FindViewById<TextView>(Resource.Id.tvExpYear);
            tvExpMonth = FinaPaymentlayout.FindViewById<TextView>(Resource.Id.tvExpMonth);
            tvNameofCardHolder = FinaPaymentlayout.FindViewById<TextView>(Resource.Id.tvNameofCardHolder);
            tvCardNumber = FinaPaymentlayout.FindViewById<TextView>(Resource.Id.tvCardNumber);
            txtAmount = FinaPaymentlayout.FindViewById<EditText>(Resource.Id.txtAmount);
            txtNameofCardHolder = FinaPaymentlayout.FindViewById<EditText>(Resource.Id.txtNameofCardHolder);
            txtCardNumber = FinaPaymentlayout.FindViewById<EditText>(Resource.Id.txtCardNumber);
            txtCVV = FinaPaymentlayout.FindViewById<EditText>(Resource.Id.txtCVV);
            spinnerExpMonth = FinaPaymentlayout.FindViewById<Spinner>(Resource.Id.spinnerExpMonth);
            spinnerExpYear = FinaPaymentlayout.FindViewById<Spinner>(Resource.Id.spinnerExpYear);
            textViewMakePayment = FinaPaymentlayout.FindViewById<TextView>(Resource.Id.textViewMakePayment);
            imgCard = FinaPaymentlayout.FindViewById<ImageView>(Resource.Id.imgCard);
        }

        /// </summary>
        /// Method Name     : UIReferencesForPaymentSuccesfully
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Feb 2018
        /// Purpose         : Finds  Payment Successfully layout Control  
        /// Revision        : 
        /// </summary>
        private void UIReferencesForPaymentFail()
        {
            ViewGroup viewPaymentFail = (ViewGroup)paymentViewPager.RootView;
            LayoutInflater inflater = LayoutInflater.From(Activity);
            ViewGroup PaymentSuccesfullylayout = (ViewGroup)inflater.Inflate(PaymentFaillId, viewPaymentFail, false);

            tvPaymentfailtitle = PaymentSuccesfullylayout.FindViewById<TextView>(Resource.Id.tvPaymentfailtitle);
            tvFailmsg1 = PaymentSuccesfullylayout.FindViewById<TextView>(Resource.Id.tvFailmsg1);
            tvFailmsg2 = PaymentSuccesfullylayout.FindViewById<TextView>(Resource.Id.tvFailmsg2);
            tvFailTransectionId = PaymentSuccesfullylayout.FindViewById<TextView>(Resource.Id.tvFailTransectionId);
            tvDisplayFailTransectionId = PaymentSuccesfullylayout.FindViewById<TextView>(Resource.Id.tvDisplayFailTransectionId);
            imgPaymentFailed = PaymentSuccesfullylayout.FindViewById<ImageView>(Resource.Id.imgPaymentFailed);
        }

        /// </summary>
        /// Method Name     : UIReferencesForPaymentSuccesfully
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Feb 2018
        /// Purpose         : Finds  Payment Successfully layout Control  
        /// Revision        : 
        /// </summary>
        private void UIReferencesForPaymentSuccesfully()
        {
            ViewGroup viewPaymentSuccesfully = (ViewGroup)paymentViewPager.RootView;
            LayoutInflater inflater = LayoutInflater.From(Activity);
            ViewGroup PaymentSuccesfullylayout = (ViewGroup)inflater.Inflate(PaymentSuccesfullylId, viewPaymentSuccesfully, false);

            tvPaymentSubmittedtitle = PaymentSuccesfullylayout.FindViewById<TextView>(Resource.Id.tvPaymentSubmittedtitle);
            tvSuccessfullmsg1 = PaymentSuccesfullylayout.FindViewById<TextView>(Resource.Id.tvSuccessfullmsg1);
            tvSuccessfullmsg2 = PaymentSuccesfullylayout.FindViewById<TextView>(Resource.Id.tvSuccessfullmsg2);
            tvTransectionId = PaymentSuccesfullylayout.FindViewById<TextView>(Resource.Id.tvTransectionId);
            tvDisplayTransectionId = PaymentSuccesfullylayout.FindViewById<TextView>(Resource.Id.tvDisplayTransectionId);
            tvAmountPaid = PaymentSuccesfullylayout.FindViewById<TextView>(Resource.Id.tvAmountPaid);
            tvDisaplyAmountPaid = PaymentSuccesfullylayout.FindViewById<TextView>(Resource.Id.tvDisaplyAmountPaid);
            imgPaymentSuccessfull = PaymentSuccesfullylayout.FindViewById<ImageView>(Resource.Id.imgPaymentSuccessfull);
        }

        /// </summary>
        /// Method Name     : BindPaymentControlAdapter
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Feb 2018
        /// Purpose         : Use for bind Payment control adapter 
        /// Revision        : 
        /// </summary>
        private void BindPaymentControlAdapter()
        {
            layoutModelList.Last<LayoutModel>().transactionAmout = txtAmount.Text;
            PaymentControlAdapter paymentControlAdapter = new PaymentControlAdapter(Activity, layoutModelList, this, paymentViewPager, this);
            paymentViewPager.Adapter = paymentControlAdapter;
            linearLayoutClose.Enabled = false;
            linearLayoutAddOtherCard.Enabled = false;

            //disable color
            linearLayoutAddOtherCard.SetBackgroundResource(Resource.Drawable.RoundCornerAddOtherCardDisable);
            linearLayoutClose.SetBackgroundResource(Resource.Drawable.RoundCornerCloseDisable);
        }

        /// </summary>
        /// Method Name     : UIClickEvents
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Feb 2018
        /// Purpose         : Set control events
        /// Revision        : 
        /// </summary>
        private void UIClickEvents()
        {
            linearLayoutClose.Click += LinearLayoutClose_Click;
            linearLayoutAddOtherCard.Click += TvAddOtherCard_Click;
        }

        /// </summary>
        /// Click Name      : LinearLayoutClose_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Feb 2018
        /// Purpose         : Use for close payment card 
        /// Revision        : 
        /// </summary>
        private void LinearLayoutClose_Click(object sender, EventArgs e)
        {
            int lastIndex = layoutModelList.Last<LayoutModel>().Index;
            layoutModelList.RemoveAt(lastIndex);
            PaymentControlAdapter paymentControlAdapter = new PaymentControlAdapter(Activity, layoutModelList, this, paymentViewPager, this);
            paymentViewPager.Adapter = paymentControlAdapter;
            paymentViewPager.CurrentItem = layoutModelList.Last<LayoutModel>().Index;
            linearLayoutClose.Enabled = false;
            linearLayoutAddOtherCard.Enabled = true;

            //Disable color
            linearLayoutClose.SetBackgroundResource(Resource.Drawable.RoundCornerCloseDisable);
            //Enable color
            linearLayoutAddOtherCard.SetBackgroundResource(Resource.Drawable.RoundCornerAddOtherCard);

        }

        /// </summary>
        /// Click Name      : TvAddOtherCard_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Feb 2018
        /// Purpose         : Use for add payment card 
        /// Revision        : 
        /// </summary>
        private void TvAddOtherCard_Click(object sender, EventArgs e)
        {
            BindPaymentData();
            DisablePaymentControl();
            int lastIndex = layoutModelList.Last<LayoutModel>().Index;
            layoutModelList.Add(new LayoutModel(lastIndex + 1, FinalPaymentlId) { transactionAmout = txtAmount.Text });
            layoutModelList[lastIndex + 1].transactionAmout = txtAmount.Text;
            PaymentControlAdapter paymentControlAdapter = new PaymentControlAdapter(Activity, layoutModelList, this, paymentViewPager, this);
            paymentViewPager.Adapter = paymentControlAdapter;
            paymentViewPager.CurrentItem = layoutModelList.Last<LayoutModel>().Index;
            linearLayoutClose.Enabled = true;
            linearLayoutAddOtherCard.Enabled = false;

            //disable color
            linearLayoutAddOtherCard.SetBackgroundResource(Resource.Drawable.RoundCornerAddOtherCardDisable);
            //Enable color
            linearLayoutClose.SetBackgroundResource(Resource.Drawable.RoundCornerClose);
        }

        public List<LayoutModel> LayoutModelList()
        {
            layoutModelList = new List<LayoutModel>();
            layoutModelList.Add(new LayoutModel(0, FinalPaymentlId));
            return layoutModelList;
        }

        /// </summary>
        /// Class Name      : LayoutModel
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Feb 2018
        /// Purpose         : Use for set layout and find index in adapter 
        /// Revision        : 
        /// </summary>
        public class LayoutModel
        {
            public int Index { get; set; }
            public int layoutResId { get; set; }
            public string TransactionID { get; set; }
            public string transactionAmout { get; set; }

            public LayoutModel(int index, int LayoutResId)
            {
                Index = index;
                layoutResId = LayoutResId;
            }
            public int getTitleResId()
            {
                return Index;
            }
            public int getLayoutResId()
            {
                return layoutResId;
            }
        }

        /// <summary>
        /// Method Name     : PopulateData
        /// Author          : Hiren Patel
        /// Creation Date   : 28 feb 2018
        /// Purpose         : Use for payment populate data 
        /// Revision        : 
        /// </summary>
        public async void PopulateData()
        {
            await GetPaymentData();
        }

        /// <summary>
        /// Method Name     : GetPaymentData
        /// Author          : Hiren Patel
        /// Creation Date   : 28 feb 2018
        /// Purpose         : Use for payment populate data 
        /// Revision        : 
        /// </summary>
        public async Task GetPaymentData()
        {
            APIResponse<PaymentModel> serviceResponse;
            paymentModel = new PaymentModel() { CustomerID = UtilityPCL.LoginCustomerData.CustomerId, MoveID = DTOConsumer.dtoMoveData.MoveNumber };
            serviceResponse = new APIResponse<PaymentModel>() { STATUS = false };
            string errorMessage = string.Empty;
            try
            {
                payment = new Payment();
                progressDialog = UIHelper.SetProgressDailoge(Activity);
                serviceResponse = await payment.GetPaymentAmount(paymentModel);
                if (serviceResponse.STATUS)
                {
                    if (serviceResponse.DATA != null)
                    {
                        paymentModel = serviceResponse.DATA;
                        BindPaymentData();
                        BindPaymentControlAdapter();
                        DisablePaymentControl();
                    }
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
                progressDialog.Dismiss();
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    AlertMessage(errorMessage);
                }
            }
        }

        /// </summary>
        /// Event Name      : CalculateDueAmountToPaymentModel
        /// Author          : Sanket Prajapati
        /// Creation Date   : 28 feb 2018
        /// Purpose         : Use for  Calculate DueAmount To PaymentModel
        /// Revision        : 
        /// </summary>
        private void CalculateDueAmountToPaymentModel()
        {
            decimal totalCost = 0;
            decimal totalPaid = 0;
            decimal transactionAmount = 0;

            if (!string.IsNullOrEmpty(paymentModel.TransactionAmount))
            {
                transactionAmount = Convert.ToDecimal(paymentModel.TransactionAmount);
            }

            if (!string.IsNullOrEmpty(paymentModel.TotalCost))
            {
                totalCost = Convert.ToDecimal(paymentModel.TotalCost);
            }

            if (!string.IsNullOrEmpty(paymentModel.TotalPaid))
            {
                totalPaid = Convert.ToDecimal(paymentModel.TotalPaid) + transactionAmount;
                paymentModel.TotalPaid = Convert.ToString(totalPaid);
            }
            else
            {
                totalPaid = transactionAmount;
                paymentModel.TotalPaid = Convert.ToString(totalPaid);
            }

            paymentModel.TotalDue = UtilityPCL.RemoveCurrencyFormat(Convert.ToString(totalCost - totalPaid));

        }

        /// </summary>
        /// Event Name      : BindPaymentData
        /// Author          : Sanket Prajapati
        /// Creation Date   : 28 feb 2018
        /// Purpose         : Use for bind and refresh data payment data 
        /// Revision        : 
        /// </summary>
        private void BindPaymentData()
        {
            decimal totalCost = 0;
            decimal totalPaid = 0;

            tvDisplayTotalCost.Text = UtilityPCL.CurrencyFormat(paymentModel.TotalCost);
            tvDateDisplayTotalPaid.Text = UtilityPCL.CurrencyFormat(paymentModel.TotalPaid);
            tvDisplayDepositCollected.Text = UtilityPCL.CurrencyFormat(paymentModel.Deposit);

            if (!string.IsNullOrEmpty(paymentModel.TotalCost))
            {
                totalCost = Convert.ToDecimal(paymentModel.TotalCost);
            }

            if (!string.IsNullOrEmpty(paymentModel.TotalPaid))
            {
                totalPaid = Convert.ToDecimal(paymentModel.TotalPaid);
            }

            tvDisplayTotalDue.Text = UtilityPCL.CurrencyFormat(Convert.ToString(totalCost - totalPaid));
            if (txtAmount != null)
            {
                txtAmount.Text = tvDisplayTotalDue.Text;
            }

            if (txtAmount != null && totalCost <= totalPaid)
            {
                txtAmount.Text = string.Empty;
            }
        }


        public void DisablePaymentControl()
        {
            decimal totalCost = 0;
            decimal totalPaid = 0;
            decimal totalDue = 0;

            if (!string.IsNullOrEmpty(paymentModel.TotalCost))
            {
                totalCost = Convert.ToDecimal(paymentModel.TotalCost);
            }

            if (!string.IsNullOrEmpty(paymentModel.TotalPaid))
            {
                totalPaid = Convert.ToDecimal(paymentModel.TotalPaid);
            }
           
            if (  totalCost<= totalPaid)
            {
                linearLayoutCenter.Enabled = false;

                framlayEnable.Visibility = ViewStates.Visible;
                framlayEnable.Clickable = true;
                txtNameofCardHolder.SetCursorVisible(false);
                txtCardNumber.SetCursorVisible(false);
                txtAmount.Text = string.Empty;
                linearLayoutClose.Enabled = false;
                linearLayoutAddOtherCard.Enabled = false;

                paymentViewPager.Enabled = false;

                //disable color
                linearLayoutAddOtherCard.SetBackgroundResource(Resource.Drawable.RoundCornerAddOtherCardDisable);
                linearLayoutClose.SetBackgroundResource(Resource.Drawable.RoundCornerCloseDisable);
                RelativeLayoutFianlPaymentControl.Enabled = false;
            }
            else
            {
                txtCardNumber.SetCursorVisible(false);
                txtNameofCardHolder.SetCursorVisible(true);
                framlayEnable.Visibility = ViewStates.Gone;
                framlayEnable.Clickable = false;
               
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
            dialogue = new AlertDialog.Builder(new ContextThemeWrapper(Activity, Resource.Style.AlertDialogCustom));
            alert = dialogue.Create();
            alert.SetMessage(StrErrorMessage);
            alert.SetButton(StringResource.msgOK, (c, ev) =>
            {
                alert.Dispose();
            });
            alert.Show();
        }

        /// </summary>
        /// Event Name      : FindPaymentControl
        /// Author          : Sanket Prajapati
        /// Creation Date   : 28 feb 2018
        /// Purpose         : Use for Find payment control 
        /// Revision        : 
        /// </summary>
        private void FindPaymentControl(ViewGroup layout)
        {
            RelativeLayoutFianlPaymentControl = layout.FindViewById<RelativeLayout>(Resource.Id.RelativeLayoutFianlPaymentControl);

            tvAmount = layout.FindViewById<TextView>(Resource.Id.tvAmount);
            tvCVV = layout.FindViewById<TextView>(Resource.Id.tvCVV);
            tvExpYear = layout.FindViewById<TextView>(Resource.Id.tvExpYear);
            tvExpMonth = layout.FindViewById<TextView>(Resource.Id.tvExpMonth);
            tvNameofCardHolder = layout.FindViewById<TextView>(Resource.Id.tvNameofCardHolder);
            tvCardNumber = layout.FindViewById<TextView>(Resource.Id.tvCardNumber);
            txtAmount = layout.FindViewById<EditText>(Resource.Id.txtAmount);
            txtNameofCardHolder = layout.FindViewById<EditText>(Resource.Id.txtNameofCardHolder);
            txtCardNumber = layout.FindViewById<EditText>(Resource.Id.txtCardNumber);
            txtCVV = layout.FindViewById<EditText>(Resource.Id.txtCVV);
            spinnerExpMonth = layout.FindViewById<Spinner>(Resource.Id.spinnerExpMonth);
            spinnerExpYear = layout.FindViewById<Spinner>(Resource.Id.spinnerExpYear);
            textViewMakePayment = layout.FindViewById<TextView>(Resource.Id.textViewMakePayment);
            imgCard = layout.FindViewById<ImageView>(Resource.Id.imgCard);
        }

        /// </summary>
        /// Event Name      : PaymentTransaction
        /// Author          : Sanket Prajapati
        /// Creation Date   : 31 jan 2018
        /// Purpose         : Set payment details
        /// Revision        : 
        /// </summary>
        async void IAdapterTextViewClickListener.OnTextviewActionClick(View v, ViewGroup layout)
        {
            string errormessage = string.Empty;
            try
            {
                FindPaymentControl(layout);
                progressDialog = UIHelper.SetProgressDailoge(Activity);
                errormessage = Validation();
                if (string.IsNullOrEmpty(errormessage))
                {
                    APIResponse<PaymentTransactonModel> paymentTransacton = await ProcessPaymentTransactionAsync();
                    if (paymentTransacton.STATUS)
                    {
                        SetPaymentStatusCard(true, paymentTransacton);
                    }
                    else
                    {
                        SetPaymentStatusCard(false, paymentTransacton);
                    }
                }

            }
            catch (Exception error)
            {
                errormessage = error.Message;
            }
            finally
            {
                progressDialog.Dismiss();
                if (!string.IsNullOrEmpty(errormessage))
                {
                    AlertMessage(errormessage);
                }
            }
        }

        /// </summary>
        /// Method Name     : PaymentTransaction
        /// Author          : Sanket Prajapati
        /// Creation Date   : 31 jan 2018
        /// Purpose         : Set payment details
        /// Revision        : 
        /// </summary>
        public async Task<APIResponse<PaymentTransactonModel>> ProcessPaymentTransactionAsync()
        {
            payment = new Payment();
            APIResponse<PaymentModel> serviceResponse;
            APIResponse<PaymentTransactonModel> paymentTransacton = new APIResponse<PaymentTransactonModel>();
            PaymentGatewayModel paymentGatewayModel = PaymentTransactionAsync();

            if (paymentGatewayModel != null)
            {
                paymentTransacton = await payment.ProcessPaymentTransaction(paymentGatewayModel);
                if (paymentTransacton.STATUS)
                {
                    serviceResponse = await CallPutPaymentTransaction(paymentTransacton);
                    if (serviceResponse.STATUS)
                    {
                        paymentTransacton.STATUS = true;
                    }
                    else
                    {
                        paymentTransacton.STATUS = false;
                    }
                }
                else
                {
                    paymentTransacton.STATUS = false;
                }
            }
            return paymentTransacton;
        }

        /// </summary>
        /// Method Name     : SetLayoutStatusWays
        /// Author          : Sanket Prajapati
        /// Creation Date   : 31 jan 2018
        /// Purpose         : Set Layout payment staus ways
        /// Revision        : 
        /// </summary>
        private void SetPaymentStatusCard(bool isPayment, APIResponse<PaymentTransactonModel> paymentTransacton)
        {
            LayoutModel mobject;
            int lastIndex = layoutModelList.Last<LayoutModel>().Index;
            mobject = SetLayoutStatusWays(isPayment, paymentTransacton);
            layoutModelList[lastIndex] = mobject;
            PaymentControlAdapter paymentControlAdapter = new PaymentControlAdapter(Activity, layoutModelList, this, paymentViewPager, this);
            paymentViewPager.Adapter = paymentControlAdapter;
            paymentViewPager.CurrentItem = layoutModelList.Last<LayoutModel>().Index;
            linearLayoutClose.Enabled = false;
            linearLayoutAddOtherCard.Enabled = true;
            //Disable color
            linearLayoutClose.SetBackgroundResource(Resource.Drawable.RoundCornerCloseDisable);
            //Enable color
            linearLayoutAddOtherCard.SetBackgroundResource(Resource.Drawable.RoundCornerAddOtherCard);
            BindPaymentData();
            DisablePaymentControl();
        }

        /// </summary>
        /// Method Name     : SetLayoutStatusWays
        /// Author          : Sanket Prajapati
        /// Creation Date   : 31 jan 2018
        /// Purpose         : Set Layout payment staus ways
        /// Revision        : 
        /// </summary>
        public LayoutModel SetLayoutStatusWays(bool isPayment, APIResponse<PaymentTransactonModel> paymentTransacton)
        {
            LayoutModel layoutModel = null;
            int lastIndex = layoutModelList.Last<LayoutModel>().Index;
            if (isPayment)
            {
                layoutModel = new LayoutModel(lastIndex, PaymentSuccesfullylId);
                layoutModel.TransactionID = paymentTransacton.DATA.TransactionID;
                layoutModel.transactionAmout = txtAmount.Text;
            }
            else
            {
                layoutModel = new LayoutModel(lastIndex, PaymentFaillId);
                if (!string.IsNullOrEmpty(paymentTransacton.DATA.TransactionID))
                {
                    layoutModel.TransactionID = paymentTransacton.DATA.TransactionID;
                }
                else
                {
                    layoutModel.TransactionID =StringResource.msg000000000;
                }
            }
            return layoutModel;
        }

        /// <summary>
        /// Method Name     : CallPostPaymentTransaction
        /// Author          : Hiren Patel
        /// Creation Date   : 15 Feb 2018
        /// Purpose         : Calls the post payment transaction.
        /// Revision        : 
        /// </summary>
        /// <returns>The post payment transaction.</returns>
        /// <param name="paymentTransactonModel">Payment transacton model.</param>
        /// <param name="estimateModel">Estimate model.</param>
        private async Task<APIResponse<PaymentModel>> CallPutPaymentTransaction(APIResponse<PaymentTransactonModel> paymentTransactonModel)
        {
            payment = new Payment();
            APIResponse<PaymentModel> serviceResponse = new APIResponse<PaymentModel>() { STATUS = false };
            string errorMessage = string.Empty;
            try
            {
                paymentModel.CustomerID = LoginCustomerData.CustomerId;
                paymentModel.MoveID = DTOConsumer.dtoMoveData.MoveNumber;
                paymentModel.TransactionNumber = paymentTransactonModel.DATA.TransactionID;
                paymentModel.TransactionDate = UtilityPCL.DisplayDateFormatForEstimate(DateTime.Now, string.Empty);
                paymentModel.TransactionAmount = txtAmount.Text;
                CalculateDueAmountToPaymentModel();
                serviceResponse = await payment.PutPaymentAmount(paymentModel);
            }
            catch (Exception error)
            {
                errorMessage = error.Message;
            }
            finally
            {
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    AlertMessage(errorMessage);
                }
            }
            return serviceResponse;
        }

        /// </summary>
        /// Method Name     : PaymentTransaction
        /// Author          : Sanket Prajapati
        /// Creation Date   : 31 jan 2018
        /// Purpose         : Set payment details
        /// Revision        : 
        /// </summary>
        public PaymentGatewayModel PaymentTransactionAsync()
        {
            PaymentGatewayModel paymentGatewayModel = new PaymentGatewayModel();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            paymentGatewayModel.CardExpiryDate = spinnerExpMonth.SelectedItem.ToString() + spinnerExpYear.SelectedItem.ToString().Substring(2, 2);
            paymentGatewayModel.CreditCardNumber = txtCardNumber.Text.Replace("-","").Trim();
            paymentGatewayModel.CVVNo = Convert.ToInt32(txtCVV.Text.Trim());
            paymentGatewayModel.TransactionAmout = Convert.ToDouble(txtAmount.Text);

            PaymentGatewayModel paymentGatewayModelFilled = FillCustomerData(paymentGatewayModel);
            if (paymentGatewayModelFilled != null)
            {
                paymentGatewayModel = paymentGatewayModelFilled;
            }
            return paymentGatewayModel;
        }

        /// </summary>
        /// Method Name     : FillCustomerData
        /// Author          : Sanket Prajapati
        /// Creation Date   : 31 jan 2018
        /// Purpose         : Set customer details
        /// Revision        : 
        /// </summary>
        private PaymentGatewayModel FillCustomerData(PaymentGatewayModel paymentModel)
        {
            if (LoginCustomerData != null)
            {
                String firstName = string.Empty;
                String lastName = string.Empty;
                if (!string.IsNullOrEmpty(txtNameofCardHolder.Text.Trim()))
                {
                    string[] customerName = txtNameofCardHolder.Text.Trim().Split(' ');
                    if (customerName.Length > 1)
                    {
                        firstName = customerName[0];
                        lastName = customerName[1];
                    }
                }
                paymentModel.CustomerID = LoginCustomerData.CustomerId;
                paymentModel.FirstName = firstName;
                paymentModel.LastName = lastName;
                paymentModel.EmailID = LoginCustomerData.EmailId;
            }
            return paymentModel;
        }

        /// </summary>
        /// Method Name     : TextViewCardNumberTextChange
        /// Author          : Sanket Prajapati
        /// Creation Date   : 1 March 2018
        /// Purpose         : Use for check card type
        /// Revision        : 
        /// </summary>
        public void OnTextChangeActionClick()
        {
            TextViewCardNumberTextChange();
        }

        /// </summary>
        /// Method Name     : TextViewCardNumberTextChange
        /// Author          : Sanket Prajapati
        /// Creation Date   : 1 March 2018
        /// Purpose         : Use for check card type
        /// Revision        : 
        /// </summary>
        private void TextViewCardNumberTextChange()
        {
            if (!string.IsNullOrEmpty(txtCardNumber.Text.Trim()) && txtCardNumber.Text.Length >= 4)
            {
                SetCardType();
                if (!isFormatCardNumber)
                {
                    txtCardNumber.SetSelection(txtCardNumber.Text.Length);
                    return;
                }
                else
                {
                    SetCardFormat();
                }
            }
            else
            {
                imgCard.SetImageResource(Resource.Drawable.icon_payment_active);
            }
        }

        /// <summary>
        /// Method Name     : SetCardType
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 1 March 2018
        /// Purpose         : check card type based on card number
        /// Revision        : 
        /// </summary>
        private void SetCardType()
        {
            cardType = GetCardType(txtCardNumber.Text);
            switch (cardType)
            {
                case CardType.MasterCard:
                    imgCard.SetImageResource(Resource.Drawable.icon_master);
                    break;
                case CardType.VISA:
                    imgCard.SetImageResource(Resource.Drawable.icon_visa);
                    break;
                default:
                    imgCard.SetImageResource(Resource.Drawable.icon_payment_active);
                    break;
            }
        }

        /// <summary>
        /// Method Name     : TxtCardNumber_KeyPress
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 28 Feb 2018
        /// Purpose         : to set flag isFormatCardNumber on erase number & to manage SetCardFormat() functionality
        /// Revision        : 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool OnKeyListener(View v, [GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            txtCardNumber = (EditText)v;

            if (e.KeyCode == Keycode.Del || e.KeyCode == Keycode.Back)
            {
                return isFormatCardNumber = false;
            }
            else
            {
                return isFormatCardNumber = true;
            }
        }

        /// <summary>
        /// Method Name     : SetCardFormat
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 28 Feb 2018
        /// Purpose         : add "-" after each 4 digit in credit card number
        /// Revision        : 
        /// </summary>
        public void SetCardFormat()
        {
            try
            {
                if (txtCardNumber.Text.Length < 16 && isFormatCardNumber)
                {
                    string cardNumber = txtCardNumber.Text.Replace("-", "").Trim();
                    string finalCardNumber = string.Empty;

                    for (int i = 0; i < cardNumber.Length; i++)
                    {
                        if (i % 4 == 0 && i > 0)
                        {
                            finalCardNumber += "-";
                            finalCardNumber += cardNumber[i].ToString();
                            isFormatCardNumber = false;
                        }
                        else
                        {
                            finalCardNumber += cardNumber[i].ToString();
                        }
                    }
                    //assign value to txtCardNumber only in case of isFormatCardNumber is false other wise it will looped infinite and application will crash
                    if (!isFormatCardNumber)
                    {
                        txtCardNumber.Text += "-";
                        int position = txtCardNumber.Text.Length;
                        txtCardNumber.SetSelection(position);
                        txtCardNumber.Text = finalCardNumber;
                    }
                }
            }
            catch
            {
                //To be implemented later
            }
        }

        /// <summary>
        /// Method Name     : Validation
        /// Author          : Sanket Prajapati
        /// Creation Date   : 13 jan 2018
        /// Purpose         : Use for validation   
        /// Revision        : 
        /// </summary>
        private string Validation()
        {
            MonthYearModel yearValue = yearList.FirstOrDefault(rc => rc.Year == spinnerExpYear.SelectedItem.ToString());
            MonthYearModel monthValue = monthList.FirstOrDefault(rc => rc.Month == spinnerExpMonth.SelectedItem.ToString());
            string errormessage = string.Empty;

            CardTypeInfoModel cardTypeInfoModel = UtilityPCL.GetCardTypes().FirstOrDefault(rc => rc.CardType == cardType);
            if (string.IsNullOrEmpty(txtAmount.Text.Trim()))
            {
                errormessage = StringResource.msgPleaseEnterAmount;
            }
            else if (!string.IsNullOrEmpty(txtAmount.Text.Trim()) && Convert.ToDouble(txtAmount.Text.Trim()) > Convert.ToDouble(UtilityPCL.RemoveCurrencyFormat(tvDisplayTotalDue.Text.Trim())))
            {
                errormessage = StringResource.msgValidAmount;
            }
            else
            {
                errormessage = CardHolderAndValidCardValidation(yearValue, monthValue, cardTypeInfoModel);
            }
            return errormessage;
        }

        /// <summary>
        /// Method Name     : CardHolderAndValidCardValidation
        /// Author          : Sanket Prajapati
        /// Creation Date   : 13 jan 2018
        /// Purpose         : Use for card holder and card validation   
        /// Revision        : 
        /// </summary>
        public string CardHolderAndValidCardValidation(MonthYearModel yearValue, MonthYearModel monthValue, CardTypeInfoModel cardTypeInfoModel)
        {
            string errormessage = string.Empty;

            if (string.IsNullOrEmpty(txtNameofCardHolder.Text.Trim()))
            {
                errormessage = StringResource.msgNameofCardholderisRequired;
            }
            else if (!string.IsNullOrEmpty(txtNameofCardHolder.Text.Trim()) && !Regex.IsMatch(txtNameofCardHolder.Text.Trim(), @"^[a-zA-Z ]+$"))
            {
                errormessage = StringResource.msgInvalidCharacter;
            }
            else if (!string.IsNullOrEmpty(txtNameofCardHolder.Text.Trim()))
            {
                errormessage = ValidFullName();
                if (string.IsNullOrEmpty(errormessage))
                {
                    if (string.IsNullOrEmpty(txtCardNumber.Text.Trim()))
                    {
                        errormessage = StringResource.msgCardNumberIsRequired;
                    }
                    else
                    {
                        errormessage = validExpDateAndCardnumber(yearValue, monthValue, cardTypeInfoModel);
                    }
                }
            }
            return errormessage;
        }

        /// <summary>
        /// Method Name     : ValidFullName
        /// Author          : Sanket Prajapati
        /// Creation Date   : 13 jan 2018
        /// Purpose         : Use for fullname validation   
        /// Revision        : 
        /// </summary>
        private string ValidFullName()
        {
            string errormessage = string.Empty;
            if (!string.IsNullOrEmpty(txtNameofCardHolder.Text.Trim()))
            {
                string[] customerName = txtNameofCardHolder.Text.Trim().Split(' ');
                if (customerName.Length <= 1)
                {
                    errormessage = StringResource.msgFullNameRequired;
                }
            }
            return errormessage;
        }

        /// <summary>
        /// Method Name     : validExpDateAndCardnumber
        /// Author          : Sanket Prajapati
        /// Creation Date   : 27 Feb 2018
        /// Purpose         : Use for  ExpDate and cardnumber validation   
        /// Revision        : 
        /// </summary>
        private string validExpDateAndCardnumber(MonthYearModel yearValue, MonthYearModel monthValue, CardTypeInfoModel cardTypeInfoModel)
        {
            string errormessage = string.Empty;
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            if (!estimateValidateServices.ValidateCardNumber(txtCardNumber.Text.Replace(" ", "").Replace("-", "").Trim()))
            {
                errormessage = StringResource.msgInvalidCard;
            }
            if (year > yearValue.Yearindex || (year == yearValue.Yearindex && month > monthValue.Monthindex))
            {
                errormessage = StringResource.msgExpiredateValidation;
            }
            else if (string.IsNullOrEmpty(txtCVV.Text.Trim()))
            {
                errormessage = StringResource.msgCVVIsRequired;
            }
            else if (!string.IsNullOrEmpty(txtCVV.Text.Trim())&& txtCVV.Text.Length<3)
            {
                errormessage = StringResource.msgInvalidCVV;
            }
           return errormessage;
        }

        /// <summary>
        /// Method Name     : ApplyFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Feb 2018
        /// Purpose         : Apply font all control   
        /// Revision        : 
        /// </summary>
        public void ApplyFontFinalLayout()
        {
            UIHelper.SetTextViewFont(tvTotalCost, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDisplayTotalCost, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvTotalPaid, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDateDisplayTotalPaid, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDepositCollected, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDisplayDepositCollected, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvTotalDue, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDisplayTotalDue, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvClose, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvAddOtherCard, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
        }

        /// <summary>
        /// Method Name     : ApplyFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Feb 2018
        /// Purpose         : Apply font all control   
        /// Revision        : 
        /// </summary>
        public void ApplyFontPayment()
        {
            UIHelper.SetTextViewFont(tvAmount, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(txtAmount, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvCVV, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvExpYear, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvExpMonth, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvNameofCardHolder, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvCardNumber, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewMakePayment, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetEditTextFont(txtNameofCardHolder, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetEditTextFont(txtCardNumber, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetEditTextFont(txtCVV, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
        }

        /// <summary>
        /// Method Name     : ApplyFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Feb 2018
        /// Purpose         : Apply font all control   
        /// Revision        : 
        /// </summary>
        public void ApplyFontPaymentSuccesfully()
        {
            UIHelper.SetTextViewFont(tvPaymentSubmittedtitle, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvSuccessfullmsg1, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvSuccessfullmsg2, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvTransectionId, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDisplayTransectionId, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvAmountPaid, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDisaplyAmountPaid, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
        }

        /// <summary>
        /// Method Name     : ApplyFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Feb 2018
        /// Purpose         : Apply font all control   
        /// Revision        : 
        /// </summary>
        public void ApplyFontPaymentFail()
        {
            UIHelper.SetTextViewFont(tvPaymentfailtitle, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvFailmsg1, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvFailmsg2, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvFailTransectionId, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDisplayFailTransectionId, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
        }
    }
}