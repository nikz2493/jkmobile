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
	[Register ("MyTeamViewController")]
	partial class MyTeamViewController
	{
		[Outlet]
		UIKit.UIButton btnAlert { get; set; }

		[Outlet]
		UIKit.UIButton btnContactUs { get; set; }

		[Action ("btnAlertPressed:")]
		partial void btnAlertPressed (Foundation.NSObject sender);

		[Action ("btnContactOptionPressed:")]
		partial void btnContactOptionPressed (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnContactUs != null) {
				btnContactUs.Dispose ();
				btnContactUs = null;
			}

			if (btnAlert != null) {
				btnAlert.Dispose ();
				btnAlert = null;
			}
		}
	}
}
