using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.Common;
using System.Collections.Generic;
using static JKMAndroidApp.fragment.FragmentDashboard;

namespace JKMAndroidApp.adapter
{
    public class AdapterDashboard : RecyclerView.Adapter, IItemTouchHelperAdapter
    {

        private readonly Context mContext;
        private readonly List<ModelloadData> mModelloadDataList;

        public AdapterDashboard(Context context, List<ModelloadData> modelloadDataList)
        {
            mContext = context;
            mModelloadDataList = modelloadDataList;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = null;
            var id = Resource.Layout.row_dashboard;
            itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);
            var vh = new AdapterDashboardViewHolder(itemView);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ModelloadData modelDummyData = mModelloadDataList[position];
            var viewHolder = holder as AdapterDashboardViewHolder;
            viewHolder.mTextViewTitle.Text = modelDummyData.title;
            UIHelper.SetTextViewFont(viewHolder.mTextViewTitle, (int)UIHelper.LinotteFont.LinotteSemiBold,mContext.Assets);

            if (position == 0)
            {
                List<ModelloadData> mModelDummyDataServiceList = (List<ModelloadData>)modelDummyData.mObject;
                for (int i = 0; i < mModelDummyDataServiceList.Count; i++)
                {
                    LayoutInflater vi = (LayoutInflater)mContext.GetSystemService(Context.LayoutInflaterService);
                    View v = vi.Inflate(Resource.Layout.row_dashboard_my_service, null);
                    viewHolder.mLinearLayoutDescription.Visibility = ViewStates.Gone;

                    ImageView mImageViewIcon = v.FindViewById<ImageView>(Resource.Id.imageViewIcon);

                    TextView mTextViewServiceName = v.FindViewById<TextView>(Resource.Id.textViewServiceName);
                    UIHelper.SetTextViewFont(mTextViewServiceName, (int)UIHelper.LinotteFont.LinotteRegular, mContext.Assets);
                    mTextViewServiceName.Text = mModelDummyDataServiceList[i].title;
                    SetServiceIconAndName(mModelDummyDataServiceList[i].title, mImageViewIcon, mTextViewServiceName);
                    viewHolder.mGridLayout.AddView(v);
                }
            }
            else if (position == 1)
            {
                ModelloadData mModelDummyDataMatters = (ModelloadData)modelDummyData.mObject;

                LayoutInflater vi = (LayoutInflater)mContext.GetSystemService(Context.LayoutInflaterService);
                View v = vi.Inflate(Resource.Layout.row_dashboard_my_service, null);

                ImageView mImageViewIcon = v.FindViewById<ImageView>(Resource.Id.imageViewIcon);
                TextView mTextViewServiceName = v.FindViewById<TextView>(Resource.Id.textViewServiceName);
                UIHelper.SetTextViewFont(mTextViewServiceName, (int)UIHelper.LinotteFont.LinotteRegular, mContext.Assets);
                viewHolder.mImageViewTitleIcon.SetImageResource(Resource.Drawable.icon_whm);
                mTextViewServiceName.Text = mModelDummyDataMatters.title;
                mTextViewServiceName.SetLines(5);
                mImageViewIcon.Visibility = ViewStates.Gone;
                viewHolder.mLinearLayoutDescription.AddView(v);
            }
            else
            {
                List<ModelloadData> mModelDummyDataValuationList = (List<ModelloadData>)modelDummyData.mObject;
                for (int i = 0; i < mModelDummyDataValuationList.Count; i++)
                {
                    LayoutInflater vi = (LayoutInflater)mContext.GetSystemService(Context.LayoutInflaterService);
                    View v = vi.Inflate(Resource.Layout.row_dashboard_valuation, null);
                    TextView mTextViewKey = v.FindViewById<TextView>(Resource.Id.textViewKey);
                    TextView mTextViewValue = v.FindViewById<TextView>(Resource.Id.textViewValue);
                    viewHolder.mImageViewTitleIcon.SetImageResource(Resource.Drawable.icon_Dashboard_valuation);

                    UIHelper.SetTextViewFont(mTextViewValue, (int)UIHelper.LinotteFont.LinotteRegular, mContext.Assets);
                    UIHelper.SetTextViewFont(mTextViewKey, (int)UIHelper.LinotteFont.LinotteRegular, mContext.Assets);

                    mTextViewKey.Text = mModelDummyDataValuationList[i].title;
                    mTextViewValue.Text = mModelDummyDataValuationList[i].value;

                    viewHolder.mLinearLayoutDescription.AddView(v);

                }
            }
        }

        public bool OnItemMove(int oldPosition, int newPosition)
        {
            ModelloadData oldModelDummyData = mModelloadDataList[oldPosition];
            ModelloadData newModelDummyData = new ModelloadData(oldModelDummyData.title, oldModelDummyData.subTitle);
            mModelloadDataList.Remove(oldModelDummyData);
            mModelloadDataList.Insert(newPosition, newModelDummyData);
            NotifyItemMoved(oldPosition, newPosition);
            return true;
        }

        public void OnItemDismiss(int position)
        {
            mModelloadDataList.Remove(mModelloadDataList[position]);
            NotifyItemRemoved(position);
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

    public class AdapterDashboardViewHolder : RecyclerView.ViewHolder
    {
        public ImageView mImageViewTitleIcon { get; set; }
        public TextView mTextViewTitle { get; set; }
        public LinearLayout mLinearLayoutDescription { get; set; }
        public GridLayout mGridLayout { get; set; }

        public AdapterDashboardViewHolder(View itemView) : base(itemView)
        {
            mImageViewTitleIcon = itemView.FindViewById<ImageView>(Resource.Id.imageViewTitleIcon);
            mTextViewTitle = itemView.FindViewById<TextView>(Resource.Id.textViewTitle);
            mLinearLayoutDescription = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayoutDescription);
            mGridLayout = itemView.FindViewById<GridLayout>(Resource.Id.gridLayout);

        }


        
    }
}