using Android.OS;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.activity;
using JKMAndroidApp.Common;
using Android.Content;
using JKMPCL.Model;
using System.Linq;
using Android.Support.V4.App;
using Android.App;

namespace JKMAndroidApp.fragment
{

    /// <summary>
    /// Event Name      : FragmentEstimatedReview
    /// Author          : Sanket Prajapati
    /// Creation Date   : 24 jan 2018
    /// Purpose         : Fragement for Estimate Review layout
    /// Revision        : 
    /// </summary>
    public class FragmentEstimatedReview : Android.Support.V4.App.Fragment, ActivityCompat.IOnRequestPermissionsResultCallback
    {
        private View view;
        private TextView tvBack;
        private TextView tvNext;
        private TextView tvtitleDiscription;
        private TextView tvEstimatedCostLabel;
        private EditText txtestimatedCost;
        private TextView tvViewEstimate;
        private EstimateModel estimateModel;
        private LinearLayout linearLayoutViewEstimate;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.LayoutFragmentEstimateReview, container, false);
            estimateModel = DTOConsumer.dtoEstimateData.FirstOrDefault(rc=>rc.MoveNumber==UIHelper.SelectedMoveNumber);
            UIReference();
            ApplyFont();
            PopulateData();
            UIClickEvents();

            return view;
        }

        /// <summary>
        /// Event Name      : UIClickEvents
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Set all control events
        /// Revision        : 
        /// </summary>
        private void UIClickEvents()
        {
            tvViewEstimate.Click += delegate
            {
                StartActivity(new Intent(Activity, typeof(PdfActivity)));
            };

            tvBack.Click += delegate
            {
                StartActivity(new Intent(Activity, typeof(ContactusActivity)));
            };

            linearLayoutViewEstimate.Click += delegate
            {
                StartActivity(new Intent(Activity, typeof(PdfActivity)));
            };

            tvNext.Click += delegate
            {
                ((ActivityEstimateViewPager)Activity).FragmentNext();
            };
        }

        /// <summary>
        /// Method Name     : UIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 10 Dec 2018
        /// Purpose         : Find all control   
        /// Revision        : 
        /// </summary>
        private void UIReference()
        {
            tvBack = view.FindViewById<TextView>(Resource.Id.textViewBack);
            tvNext = view.FindViewById<TextView>(Resource.Id.textViewNext);
            tvtitleDiscription = view.FindViewById<TextView>(Resource.Id.tvtitleDiscription);
            tvEstimatedCostLabel = view.FindViewById<TextView>(Resource.Id.tvEstimatedCostLabel);
            txtestimatedCost = view.FindViewById<EditText>(Resource.Id.txtestimatedCost);
            tvViewEstimate = view.FindViewById<TextView>(Resource.Id.tvViewEstimate);
            linearLayoutViewEstimate = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutViewEstimate);
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
            UIHelper.SetTextViewFont(tvtitleDiscription, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvEstimatedCostLabel, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetEditTextFont(txtestimatedCost, (int)UIHelper.LinotteFont.LinotteRegular, Activity.Assets);
            UIHelper.SetTextViewFont(tvViewEstimate, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvBack, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvNext, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
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
                txtestimatedCost.Text = estimateModel.EstimatedLineHaul;
            }
        }
     
    }
}