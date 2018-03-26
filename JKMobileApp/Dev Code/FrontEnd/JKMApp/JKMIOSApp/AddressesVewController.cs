// This file has been autogenerated from a class added in the UI designer.

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
    /// Controller Name : AddressesVewController
    /// Author          : Hiren Patel
    /// Creation Date   : 29 Dec 2017
    /// Purpose         : To display address page screen as estimate wizard step
    /// Revision        : 
    /// </summary>
	public partial class AddressesVewController : UIViewController
    {
        private EstimateModel estimateModel;
        private readonly EstimateValidateServices estimateValidateServices;

        public AddressesVewController(IntPtr handle) : base(handle)
        {
            estimateValidateServices = new EstimateValidateServices();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitilizeIntarface();
           
            PopulateData();
            imgSubmitCheck.Hidden = true;
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
            UIHelper.SetDefaultWizardScrollViewBorderProperty(scrollviewAddresses);
            UIHelper.SetDefaultEstimateButtonProperty(btnViewEstimate);
            UIHelper.DismissKeyboardOnBackgroundTap(this);
            txtOriginAddress.Editable = false;
            txtDestinationAddress.Editable = false;

            btnBack.TouchUpInside += BtnBack_TouchUpInside;
            btnConfirm.TouchUpInside += BtnConfirm_TouchUpInside;
            btnViewEstimate.TouchUpInside += BtnViewEstimate_TouchUpInside;
            btnUpdateAddresses.TouchUpInside += BtnUpdateAddresses_TouchUpInside;
        }

        /// <summary>
        /// Event Name      : BtnBack_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To redirec to back screen as service date screen.
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnBack_TouchUpInside(object sender, EventArgs e)
        {
            PerformSegue("addressToSerivceDate", this);
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
            UIHelper.CallingScreen = JKMEnum.UIViewControllerID.AddressesVew;
            UIHelper.RedirectToViewController(this, JKMEnum.UIViewControllerID.ViewEstimateReviewView);
        }

        /// <summary>
        /// Event Name      : BtnConfirm_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : confirmed address data and redirect to what matters most screen.
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnConfirm_TouchUpInside(object sender, EventArgs e)
        {
            string message = estimateValidateServices.ValidateCustomerAddress(txtOriginAddress.Text, txtDestinationAddress.Text);
            if (string.IsNullOrEmpty(message))
            {
                PerformSegue("addressTowhatMattersMost", this);
            }
            else
            {
                UIHelper.ShowMessage(message);
            }
        }

        /// <summary>
        /// Event Name      : BtnUpdateAddresses_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : save address data.
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnUpdateAddresses_TouchUpInside(object sender, EventArgs e)
        {
            if (txtOriginAddress.Editable && txtDestinationAddress.Editable)
            {
                string message = estimateValidateServices.ValidateCustomerAddress(txtOriginAddress.Text, txtDestinationAddress.Text);
                if (string.IsNullOrEmpty(message))
                {
                    SaveDataToDTO();
                    ResetControlReadOnlyMode();
                    imgSubmitCheck.Hidden = true;
                }
                else
                {
                    UIHelper.ShowMessage(message);
                }
            }
            else
            {
                imgSubmitCheck.Hidden = false;
                ResetControlEditMode();
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
            txtOriginAddress.Editable = false;
            txtDestinationAddress.Editable = false;
            btnUpdateAddresses.SetTitle(AppConstant.UPDATE_ADDRESS_BUTTON_LABEL, UIControlState.Normal);
            UIHelper.SetUpdateNeedButtonProperty(btnUpdateAddresses);
            SetEditableDateControl(false);
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
            txtOriginAddress.Editable = true;
            txtDestinationAddress.Editable = true;
            btnUpdateAddresses.SetTitle(AppConstant.SUBMIT_CHANGES_BUTTON_LABEL,UIControlState.Normal);
            UIHelper.SetSubmitButtonProperty(btnUpdateAddresses);
            SetEditableDateControl(true);
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
            txtOriginAddress.UserInteractionEnabled = isEditable;
            txtDestinationAddress.UserInteractionEnabled = isEditable;
            txtOriginAddress.UserInteractionEnabled = isEditable;

            txtOriginAddress.Layer.BorderWidth = isEditable ? 1 : 0;
            txtOriginAddress.Layer.BorderColor = UIColor.LightGray.CGColor;
            txtOriginAddress.Layer.CornerRadius = 10;

            txtDestinationAddress.Layer.BorderWidth = isEditable ? 1 : 0;
            txtDestinationAddress.Layer.BorderColor = UIColor.LightGray.CGColor;
            txtDestinationAddress.Layer.CornerRadius = 10;
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
                txtOriginAddress.Text = estimateModel.CustomOriginAddress;
                txtDestinationAddress.Text = estimateModel.CustomDestinationAddress;
            }
            UIHelper.CreateWizardHeader(4, viewHeader, estimateModel);
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
                    estimateModel.CustomOriginAddress = txtOriginAddress.Text;
                    estimateModel.CustomDestinationAddress = txtDestinationAddress.Text;
                    estimateModel.IsAddressEdited = true;
                }
            }

        }
    }
}
