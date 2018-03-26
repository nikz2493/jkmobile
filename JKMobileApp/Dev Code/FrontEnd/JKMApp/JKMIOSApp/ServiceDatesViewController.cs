using System;
using System.Linq;
using CoreGraphics;
using Foundation;
using JKMPCL.Common;
using JKMPCL.Model;
using JKMPCL.Services.Estimate;
using UIKit;

namespace JKMIOSApp
{
    /// <summary>
    /// Controller Name : ServiceDatesViewController
    /// Author          : Hiren Patel
    /// Creation Date   : 16 JAN 2018
    /// Purpose         : To display move confirmed page screen as estimate wizard step
    /// Revision        : 
    /// </summary>
	public partial class ServiceDatesViewController : UIViewController
    {
        private EstimateModel estimateModel;
        private nint currentTextBoxTag;
        private readonly EstimateValidateServices estimateValidateServices;
        private UIEdgeInsets ButtonBackImageEdgeInsets;
        public ServiceDatesViewController(IntPtr handle) : base(handle)
        {
            estimateValidateServices = new EstimateValidateServices();
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitilizeIntarface();
           
            imgSubmitCheck.Hidden = true;
            scrollViewDatePicker.Hidden = true;
            PopulateData();
            SetEditableDateControl(false);
            ButtonBackImageEdgeInsets = btnBack.ImageEdgeInsets;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        /// <summary>
        /// Method Name     : InitilizeIntarface
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To Initilizes the intarface.
        /// Revision        : 
        /// </summary>
        public void InitilizeIntarface()
        {
            UIHelper.SetDefaultWizardScrollViewBorderProperty(scrollViewDate);
            UIHelper.SetDefaultEstimateButtonProperty(btnViewEstimate);
            UIHelper.DismissKeyboardOnBackgroundTap(this);

            btnBack.TouchUpInside += BtnBack_TouchUpInside;
            btnOK.TouchUpInside += BtnOK_TouchUpInside;
            btnCancel.TouchUpInside += BtnCancel_TouchUpInside;
            btnDatesAreAccurate.TouchUpInside += BtnDatesAreAccurate_TouchUpInside;
            btnViewEstimate.TouchUpInside += BtnViewEstimate_TouchUpInside;
            btnChangeMyServiceDates.TouchUpInside += BtnChangeMyServiceDates_TouchUpInside;

            btnChangeMyServiceDates.Tag = 0;
            txtPackDate.Tag = 1;
            txtLoadDate.Tag = 2;
            txtMoveDate.Tag = 3;

            uiDatePicker.MinimumDate =Extensions.DateTimeToNSDate(DateTime.Today);
        }

        /// <summary>
        /// Method Name     : SetTapEventToPackDateTextBox
        /// Author          : Hiren Patel
        /// Creation Date   : 31 Jan 2017
        /// Purpose         : Sets the tap event to pack date text box.
        /// Revision        : 
        /// </summary>
        private void SetTapEventToPackDateTextBox()
        {
            UITapGestureRecognizer txtPackDateTap = new UITapGestureRecognizer(() =>
            {
                currentTextBoxTag = txtPackDate.Tag;
                scrollViewDatePicker.Hidden = false;
                scrollViewDatePicker.Frame = scrollViewDate.Frame;
                if (!string.IsNullOrEmpty(txtPackDate.Text))
                {
                    uiDatePicker.SetDate(Extensions.DateTimeToNSDate(Convert.ToDateTime(txtPackDate.Text)), true);
                }
            });
            txtPackDate.AddGestureRecognizer(txtPackDateTap);
        }

        /// <summary>
        /// Method Name     : SetTapEventToLoadDateTextBox
        /// Author          : Hiren Patel
        /// Creation Date   : 31 Jan 2017
        /// Purpose         : Sets the tap event to load date text box.
        /// Revision        : 
        /// </summary>
        private void SetTapEventToLoadDateTextBox()
        {
            UITapGestureRecognizer txtLoadDateTap = new UITapGestureRecognizer(() =>
            {
                currentTextBoxTag = txtLoadDate.Tag;
                scrollViewDatePicker.Hidden = false;
                scrollViewDatePicker.Frame = scrollViewDate.Frame;
                if (!string.IsNullOrEmpty(txtLoadDate.Text))
                {
                    uiDatePicker.SetDate(Extensions.DateTimeToNSDate(Convert.ToDateTime(txtLoadDate.Text)), true);
                }
            });
            txtLoadDate.AddGestureRecognizer(txtLoadDateTap);

        }

        /// <summary>
        /// Method Name     : SetTapEventToMoveDateTextBox
        /// Author          : Hiren Patel
        /// Creation Date   : 31 Jan 2017
        /// Purpose         : Sets the tap event to move date text box.
        /// Revision        : 
        /// </summary>
        private void SetTapEventToMoveDateTextBox()
        {
            UITapGestureRecognizer txtMoveDateTap = new UITapGestureRecognizer(() =>
            {
                currentTextBoxTag = txtMoveDate.Tag;
                scrollViewDatePicker.Hidden = false;
                scrollViewDatePicker.Frame = scrollViewDate.Frame;
                if (!string.IsNullOrEmpty(txtMoveDate.Text))
                {
                    uiDatePicker.SetDate(Extensions.DateTimeToNSDate(Convert.ToDateTime(txtMoveDate.Text)), true);
                }
            });

            txtMoveDate.AddGestureRecognizer(txtMoveDateTap);
        }

        /// <summary>
        /// Event Name      : BtnOK_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To redirec to back screen
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnOK_TouchUpInside(object sender, EventArgs e)
        {
            NSDate selectedDate = uiDatePicker.Date;
            scrollViewDatePicker.Hidden = true;
            string date = UIHelper.DisplayDateFormatForEstimate(selectedDate.NSDateToDateTime());
            switch (currentTextBoxTag)
            {
                case 1:
                    txtPackDate.Text = date;
                    break;
                case 2:
                    txtLoadDate.Text = date;
                    break;
                case 3:
                    txtMoveDate.Text = date;
                    break;
            }
        }

        /// <summary>
        /// Event Name      : BtnCancel_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To redirec to back screen
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnCancel_TouchUpInside(object sender, EventArgs e)
        {
            scrollViewDatePicker.Hidden = true;
        }

        /// <summary>
        /// Event Name      : BtnBack_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To redirec to back screen
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnBack_TouchUpInside(object sender, EventArgs e)
        {
            if (btnChangeMyServiceDates.Tag == 0)
            {
                PerformSegue("serviceDateToServices", this);
            }
            else
            {
                ResetControlReadOnlyMode();
                PopulateData();
            }
        }

        /// <summary>
        /// Event Name      : btnViewEstimatePressed
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To view or download estimate pdf file
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnViewEstimate_TouchUpInside(object sender, EventArgs e)
        {
            UIHelper.CallingScreen = JKMEnum.UIViewControllerID.ServiceDatesView;
            UIHelper.RedirectToViewController(this, JKMEnum.UIViewControllerID.ViewEstimateReviewView);
        }

        /// <summary>
        /// Event Name      : BtnDatesAreAccurate_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To confirmed services date and redirect to address screen
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnDatesAreAccurate_TouchUpInside(object sender, EventArgs e)
        {
            string message = estimateValidateServices.ValidateServiceDates(txtPackDate.Text, txtLoadDate.Text, txtMoveDate.Text);
            if (string.IsNullOrEmpty(message))
            {
                PerformSegue("servicesDatesToAddress", this);
            }
            else
            {
                UIHelper.ShowMessage(message);
            }
        }

        /// <summary>
        /// Event Name     : btnChangeMyServicesDatesPressed
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To update and change entered services dates
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnChangeMyServiceDates_TouchUpInside(object sender, EventArgs e)
        {
            if (btnChangeMyServiceDates.Tag == 0)
            {
                ResetControlEditMode();
            }
            else
            {
                string message = estimateValidateServices.ValidateServiceDates(txtPackDate.Text, txtLoadDate.Text, txtMoveDate.Text);
                if (string.IsNullOrEmpty(message))
                {
                    SaveDataToDTO();
                    ResetControlReadOnlyMode();
                }
                else
                {
                    UIHelper.ShowMessage(message);
                }

            }
        }

        /// <summary>
        /// Method Name     : ResetControlReadOnlyMode
        /// Author          : Hiren Patel
        /// Creation Date   : 31 Jan 2018
        /// Purpose         : Resets the control read only mode.
        /// Revision        : 
        /// </summary>
        private void ResetControlReadOnlyMode()
        {
            imgSubmitCheck.Hidden = true;
            btnChangeMyServiceDates.SetTitle(AppConstant.CHANGE_MY_SERVICE_DATE_BUTTON_LABEL, UIControlState.Normal);
            UIHelper.SetUpdateNeedButtonProperty(btnChangeMyServiceDates);
            SetEditableDateControl(false);
            btnChangeMyServiceDates.Tag = 0;
            btnDatesAreAccurate.SetTitle("Dates Are Accurate", UIControlState.Normal);
            btnBack.ImageEdgeInsets = ButtonBackImageEdgeInsets;
            btnBack.SetTitle("Back", UIControlState.Normal);
        }

        /// <summary>
        /// Method Name     : ResetControlEditMode
        /// Author          : Hiren Patel
        /// Creation Date   : 31 Jan 2018
        /// Purpose         : Resets the control edit mode.
        /// Revision        : 
        /// </summary>
        private void ResetControlEditMode()
        {
            btnChangeMyServiceDates.Tag = 1;
            btnChangeMyServiceDates.SetTitle(AppConstant.SUBMIT_CHANGES_BUTTON_LABEL, UIControlState.Normal);
            UIHelper.SetSubmitButtonProperty(btnChangeMyServiceDates);
            SetEditableDateControl(true);
            imgSubmitCheck.Hidden = false;
            SetTapEventToPackDateTextBox();
            SetTapEventToLoadDateTextBox();
            SetTapEventToMoveDateTextBox();
            btnDatesAreAccurate.SetTitle("Next Step", UIControlState.Normal);
            btnBack.ImageEdgeInsets = new UIEdgeInsets(btnBack.ImageEdgeInsets.Top, -1000, btnBack.ImageEdgeInsets.Bottom, btnBack.ImageEdgeInsets.Right);
            btnBack.SetTitle("Cancel", UIControlState.Normal);
        }


        /// <summary>
        /// Method Name     : SaveDatesToDTO
        /// Author          : Hiren Patel
        /// Creation Date   : 31 Jan 2017
        /// Purpose         : Save date to DTO 
        /// Revision        : 
        /// </summary>
        private void SaveDataToDTO()
        {
            if (DTOConsumer.dtoEstimateData != null)
            {
                estimateModel = DTOConsumer.GetSelectedEstimate();
                if (estimateModel != null)
                {
                    estimateModel.PackStartDate = txtPackDate.Text;
                    estimateModel.LoadStartDate = txtLoadDate.Text;
                    estimateModel.MoveStartDate = txtMoveDate.Text;
                    estimateModel.IsServiceDate = true;
                }
            }
        }

        /// <summary>
        /// Method Name     : PopulateData
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : fill Estimate Data  
        /// Revision        : 
        /// </summary>
        public void PopulateData()
        {
             
            if (DTOConsumer.dtoEstimateData != null)
            {
                estimateModel = DTOConsumer.GetSelectedEstimate();
                if (estimateModel != null)
                {
                    txtPackDate.Text = estimateModel.PackStartDate;
                    txtLoadDate.Text = estimateModel.LoadStartDate;
                    txtMoveDate.Text = estimateModel.MoveStartDate;
                    
                }
            }

            UIHelper.CreateWizardHeader(3, viewHeader, estimateModel);
        }

        /// <summary>
        /// Method Name     : SetEditableDateControl
        /// Author          : Hiren Patel
        /// Creation Date   : 30 JAN 2018
        /// Purpose         : Sets the editable date control. 
        /// Revision        : 
        /// </summary>
        /// <param name="isEditable">If set to <c>true</c> is editable.</param>
        public void SetEditableDateControl(bool isEditable)
        {
            txtPackDate.UserInteractionEnabled = isEditable;
            txtLoadDate.UserInteractionEnabled = isEditable;
            txtMoveDate.UserInteractionEnabled = isEditable;
            UITextBorderStyle borderStyle = isEditable ? UITextBorderStyle.RoundedRect : UITextBorderStyle.None;

            txtPackDate.BorderStyle = borderStyle;
            txtLoadDate.BorderStyle = borderStyle;
            txtMoveDate.BorderStyle = borderStyle;

        }
    }
}
