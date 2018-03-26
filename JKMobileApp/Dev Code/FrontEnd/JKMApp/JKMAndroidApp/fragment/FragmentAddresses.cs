using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.activity;
using JKMAndroidApp.Common;
using JKMPCL.Model;
using System;
using System.Linq;

namespace JKMAndroidApp.fragment
{
    /// <summary>
    /// Method Name     : FragmentAbouts
    /// Author          : Sanket Prajapati
    /// Creation Date   : 23 Jan 2018
    /// Purpose         : Fragement for addresss layout
    /// Revision        : 
    /// </summary>
    public class FragmentAddresses : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        View view;

        private ViewSwitcher viewSwitcher;
        private LinearLayout linearLayoutDisplay;
        private TextView tvDisplayAddresstitle;
        private TextView tvDisplayOrigin;
        private TextView txtDisplayOriginAddress;
        private TextView tvDisplayDestination;
        private TextView txtDisplayDestinationAddress;
        private TextView textViewUpdatesAddresses;

        private LinearLayout linearLayoutEdit;
        private TextView tvEditAddresstitle;
        private TextView tvEditOrigin;
        private TextView txtEditOriginAddress;
        private TextView tvEditDestination;
        private TextView txtEditDestinationAddress;
        private TextView textViewSubmitChanges;
        private LinearLayout linearLayoutBack;
        private TextView tvNext;
        private TextView tvback;
        private TextView tvViewEstimate;
        private EstimateModel estimateModel;
        private ImageButton btnBack;
        private LinearLayout linearLayoutViewEstimate;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            view = inflater.Inflate(Resource.Layout.LayoutFragmentAddresses, container, false);
            estimateModel = DTOConsumer.dtoEstimateData.FirstOrDefault(rc => rc.MoveNumber == UIHelper.SelectedMoveNumber);
            UIReference();
            PopulateData();
            ApplyFont();
            UIClickEvents();
            view.Invalidate();
            return view;
        }

        public override void OnResume()
        {
            UIReference();
            PopulateData();
            base.OnResume();
        }

        /// <summary>
        /// Method Name      : UIClickEvents
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Set all control events
        /// Revision        : 
        /// </summary>
        private void UIClickEvents()
        {
            btnBack.Click += MTextViewBack_Click;
            linearLayoutBack.Click += MTextViewBack_Click;
            SetNextFragmentClick();
            SetEditModeViewSwitcherClick();
            
            linearLayoutViewEstimate.Click += delegate
            {
                StartActivity(new Intent(Activity, typeof(PdfActivity)));
            };
        }

        private void SetNextFragmentClick()
        {
            tvNext.Click += delegate
            {
                string strValidation = ValidationAddress();
                if (string.IsNullOrEmpty(strValidation))
                {
                    if (viewSwitcher.CurrentView == linearLayoutEdit)
                    {
                        viewSwitcher.ShowPrevious();
                    }
                    ((ActivityEstimateViewPager)Activity).FragmentNext();
                }
                else
                {
                    AlertMessage(strValidation);
                }
            };
            
        }

        /// Method Name     : SetEditModeViewSwitcherClick
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for ViewSwitcher switch layout 
        /// Revision        : 
        /// </summary>
        private void SetEditModeViewSwitcherClick()
        {
            textViewUpdatesAddresses.Click += delegate
            {
                if (viewSwitcher.CurrentView == linearLayoutDisplay)
                {
                    viewSwitcher.ShowNext();
                    tvback.Text = "Cancel";
                    tvNext.Text = "Next Step";

                    txtEditOriginAddress.Text = txtDisplayOriginAddress.Text;
                    txtEditDestinationAddress.Text = txtDisplayDestinationAddress.Text;
                }
            };

            textViewSubmitChanges.Click += delegate
            {
                if (viewSwitcher.CurrentView == linearLayoutEdit)
                {
                    string strValidation = ValidationAddress();
                    if (string.IsNullOrEmpty(strValidation))
                    {
                        viewSwitcher.ShowPrevious();
                        tvback.Text = "Back";
                        tvNext.Text = "Yes, Captured Correctly";

                        txtDisplayOriginAddress.Text = txtEditOriginAddress.Text;
                        txtDisplayDestinationAddress.Text = txtEditDestinationAddress.Text;
                        EditData();
                    }
                    else
                    {
                        AlertMessage(strValidation);
                    }
                }
            };
        }

        /// <summary>
        /// Method Name     : PopulateData
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : fill Estimate Data  
        /// Revision        : 
        /// </summary>
        public void AlertMessage(String StrErrorMessage)
        {
            Android.App.AlertDialog.Builder dialogue;
            Android.App.AlertDialog alert;
            dialogue = new Android.App.AlertDialog.Builder(new ContextThemeWrapper(Activity, Resource.Style.AlertDialogCustom));
            alert = dialogue.Create();
            alert.SetMessage(StrErrorMessage);
            alert.SetButton(StringResource.msgOK, (c, ev) =>
            {
                alert.Dispose();
            });
            alert.Show();
        }

        /// <summary>
        /// Method Name     : ValidAddress
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for address validation 
        /// Revision        : 
        /// </summary>
        public string ValidationAddress()
        {
            string errorMessage = string.Empty;
            if (string.IsNullOrEmpty(txtEditOriginAddress.Text.Trim()))
            {
                errorMessage = StringResource.msgOriginAddressRequired;
            }
            else if (string.IsNullOrEmpty(txtEditDestinationAddress.Text.Trim()))
            {
                errorMessage = StringResource.msgDestinationAddressRequired;
            }
            return errorMessage;
        }

        /// Method Name     : MTextViewBack_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Move for next fragment
        /// Revision        : 
        /// </summary>
        private void MTextViewBack_Click(object sender, EventArgs e)
        {
            if (viewSwitcher.CurrentView == linearLayoutEdit)
            {
                viewSwitcher.ShowPrevious();
            }
            ((ActivityEstimateViewPager)Activity).FragmentBack();
            
            tvback.Text = "Back";
            tvNext.Text = "Yes, Captured Correctly";
        }

        /// Method Name     : UIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Find all control   
        /// Revision        : 
        /// </summary>
        private void UIReference()
        {
            viewSwitcher = view.FindViewById<ViewSwitcher>(Resource.Id.viewSwitcher);
            linearLayoutDisplay = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutDisplay);
            tvDisplayAddresstitle = view.FindViewById<TextView>(Resource.Id.tvDisplayAddresstitle);
            tvDisplayOrigin = view.FindViewById<TextView>(Resource.Id.tvDisplayOrigin);
            txtDisplayOriginAddress = view.FindViewById<TextView>(Resource.Id.txtDisplayOriginAddress);
            tvDisplayDestination = view.FindViewById<TextView>(Resource.Id.tvDisplayDestination);
            txtDisplayDestinationAddress = view.FindViewById<TextView>(Resource.Id.txtDisplayDestinationAddress);
            textViewUpdatesAddresses = view.FindViewById<TextView>(Resource.Id.textViewUpdatesAddresses);
         
            linearLayoutEdit = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutEdit);
            tvEditAddresstitle = view.FindViewById<TextView>(Resource.Id.tvEditAddresstitle);
            tvEditOrigin = view.FindViewById<TextView>(Resource.Id.tvEditOrigin);
            txtEditOriginAddress = view.FindViewById<TextView>(Resource.Id.txtEditOriginAddress);
            tvEditDestination = view.FindViewById<TextView>(Resource.Id.tvEditDestination);
            txtEditDestinationAddress = view.FindViewById<TextView>(Resource.Id.txtEditDestinationAddress);
            textViewSubmitChanges = view.FindViewById<TextView>(Resource.Id.textViewSubmitChanges);

            tvViewEstimate = view.FindViewById<TextView>(Resource.Id.tvViewEstimate);
            tvNext = view.FindViewById<TextView>(Resource.Id.textViewNext);
            tvback = view.FindViewById<TextView>(Resource.Id.tvback);
            btnBack = view.FindViewById<ImageButton>(Resource.Id.btnBack);
            linearLayoutBack = view.FindViewById<LinearLayout>(Resource.Id.textViewBack);
            linearLayoutViewEstimate = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutViewEstimate);
        }

        /// Method Name     : ApplyFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Apply font all control   
        /// Revision        : 
        /// </summary>
        public void ApplyFont()
        {
            UIHelper.SetTextViewFont(tvDisplayAddresstitle, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvEditAddresstitle, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDisplayOrigin, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvEditOrigin, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(txtDisplayOriginAddress, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(txtEditOriginAddress, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDisplayDestination, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvEditDestination, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(txtDisplayDestinationAddress, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(txtEditDestinationAddress, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewUpdatesAddresses, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewSubmitChanges, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvViewEstimate, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvback, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvNext, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
        }

        /// <summary>
        /// Method Name     : PopulateData
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : fill Estimate Data  
        /// Revision        : 
        /// </summary>
        public void PopulateData()
        {
            if (estimateModel != null && string.IsNullOrEmpty(estimateModel.message))
            {
                txtDisplayOriginAddress.Text = estimateModel.CustomOriginAddress;
                txtDisplayDestinationAddress.Text = estimateModel.CustomDestinationAddress;
                txtEditOriginAddress.Text = estimateModel.CustomOriginAddress;
                txtEditDestinationAddress.Text = estimateModel.CustomDestinationAddress;
            }
        }

        /// <summary>
        /// Method Name     : EditData
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : edit Estimate Data  
        /// Revision        : 
        /// </summary>
        public void EditData()
        {
            if (estimateModel != null && string.IsNullOrEmpty(estimateModel.message))
            {
                int index;
                estimateModel.CustomOriginAddress = txtEditOriginAddress.Text;
                estimateModel.CustomDestinationAddress = txtEditDestinationAddress.Text;
                estimateModel.IsAddressEdited = true;
                index = DTOConsumer.dtoEstimateData.IndexOf(estimateModel);
                DTOConsumer.dtoEstimateData[index] = estimateModel;
            }
        }
    }
}