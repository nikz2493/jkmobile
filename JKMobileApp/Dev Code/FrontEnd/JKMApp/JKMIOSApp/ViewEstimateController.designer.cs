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
	[Register ("ViewEstimateController")]
	partial class ViewEstimateController
	{
		[Outlet]
		UIKit.UIButton btnBack { get; set; }

		[Outlet]
		UIKit.UIButton btnContactUs { get; set; }

		[Outlet]
		UIKit.UILabel lblEstimateName { get; set; }

		[Outlet]
		UIKit.UILabel lblTitle { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollviewPDFContainer { get; set; }

		[Outlet]
		UIKit.UIWebView uiWebViewPDF { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnContactUs != null) {
				btnContactUs.Dispose ();
				btnContactUs = null;
			}

			if (lblTitle != null) {
				lblTitle.Dispose ();
				lblTitle = null;
			}

			if (scrollviewPDFContainer != null) {
				scrollviewPDFContainer.Dispose ();
				scrollviewPDFContainer = null;
			}

			if (uiWebViewPDF != null) {
				uiWebViewPDF.Dispose ();
				uiWebViewPDF = null;
			}

			if (lblEstimateName != null) {
				lblEstimateName.Dispose ();
				lblEstimateName = null;
			}

			if (btnBack != null) {
				btnBack.Dispose ();
				btnBack = null;
			}
		}
	}
}
