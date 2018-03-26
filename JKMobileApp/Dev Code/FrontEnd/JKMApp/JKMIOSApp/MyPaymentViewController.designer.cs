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
	[Register ("MyPaymentViewController")]
	partial class MyPaymentViewController
	{
		[Outlet]
		UIKit.UIButton btnAddAnotherCard { get; set; }

		[Outlet]
		UIKit.UIButton btnAlert { get; set; }

		[Outlet]
		UIKit.UIButton btnCancel { get; set; }

		[Outlet]
		UIKit.UIButton btnClose { get; set; }

		[Outlet]
		UIKit.UIButton btnContactUs { get; set; }

		[Outlet]
		UIKit.UIButton btnMakePayment { get; set; }

		[Outlet]
		UIKit.UIButton btnOK { get; set; }

		[Outlet]
		UIKit.UIImageView imgPaymentCardType { get; set; }

		[Outlet]
		UIKit.UIImageView imgPaymentStatus { get; set; }

		[Outlet]
		UIKit.UILabel lblAmountPaidText { get; set; }

		[Outlet]
		UIKit.UILabel lblDepositCollected { get; set; }

		[Outlet]
		UIKit.UILabel lblPaymentStatus { get; set; }

		[Outlet]
		UIKit.UILabel lblPaymentStatusAmountPaid { get; set; }

		[Outlet]
		UIKit.UILabel lblPaymentStatusMessageLine1 { get; set; }

		[Outlet]
		UIKit.UILabel lblPaymentStatusMessageLine2 { get; set; }

		[Outlet]
		UIKit.UILabel lblTotalCost { get; set; }

		[Outlet]
		UIKit.UILabel lblTotalDue { get; set; }

		[Outlet]
		UIKit.UILabel lblTotalPaid { get; set; }

		[Outlet]
		UIKit.UILabel lblTransactionId { get; set; }

		[Outlet]
		UIKit.UILabel lblTransactionIDText { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewDatePicker { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewMakePayment { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewPaymentSubmitted { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewSwipeCard { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollviewTotalCost { get; set; }

		[Outlet]
		UIKit.UITextField txtAmount { get; set; }

		[Outlet]
		UIKit.UITextField txtCardNumber { get; set; }

		[Outlet]
		UIKit.UITextField txtCVV { get; set; }

		[Outlet]
		UIKit.UITextField txtExpiredDate { get; set; }

		[Outlet]
		UIKit.UITextField txtNameOfCardHolder { get; set; }

		[Outlet]
		UIKit.UIDatePicker uiDatePicker { get; set; }

		[Outlet]
		UIKit.UIView viewDepositCollected { get; set; }

		[Outlet]
		UIKit.UIView viewPaymentAmountPaid { get; set; }

		[Outlet]
		UIKit.UIView viewPaymentAmountSeparatorLine { get; set; }

		[Outlet]
		UIKit.UIView viewTransaction { get; set; }

		[Outlet]
		UIKit.UIView viewTransactionContainer { get; set; }

		[Outlet]
		UIKit.UIView viewTransactionSeparatorLine { get; set; }

		[Action ("btnAlertPressed:")]
		partial void btnAlertPressed (Foundation.NSObject sender);

		[Action ("btnContactPressed:")]
		partial void btnContactPressed (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnAddAnotherCard != null) {
				btnAddAnotherCard.Dispose ();
				btnAddAnotherCard = null;
			}

			if (btnAlert != null) {
				btnAlert.Dispose ();
				btnAlert = null;
			}

			if (btnCancel != null) {
				btnCancel.Dispose ();
				btnCancel = null;
			}

			if (btnClose != null) {
				btnClose.Dispose ();
				btnClose = null;
			}

			if (btnContactUs != null) {
				btnContactUs.Dispose ();
				btnContactUs = null;
			}

			if (btnMakePayment != null) {
				btnMakePayment.Dispose ();
				btnMakePayment = null;
			}

			if (btnOK != null) {
				btnOK.Dispose ();
				btnOK = null;
			}

			if (imgPaymentCardType != null) {
				imgPaymentCardType.Dispose ();
				imgPaymentCardType = null;
			}

			if (imgPaymentStatus != null) {
				imgPaymentStatus.Dispose ();
				imgPaymentStatus = null;
			}

			if (lblDepositCollected != null) {
				lblDepositCollected.Dispose ();
				lblDepositCollected = null;
			}

			if (lblPaymentStatus != null) {
				lblPaymentStatus.Dispose ();
				lblPaymentStatus = null;
			}

			if (lblPaymentStatusAmountPaid != null) {
				lblPaymentStatusAmountPaid.Dispose ();
				lblPaymentStatusAmountPaid = null;
			}

			if (lblPaymentStatusMessageLine1 != null) {
				lblPaymentStatusMessageLine1.Dispose ();
				lblPaymentStatusMessageLine1 = null;
			}

			if (lblPaymentStatusMessageLine2 != null) {
				lblPaymentStatusMessageLine2.Dispose ();
				lblPaymentStatusMessageLine2 = null;
			}

			if (lblTotalCost != null) {
				lblTotalCost.Dispose ();
				lblTotalCost = null;
			}

			if (lblTotalDue != null) {
				lblTotalDue.Dispose ();
				lblTotalDue = null;
			}

			if (lblTotalPaid != null) {
				lblTotalPaid.Dispose ();
				lblTotalPaid = null;
			}

			if (lblTransactionId != null) {
				lblTransactionId.Dispose ();
				lblTransactionId = null;
			}

			if (scrollViewDatePicker != null) {
				scrollViewDatePicker.Dispose ();
				scrollViewDatePicker = null;
			}

			if (scrollViewMakePayment != null) {
				scrollViewMakePayment.Dispose ();
				scrollViewMakePayment = null;
			}

			if (scrollViewPaymentSubmitted != null) {
				scrollViewPaymentSubmitted.Dispose ();
				scrollViewPaymentSubmitted = null;
			}

			if (scrollViewSwipeCard != null) {
				scrollViewSwipeCard.Dispose ();
				scrollViewSwipeCard = null;
			}

			if (scrollviewTotalCost != null) {
				scrollviewTotalCost.Dispose ();
				scrollviewTotalCost = null;
			}

			if (txtAmount != null) {
				txtAmount.Dispose ();
				txtAmount = null;
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

			if (txtNameOfCardHolder != null) {
				txtNameOfCardHolder.Dispose ();
				txtNameOfCardHolder = null;
			}

			if (uiDatePicker != null) {
				uiDatePicker.Dispose ();
				uiDatePicker = null;
			}

			if (viewDepositCollected != null) {
				viewDepositCollected.Dispose ();
				viewDepositCollected = null;
			}

			if (viewPaymentAmountPaid != null) {
				viewPaymentAmountPaid.Dispose ();
				viewPaymentAmountPaid = null;
			}

			if (viewTransaction != null) {
				viewTransaction.Dispose ();
				viewTransaction = null;
			}

			if (viewTransactionContainer != null) {
				viewTransactionContainer.Dispose ();
				viewTransactionContainer = null;
			}

			if (viewTransactionSeparatorLine != null) {
				viewTransactionSeparatorLine.Dispose ();
				viewTransactionSeparatorLine = null;
			}

			if (viewPaymentAmountSeparatorLine != null) {
				viewPaymentAmountSeparatorLine.Dispose ();
				viewPaymentAmountSeparatorLine = null;
			}

			if (lblTransactionIDText != null) {
				lblTransactionIDText.Dispose ();
				lblTransactionIDText = null;
			}

			if (lblAmountPaidText != null) {
				lblAmountPaidText.Dispose ();
				lblAmountPaidText = null;
			}
		}
	}
}
