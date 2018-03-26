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
	[Register ("MoveConfirmedViewController")]
	partial class MoveConfirmedViewController
	{
		[Outlet]
		UIKit.UIButton btnGotToDashBoard { get; set; }

		[Outlet]
		UIKit.UIButton btnViewEstimate { get; set; }

		[Outlet]
		UIKit.UIImageView imgBigStepNumber { get; set; }

		[Outlet]
		UIKit.UIImageView imgThumbIcon { get; set; }

		[Outlet]
		UIKit.UILabel lblCongratulationTitle { get; set; }

		[Outlet]
		UIKit.UILabel lblDepositCollected { get; set; }

		[Outlet]
		UIKit.UILabel lblLine1 { get; set; }

		[Outlet]
		UIKit.UILabel lblLine2 { get; set; }

		[Outlet]
		UIKit.UILabel lblLine3 { get; set; }

		[Outlet]
		UIKit.UILabel lblLine4 { get; set; }

		[Outlet]
		UIKit.UILabel lblLine5 { get; set; }

		[Outlet]
		UIKit.UILabel lblTransactionId { get; set; }

		[Outlet]
		UIKit.UIView viewDepositCollected { get; set; }

		[Outlet]
		UIKit.UIView viewHeader { get; set; }

		[Outlet]
		UIKit.UIView viewTransactionDetails { get; set; }

		[Action ("btnGotToDashBoardPressed:")]
		partial void btnGotToDashBoardPressed (Foundation.NSObject sender);

		[Action ("btnViewEstimatePressed:")]
		partial void btnViewEstimatePressed (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnGotToDashBoard != null) {
				btnGotToDashBoard.Dispose ();
				btnGotToDashBoard = null;
			}

			if (btnViewEstimate != null) {
				btnViewEstimate.Dispose ();
				btnViewEstimate = null;
			}

			if (imgBigStepNumber != null) {
				imgBigStepNumber.Dispose ();
				imgBigStepNumber = null;
			}

			if (lblDepositCollected != null) {
				lblDepositCollected.Dispose ();
				lblDepositCollected = null;
			}

			if (lblTransactionId != null) {
				lblTransactionId.Dispose ();
				lblTransactionId = null;
			}

			if (viewDepositCollected != null) {
				viewDepositCollected.Dispose ();
				viewDepositCollected = null;
			}

			if (viewHeader != null) {
				viewHeader.Dispose ();
				viewHeader = null;
			}

			if (viewTransactionDetails != null) {
				viewTransactionDetails.Dispose ();
				viewTransactionDetails = null;
			}

			if (imgThumbIcon != null) {
				imgThumbIcon.Dispose ();
				imgThumbIcon = null;
			}

			if (lblCongratulationTitle != null) {
				lblCongratulationTitle.Dispose ();
				lblCongratulationTitle = null;
			}

			if (lblLine1 != null) {
				lblLine1.Dispose ();
				lblLine1 = null;
			}

			if (lblLine2 != null) {
				lblLine2.Dispose ();
				lblLine2 = null;
			}

			if (lblLine3 != null) {
				lblLine3.Dispose ();
				lblLine3 = null;
			}

			if (lblLine4 != null) {
				lblLine4.Dispose ();
				lblLine4 = null;
			}

			if (lblLine5 != null) {
				lblLine5.Dispose ();
				lblLine5 = null;
			}
		}
	}
}
