using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.activity;
using Java.Util;
using JKMAndroidApp.Common;
using JKMPCL.Model;

namespace JKMAndroidApp.fragment
{
    /// <summary>
    /// Method Name     : FragmentServiceDates
    /// Author          : Sanket Prajapati
    /// Creation Date   : 23 Jan 2018
    /// Purpose         : Fragement for ServiceDates page
    /// Revision        : 
    /// </summary>
    public class FragmentServiceDates : Android.Support.V4.App.Fragment
    {
        private ViewSwitcher viewSwitcher;
        private View view;
        private TextView tvtitleDiscriptions, tvServiceDatetitle, tvEditServiceDatetitle, tvPackDate, tvLoadDate, tvMoveDate, tvDisplayPack, tvDisplayLoad, tvDisplayMove,
                            tvPackDateEdit, tvLoadDateEdit, tvMoveDateEdit, tvDateEditPack, tvDateEditLoad, tvDateEditMove, tvViewEstimate,
                                tvChangeDates, tvSubmitChanges, tvNext, tvback, textViewChangeDates;

        private LinearLayout lnViewBack, linearLayoutViewEstimate, linearLayoutDisplay, linearLayoutEdit;

        private DatePickerDialog dateDialogPack = null;
        private DatePickerDialog dateDialogLoad = null;
        private DatePickerDialog dateDialogMove = null;
        private EstimateModel estimateModel;
        private ImageButton btnBack;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.LayoutFragmentServiceDates, container, false);
            estimateModel = DTOConsumer.dtoEstimateData.FirstOrDefault(rc => rc.MoveNumber == UIHelper.SelectedMoveNumber);
            UIReferences();
            PopulateData();
            UIClickEvents();
            ApplyFont();

            return view;
        }

        public override void OnResume()
        {
            UIReferences();
            PopulateData();
            base.OnResume();
        }

        /// Method Name     : UIReferences
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Finds Control  
        /// Revision        : 
        /// </summary>
        private void UIReferences()
        {
            FindDispaltextviewAndEdittextview();
            FindViewSwicherAndEdittextviewandLinerlayot();
        }

        /// Method Name     : FindDispaltextviewAndEdittextview
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Finds Control  
        /// Revision        : 
        /// </summary>
        private void FindDispaltextviewAndEdittextview()
        {
            tvPackDate = view.FindViewById<TextView>(Resource.Id.tvPackDate);
            tvLoadDate = view.FindViewById<TextView>(Resource.Id.tvLoadDate);
            tvMoveDate = view.FindViewById<TextView>(Resource.Id.tvMoveDate);

            tvDisplayPack = view.FindViewById<TextView>(Resource.Id.textViewDateDisplayPack);
            tvDisplayLoad = view.FindViewById<TextView>(Resource.Id.textViewDateDisplayLoad);
            tvDisplayMove = view.FindViewById<TextView>(Resource.Id.textViewDateDisplayMove);

            tvPackDateEdit = view.FindViewById<TextView>(Resource.Id.tvPackDateEdit);
            tvLoadDateEdit = view.FindViewById<TextView>(Resource.Id.tvLoadDateEdit);
            tvMoveDateEdit = view.FindViewById<TextView>(Resource.Id.tvMoveDateEdit);

            tvDateEditPack = view.FindViewById<TextView>(Resource.Id.textViewDateEditPack);
            tvDateEditLoad = view.FindViewById<TextView>(Resource.Id.textViewDateEditLoad);
            tvDateEditMove = view.FindViewById<TextView>(Resource.Id.textViewDateEditMove);
        }

        /// Method Name     : FindViewSwicherAndEdittextviewandLinerlayot
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Finds Control  
        /// Revision        : 
        /// </summary>
        private void FindViewSwicherAndEdittextviewandLinerlayot()
        {
            viewSwitcher = view.FindViewById<ViewSwitcher>(Resource.Id.viewSwitcher);
            tvEditServiceDatetitle= view.FindViewById<TextView>(Resource.Id.tvEditServiceDatetitle);
            linearLayoutDisplay = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutDisplay);
            linearLayoutEdit = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutEdit);
            tvChangeDates = view.FindViewById<TextView>(Resource.Id.textViewChangeDates);
            tvSubmitChanges = view.FindViewById<TextView>(Resource.Id.textViewSubmitChanges);
            lnViewBack = view.FindViewById<LinearLayout>(Resource.Id.textViewBack);
            tvNext = view.FindViewById<TextView>(Resource.Id.textViewNext);
            tvtitleDiscriptions = view.FindViewById<TextView>(Resource.Id.tvtitleDiscriptions);
            tvServiceDatetitle = view.FindViewById<TextView>(Resource.Id.tvServiceDatetitle);
            tvViewEstimate = view.FindViewById<TextView>(Resource.Id.tvViewEstimate);
            textViewChangeDates = view.FindViewById<TextView>(Resource.Id.textViewChangeDates);
            tvback = view.FindViewById<TextView>(Resource.Id.tvback);
            btnBack = view.FindViewById<ImageButton>(Resource.Id.btnBack);
            linearLayoutViewEstimate = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutViewEstimate);
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
            UIHelper.SetTextViewFont(tvServiceDatetitle, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets); 
            UIHelper.SetTextViewFont(tvEditServiceDatetitle, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvPackDate, (int)UIHelper.LinotteFont.LinotteRegular, Activity.Assets);
            UIHelper.SetTextViewFont(tvLoadDate, (int)UIHelper.LinotteFont.LinotteRegular, Activity.Assets);
            UIHelper.SetTextViewFont(tvMoveDate, (int)UIHelper.LinotteFont.LinotteRegular, Activity.Assets);
            UIHelper.SetTextViewFont(tvDisplayPack, (int)UIHelper.LinotteFont.LinotteBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDisplayLoad, (int)UIHelper.LinotteFont.LinotteBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDisplayMove, (int)UIHelper.LinotteFont.LinotteBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvPackDateEdit, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvLoadDateEdit, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvMoveDateEdit, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDateEditPack, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDateEditLoad, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvDateEditMove, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvViewEstimate, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvback, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvNext, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvSubmitChanges, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(textViewChangeDates, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
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
            TextViewDateEditPackClick();
            TextViewDateEditLoadClick();
            TextViewDateEditMoveClick();
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
            tvChangeDates.Click += delegate
            {
                if (viewSwitcher.CurrentView == linearLayoutDisplay)
                {
                    viewSwitcher.ShowNext();

                    tvDateEditPack.Text = tvDisplayPack.Text;
                    tvDateEditLoad.Text = tvDisplayLoad.Text;
                    tvDateEditMove.Text = tvDisplayMove.Text;

                    tvback.Text = "Cancel";
                    tvNext.Text = "Next Step";
                }
            };

            tvSubmitChanges.Click += delegate
            {
                if (viewSwitcher.CurrentView == linearLayoutEdit)
                {
                    string strValidate = ValidEditDate(false);
                    if (string.IsNullOrEmpty(strValidate))
                    {
                        viewSwitcher.ShowPrevious();
                        tvback.Text = "Back";
                        tvNext.Text = "Dates Are Accurate";
                        tvDisplayPack.Text = tvDateEditPack.Text;
                        tvDisplayLoad.Text = tvDateEditLoad.Text;
                        tvDisplayMove.Text = tvDateEditMove.Text;
                        EditData();
                    }
                    else
                    {
                        AlertMessage(strValidate);
                    }
                   
                }
            };
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
                string strValidate = ValidEditDate();
                if (string.IsNullOrEmpty(strValidate))
                {
                    if (viewSwitcher.CurrentView == linearLayoutEdit)
                    {
                        viewSwitcher.ShowPrevious();
                    }
                    ((ActivityEstimateViewPager)Activity).FragmentNext();
                }
                else
                {
                    AlertMessage(strValidate);
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
            lnViewBack.Click += MTextViewBack_Click;
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
            tvDateEditPack.Click += delegate
            {
                DateTime dateTime;
                if (!string.IsNullOrEmpty(tvDateEditPack.Text))
                {
                    dateTime = Convert.ToDateTime(tvDateEditPack.Text);
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
            tvDateEditLoad.Click += delegate
            {
                DateTime dateTime;
                if (!string.IsNullOrEmpty(tvDateEditLoad.Text))
                {
                    dateTime = Convert.ToDateTime(tvDateEditLoad.Text);
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
        private void TextViewDateEditMoveClick()
        {
            tvDateEditMove.Click += delegate
            {
                DateTime dateTime;
                if (!string.IsNullOrEmpty(tvDateEditMove.Text))
                {
                    dateTime = Convert.ToDateTime(tvDateEditMove.Text);
                }
                else
                {
                    dateTime = System.DateTime.Today;
                }
                dateDialogMove = new DatePickerDialog(Activity, AlertDialog.ThemeDeviceDefaultLight, OnMoveDateSelected, dateTime.Year, dateTime.Month-1, dateTime.Day);

                if (!dateDialogMove.IsShowing)
                    dateDialogMove.Show();
            };
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
            tvback.Text = "Back";
            tvNext.Text = "Dates Are Accurate";

        }

        /// Event Name      : OnPackDateSelected
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for Select date from calendar
        /// Revision        : 
        /// </summary>
        void OnPackDateSelected(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            var monthname = String.Format("{0:MM}", new DateTime(e.Year, e.Month + 1, e.DayOfMonth)).Clone();
            var date = String.Format("{0:dd}", new DateTime(e.Year, e.Month + 1, e.DayOfMonth)).ToUpper();
            tvDateEditPack.Text = monthname + "/" + date + "/" + e.Year;
        }

        /// Event Name      : OnLoadDateSelected
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for Select date from calendar
        /// Revision        : 
        /// </summary>
        void OnLoadDateSelected(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            var monthname = String.Format("{0:MM}", new DateTime(e.Year, e.Month + 1, e.DayOfMonth)).Clone();
            var date = String.Format("{0:dd}", new DateTime(e.Year, e.Month + 1, e.DayOfMonth)).ToUpper();
            tvDateEditLoad.Text = monthname + "/" + date + "/" + e.Year;
        }

        /// Event Name      : OnMoveDateSelected
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for Select date from calendar
        /// Revision        : 
        /// </summary>
        void OnMoveDateSelected(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            var monthname = String.Format("{0:MM}", new DateTime(e.Year, e.Month + 1, e.DayOfMonth)).Clone();
            var date = String.Format("{0:dd}", new DateTime(e.Year, e.Month + 1, e.DayOfMonth)).ToUpper();
            tvDateEditMove.Text = monthname + "/" + date + "/" + e.Year;
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
            if (string.IsNullOrEmpty(tvDateEditPack.Text))
            {
                errorMessage =StringResource.msgPackDateIsRequire;
            }
            else if (string.IsNullOrEmpty(tvDateEditLoad.Text))
            {
                errorMessage = StringResource.msgLoadDateIsRequire;
            }
            else if (string.IsNullOrEmpty(tvDateEditMove.Text))
            {
                errorMessage = StringResource.msgMoveDateIsRequire;
            }
            else
            {
                errorMessage = ValidEditDate();
            }
            return errorMessage;
        }

        /// <summary>
        /// Method Name     : ValidEditDate
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for date validation  
        /// Revision        : 
        /// </summary>
        public string ValidEditDate(bool validateDisplayDate=true)
        {
            string errorMessage = string.Empty;
            DateTime packDate= GetValidDate(tvDateEditPack.Text); 
            DateTime loadDate= GetValidDate(tvDateEditLoad.Text);
            DateTime moveDate= GetValidDate(tvDateEditMove.Text);

            if (packDate == DateTime.MinValue || loadDate == DateTime.MinValue || moveDate == DateTime.MinValue)
            {
                return StringResource.msgEditDateValidation;
            }
            if (validateDisplayDate)
            {
                errorMessage = ValidDisplayDate();
            }

            if (viewSwitcher.CurrentView == linearLayoutEdit)
            {
                if (packDate <= DateTime.Today || loadDate <= DateTime.Today|| moveDate <= DateTime.Today)
                {
                    return StringResource.msgValidDates;
                }
                errorMessage = ValidateDates(packDate, loadDate, moveDate);
            }
            else
            {
                if (string.IsNullOrEmpty(errorMessage))
                {
                    errorMessage = ValidateDates(packDate, loadDate, moveDate);
                }
               
            }

            return errorMessage;
        }

        /// <summary>
        /// Method Name     : ValidateDates
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 14 Feb 2018
        /// Purpose         : validate dates
        /// Revision        :  
        /// </summary>
        /// <param name="packDate"></param>
        /// <param name="loadDate"></param>
        /// <param name="moveDate"></param>
        /// <returns></returns>
        private string ValidateDates(DateTime packDate, DateTime loadDate, DateTime moveDate)
        {
            if (packDate > loadDate)
            {
                return StringResource.msgPackdatemustbelessthanloaddate;
            }
            else if (packDate > moveDate)
            {
                return StringResource.msgPackdatemustbelessthanmovedate;
            }
            else if (loadDate > moveDate)
            {
                return StringResource.msgPleaseSelectLoadDateGreaterThanorEqualMoveDate;
            }
            return string.Empty;
        }

        /// <summary>
        /// Method Name     : ValidDisplayDate
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for date validation  
        /// Revision        : 
        /// </summary>
        public string ValidDisplayDate()
        {
            string errorMessage = string.Empty;
            DateTime packDate = GetValidDate(tvDisplayPack.Text);
            DateTime loadDate = GetValidDate(tvDisplayLoad.Text);
            DateTime moveDate = GetValidDate(tvDisplayMove.Text);

            if (packDate == DateTime.MinValue || loadDate == DateTime.MinValue || moveDate == DateTime.MinValue)
            {
                return StringResource.msgDisplayDateValidation;
            }
            else if (packDate <= DateTime.Today || loadDate <= DateTime.Today || moveDate <= DateTime.Today)
            {
                return StringResource.msgValidDates;
            }
            errorMessage = ValidateDates(packDate, loadDate, moveDate);

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
                tvDisplayPack.Text = estimateModel.PackStartDate;
                tvDisplayLoad.Text = estimateModel.LoadStartDate;
                tvDisplayMove.Text = estimateModel.MoveStartDate;

                tvDateEditPack.Text = estimateModel.PackStartDate;
                tvDateEditLoad.Text = estimateModel.LoadStartDate;
                tvDateEditMove.Text = estimateModel.MoveStartDate;
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
                estimateModel.PackStartDate = tvDisplayPack.Text.Trim();
                estimateModel.LoadStartDate = tvDisplayLoad.Text.Trim();
                estimateModel.MoveStartDate = tvDisplayMove.Text.Trim();
                estimateModel.IsServiceDate = true;
                index = DTOConsumer.dtoEstimateData.IndexOf(estimateModel);
                DTOConsumer.dtoEstimateData[index] = estimateModel;
               
            }
        }
    }
}