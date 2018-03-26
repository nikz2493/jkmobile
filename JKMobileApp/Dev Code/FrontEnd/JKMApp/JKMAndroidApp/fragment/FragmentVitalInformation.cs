using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.activity;
using JKMAndroidApp.Common;
using Android.Webkit;

namespace JKMAndroidApp.fragment
{
    public class FragmentVitalInformation : Android.Support.V4.App.Fragment
    {

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        View view;
        private LinearLayout linearLayoutBack;
        private TextView mTextViewNext;
        private TextView tvtitleDiscriptions;
        private TextView tvViewEstimate;
        private TextView tvback;
        private ImageButton btnBack;
        
        private LinearLayout linearLayoutViewEstimate;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
             view = inflater.Inflate(Resource.Layout.LayoutFragmentVitalInformation, container, false);
            WebView webview = view.FindViewById<WebView>(Resource.Id.webView1);
            webview.LoadUrl(StringResource.VitalInfoURl);
            UIReference();
            ApplyFont();
            UIClickEvents();
            return view;
        }

        /// Method Name     : UIClickEvents
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Set click event   
        /// Revision        : 
        /// </summary>
        private void UIClickEvents()
        {
            btnBack.Click += MTextViewBack_Click;
            linearLayoutBack.Click += MTextViewBack_Click;
            mTextViewNext.Click += delegate
            {
                ((ActivityEstimateViewPager)Activity).FragmentNext();
            };
            linearLayoutViewEstimate.Click += delegate
            {
                StartActivity(new Intent(Activity, typeof(PdfActivity)));
            };
        }

        /// Method Name     : MTextViewBack_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for move back fragment 
        /// Revision        : 
        /// </summary>
        private void MTextViewBack_Click(object sender, EventArgs e)
        {
            ((ActivityEstimateViewPager)Activity).FragmentBack();
        }

        /// Method Name     : UIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Find all control   
        /// Revision        : 
        /// </summary>
        private void UIReference()
        {
            linearLayoutBack = view.FindViewById<LinearLayout>(Resource.Id.textViewBack);
            mTextViewNext = view.FindViewById<TextView>(Resource.Id.textViewNext);

            tvtitleDiscriptions = view.FindViewById<TextView>(Resource.Id.tvtitleDiscriptions);
            tvViewEstimate = view.FindViewById<TextView>(Resource.Id.tvViewEstimate);
            tvback = view.FindViewById<TextView>(Resource.Id.tvback);
            btnBack = view.FindViewById<ImageButton>(Resource.Id.btnBack);
            linearLayoutViewEstimate = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutViewEstimate);
        }

        /// <summary>
        /// Method Name     : ApplyFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 4 jan 2017
        /// Purpose         : Apply font all control   
        /// Revision        : 
        /// </summary>
        public void ApplyFont()
        {
            UIHelper.SetTextViewFont(tvtitleDiscriptions, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvViewEstimate, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvback, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(mTextViewNext, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
        }
    }
}