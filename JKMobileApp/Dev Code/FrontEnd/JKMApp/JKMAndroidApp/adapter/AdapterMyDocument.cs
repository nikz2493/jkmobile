using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.Utility;
using JKMPCL.Model;
using System;
using System.Collections.Generic;
using static JKMAndroidApp.adapter.AdapterMyDocument;

namespace JKMAndroidApp.adapter
{
    /// class Name      : AdapterMyDocument
    /// Author          : Sanket Prajapati
    /// Creation Date   : 7 fab 2018
    /// Purpose         : Adapter use for Card view bind 
    /// Revision        : 
    /// </summary>
    class AdapterMyDocument : RecyclerView.Adapter
    {
        private readonly List<DocumentModel> docList;

        /// interface Name  : IAdapterImageviewClickListener
        /// Author          : Sanket Prajapati
        /// Creation Date   : 7 fab 2018
        /// Purpose         : Use for OnClick Imageview Click handler
        /// Revision        : 
        /// </summary>
        public interface IAdapterImageviewClickListener
        {
            void OnImageviewActionClick(int position);
        }
        public static IAdapterImageviewClickListener itemClickListener { get; set; }
        public override int ItemCount => docList.Count;

        /// class Name      : AdapterMyDocument
        /// Author          : Sanket Prajapati
        /// Creation Date   : 7 fab 2018
        /// Purpose         : Use for bind data
        /// Revision        : 
        /// </summary>
        public AdapterMyDocument(List<DocumentModel> adapterdoclist, IAdapterImageviewClickListener iImageListener)
        {
            docList = adapterdoclist;
            itemClickListener = iImageListener;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = null;
            var DocumentLayoutId = Resource.Layout.row_mydocument;
            itemView = LayoutInflater.From(parent.Context).Inflate(DocumentLayoutId, parent, false);
            var viewholder = new AdapterMyDocumentViewHolder(itemView);
            return viewholder;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            DocumentModel docModel = docList[position];
            var viewHolder = holder as AdapterMyDocumentViewHolder;
            viewHolder.tvDocTitle.Text = docModel.DocumentTitle;
            AdapterMyDocumentViewHolder.SetDocIconAndColor(Convert.ToString(docModel.DocumentType), viewHolder.imgViewDocumentIcon, viewHolder.linearLayoutRandomColor);
        }
    }

    /// class Name      : AdapterMyDocumentViewHolder
    /// Author          : Sanket Prajapati
    /// Creation Date   : 7 fab 2018
    /// Purpose         : Use for hold control
    /// Revision        : 
    /// </summary>
    public class AdapterMyDocumentViewHolder : RecyclerView.ViewHolder,View.IOnClickListener
    {
        public LinearLayout linearLayoutRandomColor { get; set; }
        public ImageView imgViewDocumentIcon { get; set; }
        public ImageView imgViewPdf { get; set; }
        public TextView tvDocTitle { get; set; }

        public AdapterMyDocumentViewHolder(View itemView) : base(itemView)
        {
            linearLayoutRandomColor = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayoutRandomColor);
            imgViewDocumentIcon = itemView.FindViewById<ImageView>(Resource.Id.imgviewdocIcon);
            imgViewPdf = itemView.FindViewById<ImageView>(Resource.Id.imageViewpdf);
            tvDocTitle = itemView.FindViewById<TextView>(Resource.Id.tvAlertTitle);

            imgViewPdf.SetOnClickListener(this);
        }

        public void OnClick(View v)
        {
            if (itemClickListener != null && v == imgViewPdf)
            {
                itemClickListener.OnImageviewActionClick(AdapterPosition);
            }
        }

        /// Method Name     : AdapterMyDocumentViewHolder
        /// Author          : Sanket Prajapati
        /// Creation Date   : 7 fab 2018
        /// Purpose         : Use for set icon and color
        /// Revision        : 
        /// </summary>
        public static void SetDocIconAndColor(string docType, ImageView DocIcon, LinearLayout linearLayoutRandomColor)
        {
            switch (docType)
            {
                case "0":
                    DocIcon.SetImageResource(Resource.Drawable.icon_valuation);
                    LeftCornerDrawable.CustomView(linearLayoutRandomColor, 0);
                    linearLayoutRandomColor.SetBackgroundColor(Color.Rgb(226, 194, 131)); /// Valuation
                    break;

                case "1":
                    DocIcon.SetImageResource(Resource.Drawable.icon_rights_responsibilities);
                    LeftCornerDrawable.CustomView(linearLayoutRandomColor, 0);
                    linearLayoutRandomColor.SetBackgroundColor(Color.Rgb(131, 222, 226));// Rights & responsibilities
                    break;

                case "2":
                    DocIcon.SetImageResource(Resource.Drawable.icon_pre_move_confrimation);
                    LeftCornerDrawable.CustomView(linearLayoutRandomColor, 0);
                    linearLayoutRandomColor.SetBackgroundColor(Color.Rgb(134, 172, 219));  //Estimate
                    break;

                default:
                    DocIcon.SetImageResource(Resource.Drawable.icon_day_of_service_check_in);
                    LeftCornerDrawable.CustomView(linearLayoutRandomColor, 0);
                    linearLayoutRandomColor.SetBackgroundColor(Color.Rgb(234, 65, 87));  //Others
                    break;
            }
        }
    }
}