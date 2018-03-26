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
    [Register ("EnterPasswordViewController")]
    partial class EnterPasswordViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnContinue { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnForgotPassoword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgPasswordEntry { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblEnterYourPassword { get; set; }

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
        UIKit.UITextField txtPassword { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnContinue != null) {
                btnContinue.Dispose ();
                btnContinue = null;
            }

            if (btnForgotPassoword != null) {
                btnForgotPassoword.Dispose ();
                btnForgotPassoword = null;
            }

            if (imgPasswordEntry != null) {
                imgPasswordEntry.Dispose ();
                imgPasswordEntry = null;
            }

            if (lblEnterYourPassword != null) {
                lblEnterYourPassword.Dispose ();
                lblEnterYourPassword = null;
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

            if (txtPassword != null) {
                txtPassword.Dispose ();
                txtPassword = null;
            }
        }
    }
}