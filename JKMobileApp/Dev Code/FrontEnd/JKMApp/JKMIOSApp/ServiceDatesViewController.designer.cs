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
	[Register ("ServiceDatesViewController")]
	partial class ServiceDatesViewController
	{
		[Outlet]
		UIKit.UIButton btnBack { get; set; }

		[Outlet]
		UIKit.UIButton btnCancel { get; set; }

		[Outlet]
		UIKit.UIButton btnChangeMyServiceDates { get; set; }

		[Outlet]
		UIKit.UIButton btnDatesAreAccurate { get; set; }

		[Outlet]
		UIKit.UIButton btnOK { get; set; }

		[Outlet]
		UIKit.UIButton btnViewEstimate { get; set; }

		[Outlet]
		UIKit.UIImageView imgSubmitCheck { get; set; }

		[Outlet]
		UIKit.UILabel lblDescription { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewDate { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewDatePicker { get; set; }

		[Outlet]
		UIKit.UITextField txtLoadDate { get; set; }

		[Outlet]
		UIKit.UITextField txtMoveDate { get; set; }

		[Outlet]
		UIKit.UITextField txtPackDate { get; set; }

		[Outlet]
		UIKit.UIDatePicker uiDatePicker { get; set; }

		[Outlet]
		UIKit.UIView viewHeader { get; set; }

		[Action ("btnBackPressed:")]
		partial void btnBackPressed (Foundation.NSObject sender);

		[Action ("btnChangeMyServicesDatesPressed:")]
		partial void btnChangeMyServicesDatesPressed (Foundation.NSObject sender);

		[Action ("btnDatesAreAccuratePressed:")]
		partial void btnDatesAreAccuratePressed (Foundation.NSObject sender);

		[Action ("btnViewEstimatePressed:")]
		partial void btnViewEstimatePressed (Foundation.NSObject sender);
		
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

			if (btnChangeMyServiceDates != null) {
				btnChangeMyServiceDates.Dispose ();
				btnChangeMyServiceDates = null;
			}

			if (btnDatesAreAccurate != null) {
				btnDatesAreAccurate.Dispose ();
				btnDatesAreAccurate = null;
			}

			if (btnOK != null) {
				btnOK.Dispose ();
				btnOK = null;
			}

			if (btnViewEstimate != null) {
				btnViewEstimate.Dispose ();
				btnViewEstimate = null;
			}

			if (imgSubmitCheck != null) {
				imgSubmitCheck.Dispose ();
				imgSubmitCheck = null;
			}

			if (scrollViewDate != null) {
				scrollViewDate.Dispose ();
				scrollViewDate = null;
			}

			if (scrollViewDatePicker != null) {
				scrollViewDatePicker.Dispose ();
				scrollViewDatePicker = null;
			}

			if (txtLoadDate != null) {
				txtLoadDate.Dispose ();
				txtLoadDate = null;
			}

			if (txtMoveDate != null) {
				txtMoveDate.Dispose ();
				txtMoveDate = null;
			}

			if (txtPackDate != null) {
				txtPackDate.Dispose ();
				txtPackDate = null;
			}

			if (uiDatePicker != null) {
				uiDatePicker.Dispose ();
				uiDatePicker = null;
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
