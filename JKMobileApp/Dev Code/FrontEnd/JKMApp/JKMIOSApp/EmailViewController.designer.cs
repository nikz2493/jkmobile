// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace JKMIOSApp
{
    [Register ("EmailViewController")]
    partial class EmailViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnContinue { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgEmailIcon { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblEnterYourEmailID { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIScrollView svEmaliMiddle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIScrollView svHeaderImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIScrollView svMain { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtEmailID { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnContinue != null) {
                btnContinue.Dispose ();
                btnContinue = null;
            }

            if (imgEmailIcon != null) {
                imgEmailIcon.Dispose ();
                imgEmailIcon = null;
            }

            if (lblEnterYourEmailID != null) {
                lblEnterYourEmailID.Dispose ();
                lblEnterYourEmailID = null;
            }

            if (svEmaliMiddle != null) {
                svEmaliMiddle.Dispose ();
                svEmaliMiddle = null;
            }

            if (svHeaderImage != null) {
                svHeaderImage.Dispose ();
                svHeaderImage = null;
            }

            if (svMain != null) {
                svMain.Dispose ();
                svMain = null;
            }

            if (txtEmailID != null) {
                txtEmailID.Dispose ();
                txtEmailID = null;
            }
        }
    }
}