using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.activity;
using JKMAndroidApp.adapter;
using Android.Support.V7.Widget;
using JKMAndroidApp.Common;
using JKMPCL.Model;
using Android.App;

namespace JKMAndroidApp.fragment
{
    /// <summary>
    /// Method Name     : FragmentServices
    /// Author          : Sanket Prajapati
    /// Creation Date   : 23 Jan 2018
    /// Purpose         : Fragement for Services page
    /// Revision        : 
    /// </summary>
    public class FragmentServices : Android.Support.V4.App.Fragment
    {
        private View view;
        private RecyclerView recylerViewEstimate;
        private LinearLayout linearLayoutBack;
        private TextView tvNext;
        private TextView tvtitleDiscription;
        private TextView tvViewEstimate;
        private TextView tvUpdateService;
        private TextView tvContactSales;
        private TextView tvback;
        private EstimateModel estimateModel;
        private ImageButton btnBack;
        private LinearLayout linearLayoutViewEstimate;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.LayoutEstimateService, container, false);
            estimateModel = DTOConsumer.dtoEstimateData.FirstOrDefault();
            UIReference();
            ApplyFont();
            SetAdapter();
            UIClickEvents();
          
            return view;
        }

        /// Method Name     : SetBackFragmentClick
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for move back fragment 
        /// Revision        : 
        /// </summary>
        private void MTextViewBack_Click(object sender, EventArgs e)
        {
            ((ActivityEstimateViewPager)Activity).FragmentBack();
        }

        /// Method Name     : UIClickEvents
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Set click event   
        /// Revision        : 
        /// </summary>
        private void UIClickEvents()
        {
            btnBack.Click += MTextViewBack_Click;
            linearLayoutBack.Click += MTextViewBack_Click;

            tvNext.Click += delegate
            {
                ((ActivityEstimateViewPager)Activity).FragmentNext();
            };
            tvContactSales.Click += delegate
            {
                StartActivity(new Intent(Activity, typeof(ContactusActivity)));
            };
            linearLayoutViewEstimate.Click += delegate
            {
                StartActivity(new Intent(Activity, typeof(PdfActivity)));
            };
        }

        /// <summary>
        /// Method Name     : SetAdapter
        /// Author          : Sanket Prajapati
        /// Creation Date   : 9 jan 2018
        /// Purpose         : Bind adapter 
        /// Revision        : 
        /// </summary>
        private void SetAdapter()
        {
            List<ModelloadData> mModelDummyDataServiceList = new List<ModelloadData>();
            if (estimateModel != null && string.IsNullOrEmpty(estimateModel.message))
            {
                foreach (MyServicesModel myServicesmodal in estimateModel.MyServices)
                {
                    mModelDummyDataServiceList.Add(new ModelloadData() { title = myServicesmodal.ServicesCode, subTitle = string.Empty });
                }
            }

            List<ModelloadData> modelloadDatalist = new List<ModelloadData>();
            modelloadDatalist.Add(new ModelloadData() { listModelloadData = mModelDummyDataServiceList });

            AdapterEstimateService adapterEstimateService = new AdapterEstimateService(Activity, modelloadDatalist);
            recylerViewEstimate.SetLayoutManager(new LinearLayoutManager(Activity));
            recylerViewEstimate.SetItemAnimator(new DefaultItemAnimator());
            recylerViewEstimate.NestedScrollingEnabled = false;
            recylerViewEstimate.HasFixedSize = false;
            recylerViewEstimate.SetAdapter(adapterEstimateService);
        }

        /// Method Name     : UIReferences
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Finds Control  
        /// Revision        : 
        /// </summary>
        private void UIReference()
        {
            recylerViewEstimate = view.FindViewById<RecyclerView>(Resource.Id.recylerViewEstimate);
            linearLayoutBack = view.FindViewById<LinearLayout>(Resource.Id.textViewBack);
            tvNext = view.FindViewById<TextView>(Resource.Id.textViewNext);
            tvtitleDiscription = view.FindViewById<TextView>(Resource.Id.tvtitleDiscription);
            tvViewEstimate = view.FindViewById<TextView>(Resource.Id.tvViewEstimate);
            tvUpdateService = view.FindViewById<TextView>(Resource.Id.tvUpdateService);
            tvContactSales = view.FindViewById<TextView>(Resource.Id.tvContactSales);
            tvback= view.FindViewById<TextView>(Resource.Id.tvback);
            btnBack = view.FindViewById<ImageButton>(Resource.Id.btnBack);
            linearLayoutViewEstimate = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutViewEstimate);
        }

        /// class Name      :  ModelloadData
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Use for Card view bind
        /// Revision        : 
        /// </summary>
        public class ModelloadData
        {
            public string title { get; set; }
            public string subTitle { get; set; }
            public int id { get; set; }
            public string value { get; set; }
            public List<ModelloadData> listModelloadData { get; set; }
        }

        /// Method Name     : ApplyFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Apply font all control   
        /// Revision        : 
        /// </summary>
        public void ApplyFont()
        {
            UIHelper.SetTextViewFont(tvtitleDiscription, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvUpdateService, (int)UIHelper.LinotteFont.LinotteRegular, Activity.Assets);
            UIHelper.SetTextViewFont(tvContactSales, (int)UIHelper.LinotteFont.LinotteRegular, Activity.Assets);
            UIHelper.SetTextViewFont(tvViewEstimate, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvback, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvNext, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
        }
    }
}