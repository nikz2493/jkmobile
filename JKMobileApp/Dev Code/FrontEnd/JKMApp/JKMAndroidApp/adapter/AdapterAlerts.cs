using System;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using JKMPCL.Model;
using Android.Support.V7.Widget;
using JKMAndroidApp.Utility;
using Android.Graphics;
using Android.Content;
using Android.Provider;
using System.Collections.Generic;

namespace JKMAndroidApp.adapter
{
    class AdapterAlerts : RecyclerView.Adapter
    {
        
        public interface IAdapterTextViewClickListener
        {
            void OnTextviewActionClick(int position);
        }

        private readonly List<AlertModel> alertList;
        public static IAdapterTextViewClickListener itemClickListener { get; set; }

        public AdapterAlerts(List<AlertModel> adpterAlertlist, IAdapterTextViewClickListener listener)
        {
            alertList = adpterAlertlist;
            itemClickListener = listener;
        }

        /// <summary>
        /// /fill Alert Card layout 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="viewType"></param>
        /// <returns></returns>
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = null;
            var id = Resource.Layout.row_alerts;
            itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);
            var vh = new AdapterAlertsViewHolder(itemView);
            return vh;
        }

        /// <summary>
        /// //Set Color Alert Box And  Time And Day Textview
        /// </summary>
        /// <param name="viewHolder"></param>
        /// <param name="position"></param>
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            AlertModel alertModel = alertList[position];
            var viewholder = holder as AdapterAlertsViewHolder;
            viewholder.mTextViewTime.Text = AdapterAlertsViewHolder.GetTimeAndDay(alertModel.StartDate);
            viewholder.tvAlertTitle.Text = alertModel.AlertTitle;
            AdapterAlertsViewHolder.SetAlertIconAndColor(Convert.ToString(alertModel.AlertType), viewholder.alertIcon, viewholder.mLinearLayoutRandomColor, viewholder.mTextViewTime);
        }

        public override int ItemCount => alertList.Count;

        public class AdapterAlertsViewHolder : RecyclerView.ViewHolder, View.IOnClickListener
        {

            public LinearLayout mLinearLayoutRandomColor;
            public TextView mTextViewTime;
            public ImageView alertIcon;
            public TextView tvActionAlert;
            public TextView tvAlertTitle;

            public AdapterAlertsViewHolder(View itemView) : base(itemView)
            {
                mTextViewTime = itemView.FindViewById<TextView>(Resource.Id.textViewTime);
                mLinearLayoutRandomColor = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayoutRandomColor);
                alertIcon = itemView.FindViewById<ImageView>(Resource.Id.alertIcon);
                tvActionAlert = itemView.FindViewById<TextView>(Resource.Id.tvActionAlert);
                tvAlertTitle = itemView.FindViewById<TextView>(Resource.Id.tvAlertTitle);

                tvActionAlert.SetOnClickListener(this);
            }

            public void OnClick(View v)
            {
                if (itemClickListener != null)
                {
                    itemClickListener.OnTextviewActionClick(AdapterPosition);
                }
            }

            public static void SetAlertIconAndColor(string alertType, ImageView alertIcon, LinearLayout mLinearLayoutRandomColor, TextView mTextViewTime)
            {
                switch (alertType)
                {
                    case "0":
                        alertIcon.SetImageResource(Resource.Drawable.icon_complete_wizard_reminder);
                        LeftCornerDrawable.CustomView(mLinearLayoutRandomColor, 0);
                        mLinearLayoutRandomColor.SetBackgroundColor(Color.Rgb(224, 139, 161));
                        mTextViewTime.SetTextColor(Color.Rgb(224, 139, 161));
                        break;

                    case "1":
                        alertIcon.SetImageResource(Resource.Drawable.icon_book_your_move);
                        LeftCornerDrawable.CustomView(mLinearLayoutRandomColor, 0);
                        mLinearLayoutRandomColor.SetBackgroundColor(Color.Rgb(224, 139, 161));
                        mTextViewTime.SetTextColor(Color.Rgb(224, 139, 161));
                        break;

                    case "2":
                        alertIcon.SetImageResource(Resource.Drawable.icon_pre_move_confrimation);
                        LeftCornerDrawable.CustomView(mLinearLayoutRandomColor, 0);
                        mLinearLayoutRandomColor.SetBackgroundColor(Color.Rgb(226, 194, 132));
                        mTextViewTime.SetTextColor(Color.Rgb(226, 194, 132));
                        break;

                    case "3":
                        alertIcon.SetImageResource(Resource.Drawable.icon_day_of_service_check_in);
                        LeftCornerDrawable.CustomView(mLinearLayoutRandomColor, 0);
                        mLinearLayoutRandomColor.SetBackgroundColor(Color.Rgb(134, 219, 180));
                        mTextViewTime.SetTextColor(Color.Rgb(134, 219, 180));
                        break;

                    case "4":
                        alertIcon.SetImageResource(Resource.Drawable.icon_end_of_service_check_in);
                        LeftCornerDrawable.CustomView(mLinearLayoutRandomColor, 0);
                        mLinearLayoutRandomColor.SetBackgroundColor(Color.Rgb(131, 205, 226));
                        mTextViewTime.SetTextColor(Color.Rgb(131, 205, 226));
                        break;

                    case "5":
                        alertIcon.SetImageResource(Resource.Drawable.icon_final_payment_made);
                        LeftCornerDrawable.CustomView(mLinearLayoutRandomColor, 0);
                        mLinearLayoutRandomColor.SetBackgroundColor(Color.Rgb(244, 191, 139));
                        mTextViewTime.SetTextColor(Color.Rgb(244, 191, 139));
                        break;

                    case "6":
                        alertIcon.SetImageResource(Resource.Drawable.icon_date_of_service_change);
                        LeftCornerDrawable.CustomView(mLinearLayoutRandomColor, 0);
                        mLinearLayoutRandomColor.SetBackgroundColor(Color.Rgb(134, 172, 219));
                        mTextViewTime.SetTextColor(Color.Rgb(134, 172, 219));
                        break;
                }
               
            }

            public static string GetTimeAndDay(object dtInsertTime)
            {
                DateTime dtTime = (DateTime)dtInsertTime;
                string strCreateTime = "";
                TimeSpan ts = DateTime.Now - dtTime;
                if (ts.Days == 1)
                {
                    strCreateTime = ts.Days + " day ago";
                }
                else if (ts.Days >= 1)
                {
                    strCreateTime = ts.Days + " days ago";
                }
                else if (ts.Hours == 1)
                {
                    strCreateTime = ts.Hours + " hour ago";
                }
                else if (ts.Hours >= 1)
                {
                    strCreateTime = ts.Hours + " hours ago";
                }
                else if (ts.Minutes == 1)
                {
                    strCreateTime = ts.Minutes + " minute ago";
                }
                else if (ts.Minutes >= 1)
                {
                    strCreateTime = ts.Minutes + " minutes ago";
                }
                else if (ts.Minutes >= 0)
                {
                    strCreateTime = "just now";
                }
                return strCreateTime;
            }

        }
    }
}