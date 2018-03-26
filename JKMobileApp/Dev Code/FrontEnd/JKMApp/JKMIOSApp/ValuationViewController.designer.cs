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
	[Register ("ValuationViewController")]
	partial class ValuationViewController
	{
		[Outlet]
		UIKit.UIButton btnBack { get; set; }

		[Outlet]
		UIKit.UIButton btnCancel { get; set; }

		[Outlet]
		UIKit.UIButton btnOK { get; set; }

		[Outlet]
		UIKit.UIButton btnUpdatesNeed { get; set; }

		[Outlet]
		UIKit.UIButton btnViewEstimation { get; set; }

		[Outlet]
		UIKit.UIButton btnYesCapturedCorrectly { get; set; }

		[Outlet]
		UIKit.UIImageView imgSubmitCheck { get; set; }

		[Outlet]
		UIKit.UILabel lblCost { get; set; }

		[Outlet]
		UIKit.UILabel lblCoverage { get; set; }

		[Outlet]
		UIKit.UILabel lblDeclaredValue { get; set; }

		[Outlet]
		UIKit.UILabel lblDescription { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollviewValuation { get; set; }

		[Outlet]
		UIKit.UITextField txtCost { get; set; }

		[Outlet]
		UIKit.UITextField txtCoverageValue { get; set; }

		[Outlet]
		UIKit.UITextField txtDeclaredValue { get; set; }

		[Outlet]
		UIKit.UIPickerView uiPickerViewCoverage { get; set; }

		[Outlet]
		UIKit.UIScrollView uiScrollViewPickerContainer { get; set; }

		[Outlet]
		UIKit.UIView viewHeader { get; set; }

		[Action ("btnBackPressed:")]
		partial void btnBackPressed (Foundation.NSObject sender);

		[Action ("btnViewEstimationPressed:")]
		partial void btnViewEstimationPressed (Foundation.NSObject sender);

		[Action ("btnYesCapturedCorrectlyPressed:")]
		partial void btnYesCapturedCorrectlyPressed (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnBack != null) {
				btnBack.Dispose ();
				btnBack = null;
			}

			if (btnCancel != null) {
				btnCancel.Dispose ();
				btnCancel = null;
			}

			if (btnOK != null) {
				btnOK.Dispose ();
				btnOK = null;
			}

			if (btnUpdatesNeed != null) {
				btnUpdatesNeed.Dispose ();
				btnUpdatesNeed = null;
			}

			if (btnViewEstimation != null) {
				btnViewEstimation.Dispose ();
				btnViewEstimation = null;
			}

			if (btnYesCapturedCorrectly != null) {
				btnYesCapturedCorrectly.Dispose ();
				btnYesCapturedCorrectly = null;
			}

			if (imgSubmitCheck != null) {
				imgSubmitCheck.Dispose ();
				imgSubmitCheck = null;
			}

			if (lblCost != null) {
				lblCost.Dispose ();
				lblCost = null;
			}

			if (lblCoverage != null) {
				lblCoverage.Dispose ();
				lblCoverage = null;
			}

			if (lblDeclaredValue != null) {
				lblDeclaredValue.Dispose ();
				lblDeclaredValue = null;
			}

			if (scrollviewValuation != null) {
				scrollviewValuation.Dispose ();
				scrollviewValuation = null;
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

			if (uiPickerViewCoverage != null) {
				uiPickerViewCoverage.Dispose ();
				uiPickerViewCoverage = null;
			}

			if (uiScrollViewPickerContainer != null) {
				uiScrollViewPickerContainer.Dispose ();
				uiScrollViewPickerContainer = null;
			}

			if (viewHeader != null) {
				viewHeader.Dispose ();
				viewHeader = null;
			}

			if (lblDescription != null) {
				lblDescription.Dispose ();
				lblDescription = null;
			}
		}
	}
}
