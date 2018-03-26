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
	[Register ("MyMoreViewController")]
	partial class MyMoreViewController
	{
		[Outlet]
		UIKit.UIButton btnAboutUS { get; set; }

		[Outlet]
		UIKit.UIButton btnAboutUsIcon { get; set; }

		[Outlet]
		UIKit.UIButton btnClose { get; set; }

		[Outlet]
		UIKit.UIButton btnColseIocn { get; set; }

		[Outlet]
		UIKit.UIButton btnMoveDetails { get; set; }

		[Outlet]
		UIKit.UIButton btnTerms { get; set; }

		[Outlet]
		UIKit.UIButton btnTermsIcon { get; set; }

		[Outlet]
		UIKit.UIView MenuView { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollviewData { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollviewOption { get; set; }

		[Action ("btnAboutUs:")]
		partial void btnAboutUs (Foundation.NSObject sender);

		[Action ("btnAlertPressed:")]
		partial void btnAlertPressed (Foundation.NSObject sender);

		[Action ("btnClosePressed:")]
		partial void btnClosePressed (Foundation.NSObject sender);

		[Action ("btnContactPressed:")]
		partial void btnContactPressed (Foundation.NSObject sender);

		[Action ("BtnOptionPressed:")]
		partial void BtnOptionPressed (Foundation.NSObject sender);

		[Action ("btnTermsPressed:")]
		partial void btnTermsPressed (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnAboutUS != null) {
				btnAboutUS.Dispose ();
				btnAboutUS = null;
			}

			if (btnAboutUsIcon != null) {
				btnAboutUsIcon.Dispose ();
				btnAboutUsIcon = null;
			}

			if (btnClose != null) {
				btnClose.Dispose ();
				btnClose = null;
			}

			if (btnColseIocn != null) {
				btnColseIocn.Dispose ();
				btnColseIocn = null;
			}

			if (btnTerms != null) {
				btnTerms.Dispose ();
				btnTerms = null;
			}

			if (btnTermsIcon != null) {
				btnTermsIcon.Dispose ();
				btnTermsIcon = null;
			}

			if (MenuView != null) {
				MenuView.Dispose ();
				MenuView = null;
			}

			if (scrollviewData != null) {
				scrollviewData.Dispose ();
				scrollviewData = null;
			}

			if (scrollviewOption != null) {
				scrollviewOption.Dispose ();
				scrollviewOption = null;
			}

			if (btnMoveDetails != null) {
				btnMoveDetails.Dispose ();
				btnMoveDetails = null;
			}
		}
	}
}
