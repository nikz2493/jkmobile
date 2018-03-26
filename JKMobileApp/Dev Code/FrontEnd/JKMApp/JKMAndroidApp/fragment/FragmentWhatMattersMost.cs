using System;
using System.Linq;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.activity;
using JKMAndroidApp.Common;
using JKMPCL.Model;

namespace JKMAndroidApp.fragment
{
    public class FragmentWhatMattersMost : Android.Support.V4.App.Fragment
    {
        private View view;
        private ViewSwitcher ViewSwitcher;
        private TextView tvtitleDiscriptions;
        private TextView textViewEditHeading;
        private TextView textViewHeading;
        private EditText tvWMMtitle;
        private TextView textViewUpdates;
        private EditText txtWMM;
        private TextView tvViewEstimate;

        private RelativeLayout relativeLayoutDisplay;
        private RelativeLayout relativeLayoutEdit;
        private LinearLayout linearLayoutBack;
        private TextView tvNext;
        private TextView tvUpdates;
        private TextView tvSubmitChanges;
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
            // Use this to return your custom view for this Fragment
            view = inflater.Inflate(Resource.Layout.LayoutFragmentWhatMattersMost, container, false);
            estimateModel = DTOConsumer.dtoEstimateData.FirstOrDefault(rc => rc.MoveNumber == UIHelper.SelectedMoveNumber);
            UIReferences();
            PopulateData();
            ApplyFont();
            UIClickEvents();
            
            return view;
        }

        public override void OnResume()
        {
            UIReferences();
            PopulateData();
            base.OnResume();
        }
        private void UIReferences()
        {
            ViewSwitcher = view.FindViewById<ViewSwitcher>(Resource.Id.viewSwitcher);
            relativeLayoutDisplay = view.FindViewById<RelativeLayout>(Resource.Id.relativeLayoutDisplay);
            relativeLayoutEdit = view.FindViewById<RelativeLayout>(Resource.Id.relativeLayoutEdit);
            tvUpdates = view.FindViewById<TextView>(Resource.Id.textViewUpdates);
            textViewEditHeading = view.FindViewById<TextView>(Resource.Id.textViewEditHeading);
            textViewHeading = view.FindViewById<TextView>(Resource.Id.textViewHeading);
            tvSubmitChanges = view.FindViewById<TextView>(Resource.Id.textViewSubmitChanges);
            linearLayoutBack = view.FindViewById<LinearLayout>(Resource.Id.textViewBack);
            tvNext = view.FindViewById<TextView>(Resource.Id.textViewNext);
            tvtitleDiscriptions = view.FindViewById<TextView>(Resource.Id.tvtitleDiscriptions);
            tvWMMtitle = view.FindViewById<EditText>(Resource.Id.tvWMMtitle);
            textViewUpdates = view.FindViewById<TextView>(Resource.Id.textViewUpdates);
            txtWMM = view.FindViewById<EditText>(Resource.Id.txtWMM);
            txtWMM.SetSelection(0);
            tvViewEstimate = view.FindViewById<TextView>(Resource.Id.tvViewEstimate);
            tvback = view.FindViewById<TextView>(Resource.Id.tvback);
            linearLayoutViewEstimate = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutViewEstimate);
            btnBack = view.FindViewById<ImageButton>(Resource.Id.btnBack);
        }

        /// Method Name     : ApplyFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Apply font all control   
        /// Revision        : 
        /// </summary>
        public void ApplyFont()
        {
            UIHelper.SetTextViewFont(tvtitleDiscriptions, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvWMMtitle, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewEditHeading, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewHeading, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewUpdates, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvSubmitChanges, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetEditTextFont(txtWMM, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvViewEstimate, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvback, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvNext, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
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
            SetNextFragmentClick();
            SetEditModeViewSwitcherClick();
            OpenPdfClick();
        }

        /// Method Name     : OpenPdfClick
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for open PDF 
        /// Revision        : 
        /// </summary>
        private void OpenPdfClick()
        {
            linearLayoutViewEstimate.Click += delegate
            {
                StartActivity(new Intent(Activity, typeof(PdfActivity)));
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
            tvUpdates.Click += delegate
            {
                if (ViewSwitcher.CurrentView == relativeLayoutDisplay)
                {
                    ViewSwitcher.ShowNext();
                    tvback.Text = "Cancel";
                    tvNext.Text = "Next Step";
                    txtWMM.Text = tvWMMtitle.Text;
                }
            };

            tvSubmitChanges.Click += delegate
            {
                if (ViewSwitcher.CurrentView == relativeLayoutEdit)
                {
                    string strValidation = ValidationWMM();
                    if (string.IsNullOrEmpty(strValidation))
                    {
                        ViewSwitcher.ShowPrevious();
                        tvback.Text = "Back";
                        tvNext.Text = "Yes, Captured Correctly";
                        tvWMMtitle.Text = txtWMM.Text;
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
        /// Method Name     : ValidationWHM
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for WMM validation  
        /// Revision        : 
        /// </summary>
        public string ValidationWMM()
        {
            string errorMessage = string.Empty;
            if (string.IsNullOrEmpty(txtWMM.Text.Trim()))
            {
                errorMessage = StringResource.msgWMMRequired;
            }
          
            return errorMessage;
        }

        /// Method Name     : SetNextFragmentClick
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for move next fragment 
        /// Revision        : 
        /// </summary>
        private void SetNextFragmentClick()
        {
            tvNext.Click += delegate
            {
                string strValidation = ValidationWMM();
                if (string.IsNullOrEmpty(strValidation))
                {
                    if (ViewSwitcher.CurrentView == relativeLayoutEdit)
                    {
                        ViewSwitcher.ShowPrevious();
                    }
                    ((ActivityEstimateViewPager)Activity).FragmentNext();
                }
                else
                {
                    AlertMessage(strValidation);
                }
            };

        }
        // Method Name      : MTextViewBack_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for move back fragment 
        /// Revision        : 
        /// </summary>
        private void MTextViewBack_Click(object sender, EventArgs e)
        {
           
            if (ViewSwitcher.CurrentView == relativeLayoutEdit)
            {
                ViewSwitcher.ShowPrevious();
            }
            ((ActivityEstimateViewPager)Activity).FragmentBack();
            tvback.Text = "Back";
            tvNext.Text = "Yes, Captured Correctly";
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
                tvWMMtitle.Text = estimateModel.WhatMattersMost;
                txtWMM.Text = estimateModel.WhatMattersMost;
            }
        }

        /// <summary>
        /// Method Name     : PopulateData
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : fill Estimate Data  
        /// Revision        : 
        /// </summary>
        public void EditData()
        {
            if (estimateModel != null && string.IsNullOrEmpty(estimateModel.message))
            {
                int index;
                estimateModel.WhatMattersMost= tvWMMtitle.Text.Trim();
                estimateModel.IsWhatMatterMostEdited = true;
                index = DTOConsumer.dtoEstimateData.IndexOf(estimateModel);
                DTOConsumer.dtoEstimateData[index] = estimateModel;
            }
        }
    }
}