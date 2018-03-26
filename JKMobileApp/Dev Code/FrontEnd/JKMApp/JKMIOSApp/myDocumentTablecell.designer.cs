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
	[Register ("myDocumentTablecell")]
	partial class myDocumentTablecell
	{
		[Outlet]
		UIKit.UIImageView imgDocumentType { get; set; }

		[Outlet]
		UIKit.UIImageView imgVIewDocument { get; set; }

		[Outlet]
		UIKit.UILabel lblDocumentId { get; set; }

		[Outlet]
		UIKit.UILabel lblDocumentTitle { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewListItem { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewListItemContainer { get; set; }

		[Outlet]
		UIKit.UIView viewDocumentImageTypeContainer { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (scrollViewListItem != null) {
				scrollViewListItem.Dispose ();
				scrollViewListItem = null;
			}

			if (scrollViewListItemContainer != null) {
				scrollViewListItemContainer.Dispose ();
				scrollViewListItemContainer = null;
			}

			if (imgDocumentType != null) {
				imgDocumentType.Dispose ();
				imgDocumentType = null;
			}

			if (viewDocumentImageTypeContainer != null) {
				viewDocumentImageTypeContainer.Dispose ();
				viewDocumentImageTypeContainer = null;
			}

			if (lblDocumentTitle != null) {
				lblDocumentTitle.Dispose ();
				lblDocumentTitle = null;
			}

			if (imgVIewDocument != null) {
				imgVIewDocument.Dispose ();
				imgVIewDocument = null;
			}

			if (lblDocumentId != null) {
				lblDocumentId.Dispose ();
				lblDocumentId = null;
			}
		}
	}
}
