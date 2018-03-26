using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.activity;
using JKMAndroidApp.Common;
using JKMPCL.Model;
using JKMPCL.Services;

namespace JKMAndroidApp.fragment
{
    /// <summary>
    /// Method Name     : FragmentValuation
    /// Author          : Sanket Prajapati
    /// Creation Date   : 23 Jan 2018
    /// Purpose         : Fragement for Valuation layout
    /// Revision        : 
    /// </summary>
    public class FragmentValuation : Android.Support.V4.App.Fragment
    {

        private View view;
        ViewSwitcher viewSwitcher;
        TextView tvtitleDiscriptions, tvtitEditDescriptions, tvDeclaredvalue, tvDisplayDeclaredvalue, tvCoverage, tvDisplayCoverage, tvCost,
               tvDisplayCost, tvUpdatedneedes, tvEDitDeclaredvalue, tvEditCoverage, tvEditCost, tvSubmitChanges,
               tvViewEstimate, tvBack, tvNext;
        LinearLayout linearLayoutDisplay, linearLayoutEdit, linearLayoutViewEstimate, linearLayoutBack;
        EditText txtDeclaredvalue;
        EditText txtCost;
        Spinner spinnerCoverage;
        ImageButton btnBack;
        private EstimateModel estimateModel;
        private ValuationDeductibleModel valuationDeductible;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.LayoutFragmentEstimateValution, container, false);
            valuationDeductible = new ValuationDeductibleModel();
            estimateModel = DTOConsumer.dtoEstimateData.FirstOrDefault(rc => rc.MoveNumber == UIHelper.SelectedMoveNumber);
            UIReferences();
            BindSpiner();
            PopulateData();
            ApplyFont();
            view.Invalidate();
            UIClickEvents();
          
            return view;
        }

        public override void OnResume()
        {
            UIReferences();
            BindSpiner();
            PopulateData();
            base.OnResume();
        }

        /// </summary>
        /// Method Name     : UIReferences
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Finds Control  
        /// Revision        : 
        /// </summary>
        private void UIReferences()
        {
            FindLinerlayout();
            FindDisplayTextView();
            FindEditValueTextView();
            FindEditText();
        }

        /// Method Name     : FindDisplayTextView
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Find Dispaly Textview Control  
        /// Revision        : 
        /// </summary>
        private void FindDisplayTextView()
        {
            tvtitleDiscriptions = view.FindViewById<TextView>(Resource.Id.tvDispalytitleDescriptions);
            tvtitEditDescriptions = view.FindViewById<TextView>(Resource.Id.tvtitEditDescriptions);
            tvDeclaredvalue = view.FindViewById<TextView>(Resource.Id.tvDeclaredvalue);
            tvDisplayDeclaredvalue = view.FindViewById<TextView>(Resource.Id.tvDisplayDeclaredvalue);
            tvCoverage = view.FindViewById<TextView>(Resource.Id.tvCoverage);
            tvDisplayCoverage = view.FindViewById<TextView>(Resource.Id.tvDisplayCoverage);
            tvCost = view.FindViewById<TextView>(Resource.Id.tvCost);
            tvDisplayCost = view.FindViewById<TextView>(Resource.Id.tvDisplayCost);
            tvUpdatedneedes = view.FindViewById<TextView>(Resource.Id.tvUpdatedneedes);
        }


        

        /// Method Name     : FindEditValueTextView
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Find EditTextview Control  
        /// Revision        : 
        /// </summary>
        private void FindEditValueTextView()
        {
            tvEDitDeclaredvalue = view.FindViewById<TextView>(Resource.Id.tvEditDeclaredvalue);
            tvEditCoverage = view.FindViewById<TextView>(Resource.Id.tvEditCoverage);
            tvEditCost = view.FindViewById<TextView>(Resource.Id.tvEditCost);
            tvSubmitChanges = view.FindViewById<TextView>(Resource.Id.tvSubmitChanges);
            tvCost = view.FindViewById<TextView>(Resource.Id.tvCost);
            tvDisplayCost = view.FindViewById<TextView>(Resource.Id.tvDisplayCost);
            tvViewEstimate = view.FindViewById<TextView>(Resource.Id.tvViewEstimate);
            tvBack = view.FindViewById<TextView>(Resource.Id.tvBack);
            tvNext = view.FindViewById<TextView>(Resource.Id.tvNext);
        }

        /// Method Name     : FindEditText
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Find EditText Control  
        /// Revision        : 
        /// </summary>
        private void FindEditText()
        {
            txtDeclaredvalue = view.FindViewById<EditText>(Resource.Id.txtDeclaredvalue);
            spinnerCoverage = view.FindViewById<Spinner>(Resource.Id.spinnerCoverage);
            txtCost = view.FindViewById<EditText>(Resource.Id.txtCost);
            txtCost.LongClickable = false;
        }

        /// Method Name     : FindLinerlayout
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Find layout Control  
        /// Revision        : 
        /// </summary>
        private void FindLinerlayout()
        {
            viewSwitcher = view.FindViewById<ViewSwitcher>(Resource.Id.viewSwitcher);
            linearLayoutDisplay = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutDisplay);
            linearLayoutEdit = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutEdit);

            linearLayoutViewEstimate = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutViewEstimate);
            linearLayoutBack = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutBack);
            btnBack = view.FindViewById<ImageButton>(Resource.Id.btnBack);
        }

        /// Method Name     : UIClickEvents
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Set click event   
        /// Revision        : 
        /// </summary>
        private void UIClickEvents()
        {
            SetBackFragmentClick();
            SetNextFragmentClick();
            OpenPdfClick();
            SetEditModeViewSwitcherClick();
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
            tvUpdatedneedes.Click += delegate
            {
                if (viewSwitcher.CurrentView == linearLayoutDisplay)
                {
                    viewSwitcher.ShowNext();
                    txtDeclaredvalue.Text = UtilityPCL.RemoveCurrencyFormat(tvDisplayDeclaredvalue.Text);
                    var list = UtilityPCL.ValuationDeductibleList();
                    valuationDeductible = new ValuationDeductibleModel();
                    valuationDeductible = list.FirstOrDefault(rc => rc.DeductibleName == tvDisplayCoverage.Text);
                    if (valuationDeductible != null)
                    {
                        spinnerCoverage.SetSelection(valuationDeductible.Index);
                    }
                    else
                    {
                        spinnerCoverage.SetSelection(1);
                    }
                    txtCost.Text = UtilityPCL.RemoveCurrencyFormat(tvDisplayCost.Text);
                    tvBack.Text = StringResource.wizBtnCancel;
                    tvNext.Text = StringResource.wizBtnNextStep;
                }
            };

            tvSubmitChanges.Click += delegate
            {
                if (viewSwitcher.CurrentView == linearLayoutEdit && Validation())
                {
                    viewSwitcher.ShowPrevious();
                    tvDisplayDeclaredvalue.Text = UtilityPCL.CurrencyFormat(txtDeclaredvalue.Text);
                    if (valuationDeductible != null)
                    {
                        tvDisplayCoverage.Text = valuationDeductible.DeductibleName;
                    }
                    else
                    {
                        var list = UtilityPCL.ValuationDeductibleList();
                        valuationDeductible = new ValuationDeductibleModel();
                        valuationDeductible = list.FirstOrDefault(rc => rc.DeductibleName == tvDisplayCoverage.Text);
                        tvDisplayCoverage.Text = valuationDeductible.DeductibleName;
                    }
                    tvDisplayCost.Text = UtilityPCL.CurrencyFormat(txtCost.Text);
                    EditData();
                    tvBack.Text = StringResource.wizBtnBack;
                    tvNext.Text = StringResource.wizYesCapturedCorrectly;
                }
            };
        }

        /// Method Name     : Validation
        /// Author          : Sanket Prajapati
        /// Creation Date   : 29 jan 2018
        /// Purpose         : Use for valuation Validation
        /// Revision        : 
        /// </summary>
        private bool Validation()
        {
            bool iValue = true;
            valuationDeductible = new ValuationDeductibleModel();
            valuationDeductible = UtilityPCL.ValuationDeductibleList().FirstOrDefault(rc => rc.DeductibleName == spinnerCoverage.SelectedItem.ToString());
            if (viewSwitcher.CurrentView == linearLayoutEdit)
            {
                if (string.IsNullOrEmpty(txtDeclaredvalue.Text.Trim()))
                {
                    iValue = false;
                    AlertMessage(StringResource.msgPleaseEnterDeclaredValue);
                }
                else if (string.IsNullOrEmpty(txtCost.Text.Trim()))
                {
                    iValue = false;
                    AlertMessage(StringResource.msgPleaseEnetrCostValue);
                }
            }
            return iValue;
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
                if (Validation())
                {
                    if (viewSwitcher.CurrentView == linearLayoutEdit)
                    {
                        viewSwitcher.ShowPrevious();
                    }
                   ((ActivityEstimateViewPager)Activity).FragmentNext();
                }
            };
        }

        /// Method Name     : SetBackFragmentClick
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for move back fragment 
        /// Revision        : 
        /// </summary>
        private void SetBackFragmentClick()
        {
            btnBack.Click += MTextViewBack_Click;
            linearLayoutBack.Click += MTextViewBack_Click;
            tvBack.Text = StringResource.wizBtnBack;
            tvNext.Text = StringResource.wizYesCapturedCorrectly;
        }

        /// Method Name     : SetBackFragmentClick
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for move back fragment 
        /// Revision        : 
        /// </summary>
        private void MTextViewBack_Click(object sender, EventArgs e)
        {
            if (viewSwitcher.CurrentView == linearLayoutEdit)
            {
                viewSwitcher.ShowPrevious();
            }
            ((ActivityEstimateViewPager)Activity).FragmentBack();
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
                tvDisplayDeclaredvalue.Text = UtilityPCL.CurrencyFormat(estimateModel.ExcessValuation);
                tvDisplayCost.Text = UtilityPCL.CurrencyFormat(estimateModel.ValuationCost);
                if (!string.IsNullOrEmpty(estimateModel.ValuationDeductible))
                {
                    valuationDeductible = UtilityPCL.ValuationDeductibleList().Find(rc => rc.DeductibleName == estimateModel.ValuationDeductible || rc.DeductibleCode == estimateModel.ValuationDeductible);
                    tvDisplayCoverage.Text = valuationDeductible.DeductibleName;
                }
                else
                {
                    valuationDeductible = new ValuationDeductibleModel();
                    valuationDeductible = UtilityPCL.ValuationDeductibleList().FirstOrDefault(rc => rc.Index == 1);
                    if (valuationDeductible != null)
                    {
                        tvDisplayCoverage.Text = valuationDeductible.DeductibleName;
                    }
                }
            }
        }

        /// <summary>
        /// Method Name     : EditData
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Edit Estimate Data  
        /// Revision        : 
        /// </summary>
        public void EditData()
        {
            if (estimateModel != null && string.IsNullOrEmpty(estimateModel.message))
            {
                int index;
                estimateModel.ExcessValuation = UtilityPCL.RemoveCurrencyFormat(txtDeclaredvalue.Text);
                estimateModel.ValuationCost = txtCost.Text;
                if (valuationDeductible != null) { estimateModel.ValuationDeductible = valuationDeductible.DeductibleCode; }
                estimateModel.IsValuationEdited = true;

                index = DTOConsumer.dtoEstimateData.IndexOf(estimateModel);
                DTOConsumer.dtoEstimateData[index] = estimateModel;

            }
        }

        /// Method Name     : ApplyFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Apply font all control   
        /// Revision        : 
        /// </summary>
        public void ApplyFont()
        {
            UIHelper.SetTextViewFont(tvDeclaredvalue, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvtitEditDescriptions, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDisplayDeclaredvalue, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvCoverage, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDisplayCoverage, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvCost, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDisplayCost, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvUpdatedneedes, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvEDitDeclaredvalue, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(txtDeclaredvalue, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvEditCoverage, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvEditCost, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(txtCost, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvSubmitChanges, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvtitleDiscriptions, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvViewEstimate, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvBack, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvNext, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
        }

        


        /// Method Name     : BindSpiner
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use fort bind Spinner  
        /// Revision        : 
        /// </summary>
        private void BindSpiner()
        {
            List<String> valutionList = UtilityPCL.ValuationDeductibleBindingList();
            if (valutionList.Count > 0)
            {
                var categoryAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerItem, valutionList);
                categoryAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerCoverage.ItemSelected += spinnerCoverage_ItemSelected;
                spinnerCoverage.Adapter = categoryAdapter;
            }
        }
        /// Event Name      : spinnerCoverage_ItemSelected
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for selection 
        /// Revision        : 
        /// </summary>
        private void spinnerCoverage_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            valuationDeductible = new ValuationDeductibleModel();
            var spinner = (Spinner)sender;
            string strValue;
            strValue = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            var valuationDeductibleList = UtilityPCL.ValuationDeductibleList();
            if (valuationDeductibleList.Count > 0)
            {
                valuationDeductible = valuationDeductibleList.FirstOrDefault(rc => rc.DeductibleName == strValue);
            }
        }
    }
}
