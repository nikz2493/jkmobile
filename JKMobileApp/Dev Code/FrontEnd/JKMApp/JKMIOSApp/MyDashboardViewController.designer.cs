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
	[Register ("MyDashboardViewController")]
	partial class MyDashboardViewController
	{
		[Outlet]
		UIKit.UIButton btnAlert { get; set; }

		[Outlet]
		UIKit.UIButton btnContactUs { get; set; }

		[Outlet]
		UIKit.UIImageView imgChartTruck { get; set; }

		[Outlet]
		UIKit.UIImageView imgMoveStatus { get; set; }

		[Outlet]
		UIKit.UIImageView imgTruckAddress { get; set; }

		[Outlet]
		UIKit.UILabel lblClose { get; set; }

		[Outlet]
		UIKit.UILabel lblCostValue { get; set; }

		[Outlet]
		UIKit.UILabel lblCoverageValue { get; set; }

		[Outlet]
		UIKit.UILabel lblDaysLeft { get; set; }

		[Outlet]
		UIKit.UILabel lblDaysLeftNumber { get; set; }

		[Outlet]
		UIKit.UILabel lblDeclaredValue { get; set; }

		[Outlet]
		UIKit.UILabel lblDestinationAddress { get; set; }

		[Outlet]
		UIKit.UILabel lbldestinationCity { get; set; }

		[Outlet]
		UIKit.UILabel lblLoadFirstDay { get; set; }

		[Outlet]
		UIKit.UILabel lblLoadLastDay { get; set; }

		[Outlet]
		UIKit.UILabel lblMoveStatus { get; set; }

		[Outlet]
		UIKit.UILabel lblOriginAddress { get; set; }

		[Outlet]
		UIKit.UILabel lblOriginCity { get; set; }

		[Outlet]
		UIKit.UILabel lblServiceName { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollview { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollviewAddress { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollviewChart { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewIntro { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollviewMyServices { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollviewStartEndDate { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollviewValuation { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollviewWhatMattersMost { get; set; }

		[Outlet]
		UIKit.UITextView txtViewWhatMattersMost { get; set; }

		[Outlet]
		UIKit.UIView viewChart { get; set; }

		[Outlet]
		UIKit.UIView viewCloseButton { get; set; }

		[Outlet]
		UIKit.UIView viewDaysLeft { get; set; }

		[Outlet]
		UIKit.UIView viewFirstDate { get; set; }

		[Outlet]
		UIKit.UIView viewLastDate { get; set; }

		[Outlet]
		UIKit.UIView viewMyNeedHelpBottom { get; set; }

		[Outlet]
		UIKit.UIView viewMyNeedHelpTop { get; set; }

		[Outlet]
		UIKit.UIScrollView viewNeedHelp { get; set; }

		[Action ("btnAlertPressed:")]
		partial void btnAlertPressed (Foundation.NSObject sender);

		[Action ("btnContactUsPressed:")]
		partial void btnContactUsPressed (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (imgChartTruck != null) {
				imgChartTruck.Dispose ();
				imgChartTruck = null;
			}

			if (imgMoveStatus != null) {
				imgMoveStatus.Dispose ();
				imgMoveStatus = null;
			}

			if (imgTruckAddress != null) {
				imgTruckAddress.Dispose ();
				imgTruckAddress = null;
			}

			if (lblClose != null) {
				lblClose.Dispose ();
				lblClose = null;
			}

			if (lblCostValue != null) {
				lblCostValue.Dispose ();
				lblCostValue = null;
			}

			if (lblCoverageValue != null) {
				lblCoverageValue.Dispose ();
				lblCoverageValue = null;
			}

			if (lblDaysLeft != null) {
				lblDaysLeft.Dispose ();
				lblDaysLeft = null;
			}

			if (lblDaysLeftNumber != null) {
				lblDaysLeftNumber.Dispose ();
				lblDaysLeftNumber = null;
			}

			if (lblDeclaredValue != null) {
				lblDeclaredValue.Dispose ();
				lblDeclaredValue = null;
			}

			if (lblDestinationAddress != null) {
				lblDestinationAddress.Dispose ();
				lblDestinationAddress = null;
			}

			if (lbldestinationCity != null) {
				lbldestinationCity.Dispose ();
				lbldestinationCity = null;
			}

			if (lblLoadFirstDay != null) {
				lblLoadFirstDay.Dispose ();
				lblLoadFirstDay = null;
			}

			if (lblLoadLastDay != null) {
				lblLoadLastDay.Dispose ();
				lblLoadLastDay = null;
			}

			if (lblMoveStatus != null) {
				lblMoveStatus.Dispose ();
				lblMoveStatus = null;
			}

			if (lblOriginAddress != null) {
				lblOriginAddress.Dispose ();
				lblOriginAddress = null;
			}

			if (lblOriginCity != null) {
				lblOriginCity.Dispose ();
				lblOriginCity = null;
			}

			if (lblServiceName != null) {
				lblServiceName.Dispose ();
				lblServiceName = null;
			}

			if (scrollview != null) {
				scrollview.Dispose ();
				scrollview = null;
			}

			if (scrollviewAddress != null) {
				scrollviewAddress.Dispose ();
				scrollviewAddress = null;
			}

			if (scrollviewChart != null) {
				scrollviewChart.Dispose ();
				scrollviewChart = null;
			}

			if (scrollViewIntro != null) {
				scrollViewIntro.Dispose ();
				scrollViewIntro = null;
			}

			if (scrollviewMyServices != null) {
				scrollviewMyServices.Dispose ();
				scrollviewMyServices = null;
			}

			if (scrollviewStartEndDate != null) {
				scrollviewStartEndDate.Dispose ();
				scrollviewStartEndDate = null;
			}

			if (scrollviewValuation != null) {
				scrollviewValuation.Dispose ();
				scrollviewValuation = null;
			}

			if (scrollviewWhatMattersMost != null) {
				scrollviewWhatMattersMost.Dispose ();
				scrollviewWhatMattersMost = null;
			}

			if (txtViewWhatMattersMost != null) {
				txtViewWhatMattersMost.Dispose ();
				txtViewWhatMattersMost = null;
			}

			if (viewChart != null) {
				viewChart.Dispose ();
				viewChart = null;
			}

			if (viewCloseButton != null) {
				viewCloseButton.Dispose ();
				viewCloseButton = null;
			}

			if (viewDaysLeft != null) {
				viewDaysLeft.Dispose ();
				viewDaysLeft = null;
			}

			if (viewFirstDate != null) {
				viewFirstDate.Dispose ();
				viewFirstDate = null;
			}

			if (viewLastDate != null) {
				viewLastDate.Dispose ();
				viewLastDate = null;
			}

			if (viewMyNeedHelpBottom != null) {
				viewMyNeedHelpBottom.Dispose ();
				viewMyNeedHelpBottom = null;
			}

			if (viewMyNeedHelpTop != null) {
				viewMyNeedHelpTop.Dispose ();
				viewMyNeedHelpTop = null;
			}

			if (viewNeedHelp != null) {
				viewNeedHelp.Dispose ();
				viewNeedHelp = null;
			}

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
