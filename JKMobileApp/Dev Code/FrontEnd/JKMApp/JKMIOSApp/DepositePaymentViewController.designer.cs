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
	[Register ("DepositePaymentViewController")]
	partial class DepositePaymentViewController
	{
		[Outlet]
		UIKit.UIButton btnAreYouPaying { get; set; }

		[Outlet]
		UIKit.UIButton btnBack { get; set; }

		[Outlet]
		UIKit.UIButton btnCancel { get; set; }

		[Outlet]
		UIKit.UIButton btnOK { get; set; }

		[Outlet]
		UIKit.UIButton btnSubmitPayment { get; set; }

		[Outlet]
		UIKit.UIImageView imgCardType { get; set; }

		[Outlet]
		UIKit.UILabel lblDepositAmountValue { get; set; }

		[Outlet]
		UIKit.UILabel lblDescription { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewDatePicker { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewDepositAmount { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewPaymentDetails { get; set; }

		[Outlet]
		UIKit.UITextField txtCardNumber { get; set; }

		[Outlet]
		UIKit.UITextField txtCVV { get; set; }

		[Outlet]
		UIKit.UITextField txtExpiredDate { get; set; }

		[Outlet]
		UIKit.UITextField txtNameOfCardholder { get; set; }

		[Outlet]
		UIKit.UIDatePicker uiDatePicker { get; set; }

		[Outlet]
		UIKit.UIView viewStepsProgressBar { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnAreYouPaying != null) {
				btnAreYouPaying.Dispose ();
				btnAreYouPaying = null;
			}

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

			if (btnSubmitPayment != null) {
				btnSubmitPayment.Dispose ();
				btnSubmitPayment = null;
			}

			if (imgCardType != null) {
				imgCardType.Dispose ();
				imgCardType = null;
			}

			if (lblDepositAmountValue != null) {
				lblDepositAmountValue.Dispose ();
				lblDepositAmountValue = null;
			}

			if (scrollViewDatePicker != null) {
				scrollViewDatePicker.Dispose ();
				scrollViewDatePicker = null;
			}

			if (scrollViewDepositAmount != null) {
				scrollViewDepositAmount.Dispose ();
				scrollViewDepositAmount = null;
			}

			if (scrollViewPaymentDetails != null) {
				scrollViewPaymentDetails.Dispose ();
				scrollViewPaymentDetails = null;
			}

			if (txtCardNumber != null) {
				txtCardNumber.Dispose ();
				txtCardNumber = null;
			}

			if (txtCVV != null) {
				txtCVV.Dispose ();
				txtCVV = null;
			}

			if (txtExpiredDate != null) {
				txtExpiredDate.Dispose ();
				txtExpiredDate = null;
			}

			if (txtNameOfCardholder != null) {
				txtNameOfCardholder.Dispose ();
				txtNameOfCardholder = null;
			}

			if (uiDatePicker != null) {
				uiDatePicker.Dispose ();
				uiDatePicker = null;
			}

			if (viewStepsProgressBar != null) {
				viewStepsProgressBar.Dispose ();
				viewStepsProgressBar = null;
			}

			if (lblDescription != null) {
				lblDescription.Dispose ();
				lblDescription = null;
			}
		}
	}
}
