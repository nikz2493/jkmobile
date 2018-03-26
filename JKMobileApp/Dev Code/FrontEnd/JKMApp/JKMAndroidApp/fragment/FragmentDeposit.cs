using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.Common;
using JKMPCL.Model;
using JKMPCL.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using static JKMPCL.Services.UtilityPCL;
using JKMAndroidApp.activity;
using System.Threading.Tasks;
using System.Net;
using JKMPCL.Services.Payment;
using System.Text.RegularExpressions;
using Android.Content;

namespace JKMAndroidApp.fragment
{
    public class FragmentDeposit : Android.Support.V4.App.Fragment
    {
        View view;
        TextView tvtitleDiscriptions, tvDisplayDepositAmount, tvCVV, tvDepositAmount, tvExpYear, tvExpMonth,
                 tvNameofCardHolder, tvCardNumber, tvback, tvNext;
        EditText txtNameofCardHolder, txtCardNumber, txtCVV;
        Spinner spinnerExpMonth, spinnerExpYear;
        ImageView imgCard;
        LinearLayout linearLayoutEdit, linearLayoutBack;
        RelativeLayout paymentControl;
        ImageButton btnBack;
        FrameLayout framlayEnable;
        CheckBox depositCheckBox;
        AlertDialog.Builder dialogue;
        AlertDialog alert;
        List<MonthYearModel> monthList, yearList;
        CardType cardType;
        private EstimateModel estimateModel;
        private bool isFormatCardNumber = true;
        Payment paymentGateway;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.LayoutFragmentDeposit, container, false);
            estimateModel = DTOConsumer.dtoEstimateData.FirstOrDefault(rc => rc.MoveNumber == UIHelper.SelectedMoveNumber);
            UIReferencesPaymentControl();
            UIReferences();
            PopulateData();
            UIClickEvents();
            monthList = BindMonthList();
            yearList = BindYearList();
            BindSpinerMonth();
            BindSpinerYear();
            view.Invalidate();
            return view;
        }

        public override void OnResume()
        {
            UIReferencesPaymentControl();
            UIReferences();
            PopulateData();
            base.OnResume();
        }
        /// Method Name     : SetNextFragmentClick
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for move next fragment 
        /// Revision        : 
        /// </summary>
        private void SetNextFragmentClick()
        {
            tvNext.Click += TvNext_ClickAsync;
        }

        private async void TvNext_ClickAsync(object sender, EventArgs e)
        {
            if (!depositCheckBox.Checked)
            {
                string errormessage = Validation();
                if (string.IsNullOrEmpty(errormessage))
                {
                    APIResponse<PaymentTransactonModel> paymentTransacton = await ProcessPaymentTransactionAsync();
                    if (paymentTransacton.STATUS)
                    {
                        estimateModel.IsDepositPaidByCheck = false;
                        estimateModel.PaymentStatus = true;
                        StartActivity(new Intent(Activity, typeof(ActivityMoveConfirmed)));
                    }
                    else
                    {
                        dialogue = new AlertDialog.Builder(new ContextThemeWrapper(Activity, Resource.Style.AlertDialogCustom));
                        alert = dialogue.Create();
                        alert.SetMessage(StringResource.msgPaymentFailMessage);
                        alert.SetButton(StringResource.msgOK, (c, ev) =>
                        {
                            alert.Dispose();
                        });
                        alert.Show();
                    }
                }
                else
                {
                    AlertMessage(errormessage);
                }
            }
            else
            {
                estimateModel.IsDepositPaidByCheck = true;
                StartActivity(new Intent(Activity, typeof(ActivityMoveConfirmed)));
            }
        }

        /// Method Name     : SetBackFragmentClick
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for move back fragment 
        /// Revision        : 
        /// </summary>
        private void SetBackFragmentClick()
        {
            btnBack.Click += MTextViewBack_Click;
            linearLayoutBack.Click += MTextViewBack_Click;
        }

        /// Method Name     : SetBackFragmentClick
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for move back fragment 
        /// Revision        : 
        /// </summary>
        private void MTextViewBack_Click(object sender, EventArgs e)
        {
            ClearData();
            tvback.Text =StringResource.wizBtnBack;
            tvNext.Text = StringResource.msgbtnSubmitPayment;
            ((ActivityEstimateViewPager)Activity).FragmentBack();
        }

        /// </summary>
        /// Method Name     : UIReferences
        /// Author          : Sanket Prajapati
        /// Creation Date   : 31 jan 2018
        /// Purpose         : Finds Control  
        /// Revision        : 
        /// </summary>
        private void UIReferences()
        {
            tvtitleDiscriptions = view.FindViewById<TextView>(Resource.Id.tvtitleDiscriptions);
            tvDepositAmount = view.FindViewById<TextView>(Resource.Id.tvDepositAmount);
            tvDisplayDepositAmount = view.FindViewById<TextView>(Resource.Id.tvDisplayDepositAmount);
            linearLayoutBack = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutBack);
            btnBack = view.FindViewById<ImageButton>(Resource.Id.btnBack);
            tvback = view.FindViewById<TextView>(Resource.Id.tvback);
            tvNext = view.FindViewById<TextView>(Resource.Id.tvNext);
            depositCheckBox = view.FindViewById<CheckBox>(Resource.Id.depositCheckBox);
        }

        /// </summary>
        /// Method Name     : UIReferencesPaymentControl
        /// Author          : Sanket Prajapati
        /// Creation Date   : 31 jan 2018
        /// Purpose         : Finds Control  
        /// Revision        : 
        /// </summary>
        private void UIReferencesPaymentControl()
        {
            paymentControl = (RelativeLayout)view.FindViewById(Resource.Id.paymentControl);
            tvExpMonth = paymentControl.FindViewById<TextView>(Resource.Id.tvExpMonth);
            spinnerExpMonth = paymentControl.FindViewById<Spinner>(Resource.Id.spinnerExpMonth);
            tvExpYear = paymentControl.FindViewById<TextView>(Resource.Id.tvExpYear);
            spinnerExpYear = paymentControl.FindViewById<Spinner>(Resource.Id.spinnerExpYear);
            tvNameofCardHolder = paymentControl.FindViewById<TextView>(Resource.Id.tvNameofCardHolder);
            txtNameofCardHolder = paymentControl.FindViewById<EditText>(Resource.Id.txtNameofCardHolder);
            tvCardNumber = paymentControl.FindViewById<TextView>(Resource.Id.tvCardNumber);
            txtCardNumber = paymentControl.FindViewById<EditText>(Resource.Id.txtCardNumber);
            tvCVV = paymentControl.FindViewById<TextView>(Resource.Id.tvCVV);
            txtCVV = paymentControl.FindViewById<EditText>(Resource.Id.txtCVV);
            imgCard = paymentControl.FindViewById<ImageView>(Resource.Id.imgCard);
            linearLayoutEdit = paymentControl.FindViewById<LinearLayout>(Resource.Id.linearLayoutPayment);
            framlayEnable = paymentControl.FindViewById<FrameLayout>(Resource.Id.framlayEnable);
        }

        /// </summary>
        /// Method Name     : UIClickEvents
        /// Author          : Sanket Prajapati
        /// Creation Date   : 31 jan 2018
        /// Purpose         : Set control events
        /// Revision        : 
        /// </summary>
        private void UIClickEvents()
        {
            SetNextFragmentClick();
            SetBackFragmentClick();
            TextViewCardNumberTextChange();
            depositCheckBox.CheckedChange += DepositCheckBox_CheckedChange;
            txtCardNumber.KeyPress += TxtCardNumber_KeyPress;
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
            PaymentGatewayModel paymentModel = new PaymentGatewayModel();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            string expiryYear = spinnerExpYear.SelectedItem.ToString().Substring(2, 2);
            paymentModel.CardExpiryDate = spinnerExpMonth.SelectedItem.ToString() + expiryYear;
            paymentModel.CreditCardNumber = txtCardNumber.Text.Replace("-","").Trim();
            paymentModel.CVVNo = Convert.ToInt32(txtCVV.Text.Trim());
            if (estimateModel != null)
            {
                paymentModel.TransactionAmout = Convert.ToDouble(estimateModel.Deposit);
            }
            PaymentGatewayModel payment = FillCustomerData(paymentModel);
            if (payment != null)
            {
                paymentModel = payment;
            }

            return paymentModel;
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
            paymentGateway = new Payment();
            APIResponse<PaymentTransactonModel> paymentTransacton = new APIResponse<PaymentTransactonModel>();
            PaymentGatewayModel paymentModel = PaymentTransactionAsync();
            if (paymentModel != null)
            {
                paymentTransacton = await paymentGateway.ProcessPaymentTransaction(paymentModel);
                if (paymentTransacton.STATUS)
                {
                    estimateModel.TransactionId = paymentTransacton.DATA.TransactionID;
                    estimateModel.PaymentStatus = true;
                    estimateModel.IsDepositPaid = true;
                    await CallPostPaymentTransaction(paymentTransacton, estimateModel);
                }
                else
                {
                    estimateModel.PaymentStatus = false;
                }
            }
            return paymentTransacton;
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
        private async Task CallPostPaymentTransaction(APIResponse<PaymentTransactonModel> paymentTransactonModel, EstimateModel estimateModel)
        {
            paymentGateway = new Payment();
            APIResponse<PaymentModel> serviceResponse = new APIResponse<PaymentModel>() { STATUS = false };
            string errorMessage = string.Empty;
            try
            {
                List<PaymentModel> paymentModelList = new List<PaymentModel>();
                PaymentModel paymentModel = new PaymentModel();
                paymentModel.MoveID = estimateModel.MoveId;
                paymentModel.TransactionNumber = paymentTransactonModel.DATA.TransactionID;
                if (!IsNullOrEmptyOrWhiteSpace(estimateModel.Deposit))
                {
                    paymentModel.TransactionAmount = RemoveCurrencyFormat(estimateModel.Deposit);
                }

                paymentModel.TransactionDate = UtilityPCL.DisplayDateFormatForEstimate(DateTime.Now, string.Empty);
                paymentModel.CustomerID = LoginCustomerData.CustomerId;

                paymentModelList.Add(paymentModel);
                serviceResponse = await paymentGateway.PostPaymentTransaction(paymentModelList);
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
        /// Event Name      : DepositCheckBox_CheckedChange
        /// Author          : Sanket Prajapati
        /// Creation Date   : 31 jan 2018
        /// Purpose         : Set enable disable payment control 
        /// Revision        : 
        /// </summary>
        private void DepositCheckBox_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (depositCheckBox.Checked)
            {
                framlayEnable.Visibility = ViewStates.Visible;
                framlayEnable.Clickable = true;
                txtNameofCardHolder.SetCursorVisible(false);
                txtCardNumber.SetCursorVisible(false);
                ClearData();
            }
            else
            {
                txtCardNumber.SetCursorVisible(true);
                txtNameofCardHolder.SetCursorVisible(true);
                framlayEnable.Visibility = ViewStates.Gone;
                framlayEnable.Clickable = false;
            }
        }

        /// Method Name     : BindSpinerMonth
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use fort bind month   
        /// Revision        : 
        /// </summary>
        private void BindSpinerMonth()
        {
            List<String> valutionList = MonthList();
            if (valutionList.Count > 0)
            {
                var monthAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerItem, valutionList);
                monthAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerExpMonth.Adapter = monthAdapter;
                string currentMonth = DateTime.Now.Month.ToString("00");
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
            List<String> valutionList = YearList();
            if (valutionList.Count > 0)
            {
                var yearAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerItem, valutionList);
                yearAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerExpYear.Adapter = yearAdapter;
            }
        }

        /// </summary>
        /// Method Name     : TextViewCardNumberTextChange
        /// Author          : Sanket Prajapati
        /// Creation Date   : 31 jan 2018
        /// Purpose         : Use for check card type
        /// Revision        : 
        /// </summary>
        private void TextViewCardNumberTextChange()
        {
            txtCardNumber.TextChanged += delegate
            {
                if (!string.IsNullOrEmpty(txtCardNumber.Text.Trim()) && txtCardNumber.Text.Length >= 4)
                {
                    SetCardType();
                    if (isFormatCardNumber)
                    {
						SetCardFormat();
                    }
                    else
                    {
						txtCardNumber.SetSelection(txtCardNumber.Text.Length);
                    }
                }
                else
                {
                    imgCard.SetImageResource(Resource.Drawable.icon_payment_active);
                }
            };
        }

        /// <summary>
        /// Method Name     : SetCardType
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 28 Feb 2018
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
        private void TxtCardNumber_KeyPress(object sender, View.KeyEventArgs e)
        {
            e.Handled = false;
            if (e.KeyCode == Keycode.Del || e.KeyCode == Keycode.Back)
            {
                isFormatCardNumber = false;
            }
            else
            {
                isFormatCardNumber = true;
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
        /// Method Name     : ApplyFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Apply font all control   
        /// Revision        : 
        /// </summary>
        public void ApplyFont()
        {
            UIHelper.SetTextViewFont(tvtitleDiscriptions, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDisplayDepositAmount, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvCVV, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvNameofCardHolder, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvCardNumber, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvExpMonth, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvExpYear, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvback, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvNext, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetEditTextFont(txtNameofCardHolder, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetEditTextFont(txtCardNumber, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetEditTextFont(txtCVV, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
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
            if (string.IsNullOrEmpty(txtNameofCardHolder.Text))
            {
                errormessage = StringResource.msgNameofCardholderisRequired;
            }
            else if (!string.IsNullOrEmpty(txtNameofCardHolder.Text.Trim()))
            {
                errormessage = ValidFullName();
                if (string.IsNullOrEmpty(errormessage))
                {
                    if (string.IsNullOrEmpty(txtCardNumber.Text))
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
        /// Method Name     : Validation
        /// Author          : Sanket Prajapati
        /// Creation Date   : 13 jan 2018
        /// Purpose         : Use for validation   
        /// Revision        : 
        /// </summary>
        private string validExpDateAndCardnumber(MonthYearModel yearValue, MonthYearModel monthValue, CardTypeInfoModel cardTypeInfoModel)
        {
            string errormessage = string.Empty;
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            string cardNumber = txtCardNumber.Text.Replace("-", "").Trim();
            if (cardTypeInfoModel == null || (cardTypeInfoModel.CardNumberLength != cardNumber.Length))
            {
                errormessage = StringResource.msgInvalidCard;
            }
            else if (year > yearValue.Yearindex || (year == yearValue.Yearindex && month > monthValue.Monthindex))
            {
                errormessage = StringResource.msgExpiredateValidation;
            }
            else if (string.IsNullOrEmpty(txtCVV.Text))
            {
                errormessage = StringResource.msgCVVIsRequired;
            }

            return errormessage;
        }

        /// <summary>
        /// Method Name     : ClearData
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Clear payment data   
        /// Revision        : 
        /// </summary>
        private void ClearData()
        {
            if (!string.IsNullOrEmpty(txtNameofCardHolder.Text.Trim()))
            {
                txtNameofCardHolder.Text = string.Empty;
            }
            if (!string.IsNullOrEmpty(txtCVV.Text.Trim()))
            {
                txtCVV.Text = string.Empty;
            }
            if (!string.IsNullOrEmpty(txtCardNumber.Text.Trim()))
            {
                txtCardNumber.Text = string.Empty;
            }
            BindSpinerMonth();
            BindSpinerYear();
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
                tvDisplayDepositAmount.Text = CurrencyFormat(estimateModel.DepositValue);
                if (!estimateModel.IsDepositPaid)
                {
                    estimateModel.Deposit = CurrencyFormat(estimateModel.DepositValue);
                }
            }
        }
    }
}