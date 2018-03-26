using System;
using CoreGraphics;
using Foundation;
using JKMPCL.Common;
using JKMPCL.Model;
using JKMPCL.Services.Estimate;
using UIKit;

namespace JKMIOSApp
{
    /// <summary>
    /// Controller Name : WhatMattersMostViewController
    /// Author          : Hiren Patel
    /// Creation Date   : 23 JAN 2018
    /// Purpose         : To display what matters most page screen as estimate wizard step
    /// Revision        : 
    /// </summary>
    public partial class WhatMattersMostViewController : UIViewController
    {
        private EstimateModel estimateModel;
        private readonly EstimateValidateServices estimateValidateServices;
        private UIEdgeInsets ButtonBackImageEdgeInsets;
        public WhatMattersMostViewController(IntPtr handle) : base(handle)
        {
            estimateValidateServices = new EstimateValidateServices();
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitilizeIntarface();
            PopulateData();
            imgSubmitCheck.Hidden = true;
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
            UIHelper.SetDefaultWizardScrollViewBorderProperty(scrollviewWhatMattersMost);
            UIHelper.SetDefaultEstimateButtonProperty(btnViewEstimate);
            UIHelper.DismissKeyboardOnBackgroundTap(this);

            UIHelper.DismissKeyboardOnBackgroundTap(this);
            txtWhatMattersMost.Editable = false;

            btnBack.TouchUpInside += BtnBack_TouchUpInside;
            btnYesCapturedCorrectly.TouchUpInside += BtnYesCapturedCorrectly_TouchUpInside;
            btnViewEstimate.TouchUpInside += BtnViewEstimate_TouchUpInside;
            btnUpdateNeeded.TouchUpInside += BtnUpdateNeeded_TouchUpInside;
        }

        /// <summary>
        /// Event Name      : BtnBack_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To redirec to back screen as service date.
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnBack_TouchUpInside(object sender, EventArgs e)
        {
            if (txtWhatMattersMost.Editable)
            {
                ResetControlReadOnlyMode();
                PopulateData();
            }
            else
            {
                PerformSegue("whatMattersMostToAddress", this);
            }
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
            UIHelper.CallingScreen = JKMEnum.UIViewControllerID.WhatMattersMostView;
            UIHelper.RedirectToViewController(this, JKMEnum.UIViewControllerID.ViewEstimateReviewView);
        }

        /// <summary>
        /// Event Name      : BtnYesCapturedCorrectly_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To confirmed what matters most data and redirect to valuation screen.
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnYesCapturedCorrectly_TouchUpInside(object sender, EventArgs e)
        {
            string message = estimateValidateServices.ValidateWhatMattersMost(txtWhatMattersMost.Text.Trim());
            if (string.IsNullOrEmpty(message))
            {
                PerformSegue("whatMattersMostToValuation", this);

            }
            else
            {
                UIHelper.ShowMessage(message);
            }
        }

        /// <summary>
        /// Event Name      : BtnUpdateNeeded_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To save edited what matters most data.
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnUpdateNeeded_TouchUpInside(object sender, EventArgs e)
        {
            if (txtWhatMattersMost.Editable)
            {
                string message = estimateValidateServices.ValidateWhatMattersMost(txtWhatMattersMost.Text.Trim());
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
            else
            {
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


            txtWhatMattersMost.Layer.BorderWidth = 0;
            txtWhatMattersMost.Layer.BorderColor = UIColor.LightGray.CGColor;

            imgSubmitCheck.Hidden = true;
            txtWhatMattersMost.Editable = false;
            btnUpdateNeeded.SetTitle(AppConstant.UPDATE_WHAT_MATTERS_MOST_BUTTON_LABEL, UIControlState.Normal);
            UIHelper.SetUpdateNeedButtonProperty(btnUpdateNeeded);
            btnYesCapturedCorrectly.SetTitle("Yes, Captured Correctly", UIControlState.Normal);
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
            txtWhatMattersMost.Layer.BorderWidth = 1;
            txtWhatMattersMost.Layer.BorderColor = UIColor.LightGray.CGColor;
            txtWhatMattersMost.Layer.CornerRadius = 5;
            imgSubmitCheck.Hidden = false;
            txtWhatMattersMost.Editable = true;
            btnUpdateNeeded.SetTitle(AppConstant.SUBMIT_CHANGES_BUTTON_LABEL,UIControlState.Normal);
            UIHelper.SetSubmitButtonProperty(btnUpdateNeeded);

            btnYesCapturedCorrectly.SetTitle("Next Step", UIControlState.Normal);

            btnBack.ImageEdgeInsets = new UIEdgeInsets(btnBack.ImageEdgeInsets.Top, -1000, btnBack.ImageEdgeInsets.Bottom, btnBack.ImageEdgeInsets.Right);
            btnBack.SetTitle("Cancel", UIControlState.Normal);
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
                txtWhatMattersMost.Text = estimateModel.WhatMattersMost;
            }
            UIHelper.CreateWizardHeader(5, viewHeader, estimateModel);
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
                    estimateModel.WhatMattersMost = txtWhatMattersMost.Text;
                    estimateModel.IsWhatMatterMostEdited = true;
                }
            }
        }
    }
}
