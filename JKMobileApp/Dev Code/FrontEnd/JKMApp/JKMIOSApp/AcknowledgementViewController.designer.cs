// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace JKMIOSApp
{
	[Register ("AcknowledgementViewController")]
	partial class AcknowledgementViewController
	{
		[Outlet]
		UIKit.UIButton btnAddressesEdit { get; set; }

		[Outlet]
		UIKit.UIButton btnBack { get; set; }

		[Outlet]
		UIKit.UIButton btnCancelDatePicker { get; set; }

		[Outlet]
		UIKit.UIButton btnCancelPickerValuation { get; set; }

		[Outlet]
		UIKit.UIButton btnChangeMyServiceDates { get; set; }

		[Outlet]
		UIKit.UIButton btnContinue { get; set; }

		[Outlet]
		UIKit.UIButton btnIagree { get; set; }

		[Outlet]
		UIKit.UIButton btnOkDatePicker { get; set; }

		[Outlet]
		UIKit.UIButton btnOKPickerValuation { get; set; }

		[Outlet]
		UIKit.UIButton btnServiceDateEdit { get; set; }

		[Outlet]
		UIKit.UIButton btnServicesEdit { get; set; }

		[Outlet]
		UIKit.UIButton btnUpdateAddresses { get; set; }

		[Outlet]
		UIKit.UIButton btnUpdateNeeded { get; set; }

		[Outlet]
		UIKit.UIButton btnUpdatesNeed { get; set; }

		[Outlet]
		UIKit.UIButton btnValuationEdit { get; set; }

		[Outlet]
		UIKit.UIButton btnViewEstimate { get; set; }

		[Outlet]
		UIKit.UIButton btnVitalInformationEdit { get; set; }

		[Outlet]
		UIKit.UIButton btnWhatMatteresMostEdit { get; set; }

		[Outlet]
		UIKit.UIButton btnWhatMattersMost { get; set; }

		[Outlet]
		UIKit.UIImageView imgAddresses { get; set; }

		[Outlet]
		UIKit.UIImageView imgServiceDate { get; set; }

		[Outlet]
		UIKit.UIImageView imgServices { get; set; }

		[Outlet]
		UIKit.UIImageView imgValuation { get; set; }

		[Outlet]
		UIKit.UIImageView imgVitalInformation { get; set; }

		[Outlet]
		UIKit.UIImageView imgWhatMattersMost { get; set; }

		[Outlet]
		UIKit.UILabel lblAddressesDescription { get; set; }

		[Outlet]
		UIKit.UILabel lblCoverage { get; set; }

		[Outlet]
		UIKit.UILabel lblDescription { get; set; }

		[Outlet]
		UIKit.UILabel lblIAgree { get; set; }

		[Outlet]
		UIKit.UILabel lblIagreeDescription { get; set; }

		[Outlet]
		UIKit.UILabel lblServiceDateDescription { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollviewAddresses { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewDate { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewDatePicker { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewIAgree { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewMainContainer { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewStepsContainer { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollviewValuation { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollviewWhatMattersMost { get; set; }

		[Outlet]
		UIKit.UITextField txtCost { get; set; }

		[Outlet]
		UIKit.UITextField txtCoverageValue { get; set; }

		[Outlet]
		UIKit.UITextField txtDeclaredValue { get; set; }

		[Outlet]
		UIKit.UITextView txtDestinationAddress { get; set; }

		[Outlet]
		UIKit.UITextField txtLoadDate { get; set; }

		[Outlet]
		UIKit.UITextField txtMoveDate { get; set; }

		[Outlet]
		UIKit.UITextView txtOriginAddress { get; set; }

		[Outlet]
		UIKit.UITextField txtPackDate { get; set; }

		[Outlet]
		UIKit.UITextView txtWhatMattersMost { get; set; }

		[Outlet]
		UIKit.UIDatePicker uiDatePicker { get; set; }

		[Outlet]
		UIKit.UIPickerView uiPickerViewCoverage { get; set; }

		[Outlet]
		UIKit.UIScrollView uiScrollViewPickerContainer { get; set; }

		[Outlet]
		UIKit.UIView viewAddresses { get; set; }

		[Outlet]
		UIKit.UIView viewHeader { get; set; }

		[Outlet]
		UIKit.UIView viewInnerSpetsContainer { get; set; }

		[Outlet]
		UIKit.UIView viewServiceDates { get; set; }

		[Outlet]
		UIKit.UIView viewServices { get; set; }

		[Outlet]
		UIKit.UIView viewValuation { get; set; }

		[Outlet]
		UIKit.UIView viewVitalInformation { get; set; }

		[Outlet]
		UIKit.UIView viewWhatMaattersMost { get; set; }

		[Action ("btnBackPressed:")]
		partial void btnBackPressed (Foundation.NSObject sender);

		[Action ("btnContinuePressed:")]
		partial void btnContinuePressed (Foundation.NSObject sender);

		[Action ("btnViewEstimatePressed:")]
		partial void btnViewEstimatePressed (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnAddressesEdit != null) {
				btnAddressesEdit.Dispose ();
				btnAddressesEdit = null;
			}

			if (btnBack != null) {
				btnBack.Dispose ();
				btnBack = null;
			}

			if (btnCancelDatePicker != null) {
				btnCancelDatePicker.Dispose ();
				btnCancelDatePicker = null;
			}

			if (btnCancelPickerValuation != null) {
				btnCancelPickerValuation.Dispose ();
				btnCancelPickerValuation = null;
			}

			if (btnChangeMyServiceDates != null) {
				btnChangeMyServiceDates.Dispose ();
				btnChangeMyServiceDates = null;
			}

			if (btnContinue != null) {
				btnContinue.Dispose ();
				btnContinue = null;
			}

			if (btnIagree != null) {
				btnIagree.Dispose ();
				btnIagree = null;
			}

			if (btnOkDatePicker != null) {
				btnOkDatePicker.Dispose ();
				btnOkDatePicker = null;
			}

			if (btnOKPickerValuation != null) {
				btnOKPickerValuation.Dispose ();
				btnOKPickerValuation = null;
			}

			if (btnServiceDateEdit != null) {
				btnServiceDateEdit.Dispose ();
				btnServiceDateEdit = null;
			}

			if (btnServicesEdit != null) {
				btnServicesEdit.Dispose ();
				btnServicesEdit = null;
			}

			if (btnUpdateAddresses != null) {
				btnUpdateAddresses.Dispose ();
				btnUpdateAddresses = null;
			}

			if (btnUpdateNeeded != null) {
				btnUpdateNeeded.Dispose ();
				btnUpdateNeeded = null;
			}

			if (btnUpdatesNeed != null) {
				btnUpdatesNeed.Dispose ();
				btnUpdatesNeed = null;
			}

			if (btnValuationEdit != null) {
				btnValuationEdit.Dispose ();
				btnValuationEdit = null;
			}

			if (btnViewEstimate != null) {
				btnViewEstimate.Dispose ();
				btnViewEstimate = null;
			}

			if (btnVitalInformationEdit != null) {
				btnVitalInformationEdit.Dispose ();
				btnVitalInformationEdit = null;
			}

			if (btnWhatMatteresMostEdit != null) {
				btnWhatMatteresMostEdit.Dispose ();
				btnWhatMatteresMostEdit = null;
			}

			if (btnWhatMattersMost != null) {
				btnWhatMattersMost.Dispose ();
				btnWhatMattersMost = null;
			}

			if (imgAddresses != null) {
				imgAddresses.Dispose ();
				imgAddresses = null;
			}

			if (imgServiceDate != null) {
				imgServiceDate.Dispose ();
				imgServiceDate = null;
			}

			if (imgServices != null) {
				imgServices.Dispose ();
				imgServices = null;
			}

			if (imgValuation != null) {
				imgValuation.Dispose ();
				imgValuation = null;
			}

			if (imgVitalInformation != null) {
				imgVitalInformation.Dispose ();
				imgVitalInformation = null;
			}

			if (imgWhatMattersMost != null) {
				imgWhatMattersMost.Dispose ();
				imgWhatMattersMost = null;
			}

			if (lblAddressesDescription != null) {
				lblAddressesDescription.Dispose ();
				lblAddressesDescription = null;
			}

			if (lblCoverage != null) {
				lblCoverage.Dispose ();
				lblCoverage = null;
			}

			if (lblIAgree != null) {
				lblIAgree.Dispose ();
				lblIAgree = null;
			}

			if (lblIagreeDescription != null) {
				lblIagreeDescription.Dispose ();
				lblIagreeDescription = null;
			}

			if (lblServiceDateDescription != null) {
				lblServiceDateDescription.Dispose ();
				lblServiceDateDescription = null;
			}

			if (scrollviewAddresses != null) {
				scrollviewAddresses.Dispose ();
				scrollviewAddresses = null;
			}

			if (scrollViewDate != null) {
				scrollViewDate.Dispose ();
				scrollViewDate = null;
			}

			if (scrollViewDatePicker != null) {
				scrollViewDatePicker.Dispose ();
				scrollViewDatePicker = null;
			}

			if (scrollViewIAgree != null) {
				scrollViewIAgree.Dispose ();
				scrollViewIAgree = null;
			}

			if (scrollViewMainContainer != null) {
				scrollViewMainContainer.Dispose ();
				scrollViewMainContainer = null;
			}

			if (scrollViewStepsContainer != null) {
				scrollViewStepsContainer.Dispose ();
				scrollViewStepsContainer = null;
			}

			if (scrollviewValuation != null) {
				scrollviewValuation.Dispose ();
				scrollviewValuation = null;
			}

			if (scrollviewWhatMattersMost != null) {
				scrollviewWhatMattersMost.Dispose ();
				scrollviewWhatMattersMost = null;
			}

			if (txtCost != null) {
				txtCost.Dispose ();
				txtCost = null;
			}

			if (txtCoverageValue != null) {
				txtCoverageValue.Dispose ();
				txtCoverageValue = null;
			}

			if (txtDeclaredValue != null) {
				txtDeclaredValue.Dispose ();
				txtDeclaredValue = null;
			}

			if (txtDestinationAddress != null) {
				txtDestinationAddress.Dispose ();
				txtDestinationAddress = null;
			}

			if (txtLoadDate != null) {
				txtLoadDate.Dispose ();
				txtLoadDate = null;
			}

			if (txtMoveDate != null) {
				txtMoveDate.Dispose ();
				txtMoveDate = null;
			}

			if (txtOriginAddress != null) {
				txtOriginAddress.Dispose ();
				txtOriginAddress = null;
			}

			if (txtPackDate != null) {
				txtPackDate.Dispose ();
				txtPackDate = null;
			}

			if (txtWhatMattersMost != null) {
				txtWhatMattersMost.Dispose ();
				txtWhatMattersMost = null;
			}

			if (uiDatePicker != null) {
				uiDatePicker.Dispose ();
				uiDatePicker = null;
			}

			if (uiPickerViewCoverage != null) {
				uiPickerViewCoverage.Dispose ();
				uiPickerViewCoverage = null;
			}

			if (uiScrollViewPickerContainer != null) {
				uiScrollViewPickerContainer.Dispose ();
				uiScrollViewPickerContainer = null;
			}

			if (viewAddresses != null) {
				viewAddresses.Dispose ();
				viewAddresses = null;
			}

			if (viewHeader != null) {
				viewHeader.Dispose ();
				viewHeader = null;
			}

			if (viewInnerSpetsContainer != null) {
				viewInnerSpetsContainer.Dispose ();
				viewInnerSpetsContainer = null;
			}

			if (viewServiceDates != null) {
				viewServiceDates.Dispose ();
				viewServiceDates = null;
			}

			if (viewServices != null) {
				viewServices.Dispose ();
				viewServices = null;
			}

			if (viewValuation != null) {
				viewValuation.Dispose ();
				viewValuation = null;
			}

			if (viewVitalInformation != null) {
				viewVitalInformation.Dispose ();
				viewVitalInformation = null;
			}

			if (viewWhatMaattersMost != null) {
				viewWhatMaattersMost.Dispose ();
				viewWhatMaattersMost = null;
			}

			if (lblDescription != null) {
				lblDescription.Dispose ();
				lblDescription = null;
			}
		}
	}
}
