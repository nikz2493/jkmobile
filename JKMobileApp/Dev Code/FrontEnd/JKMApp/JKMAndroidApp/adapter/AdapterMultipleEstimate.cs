using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.Common;
using System.Linq;
using System.Collections.Generic;
using static JKMAndroidApp.activity.MultipleEstimatedActivity;

namespace JKMAndroidApp.adapter
{
    public class AdapteMultipleEstimates : RecyclerView.Adapter
    {
        public interface IAdapterImageviewClickListener
        {
            void OnImageviewActionClick(int position);
        }

        public interface IAdapterRadiobuttonClickListener
        {
            void OnIRadiobuttonActionClick(int position);
        }

        public readonly Context mContext;
        private readonly List<ModelloadData> mModelloadDataList;
        public static IAdapterImageviewClickListener itemClickListener { get; set; }
        public static IAdapterRadiobuttonClickListener radiobuttonClickListener { get; set; }

        public AdapteMultipleEstimates(Context context, List<ModelloadData> modelloadDataList, IAdapterImageviewClickListener iImagelistener, IAdapterRadiobuttonClickListener iRadiobuttonlistener)
        {
            mContext = context;
            mModelloadDataList = modelloadDataList;

            itemClickListener = iImagelistener;
            radiobuttonClickListener = iRadiobuttonlistener;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = null;
            var id = Resource.Layout.row_multiple_estimat_DisplayData;
            itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);
            var vh = new AdapteMultipleEstimatesViewHolder(itemView);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ModelloadData modelData = mModelloadDataList[position];
            var viewHolder = holder as AdapteMultipleEstimatesViewHolder;

            TextView tvEstimateName = viewHolder.tvEstimateName;
            TextView tvEstimateAmount = viewHolder.tvEstimateAmount;
            RadioButton radiobutton = viewHolder.radiobutton;
            UIHelper.SetTextViewFont(tvEstimateName, (int)UIHelper.LinotteFont.LinotteSemiBold, mContext.Assets);
            UIHelper.SetTextViewFont(tvEstimateAmount, (int)UIHelper.LinotteFont.LinotteSemiBold, mContext.Assets);
            
            tvEstimateName.Text = modelData.EstimteNo;
            if (!string.IsNullOrEmpty(modelData.EstimateAmount))
            {
                tvEstimateAmount.Text = modelData.EstimateAmount;
            }
            else
            {
                tvEstimateAmount.Text = "$000.00";
            }

            if (!string.IsNullOrEmpty(UIHelper.SelectedMoveNumber) && modelData.EstimteNo== UIHelper.SelectedMoveNumber)
            {
                radiobutton.Checked = true;
            }
            else
            {
                radiobutton.Checked = false;
            }
           
        }
        
        public override int ItemCount => mModelloadDataList.Count;
       

        public class AdapteMultipleEstimatesViewHolder : RecyclerView.ViewHolder,View.IOnClickListener
        {
            public LinearLayout linearlayoutEstimateDetails { get; set; }
            public TextView tvEstimateName { get; set; }
            public TextView tvEstimateAmount { get; set; }
            public RadioButton radiobutton { get; set; }
            public ImageView btnimagePdf { get; set; }

            public AdapteMultipleEstimatesViewHolder(View itemView) : base(itemView)
            {
                tvEstimateName = itemView.FindViewById<TextView>(Resource.Id.tvEstimateName);
                tvEstimateAmount = itemView.FindViewById<TextView>(Resource.Id.tvEstimateAmount);
                radiobutton = itemView.FindViewById<RadioButton>(Resource.Id.radiobutton);
                btnimagePdf = itemView.FindViewById<ImageView>(Resource.Id.btnimagePdf);
                radiobutton.SetOnClickListener(this);
                btnimagePdf.SetOnClickListener(this);
                
            }

            public void OnClick(View v)
            {
                if (itemClickListener != null && v == btnimagePdf)
                {
                   itemClickListener.OnImageviewActionClick(AdapterPosition);
                }
                if (radiobuttonClickListener != null && v == radiobutton)
                {
                    radiobuttonClickListener.OnIRadiobuttonActionClick(AdapterPosition);
                }
            }
        }
    }
}



