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
	[Register ("AddressesVewController")]
	partial class AddressesVewController
	{
		[Outlet]
		UIKit.UIButton btnBack { get; set; }

		[Outlet]
		UIKit.UIButton btnConfirm { get; set; }

		[Outlet]
		UIKit.UIButton btnUpdateAddresses { get; set; }

		[Outlet]
		UIKit.UIButton btnViewEstimate { get; set; }

		[Outlet]
		UIKit.UIImageView imgSubmitCheck { get; set; }

		[Outlet]
		UIKit.UILabel lblDescription { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollviewAddresses { get; set; }

		[Outlet]
		UIKit.UITextView txtDestinationAddress { get; set; }

		[Outlet]
		UIKit.UITextView txtOriginAddress { get; set; }

		[Outlet]
		UIKit.UIView viewHeader { get; set; }

		[Action ("btnBackPressed:")]
		partial void btnBackPressed (Foundation.NSObject sender);

		[Action ("btnConfirmPressed:")]
		partial void btnConfirmPressed (Foundation.NSObject sender);

		[Action ("btnUpdateAddressPressed:")]
		partial void btnUpdateAddressPressed (Foundation.NSObject sender);

		[Action ("btnViewEstimatePressed:")]
		partial void btnViewEstimatePressed (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnBack != null) {
				btnBack.Dispose ();
				btnBack = null;
			}

			if (btnConfirm != null) {
				btnConfirm.Dispose ();
				btnConfirm = null;
			}

			if (btnUpdateAddresses != null) {
				btnUpdateAddresses.Dispose ();
				btnUpdateAddresses = null;
			}

			if (btnViewEstimate != null) {
				btnViewEstimate.Dispose ();
				btnViewEstimate = null;
			}

			if (imgSubmitCheck != null) {
				imgSubmitCheck.Dispose ();
				imgSubmitCheck = null;
			}

			if (scrollviewAddresses != null) {
				scrollviewAddresses.Dispose ();
				scrollviewAddresses = null;
			}

			if (txtDestinationAddress != null) {
				txtDestinationAddress.Dispose ();
				txtDestinationAddress = null;
			}

			if (txtOriginAddress != null) {
				txtOriginAddress.Dispose ();
				txtOriginAddress = null;
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
