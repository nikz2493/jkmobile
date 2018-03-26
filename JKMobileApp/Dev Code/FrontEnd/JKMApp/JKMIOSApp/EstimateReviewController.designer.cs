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
	[Register ("EstimateReviewController")]
	partial class EstimateReviewController
	{
		[Outlet]
		UIKit.UIButton btnConfirm { get; set; }

		[Outlet]
		UIKit.UIButton btnMoreInformation { get; set; }

		[Outlet]
		UIKit.UIButton btnViewEstimate { get; set; }

		[Outlet]
		UIKit.UILabel lblDescription { get; set; }

		[Outlet]
		UIKit.UITextField txtEstimatedCost { get; set; }

		[Outlet]
		UIKit.UIView viewHeader { get; set; }

		[Action ("btnConfirmPressed:")]
		partial void btnConfirmPressed (Foundation.NSObject sender);

		[Action ("btnMoreInformationPressed:")]
		partial void btnMoreInformationPressed (Foundation.NSObject sender);

		[Action ("btnViewEstimatePressed:")]
		partial void btnViewEstimatePressed (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnConfirm != null) {
				btnConfirm.Dispose ();
				btnConfirm = null;
			}

			if (btnMoreInformation != null) {
				btnMoreInformation.Dispose ();
				btnMoreInformation = null;
			}

			if (btnViewEstimate != null) {
				btnViewEstimate.Dispose ();
				btnViewEstimate = null;
			}

			if (lblDescription != null) {
				lblDescription.Dispose ();
				lblDescription = null;
			}

			if (txtEstimatedCost != null) {
				txtEstimatedCost.Dispose ();
				txtEstimatedCost = null;
			}

			if (viewHeader != null) {
				viewHeader.Dispose ();
				viewHeader = null;
			}
		}
	}
}
