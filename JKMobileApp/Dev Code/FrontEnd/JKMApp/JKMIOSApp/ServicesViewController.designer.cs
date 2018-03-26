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
	[Register ("ServicesViewController")]
	partial class ServicesViewController
	{
		[Outlet]
		UIKit.UIButton btnBack { get; set; }

		[Outlet]
		UIKit.UIButton btnConfirm { get; set; }

		[Outlet]
		UIKit.UIButton btnViewEstimate { get; set; }

		[Outlet]
		UIKit.UILabel lblDescription { get; set; }

		[Outlet]
		UIKit.UILabel lblPleaseConatctSales { get; set; }

		[Outlet]
		UIKit.UILabel lblWantToUpdateYourServices { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollviewServices { get; set; }

		[Outlet]
		UIKit.UIView viewHeader { get; set; }

		[Action ("btnBackPressed:")]
		partial void btnBackPressed (Foundation.NSObject sender);

		[Action ("btnConfirmPressed:")]
		partial void btnConfirmPressed (Foundation.NSObject sender);

		[Action ("btnViewEstimatePressed:")]
		partial void btnViewEstimatePressed (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnBack != null) {
				btnBack.Dispose ();
				btnBack = null;
			}

			if (btnConfirm != null) {
				btnConfirm.Dispose ();
				btnConfirm = null;
			}

			if (btnViewEstimate != null) {
				btnViewEstimate.Dispose ();
				btnViewEstimate = null;
			}

			if (lblPleaseConatctSales != null) {
				lblPleaseConatctSales.Dispose ();
				lblPleaseConatctSales = null;
			}

			if (lblWantToUpdateYourServices != null) {
				lblWantToUpdateYourServices.Dispose ();
				lblWantToUpdateYourServices = null;
			}

			if (scrollviewServices != null) {
				scrollviewServices.Dispose ();
				scrollviewServices = null;
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
