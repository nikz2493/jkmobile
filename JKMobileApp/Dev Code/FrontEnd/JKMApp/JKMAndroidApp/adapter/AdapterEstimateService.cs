using System;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.Common;
using System.Collections.Generic;
using static JKMAndroidApp.fragment.FragmentServices;

namespace JKMAndroidApp.adapter
{
    public class AdapterEstimateService : RecyclerView.Adapter
    {
        private readonly Context mContext;
        private readonly List<ModelloadData> mModelloadDataList;

        public AdapterEstimateService(Context context, List<ModelloadData> modelloadDataList)
        {
            mContext = context;
            mModelloadDataList = modelloadDataList;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            View itemView = null;
            var id = Resource.Layout.row_Estimate_Service;
            itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);
            var vh = new AdapterEstimateServiceViewHolder(itemView);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var modelDummyData = mModelloadDataList[position];
            List<ModelloadData> mModelDummyDataServiceList = (List<ModelloadData>)modelDummyData.listModelloadData;
            for (int i = 0; i < mModelDummyDataServiceList.Count; i++)
            {
                var viewHolder = holder as AdapterEstimateServiceViewHolder;
                viewHolder.mTextViewTitle.Text = "Please confirm the services which are being offered with the selected estimate are listed below:";
                UIHelper.SetTextViewFont(viewHolder.mTextViewTitle, (int)UIHelper.LinotteFont.LinotteSemiBold, mContext.Assets);
                LayoutInflater vi = (LayoutInflater)mContext.GetSystemService(Context.LayoutInflaterService);
                View v = vi.Inflate(Resource.Layout.row_dashboard_my_service, null);
                viewHolder.mLinearLayoutDescription.Visibility = ViewStates.Gone;
                ImageView mImageViewIcon = v.FindViewById<ImageView>(Resource.Id.imageViewIcon);

                TextView mTextViewServiceName = v.FindViewById<TextView>(Resource.Id.textViewServiceName);
                UIHelper.SetTextViewFont(mTextViewServiceName, (int)UIHelper.LinotteFont.LinotteRegular, mContext.Assets);
                SetServiceIconAndName(mModelDummyDataServiceList[i].title, mImageViewIcon, mTextViewServiceName);
                viewHolder.mGridLayout.AddView(v);
            }

        }

        public override int ItemCount => mModelloadDataList.Count;

        public void SetServiceIconAndName(string StrServiceCode, ImageView mImageViewIcon, TextView mTextViewServiceName)
        {
            switch (StrServiceCode)
            {
                case "stg_Packing":
                    mImageViewIcon.SetImageResource(Resource.Drawable.icon_booked);
                    mTextViewServiceName.Text = MoveDataResource.MoveCode_Pack;
                    break;

                case "stg_UnPacking":
                    mImageViewIcon.SetImageResource(Resource.Drawable.icon_panding);
                    mTextViewServiceName.Text = MoveDataResource.MoveCode_UnPack;
                    break;

                case "stg_Loading":
                    mImageViewIcon.SetImageResource(Resource.Drawable.icon_loaded);
                    mTextViewServiceName.Text = MoveDataResource.MoveCode_Load;
                    break;

                case "stg_UnLoading":
                    mImageViewIcon.SetImageResource(Resource.Drawable.icon_panding);
                    mTextViewServiceName.Text = MoveDataResource.MoveCode_Export;
                    break;

                case "stg_Storge":
                    mImageViewIcon.SetImageResource(Resource.Drawable.icon_valuation);
                    mTextViewServiceName.Text = MoveDataResource.MoveCode_Release;
                    break;
            }
        }
    }

    public class AdapterEstimateServiceViewHolder : RecyclerView.ViewHolder
    {

        public TextView mTextViewTitle { get; set; }
        public GridLayout mGridLayout { get; set; }
        public LinearLayout mLinearLayoutDescription { get; set; }

        public AdapterEstimateServiceViewHolder(View itemView) : base(itemView)
        {
            mTextViewTitle = itemView.FindViewById<TextView>(Resource.Id.textViewTitle);
            mGridLayout = itemView.FindViewById<GridLayout>(Resource.Id.gridLayout);
            mLinearLayoutDescription = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayoutDescription);
        }
    }


}