using Android.OS;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.Common;
using JKMPCL.Model;

namespace JKMAndroidApp.fragment
{
    /// <summary>
    /// Method Name     : FragmentMyMoveDetails
    /// Author          : Sanket Prajapati
    /// Creation Date   : 23 Jan 2018
    /// Purpose         : Fragement for MoveDetails page
    /// Revision        : 
    /// </summary>
    public class FragmentMyMoveDetails : Android.Support.V4.App.Fragment
    {
        private View view;
        TextView tvPackDate, tvDisplayPackDate, tvLoadDate, tvDisplayLoadDate, tvDeliverySpread, tvDisplayDeliverySpread;
        ImageView imgArrowBlue, imgArrowPink, imgArrowGold, imgMoveDelivery;
        MoveDataModel dtoMoveDataModel;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
       
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.LayoutFragmentMyMoveDetails, container, false);
            dtoMoveDataModel = DTOConsumer.dtoMoveData;
            UIReference();
            PopulateData();
            ApplyFont();
            return view;
        }

        /// Method Name     : UIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Find all control   
        /// Revision        : 
        /// </summary>
        private void UIReference()
        {
            tvPackDate = view.FindViewById<TextView>(Resource.Id.tvPackdate);
            tvDisplayPackDate = view.FindViewById<TextView>(Resource.Id.tvDisplayPackDate);
            tvLoadDate = view.FindViewById<TextView>(Resource.Id.tvLoaddate);
            tvDisplayLoadDate = view.FindViewById<TextView>(Resource.Id.tvDispalyloaddate);
            tvDeliverySpread = view.FindViewById<TextView>(Resource.Id.tvDeliverySpread);
            tvDisplayDeliverySpread = view.FindViewById<TextView>(Resource.Id.tvDispalyDeliverySpread);
            imgArrowBlue = view.FindViewById<ImageView>(Resource.Id.imgArrowblue);
            imgArrowPink = view.FindViewById<ImageView>(Resource.Id.imgArrowpink);
            imgArrowGold = view.FindViewById<ImageView>(Resource.Id.imgArrowgold);
            imgMoveDelivery = view.FindViewById<ImageView>(Resource.Id.imgMovedelivery);
        }

        /// <summary>
        /// Method Name     : ApplyFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 12-02-2017
        /// Purpose         : Apply font all control   
        /// Revision        : 
        /// </summary>
        public void ApplyFont()
        {
            UIHelper.SetTextViewFont(tvPackDate, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDisplayPackDate, (int)UIHelper.LinotteFont.LinotteRegular, Activity.Assets);
            UIHelper.SetTextViewFont(tvLoadDate, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDisplayLoadDate, (int)UIHelper.LinotteFont.LinotteRegular, Activity.Assets);
            UIHelper.SetTextViewFont(tvDeliverySpread, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDisplayDeliverySpread, (int)UIHelper.LinotteFont.LinotteRegular, Activity.Assets);
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
            if (dtoMoveDataModel != null && string.IsNullOrEmpty(dtoMoveDataModel.message))
            {
                if(!string.IsNullOrEmpty(dtoMoveDataModel.MoveDetails_PackStartDate))
                {
                    tvDisplayPackDate.Text = dtoMoveDataModel.MoveDetails_PackStartDate;
                }
                if (!string.IsNullOrEmpty(dtoMoveDataModel.MoveDetails_LoadStartDate))
                {
                    tvDisplayLoadDate.Text = dtoMoveDataModel.MoveDetails_LoadStartDate;
                }
                if(!string.IsNullOrEmpty(dtoMoveDataModel.MoveDetails_DeliveryStartDate) && !string.IsNullOrEmpty(dtoMoveDataModel.MoveDetails_DeliveryEndDate))
                {
                    if (dtoMoveDataModel.MoveDetails_DeliveryStartDate == dtoMoveDataModel.MoveDetails_DeliveryEndDate)
                    {
                        tvDeliverySpread.Text = StringResource.msgDeliverytobeScheduled;
                        tvDisplayDeliverySpread.Text = dtoMoveDataModel.MoveDetails_DeliveryStartDate;
                        imgMoveDelivery.SetImageResource(Resource.Drawable.icon_move_scheduled);
                        imgArrowGold.SetImageResource(Resource.Drawable.icon_arrow_cyan);
                    }
                    else
                    {
                        tvDeliverySpread.Text = StringResource.msgDeliverSpread;
                        tvDisplayDeliverySpread.Text = string.Format(StringResource.msgDispalyDeliverySpread,
                                                                   dtoMoveDataModel.MoveDetails_DeliveryStartDate,
                                                                   dtoMoveDataModel.MoveDetails_DeliveryEndDate);
                        imgMoveDelivery.SetImageResource(Resource.Drawable.icon_Move_delivery);
                        imgArrowGold.SetImageResource(Resource.Drawable.icon_arrow_gold);
                    }
                }
               
            }
        }
    }
}