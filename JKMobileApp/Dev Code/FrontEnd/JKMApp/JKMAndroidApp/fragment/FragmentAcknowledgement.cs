using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.Util;
using JKMAndroidApp.activity;
using JKMAndroidApp.Common;
using JKMPCL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using JKMPCL.Services;

namespace JKMAndroidApp.fragment
{
    /// <summary>
    /// Class Name      : FragmentAcknowledgement
    /// Author          : Sanket Prajapati
    /// Creation Date   : 23 Jan 2018
    /// Purpose         : Fragement for aknowledgment page
    /// Revision        : 
    /// </summary>
    public class FragmentAcknowledgement : Android.Support.V4.App.Fragment
    {
        private View view;
        private LinearLayout textViewBack;
        private TextView textViewNext;
        private RelativeLayout relativeLayoutServiceDate;
        private LinearLayout linearLayoutServiceDateEdit;

        private RelativeLayout relativeLayoutAddresses;
        private LinearLayout linearLayoutAddressesEdit;

        private RelativeLayout relativeLayoutWhatMattersMost;
        private RelativeLayout relativeLayoutWhatMattersMostEdit;
        private RelativeLayout relativeLayoutValutionEdit;

        private ImageView imageViewEditWhatMattersMost;
        private ImageView imageViewEditAddresses;
        private ImageView imageViewEditServiceDate;
        private ImageView imageViewEditValution;

        private TextView textViewHeading;
        private TextView textViewServiceDate;
        private TextView textViewPackDate;
        private TextView textViewDateEditPack;
        private TextView textViewLoadDate;
        private TextView textViewDateEditLoad;
        private TextView textViewMoveDate;
        private TextView textViewDateEditMove;
        private TextView textViewSubmitChanges;
        private TextView textViewServices;
        private TextView textViewVitalInformation;
        private TextView textViewAddresses;
        private TextView textViewOrigin;
        private EditText editTextAddressOrigin;
        private TextView textViewDestination;
        private EditText editTextAddressDestination;
        private TextView textViewUpdatesAddresses;
        private TextView textViewValuation;
        private TextView textViewWhatMattersMost;
        private EditText editTextWhatMattersMost;
        private TextView textViewSubmitChange;

        private LinearLayout linearLayoutEdit;
        private TextView tvEDitDeclaredvalue;
        private EditText txtDeclaredvalue;
        private TextView tvEditCoverage;
        private Spinner spinnerCoverage;
        private TextView tvEditCost;
        private EditText txtCost;

        private TextView textViewCheckBox;
        private TextView textViewViewEstimate;
        private TextView tvback;

        private CheckBox agreeCheckBox;
        private ImageButton btnBack;
        private LinearLayout linearLayoutViewEstimate;

        private DatePickerDialog dateDialogPack = null;
        private DatePickerDialog dateDialogLoad = null;
        private DatePickerDialog dateDialogMove = null;
        private EstimateModel estimateModel;

        private ImageView imageViewServiceDate;
        private ImageView imageViewAddresses;
        private ImageView imageViewWhatMattersMost;
        private ImageView imageViewValuation;

        private ValuationDeductibleModel valuationDeductible;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.LayoutFragmentAcknowledgement, container, false);
            estimateModel = DTOConsumer.dtoEstimateData.FirstOrDefault(rc => rc.MoveNumber == UIHelper.SelectedMoveNumber);
            SetUIReference();
            PopulateData();
            ApplyFont();
            UIClickEvents();
            view.Invalidate();
            return view;
        }

        public override void OnResume()
        {
            SetUIReference();
            BindSpiner();
            PopulateData();
            base.OnResume();
        }

        /// <summary>
        /// Event Name      : UIClickEvents
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Set all control events
        /// Revision        : 
        /// </summary>
        private void UIClickEvents()
        {
            btnBack.Click += textViewBack_Click;
            textViewBack.Click += textViewBack_Click;
            linearLayoutViewEstimate.Click += LinearLayoutViewEstimate_Click;
            NextViewClick();
            RelativeLayoutServiceDateClick();
            RelativeLayoutAddressesClick();
            RelativeLayoutWhatMattersMostClick();
            TextViewDateEditPackClick();
            TextViewDateEditLoadClick();
            TextViewDateEditMove();
            RelativeLayoutValuetion();
        }


        /// <summary>
        /// Method Name     : NextViewClick
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Use move for next fragment
        /// Revision        : 
        /// </summary>
        private void NextViewClick()
        {
            textViewNext.Click += delegate
            {
                if (agreeCheckBox.Checked)
                {
                    string erroMessage= ValidDate();
                    if (string.IsNullOrEmpty(erroMessage))
                    {
                        erroMessage = ValidationAddress();
                    }
                    if (string.IsNullOrEmpty(erroMessage))
                    {
                        erroMessage = ValidationValution();
                    }
                    if (string.IsNullOrEmpty(erroMessage))
                    {
                        erroMessage = ValidationWMM();
                    }
                    if (string.IsNullOrEmpty(erroMessage))
                    {
                        EditData();
                        //if (estimateModel != null && !estimateModel.IsDepositPaid)
                        //{
                        //    ((ActivityEstimateViewPager)Activity).FragmentNext();
                        //}
                        //else
                        //{
                            StartActivity(new Intent(Activity, typeof(ActivityMoveConfirmed)));
                      //  }
                    }
                    else
                    {
                        AlertMessage(erroMessage);
                    }
                }
                else
                {
                    AlertMessage(StringResource.msgNotAgree);
                }
            };
        }

        /// <summary>
        /// Method Name     : RelativeLayoutServiceDateClick
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Use for edit service date
        /// Revision        : 
        /// </summary>
        private void RelativeLayoutServiceDateClick()
        {
            imageViewEditServiceDate.Click += delegate
            {
                if (linearLayoutServiceDateEdit.Visibility == ViewStates.Gone)
                {
                    linearLayoutServiceDateEdit.Visibility = ViewStates.Visible;
                    imageViewEditServiceDate.Selected = true;
                    imageViewServiceDate.SetImageResource(Resource.Drawable.checked_yellow);
                    estimateModel.IsServiceDate = true;
                }
                else
                {
                    string strValidate = ValidDate();
                    if (string.IsNullOrEmpty(strValidate))
                    {
                        linearLayoutServiceDateEdit.Visibility = ViewStates.Gone;
                        imageViewEditServiceDate.Selected = false;
                    }
                    else
                    {
                        AlertMessage(strValidate);
                    }
                }
                EditData();
            };
        }

        /// <summary>
        /// Method Name     : RelativeLayoutAddressesClick
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Use for edit address date
        /// Revision        : 
        /// </summary>
        private void RelativeLayoutAddressesClick()
        {
            imageViewEditAddresses.Click += delegate
            {
                if (linearLayoutAddressesEdit.Visibility == ViewStates.Gone)
                {
                    linearLayoutAddressesEdit.Visibility = ViewStates.Visible;
                    imageViewEditAddresses.Selected = true;
                    imageViewAddresses.SetImageResource(Resource.Drawable.checked_yellow);
                    estimateModel.IsAddressEdited = true;
                }
                else
                {
                    string strValidate = ValidationAddress();
                    if (string.IsNullOrEmpty(strValidate))
                    {
                        linearLayoutAddressesEdit.Visibility = ViewStates.Gone;
                        imageViewEditAddresses.Selected = false;
                    }
                    else
                    {
                        AlertMessage(strValidate);
                    }
                }
            };
        }

        /// <summary>
        /// Method Name     : RelativeLayoutWhatMattersMostClick
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Use for edit what matter most date
        /// Revision        : 
        /// </summary>
        private void RelativeLayoutWhatMattersMostClick()
        {
            imageViewEditWhatMattersMost.Click += delegate
            {
                if (relativeLayoutWhatMattersMostEdit.Visibility == ViewStates.Gone)
                {
                    relativeLayoutWhatMattersMostEdit.Visibility = ViewStates.Visible;
                    imageViewEditWhatMattersMost.Selected = true;
                    imageViewWhatMattersMost.SetImageResource(Resource.Drawable.checked_yellow);
                    estimateModel.IsWhatMatterMostEdited = true;
                }
                else
                {
                    string strValidate = ValidationWMM();
                    if (string.IsNullOrEmpty(strValidate))
                    {
                        relativeLayoutWhatMattersMostEdit.Visibility = ViewStates.Gone;
                        imageViewEditWhatMattersMost.Selected = false;
                    }
                    else
                    {
                        AlertMessage(strValidate);
                    }
                }
            };
        }

        /// <summary>
        /// Method Name     : RelativeLayoutValuetion
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Use for edit valution
        /// Revision        : 
        /// </summary>
        private void RelativeLayoutValuetion()
        {
            imageViewEditValution.Click += delegate
            {
                if (relativeLayoutValutionEdit.Visibility == ViewStates.Gone)
                {
                    relativeLayoutValutionEdit.Visibility = ViewStates.Visible;
                    imageViewEditValution.Selected = true;
                    imageViewValuation.SetImageResource(Resource.Drawable.checked_yellow);
                    estimateModel.IsValuationEdited = true;
                }
                else
                {
                    string strValidate = ValidationValution();
                    if (string.IsNullOrEmpty(strValidate))
                    {
                        relativeLayoutValutionEdit.Visibility = ViewStates.Gone;
                        imageViewEditValution.Selected = false;
                    }
                    else
                    {
                        AlertMessage(strValidate);
                    }
                }
            };
        }

        /// <summary>
        /// Method Name     : TextViewDateEditPackClick
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : open calendar
        /// Revision        : 
        /// </summary>
        private void TextViewDateEditPackClick()
        {
            textViewDateEditPack.Click += delegate
            {
                DateTime dateTime;
                if (!string.IsNullOrEmpty(textViewDateEditPack.Text))
                {
                    dateTime = Convert.ToDateTime(textViewDateEditPack.Text);
                }
                else
                {
                    dateTime = System.DateTime.Today;
                }
                dateDialogPack = new DatePickerDialog(Activity, AlertDialog.ThemeDeviceDefaultLight, OnPackDateSelected, dateTime.Year, dateTime.Month - 1, dateTime.Day);
                if (!dateDialogPack.IsShowing)
                    dateDialogPack.Show();
            };
        }

        /// <summary>
        /// Method Name     : TextViewDateEditLoadClick
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : open calendar
        /// Revision        : 
        /// </summary>
        private void TextViewDateEditLoadClick()
        {
            textViewDateEditLoad.Click += delegate
            {
                DateTime dateTime;
                if (!string.IsNullOrEmpty(textViewDateEditLoad.Text))
                {
                    dateTime = Convert.ToDateTime(textViewDateEditLoad.Text);
                }
                else
                {
                    dateTime = System.DateTime.Today;
                }
                dateDialogLoad = new DatePickerDialog(Activity, AlertDialog.ThemeDeviceDefaultLight, OnLoadDateSelected, dateTime.Year, dateTime.Month - 1, dateTime.Day);
                if (!dateDialogLoad.IsShowing)
                    dateDialogLoad.Show();

            };
        }

        /// <summary>
        /// Method Name     : TextViewDateEditMove
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : open calendar
        /// Revision        : 
        /// </summary>
        private void TextViewDateEditMove()
        {
            textViewDateEditMove.Click += delegate
            {
                DateTime dateTime;
                if (!string.IsNullOrEmpty(textViewDateEditMove.Text))
                {
                    dateTime = Convert.ToDateTime(textViewDateEditMove.Text);
                }
                else
                {
                    dateTime = System.DateTime.Today;
                }
                dateDialogMove = new DatePickerDialog(Activity, AlertDialog.ThemeDeviceDefaultLight, OnMoveDateSelected, dateTime.Year, dateTime.Month - 1, dateTime.Day);
                if (!dateDialogMove.IsShowing)
                    dateDialogMove.Show();
            };
        }

        /// <summary>
        /// Event Name      : OnPackDateSelected
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Use for Select Date
        /// Revision        : 
        /// </summary>
        private void OnPackDateSelected(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            var monthname = String.Format("{0:MM}", new DateTime(e.Year, e.Month + 1, e.DayOfMonth)).Clone();
            var date = String.Format("{0:dd}", new DateTime(e.Year, e.Month + 1, e.DayOfMonth)).ToUpper();
            textViewDateEditPack.Text = monthname + "/" + date + "/" + e.Year;
        }

        /// <summary>
        /// Event Name      : OnLoadDateSelected
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Use for Select Date
        /// Revision        : 
        /// </summary>
        void OnLoadDateSelected(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            var monthname = String.Format("{0:MM}", new DateTime(e.Year, e.Month + 1, e.DayOfMonth)).Clone();
            var date = String.Format("{0:dd}", new DateTime(e.Year, e.Month + 1, e.DayOfMonth)).ToUpper();
            textViewDateEditLoad.Text = monthname + "/" + date + "/" + e.Year;
        }

        /// <summary>
        /// Event Name      : OnLoadDateSelected
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Use for Select Date
        /// Revision        : 
        /// </summary>
        void OnMoveDateSelected(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            var monthname = String.Format("{0:MM}", new DateTime(e.Year, e.Month + 1, e.DayOfMonth)).Clone();
            var date = String.Format("{0:dd}", new DateTime(e.Year, e.Month + 1, e.DayOfMonth)).ToUpper();
            textViewDateEditMove.Text = monthname + "/" + date + "/" + e.Year;
        }

        /// <summary>
        /// Event Name      : LinearLayoutViewEstimate_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Use for Open pdf 
        /// Revision        : 
        /// </summary>
        private void LinearLayoutViewEstimate_Click(object sender, EventArgs e)
        {
            StartActivity(new Intent(Activity, typeof(PdfActivity)));
        }

        /// <summary>
        /// Event Name      : textViewBack_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Use for move back fragment
        /// Revision        : 
        /// </summary>
        private void textViewBack_Click(object sender, EventArgs e)
        {
            EditData();
            ((ActivityEstimateViewPager)Activity).FragmentBack();
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
                var categoryAdapter = new ArrayAdapter<string>(Activity, Resource.Layout.SpinnerItems, valutionList);
                categoryAdapter.SetDropDownViewResource(Resource.Layout.SpinnerItems);
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
            string strValue = string.Empty;
            strValue = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            valuationDeductible = UtilityPCL.ValuationDeductibleList().FirstOrDefault(rc => rc.DeductibleName == strValue);
        }

        /// <summary>
        /// Method Name      : AlertMessage
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Use for move back fragment
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
        /// Method Name     : SetImageViewUIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Find all image view control
        /// Revision        : 
        /// </summary>
        private void SetImageViewUIReference()
        {
            imageViewServiceDate = view.FindViewById<ImageView>(Resource.Id.imageViewServiceDate);
            imageViewAddresses = view.FindViewById<ImageView>(Resource.Id.imageViewAddresses);
            imageViewWhatMattersMost = view.FindViewById<ImageView>(Resource.Id.imageViewWhatMattersMost);
            imageViewValuation = view.FindViewById<ImageView>(Resource.Id.imageViewValuation);
            imageViewEditServiceDate = view.FindViewById<ImageView>(Resource.Id.imageViewEditServiceDate);
            imageViewEditAddresses = view.FindViewById<ImageView>(Resource.Id.imageViewEditAddresses);
            imageViewEditWhatMattersMost = view.FindViewById<ImageView>(Resource.Id.imageViewEditWhatMattersMost);
            imageViewEditValution = view.FindViewById<ImageView>(Resource.Id.imageViewEditValution);
        }

        /// <summary>
        /// Method Name     : SetTextViewUIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Find all textview control
        /// Revision        : 
        /// </summary>
        private void SetTextViewUIReference()
        {
            textViewHeading = view.FindViewById<TextView>(Resource.Id.textViewHeading);
            textViewServiceDate = view.FindViewById<TextView>(Resource.Id.textViewServiceDate);
            textViewPackDate = view.FindViewById<TextView>(Resource.Id.textViewPackDate);
            textViewDateEditPack = view.FindViewById<TextView>(Resource.Id.textViewDateEditPack);
            textViewLoadDate = view.FindViewById<TextView>(Resource.Id.textViewLoadDate);
            textViewDateEditLoad = view.FindViewById<TextView>(Resource.Id.textViewDateEditLoad);
            textViewMoveDate = view.FindViewById<TextView>(Resource.Id.textViewMoveDate);
            textViewDateEditMove = view.FindViewById<TextView>(Resource.Id.textViewDateEditMove);
            textViewSubmitChanges = view.FindViewById<TextView>(Resource.Id.textViewSubmitChanges);
            textViewServices = view.FindViewById<TextView>(Resource.Id.textViewServices);
            textViewVitalInformation = view.FindViewById<TextView>(Resource.Id.textViewVitalInformation);
            textViewAddresses = view.FindViewById<TextView>(Resource.Id.textViewAddresses);
            textViewOrigin = view.FindViewById<TextView>(Resource.Id.textViewOrigin);
            textViewDestination = view.FindViewById<TextView>(Resource.Id.textViewDestination);
            textViewUpdatesAddresses = view.FindViewById<TextView>(Resource.Id.textViewUpdatesAddresses);
            textViewValuation = view.FindViewById<TextView>(Resource.Id.textViewValuation);
            textViewWhatMattersMost = view.FindViewById<TextView>(Resource.Id.textViewWhatMattersMost);
            textViewSubmitChange = view.FindViewById<TextView>(Resource.Id.textViewSubmitChanges);
            textViewCheckBox = view.FindViewById<TextView>(Resource.Id.textViewCheckBox);
            textViewViewEstimate = view.FindViewById<TextView>(Resource.Id.textViewViewEstimate);
            tvEDitDeclaredvalue = view.FindViewById<TextView>(Resource.Id.tvEditDeclaredvalue);
            tvEditCoverage = view.FindViewById<TextView>(Resource.Id.tvEditCoverage);
            tvEditCost = view.FindViewById<TextView>(Resource.Id.tvEditCost);
        }

        /// <summary>
        /// Method Name     : SetLinearLayoutUIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Find all linerlayout control
        /// Revision        : 
        /// </summary>
        private void SetLinearLayoutUIReference()
        {
            linearLayoutServiceDateEdit = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutServiceDateEdit);
            linearLayoutAddressesEdit = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutAddressesEdit);
            linearLayoutViewEstimate = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutViewEstimate);
            linearLayoutEdit = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutEdit);
        }

        /// <summary>
        /// Method Name     : SetRelativeLayoutUIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Find all relativelayout control
        /// Revision        : 
        /// </summary>
        private void SetRelativeLayoutUIReference()
        {
            relativeLayoutServiceDate = view.FindViewById<RelativeLayout>(Resource.Id.relativeLayoutServiceDate);
            relativeLayoutAddresses = view.FindViewById<RelativeLayout>(Resource.Id.relativeLayoutAddresses);
            relativeLayoutWhatMattersMost = view.FindViewById<RelativeLayout>(Resource.Id.relativeLayoutWhatMattersMost);
            relativeLayoutWhatMattersMostEdit = view.FindViewById<RelativeLayout>(Resource.Id.relativeLayoutWhatMattersMostEdit);
            relativeLayoutValutionEdit = view.FindViewById<RelativeLayout>(Resource.Id.relativeLayoutValutionEdit);
        }

        /// <summary>
        /// Method Name     : SetButtonUIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Find all button control
        /// Revision        : 
        /// </summary>
        private void SetButtonUIReference()
        {
            btnBack = view.FindViewById<ImageButton>(Resource.Id.btnBack);
            textViewBack = view.FindViewById<LinearLayout>(Resource.Id.textViewBack);
            textViewNext = view.FindViewById<TextView>(Resource.Id.textViewNext);
            agreeCheckBox = view.FindViewById<CheckBox>(Resource.Id.agreeCheckBox);
            tvback = view.FindViewById<TextView>(Resource.Id.tvback);
        }

        /// <summary>
        /// Method Name     : SetButtonUIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Find all edittext control
        /// Revision        : 
        /// </summary>
        private void SetEditTextUIReference()
        {
            editTextAddressOrigin = view.FindViewById<EditText>(Resource.Id.editTextAddressOrigin);
            editTextAddressDestination = view.FindViewById<EditText>(Resource.Id.editTextAddressDestination);
            editTextWhatMattersMost = view.FindViewById<EditText>(Resource.Id.editTextWhatMattersMost);
            txtDeclaredvalue = view.FindViewById<EditText>(Resource.Id.txtDeclaredvalue);
            txtCost = view.FindViewById<EditText>(Resource.Id.txtCost);
            spinnerCoverage = view.FindViewById<Spinner>(Resource.Id.spinnerCoverage);
        }

        /// <summary>
        /// Method Name     : SetUIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 4 jan 2017
        /// Purpose         : Set UI Reference 
        /// Revision        : 
        /// </summary>
        private void SetUIReference()
        {
            SetImageViewUIReference();
            SetTextViewUIReference();
            SetLinearLayoutUIReference();
            SetRelativeLayoutUIReference();
            SetButtonUIReference();
            SetEditTextUIReference();
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
            UIHelper.SetTextViewFont(textViewServiceDate, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewPackDate, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewDateEditPack, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewLoadDate, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewDateEditLoad, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewMoveDate, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewDateEditMove, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewSubmitChanges, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewServices, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewVitalInformation, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewAddresses, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewOrigin, (int)UIHelper.LinotteFont.LinotteRegular, Activity.Assets);
            UIHelper.SetTextViewFont(textViewDestination, (int)UIHelper.LinotteFont.LinotteRegular, Activity.Assets);
            UIHelper.SetTextViewFont(textViewUpdatesAddresses, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewValuation, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewWhatMattersMost, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetEditTextFont(editTextAddressOrigin, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetEditTextFont(editTextAddressDestination, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetEditTextFont(editTextWhatMattersMost, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewSubmitChange, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewCheckBox, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewViewEstimate, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvback, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewNext, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
        }

        /// <summary>
        /// Method Name     : PopulateData
        /// Author          : Sanket Prajapati
        /// Creation Date   : 4 jan 2017
        /// Purpose         : Binding Data from modal to layout control
        /// Revision        : 
        /// </summary>
        public void PopulateData()
        {
            if (estimateModel != null && string.IsNullOrEmpty(estimateModel.message))
            {
                PopulateEstimateData();
                PopulateValuationDeductible();
                PopulateEstimateImageResource();
            }
        }

        /// <summary>
        /// Method Name     : SetEstimateData
        /// Author          : Ranjana Singh
        /// Creation Date   : 08 Feb 2018
        /// Purpose         : Set Estimate Data.
        /// Revision        : 
        /// </summary>
        private void PopulateEstimateData()
        {
            textViewDateEditPack.Text = estimateModel.PackStartDate;
            textViewDateEditLoad.Text = estimateModel.LoadStartDate;
            textViewDateEditMove.Text = estimateModel.MoveStartDate;

            editTextAddressOrigin.Text = estimateModel.CustomOriginAddress;
            editTextAddressDestination.Text = estimateModel.CustomDestinationAddress;

            editTextWhatMattersMost.Text = estimateModel.WhatMattersMost;

            txtDeclaredvalue.Text = UtilityPCL.RemoveCurrencyFormat(estimateModel.ExcessValuation);
            txtCost.Text = UtilityPCL.RemoveCurrencyFormat(estimateModel.ValuationCost);
            txtCost.LongClickable = false;
        }

        /// <summary>
        /// Method Name     : SetValuationDeductible
        /// Author          : Ranjana Singh
        /// Creation Date   : 08 Feb 2018
        /// Purpose         : Set Estimate Data.
        /// Revision        : 
        /// </summary>
        private void PopulateValuationDeductible()
        {
            valuationDeductible = new ValuationDeductibleModel();
            var valuatioList = UtilityPCL.ValuationDeductibleList();
            if (!string.IsNullOrEmpty(estimateModel.ValuationDeductible))
            {
                valuationDeductible = valuatioList.Find(rc => rc.DeductibleName == estimateModel.ValuationDeductible
                                                        || rc.DeductibleCode == estimateModel.ValuationDeductible);
                if (valuationDeductible != null)
                {
                    spinnerCoverage.SetSelection(valuationDeductible.Index);
                }
            }
            else
            {
                valuationDeductible = valuatioList.FirstOrDefault(rc => rc.Index == 1);
                if (valuationDeductible != null)
                {
                    spinnerCoverage.SetSelection(1);
                }
            }
        }

        private void PopulateEstimateImageResource()
        {
            if (estimateModel.IsAddressEdited) { imageViewAddresses.SetImageResource(Resource.Drawable.checked_yellow); }
            if (estimateModel.IsWhatMatterMostEdited) { imageViewWhatMattersMost.SetImageResource(Resource.Drawable.checked_yellow); }
            if (estimateModel.IsServiceDate) { imageViewServiceDate.SetImageResource(Resource.Drawable.checked_yellow); }
            if (estimateModel.IsValuationEdited) { imageViewValuation.SetImageResource(Resource.Drawable.checked_yellow); }
        }

        /// <summary>
        /// Method Name     : EditData
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Fill Estimate Data  
        /// Revision        : 
        /// </summary>
        public void EditData()
        {
            if (estimateModel != null && string.IsNullOrEmpty(estimateModel.message))
            {
                int index;
                estimateModel.PackStartDate = textViewDateEditPack.Text;
                estimateModel.LoadStartDate = textViewDateEditLoad.Text;
                estimateModel.MoveStartDate = textViewDateEditMove.Text;

                estimateModel.CustomOriginAddress = editTextAddressOrigin.Text;
                estimateModel.CustomDestinationAddress = editTextAddressDestination.Text;

                estimateModel.WhatMattersMost = editTextWhatMattersMost.Text.Trim();

                estimateModel.ExcessValuation = txtDeclaredvalue.Text;
                estimateModel.ValuationCost = txtCost.Text;
                if (valuationDeductible != null) { estimateModel.ValuationDeductible = valuationDeductible.DeductibleCode; }

                index = DTOConsumer.dtoEstimateData.IndexOf(estimateModel);
                DTOConsumer.dtoEstimateData[index] = estimateModel;
            }
        }

        /// <summary>
        /// Method Name     : ValidateServiceDates
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for date validation  
        /// Revision        : 
        /// </summary>
        public string ValidateServiceDates()
        {
            string errorMessage = string.Empty;
            if (string.IsNullOrEmpty(textViewDateEditPack.Text.Trim()))
            {
                errorMessage = StringResource.msgPackDateIsRequire;
            }
            else if (string.IsNullOrEmpty(textViewDateEditLoad.Text.Trim()))
            {
                errorMessage = StringResource.msgLoadDateIsRequire;
            }
            else if (string.IsNullOrEmpty(textViewDateEditMove.Text.Trim()))
            {
                errorMessage = StringResource.msgMoveDateIsRequire;
            }
            else
            {
                errorMessage = ValidDate();
            }
            return errorMessage;
        }

        /// <summary>
        /// Method Name     : ValidDate
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for date validation  
        /// Revision        : 
        /// </summary>
        public string ValidDate()
        {
            string errorMessage = string.Empty;
            DateTime packDate = GetValidDate(textViewDateEditPack.Text);
            DateTime loadDate = GetValidDate(textViewDateEditLoad.Text);
            DateTime moveDate = GetValidDate(textViewDateEditMove.Text);

            if (packDate == DateTime.MinValue || loadDate == DateTime.MinValue || moveDate == DateTime.MinValue)
            {
                return StringResource.msgEditDateValidation;
            }
            else if (packDate <= DateTime.Today || loadDate <= DateTime.Today || moveDate <= DateTime.Today)
            {
                return StringResource.msgValidDates;
            }

            if (packDate > loadDate)
            {
                errorMessage = StringResource.msgPackdatemustbelessthanloaddate;
            }
            else if (packDate > moveDate)
            {
                errorMessage = StringResource.msgPackdatemustbelessthanmovedate;
            }
            else if (loadDate > moveDate)
            {
                errorMessage = StringResource.msgPleaseSelectLoadDateGreaterThanorEqualMoveDate;
            }
            return errorMessage;
        }

        /// <summary>
        /// Method Name     : GetValidDate
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 14 Feb 2018
        /// Purpose         : validate date and convert to datetime
        /// Revision        : 
        /// </summary>
        /// <param name="dateValue"></param>
        /// <returns></returns>
        private DateTime GetValidDate(string dateValue)
        {
            if (string.IsNullOrEmpty(dateValue))
            {
                return DateTime.MinValue;
            }
            else
            {
                return Convert.ToDateTime(dateValue);
            }
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
            if (string.IsNullOrEmpty(editTextAddressOrigin.Text.Trim()))
            {
                errorMessage = StringResource.msgOriginAddressRequired;
            }
            else if (string.IsNullOrEmpty(editTextAddressDestination.Text.Trim()))
            {
                errorMessage = StringResource.msgDestinationAddressRequired;
            }
            return errorMessage;
        }

        /// Method Name     : ValidationValution
        /// Author          : Sanket Prajapati
        /// Creation Date   : 29 jan 2018
        /// Purpose         : Use for valuation Validation
        /// Revision        : 
        /// </summary>
        public string ValidationValution()
        {
            string errorMessage = string.Empty;
            if (string.IsNullOrEmpty(txtDeclaredvalue.Text.Trim()))
            {
                errorMessage = StringResource.msgPleaseEnterDeclaredValue;
            }
            else if (string.IsNullOrEmpty(txtCost.Text.Trim()))
            {
                errorMessage = StringResource.msgPleaseEnetrCostValue;
            }
            return errorMessage;
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
            if (string.IsNullOrEmpty(editTextWhatMattersMost.Text.Trim()))
            {
                errorMessage = StringResource.msgWMMRequired;
            }
            return errorMessage;
        }
    }
}