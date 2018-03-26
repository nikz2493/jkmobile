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
	[Register ("estimateListTableCell")]
	partial class estimateListTableCell
	{
		[Outlet]
		UIKit.UIButton btnSelectEstimate { get; set; }

		[Outlet]
		UIKit.UIImageView imgViewEstimate { get; set; }

		[Outlet]
		UIKit.UILabel lblEstimateTitle { get; set; }

		[Outlet]
		UIKit.UILabel lblExcessValuation { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnSelectEstimate != null) {
				btnSelectEstimate.Dispose ();
				btnSelectEstimate = null;
			}

			if (lblEstimateTitle != null) {
				lblEstimateTitle.Dispose ();
				lblEstimateTitle = null;
			}

			if (lblExcessValuation != null) {
				lblExcessValuation.Dispose ();
				lblExcessValuation = null;
			}

			if (imgViewEstimate != null) {
				imgViewEstimate.Dispose ();
				imgViewEstimate = null;
			}
		}
	}
}
