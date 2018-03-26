using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using Android.Text;
using Java.Lang;
using static JKMPCL.Services.UtilityPCL;
using static JKMAndroidApp.fragment.FragmentPayment;
using JKMPCL.Model;
using System.Linq;
using Android.Runtime;

namespace JKMAndroidApp.adapter
{
    public class PaymentControlAdapter : PagerAdapter, View.IOnClickListener,ITextWatcher,View.IOnKeyListener
    {
        public interface IAdapterTextViewClickListener
        {
            void OnTextviewActionClick(View v, ViewGroup layout);
            void OnTextChangeActionClick();

        }
        public interface IAdapterTextChangeListener
        {
            bool OnKeyListener(View v, [GeneratedEnum] Keycode keyCode, KeyEvent e);
        }
        public static IAdapterTextViewClickListener itemClickListener { get; set; }
        public static IAdapterTextChangeListener itemChangeClickListener { get; set; }

        readonly int FinalPaymentlId = Resource.Layout.FinalPaymentControl;
        readonly int PaymentSuccesfullylId = Resource.Layout.LayoutPaymentSuccessfull;
        readonly int PaymentFaillId = Resource.Layout.LayoutPaymentFail;
        readonly List<MonthYearModel> monthList, yearList;
        readonly private Context mContext;
        readonly List<LayoutModel> modelList;
        readonly ViewPager viewPager;

        TextView textViewCardNumber, textViewMakePayment, tvDisplayTransectionId, 
                 tvDisaplyAmountPaid, tvDisplayFailTransectionId;
        Spinner spinnerExpMonth, spinnerExpYear;
        EditText txtAmount, txtNameofCardHolder, txtCVV;

       // FrameLayout framlayEnable;
        RelativeLayout RelativeLayoutFianlPaymentControl;
        ViewGroup Paymentlayout, Successfulllayout, Faillayout;

        /// Class Name      : PaymentControlAdapter
        /// Author          : Sanket Prajapati
        /// Creation Date   : 28 feb 2018
        /// Purpose         : Use for bind Adapter   
        /// Revision        : 
        /// </summary>
        public PaymentControlAdapter(Context context, List<LayoutModel> mList, IAdapterTextViewClickListener listener,ViewPager paymentViewPager, IAdapterTextChangeListener ChangeClickListener)
        {
            mContext = context;
            modelList = mList;
            itemClickListener = listener;
            itemChangeClickListener = ChangeClickListener;
            monthList = BindMonthList();
            yearList = BindYearList();
            viewPager = paymentViewPager;
        }

        public override bool IsViewFromObject(View view, Object objectValue)
        {
            return view == objectValue;
        }

        /// Mathod Name     : InstantiateItem
        /// Author          : Sanket Prajapati
        /// Creation Date   : 28 feb 2018
        /// Purpose         : Use for add and fill view  in adapter   
        /// Revision        : 
        /// </summary>
        public override Object InstantiateItem(ViewGroup collection, int position)
        {
            LayoutModel LayoutModel = modelList[position];
            LayoutInflater inflater = LayoutInflater.From(mContext);
            ViewGroup layout = (ViewGroup)inflater.Inflate(LayoutModel.layoutResId, collection, false);
            collection.AddView(layout);
            SetLayout(layout, LayoutModel);
           
            return layout;
        }

        /// Mathod Name     : DestroyItem
        /// Author          : Sanket Prajapati
        /// Creation Date   : 28 feb 2018
        /// Purpose         : Use for destroy view  in collection   
        /// Revision        : 
        /// </summary>
        public override void DestroyItem(ViewGroup collection, int position, Object view)
        {
            collection.RemoveView((View)view);
        }

        public override int Count => modelList.Count;


        /// Method Name     : SetLayout
        /// Author          : Sanket Prajapati
        /// Creation Date   : 28 feb 2018
        /// Purpose         : Use for SetLayout   
        /// Revision        : 
        /// </summary>
        private void SetLayout(ViewGroup layout, LayoutModel LayoutModel)
        {
            if(LayoutModel.layoutResId == FinalPaymentlId)
            {
                SetFinalPaymentControl(layout);
                BindSpinerMonth();
                BindSpinerYear();
                if(LayoutModel.transactionAmout != string.Empty)
                {
                    txtAmount.Text =RemoveCurrencyFormat(LayoutModel.transactionAmout);
                }
            }
            else if (LayoutModel.layoutResId == PaymentSuccesfullylId)
            {
                SetSuccessfullPaymentControl(layout);
                PopulateSuccessfullPayment(LayoutModel);
            }
            else if (LayoutModel.layoutResId == PaymentFaillId)
            {
                SetFailPaymentControl(layout);
                PopulateFailPayment(LayoutModel);
            }
        }

        /// Method Name     : PopulateSuccessfullPayment
        /// Author          : Sanket Prajapati
        /// Creation Date   : 28 feb 2018
        /// Purpose         : Use for bind successfull layout control 
        /// Revision        : 
        /// </summary>
        public void PopulateSuccessfullPayment(LayoutModel LayoutModel)
        {
            tvDisplayTransectionId.Text = LayoutModel.TransactionID;
            tvDisaplyAmountPaid.Text = CurrencyFormat(LayoutModel.transactionAmout);
        }

        /// Method Name     : PopulateFailPayment
        /// Author          : Sanket Prajapati
        /// Creation Date   : 28 feb 2018
        /// Purpose         : Use for bind fail layout control 
        /// Revision        : 
        /// </summary>
        public void PopulateFailPayment(LayoutModel LayoutModel)
        {
            tvDisplayFailTransectionId.Text = LayoutModel.TransactionID;
        }

        /// Method Name     : SetFinalPaymentControl
        /// Author          : Sanket Prajapati
        /// Creation Date   : 28 feb 2018
        /// Purpose         : Use for find finalpayment layout control 
        /// Revision        : 
        /// </summary>
        public void SetFinalPaymentControl(ViewGroup layout)
        {
            RelativeLayoutFianlPaymentControl= layout.FindViewById<RelativeLayout>(Resource.Id.RelativeLayoutFianlPaymentControl);
           
            txtAmount = layout.FindViewById<EditText>(Resource.Id.txtAmount);
            txtNameofCardHolder= layout.FindViewById<EditText>(Resource.Id.txtNameofCardHolder);
            txtCVV = layout.FindViewById<EditText>(Resource.Id.txtCVV);

            textViewMakePayment = layout.FindViewById<TextView>(Resource.Id.textViewMakePayment);
            textViewCardNumber = layout.FindViewById<EditText>(Resource.Id.txtCardNumber);
            spinnerExpMonth = layout.FindViewById<Spinner>(Resource.Id.spinnerExpMonth);
            spinnerExpYear = layout.FindViewById<Spinner>(Resource.Id.spinnerExpYear);
            textViewMakePayment.SetOnClickListener(this);
            textViewCardNumber.AddTextChangedListener(this);
            textViewCardNumber.SetOnKeyListener(this);

            Paymentlayout = layout;
        }

        /// Method Name     : SetSuccessfullPaymentControl
        /// Author          : Sanket Prajapati
        /// Creation Date   : 28 feb 2018
        /// Purpose         : Use for find successfull payment layout control 
        /// Revision        : 
        /// </summary>
        public void SetSuccessfullPaymentControl(ViewGroup layout)
        {
            tvDisplayTransectionId = layout.FindViewById<TextView>(Resource.Id.tvDisplayTransectionId);
            tvDisaplyAmountPaid = layout.FindViewById<TextView>(Resource.Id.tvDisaplyAmountPaid);
            Successfulllayout = layout;
        }

        /// Method Name     : SetFailPaymentControl
        /// Author          : Sanket Prajapati
        /// Creation Date   : 28 feb 2018
        /// Purpose         : Use for find fail payment layout control 
        /// Revision        : 
        /// </summary>
        public void SetFailPaymentControl(ViewGroup layout)
        {
            tvDisplayFailTransectionId = layout.FindViewById<TextView>(Resource.Id.tvDisplayFailTransectionId);
            Faillayout = layout;
        }

        /// Method Name     : BindSpinerMonth
        /// Author          : Sanket Prajapati
        /// Creation Date   : 28 feb 2018
        /// Purpose         : Use for bind month   
        /// Revision        : 
        /// </summary>
        private void BindSpinerMonth()
        {
            List<string> valutionList = MonthList();
            if (valutionList.Count > 0)
            {
                var monthAdapter = new ArrayAdapter<string>(mContext, Android.Resource.Layout.SimpleSpinnerItem, valutionList);
                monthAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerExpMonth.Adapter = monthAdapter;
                string currentMonth = System.DateTime.Now.Month.ToString("00");
                MonthYearModel monthYearModel = monthList.FirstOrDefault(rc => rc.Month == currentMonth);
                if (monthYearModel.Monthindex == 0) { spinnerExpMonth.SetSelection(monthYearModel.Monthindex); } else spinnerExpMonth.SetSelection(monthYearModel.Monthindex - 1);
            }
        }

        /// Method Name     : BindSpiner
        /// Author          : Sanket Prajapati
        /// Creation Date   : 28 feb 2018
        /// Purpose         : Use for bind year  
        /// Revision        : 
        /// </summary>
        private void BindSpinerYear()
        {
            List<string> valutionList = YearList();
            if (valutionList.Count > 0)
            {
                var yearAdapter = new ArrayAdapter<string>(mContext, Android.Resource.Layout.SimpleSpinnerItem, valutionList);
                yearAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerExpYear.Adapter = yearAdapter;
            }
        }

        /// Method Name     : OnClick
        /// Author          : Sanket Prajapati
        /// Creation Date   : 28 feb 2018
        /// Purpose         : Use for 
        /// Revision        : 
        /// </summary>
        public void OnClick(View v)
        {
            if (itemClickListener != null &&  v == textViewMakePayment)
            {
                itemClickListener.OnTextviewActionClick(v, Paymentlayout);
            }
            
        }

        public void AfterTextChanged(IEditable s)
        {
            //
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
            //
        }

        /// Method Name     : OnTextChanged
        /// Author          : Sanket Prajapati
        /// Creation Date   : 28 feb 2018
        /// Purpose         : Use for 
        /// Revision        : 
        /// </summary>
        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            if (itemClickListener != null )
            {
                itemClickListener.OnTextChangeActionClick();
            }
        }

        /// Method Name     : OnKey
        /// Author          : Sanket Prajapati
        /// Creation Date   : 28 feb 2018
        /// Purpose         : Use for 
        /// Revision        : 
        /// </summary>
        public bool OnKey(View v, [GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (itemChangeClickListener != null)
            {
                itemChangeClickListener.OnKeyListener(v, keyCode,e);
            }
            return false;
        }
    }
}