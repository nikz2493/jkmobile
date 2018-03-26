using System;
using Foundation;
using UIKit;
using JKMIOSApp.Common;
using JKMPCL.Services;
using JKMPCL.Services.Estimate;
using JKMPCL.Common;
using JKMPCL.Model;
using System.Linq;

namespace JKMIOSApp
{
    /// <summary>
    /// Controller Name : ValuationViewController
    /// Author          : Hiren Patel
    /// Creation Date   : 16 JAN 2018
    /// Purpose         : To display valuation page screen as estimate wizard step
    /// Revision        : 
    /// </summary>
    public partial class ValuationViewController : UIViewController
    {
        private EstimateModel estimateModel;
        private CoveragePickerDataModel coveragePickerDataModel;
        private readonly EstimateValidateServices estimateValidateServices;

        public ValuationViewController(IntPtr handle) : base(handle)
        {
            estimateValidateServices = new EstimateValidateServices();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitilizeIntarface();
            UIHelper.DismissKeyboardOnBackgroundTap(this);

            uiScrollViewPickerContainer.Hidden = true;
            PopulateData();
            BindCoverageValue();
            SetEditableTextControl(false);
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
            txtCost.Text = string.Empty;
            txtDeclaredValue.Text = string.Empty;
            txtCoverageValue.Text = string.Empty;
            txtCoverageValue.UserInteractionEnabled = false;

            imgSubmitCheck.Hidden = true;
            UIHelper.SetDefaultWizardScrollViewBorderProperty(scrollviewValuation);
            UIHelper.SetDefaultWizardScrollViewBorderProperty(uiScrollViewPickerContainer);
            UIHelper.SetDefaultEstimateButtonProperty(btnViewEstimation);

            btnBack.TouchUpInside += BtnBack_TouchUpInside;
            btnYesCapturedCorrectly.TouchUpInside += BtnYesCapturedCorrectly_TouchUpInside;
            btnUpdatesNeed.TouchUpInside += BtnUpdatesNeed_TouchUpInside;
            btnViewEstimation.TouchUpInside += BtnViewEstimate_TouchUpInside;

            btnOK.TouchUpInside += BtnOK_TouchUpInside;
            btnCancel.TouchUpInside += BtnCancel_TouchUpInside;
        }

        public void BindCoverageValue()
        {
            // create our simple picker model
            coveragePickerDataModel = new CoveragePickerDataModel();

            foreach (string item in UtilityPCL.ValuationDeductibleBindingList())
            {
                coveragePickerDataModel.Items.Add(item);
            }
            // set it on our picker class
            uiPickerViewCoverage.Model = coveragePickerDataModel;
        }

        /// <summary>
        /// Event Name      : BtnOK_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To set coverage picker value to coverage label
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnOK_TouchUpInside(object sender, EventArgs e)
        {
            txtCoverageValue.Text = coveragePickerDataModel.SelectedItem;
            uiScrollViewPickerContainer.Hidden = true;
        }

        /// <summary>
        /// Event Name      : BtnCancel_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To set coverage picker value to coverage label
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnCancel_TouchUpInside(object sender, EventArgs e)
        {
            uiScrollViewPickerContainer.Hidden = true;
        }

        /// <summary>
        /// Event Name      : BtnBack_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To redirec to back screen as what matters most
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnBack_TouchUpInside(object sender, EventArgs e)
        {
            PerformSegue("valuationTowhatMattersMost", this);
        }

        /// <summary>
        /// Event Name      : BtnUpdatesNeed_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To update and change entered services dates
        /// Revision        : By [Ranjana Singh] 08 Feb 2018 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnUpdatesNeed_TouchUpInside(object sender, EventArgs e)
        {
            if (!txtCoverageValue.UserInteractionEnabled)//- Removed condition as per Sonar bug.
            {
                ResetControlEditMode();
            }
            else
            {
                string message = estimateValidateServices.ValidateValuationData(txtDeclaredValue.Text, txtCoverageValue.Text, txtCost.Text);
                if (string.IsNullOrEmpty(message))
                {
                    txtDeclaredValue.Text = UtilityPCL.CurrencyFormat(txtDeclaredValue.Text);
                    txtCost.Text = UtilityPCL.CurrencyFormat(txtCost.Text);

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
        /// Event Name      : BtnYesCapturedCorrectly_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To confirmed valuation data and redirec to vital information screen.
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnYesCapturedCorrectly_TouchUpInside(object sender, EventArgs e)
        {
                PerformSegue("valuationToVItalInformation", this);
        }

        /// <summary>
        /// Event Name      : BtnViewEstimate_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To view or download estimate pdf file
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnViewEstimate_TouchUpInside(object sender, EventArgs e)
        {
            UIHelper.CallingScreen = JKMEnum.UIViewControllerID.ValuationView;
            UIHelper.RedirectToViewController(this, JKMEnum.UIViewControllerID.ViewEstimateReviewView);
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
            txtCoverageValue.UserInteractionEnabled = false;
            btnUpdatesNeed.SetTitle(AppConstant.UPDATE_NEED_BUTTON_LABEL, UIControlState.Normal);
            UIHelper.SetUpdateNeedButtonProperty(btnUpdatesNeed);
            SetEditableTextControl(false);
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
            SetTapEventToCoverageLabel();
            imgSubmitCheck.Hidden = false;

            txtCoverageValue.UserInteractionEnabled = true;
            UIHelper.SetSubmitButtonProperty(btnUpdatesNeed);
            btnUpdatesNeed.SetTitle(AppConstant.SUBMIT_CHANGES_BUTTON_LABEL, UIControlState.Normal);
            SetEditableTextControl(true);

            if (!string.IsNullOrEmpty(txtDeclaredValue.Text))
            {
                txtDeclaredValue.Text = UtilityPCL.RemoveCurrencyFormat(txtDeclaredValue.Text);
            }

            if (!string.IsNullOrEmpty(txtCost.Text))
            {
                txtCost.Text = UtilityPCL.RemoveCurrencyFormat(txtCost.Text);
            }
        }

        /// <summary>
        /// Method Name     : SetEditableDateControl
        /// Author          : Hiren Patel
        /// Creation Date   : 30 JAN 2018
        /// Purpose         : Sets the editable date control. 
        /// Revision        : 
        /// </summary>
        /// <param name="isEditable">If set to <c>true</c> is editable.</param>
        public void SetEditableTextControl(bool isEditable)
        {
            if (isEditable)
            {
                // need to make enter only decimal value in textbox
            }
            else
            {
                txtDeclaredValue.ShouldChangeCharacters = null;
                txtCost.ShouldChangeCharacters = null;
            }

            txtDeclaredValue.UserInteractionEnabled = isEditable;
            txtCost.UserInteractionEnabled = false;

            UITextBorderStyle borderStyle = isEditable ? UITextBorderStyle.RoundedRect : UITextBorderStyle.None;
            txtCost.BorderStyle = borderStyle;
            txtDeclaredValue.BorderStyle = borderStyle;
            txtCoverageValue.BorderStyle = borderStyle;

        }


        /// <summary>
        /// Method Name     : SetTapEventToMoveDateTextBox
        /// Author          : Hiren Patel
        /// Creation Date   : 31 Jan 2017
        /// Purpose         : Sets the tap event to move date text box.
        /// Revision        : 
        /// </summary>
        private void SetTapEventToCoverageLabel()
        {
            txtCoverageValue.UserInteractionEnabled = true;
            UITapGestureRecognizer txtCoverageValueTap = new UITapGestureRecognizer(() =>
            {
                uiScrollViewPickerContainer.Hidden = false;
                uiScrollViewPickerContainer.Frame = scrollviewValuation.Frame;

                if (!string.IsNullOrEmpty(txtCoverageValue.Text))
                {
                    int counter = 0;
                    int selectedIndex = 0;
                    foreach (string item in coveragePickerDataModel.Items)
                    {
                        counter++;
                        if (!string.IsNullOrEmpty(txtCoverageValue.Text) && txtCoverageValue.Text == item)
                        {
                            selectedIndex = counter;
                        }
                    }
                    // set our initial selection on the label
                    coveragePickerDataModel.SetSelectedIndex(selectedIndex);
                }
            });
            txtCoverageValue.AddGestureRecognizer(txtCoverageValueTap);
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
            estimateModel = DTOConsumer.GetSelectedEstimate();
            if (estimateModel != null)
            {
                txtCost.Text = estimateModel.ValuationCost;
                txtCoverageValue.Text = estimateModel.ValuationDeductible;
                txtDeclaredValue.Text = estimateModel.ExcessValuation;
            }
            UIHelper.CreateWizardHeader(6, viewHeader, estimateModel);
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
                    estimateModel.ExcessValuation = txtDeclaredValue.Text;
                    estimateModel.ValuationDeductible = txtCoverageValue.Text;
                    estimateModel.ValuationCost = txtCost.Text;
                    estimateModel.IsValuationEdited = true;
                }
            }
        }
    }


}
