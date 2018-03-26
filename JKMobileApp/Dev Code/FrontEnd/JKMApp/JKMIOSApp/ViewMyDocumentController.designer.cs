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
	[Register ("ViewMyDocumentController")]
	partial class ViewMyDocumentController
	{
		[Outlet]
		UIKit.UIButton btnBack { get; set; }

		[Outlet]
		UIKit.UILabel lblDocumentTitle { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewDocumentContainer { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewPackDate { get; set; }

		[Outlet]
		UIKit.UIWebView uiVebViewDocument { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnBack != null) {
				btnBack.Dispose ();
				btnBack = null;
			}

			if (lblDocumentTitle != null) {
				lblDocumentTitle.Dispose ();
				lblDocumentTitle = null;
			}

			if (uiVebViewDocument != null) {
				uiVebViewDocument.Dispose ();
				uiVebViewDocument = null;
			}

			if (scrollViewDocumentContainer != null) {
				scrollViewDocumentContainer.Dispose ();
				scrollViewDocumentContainer = null;
			}

			if (scrollViewPackDate != null) {
				scrollViewPackDate.Dispose ();
				scrollViewPackDate = null;
			}
		}
	}
}
