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
    [Register ("MyNotificationController")]
    partial class MyNotificationController
    {
        [Outlet]
        UIKit.UIButton btnAddToCalender { get; set; }

        [Outlet]
        UIKit.UIButton btnAlert { get; set; }

        [Outlet]
        UIKit.UIButton btnBack { get; set; }

        [Outlet]
        UIKit.UIImageView imgAlertIcon { get; set; }

        [Outlet]
        UIKit.UILabel lblAlertTime { get; set; }

        [Outlet]
        UIKit.UILabel lblAlertTitle { get; set; }

        [Outlet]
        UIKit.UITableView tblView { get; set; }

        [Action ("btnAddToCalenderPressed:")]
        partial void btnAddToCalenderPressed (Foundation.NSObject sender);

        [Action ("btnBackPressed:")]
        partial void btnBackPressed (Foundation.NSObject sender);

        [Action ("btnNotificationPressed:")]
        partial void btnNotificationPressed (Foundation.NSObject sender);
        
        void ReleaseDesignerOutlets ()
        {
            if (btnAlert != null) {
                btnAlert.Dispose ();
                btnAlert = null;
            }

            if (imgAlertIcon != null) {
                imgAlertIcon.Dispose ();
                imgAlertIcon = null;
            }

            if (lblAlertTime != null) {
                lblAlertTime.Dispose ();
                lblAlertTime = null;
            }

            if (lblAlertTitle != null) {
                lblAlertTitle.Dispose ();
                lblAlertTitle = null;
            }

            if (tblView != null) {
                tblView.Dispose ();
                tblView = null;
            }

            if (btnBack != null) {
                btnBack.Dispose ();
                btnBack = null;
            }

            if (btnAddToCalender != null) {
                btnAddToCalender.Dispose ();
                btnAddToCalender = null;
            }
        }
    }
}
