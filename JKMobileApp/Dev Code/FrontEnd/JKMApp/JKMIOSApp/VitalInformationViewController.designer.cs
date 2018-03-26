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
	[Register ("VitalInformationViewController")]
	partial class VitalInformationViewController
	{
		[Outlet]
		UIKit.UIButton btnBack { get; set; }

		[Outlet]
		UIKit.UIButton btnInformationReceived { get; set; }

		[Outlet]
		UIKit.UIButton btnViewRightsResponsibilities { get; set; }

		[Outlet]
		UIKit.UILabel lblDescription { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewPDFContainer { get; set; }

		[Outlet]
		UIKit.UIWebView uiWebViewVitalIformationPDF { get; set; }

		[Outlet]
		UIKit.UIView viewHeader { get; set; }

		[Outlet]
		UIKit.UIWebView webViewPDF { get; set; }

		[Action ("btnBackPressed:")]
		partial void btnBackPressed (Foundation.NSObject sender);

		[Action ("btnInformationReceivedPressed:")]
		partial void btnInformationReceivedPressed (Foundation.NSObject sender);

		[Action ("btnViewRightsResponsibilitiesPressed:")]
		partial void btnViewRightsResponsibilitiesPressed (Foundation.NSObject sender);

		[Action ("uiWebViewVitalIformationPDFPressed:")]
		partial void uiWebViewVitalIformationPDFPressed (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnBack != null) {
				btnBack.Dispose ();
				btnBack = null;
			}

			if (btnInformationReceived != null) {
				btnInformationReceived.Dispose ();
				btnInformationReceived = null;
			}

			if (btnViewRightsResponsibilities != null) {
				btnViewRightsResponsibilities.Dispose ();
				btnViewRightsResponsibilities = null;
			}

			if (scrollViewPDFContainer != null) {
				scrollViewPDFContainer.Dispose ();
				scrollViewPDFContainer = null;
			}

			if (uiWebViewVitalIformationPDF != null) {
				uiWebViewVitalIformationPDF.Dispose ();
				uiWebViewVitalIformationPDF = null;
			}

			if (viewHeader != null) {
				viewHeader.Dispose ();
				viewHeader = null;
			}

			if (webViewPDF != null) {
				webViewPDF.Dispose ();
				webViewPDF = null;
			}

			if (lblDescription != null) {
				lblDescription.Dispose ();
				lblDescription = null;
			}
		}
	}
}
