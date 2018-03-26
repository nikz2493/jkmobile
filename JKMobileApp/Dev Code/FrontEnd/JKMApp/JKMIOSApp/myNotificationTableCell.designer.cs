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
	[Register ("myNotificationTableCell")]
	partial class myNotificationTableCell
	{
		[Outlet]
		UIKit.UIButton btnAddToCalender { get; set; }

		[Outlet]
		UIKit.UIImageView imgNotificationTypeIcon { get; set; }

		[Outlet]
		UIKit.UILabel lblNotificationTime { get; set; }

		[Outlet]
		UIKit.UILabel lblNotificationTitle { get; set; }

		[Outlet]
		UIKit.UIView scrollViewCorner { get; set; }

		[Outlet]
		UIKit.UIView viewNotificationTypeContainer { get; set; }

		[Action ("btnAddToCalenderPressed:")]
		partial void btnAddToCalenderPressed (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnAddToCalender != null) {
				btnAddToCalender.Dispose ();
				btnAddToCalender = null;
			}

			if (imgNotificationTypeIcon != null) {
				imgNotificationTypeIcon.Dispose ();
				imgNotificationTypeIcon = null;
			}

			if (lblNotificationTime != null) {
				lblNotificationTime.Dispose ();
				lblNotificationTime = null;
			}

			if (lblNotificationTitle != null) {
				lblNotificationTitle.Dispose ();
				lblNotificationTitle = null;
			}

			if (scrollViewCorner != null) {
				scrollViewCorner.Dispose ();
				scrollViewCorner = null;
			}

			if (viewNotificationTypeContainer != null) {
				viewNotificationTypeContainer.Dispose ();
				viewNotificationTypeContainer = null;
			}
		}
	}
}
