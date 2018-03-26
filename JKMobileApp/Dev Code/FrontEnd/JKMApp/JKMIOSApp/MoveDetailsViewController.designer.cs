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
	[Register ("MoveDetailsViewController")]
	partial class MoveDetailsViewController
	{
		[Outlet]
		UIKit.UIButton btnAlert { get; set; }

		[Outlet]
		UIKit.UIButton btnContactUs { get; set; }

		[Outlet]
		UIKit.UIImageView imgArrowDeliveryDate { get; set; }

		[Outlet]
		UIKit.UIImageView imgDeliveryDate { get; set; }

		[Outlet]
		UIKit.UILabel lblDeliveryDate { get; set; }

		[Outlet]
		UIKit.UILabel lblDeliveryDateTitle { get; set; }

		[Outlet]
		UIKit.UILabel lblLoadDate { get; set; }

		[Outlet]
		UIKit.UILabel lblPackDate { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewDeliveryDate { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewLoadDate { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewPackDate { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imgArrowDeliveryDate != null) {
				imgArrowDeliveryDate.Dispose ();
				imgArrowDeliveryDate = null;
			}

			if (imgDeliveryDate != null) {
				imgDeliveryDate.Dispose ();
				imgDeliveryDate = null;
			}

			if (lblDeliveryDate != null) {
				lblDeliveryDate.Dispose ();
				lblDeliveryDate = null;
			}

			if (lblDeliveryDateTitle != null) {
				lblDeliveryDateTitle.Dispose ();
				lblDeliveryDateTitle = null;
			}

			if (lblLoadDate != null) {
				lblLoadDate.Dispose ();
				lblLoadDate = null;
			}

			if (lblPackDate != null) {
				lblPackDate.Dispose ();
				lblPackDate = null;
			}

			if (scrollViewDeliveryDate != null) {
				scrollViewDeliveryDate.Dispose ();
				scrollViewDeliveryDate = null;
			}

			if (scrollViewLoadDate != null) {
				scrollViewLoadDate.Dispose ();
				scrollViewLoadDate = null;
			}

			if (scrollViewPackDate != null) {
				scrollViewPackDate.Dispose ();
				scrollViewPackDate = null;
			}

			if (btnAlert != null) {
				btnAlert.Dispose ();
				btnAlert = null;
			}

			if (btnContactUs != null) {
				btnContactUs.Dispose ();
				btnContactUs = null;
			}
		}
	}
}
