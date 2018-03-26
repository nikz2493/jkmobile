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
	[Register ("MyDocumentViewController")]
	partial class MyDocumentViewController
	{
		[Outlet]
		UIKit.UIButton btnAlert { get; set; }

		[Outlet]
		UIKit.UIButton btnContactUs { get; set; }

		[Outlet]
		UIKit.UITableView tableViewMyDocumentList { get; set; }

		[Action ("btnAlertPressed:")]
		partial void btnAlertPressed (Foundation.NSObject sender);

		[Action ("btnContactUsPressed:")]
		partial void btnContactUsPressed (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnAlert != null) {
				btnAlert.Dispose ();
				btnAlert = null;
			}

			if (btnContactUs != null) {
				btnContactUs.Dispose ();
				btnContactUs = null;
			}

			if (tableViewMyDocumentList != null) {
				tableViewMyDocumentList.Dispose ();
				tableViewMyDocumentList = null;
			}
		}
	}
}
