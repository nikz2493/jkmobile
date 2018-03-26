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
	[Register ("EstimateListViewController")]
	partial class EstimateListViewController
	{
		[Outlet]
		UIKit.UIButton btnBookSelectedEstimate { get; set; }

		[Outlet]
		UIKit.UIButton btnContactUs { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollviewTableContainer { get; set; }

		[Outlet]
		UIKit.UITableView tableViewEstimateList { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnContactUs != null) {
				btnContactUs.Dispose ();
				btnContactUs = null;
			}

			if (scrollviewTableContainer != null) {
				scrollviewTableContainer.Dispose ();
				scrollviewTableContainer = null;
			}

			if (tableViewEstimateList != null) {
				tableViewEstimateList.Dispose ();
				tableViewEstimateList = null;
			}

			if (btnBookSelectedEstimate != null) {
				btnBookSelectedEstimate.Dispose ();
				btnBookSelectedEstimate = null;
			}
		}
	}
}
