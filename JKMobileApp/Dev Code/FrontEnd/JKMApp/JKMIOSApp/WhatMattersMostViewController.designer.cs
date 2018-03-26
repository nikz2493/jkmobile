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
	[Register ("WhatMattersMostViewController")]
	partial class WhatMattersMostViewController
	{
		[Outlet]
		UIKit.UIButton btnBack { get; set; }

		[Outlet]
		UIKit.UIButton btnUpdateNeeded { get; set; }

		[Outlet]
		UIKit.UIButton btnViewEstimate { get; set; }

		[Outlet]
		UIKit.UIButton btnYesCapturedCorrectly { get; set; }

		[Outlet]
		UIKit.UIImageView imgSubmitCheck { get; set; }

		[Outlet]
		UIKit.UILabel lblDescription { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollviewWhatMattersMost { get; set; }

		[Outlet]
		UIKit.UITextView txtWhatMattersMost { get; set; }

		[Outlet]
		UIKit.UIView viewHeader { get; set; }

		[Action ("btnBackPressed:")]
		partial void btnBackPressed (Foundation.NSObject sender);

		[Action ("btnUpdateNeededPressed:")]
		partial void btnUpdateNeededPressed (Foundation.NSObject sender);

		[Action ("btnViewEstimatePressed:")]
		partial void btnViewEstimatePressed (Foundation.NSObject sender);

		[Action ("btnYesCapturedCorrectlyPressed:")]
		partial void btnYesCapturedCorrectlyPressed (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnBack != null) {
				btnBack.Dispose ();
				btnBack = null;
			}

			if (btnUpdateNeeded != null) {
				btnUpdateNeeded.Dispose ();
				btnUpdateNeeded = null;
			}

			if (btnViewEstimate != null) {
				btnViewEstimate.Dispose ();
				btnViewEstimate = null;
			}

			if (btnYesCapturedCorrectly != null) {
				btnYesCapturedCorrectly.Dispose ();
				btnYesCapturedCorrectly = null;
			}

			if (imgSubmitCheck != null) {
				imgSubmitCheck.Dispose ();
				imgSubmitCheck = null;
			}

			if (scrollviewWhatMattersMost != null) {
				scrollviewWhatMattersMost.Dispose ();
				scrollviewWhatMattersMost = null;
			}

			if (txtWhatMattersMost != null) {
				txtWhatMattersMost.Dispose ();
				txtWhatMattersMost = null;
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
