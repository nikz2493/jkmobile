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
    [Register ("PrivacyPolicyViewController")]
    partial class PrivacyPolicyViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnAgree { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnDisagree { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIWebView webViewPDF { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnAgree != null) {
                btnAgree.Dispose ();
                btnAgree = null;
            }

            if (btnDisagree != null) {
                btnDisagree.Dispose ();
                btnDisagree = null;
            }

            if (lblTitle != null) {
                lblTitle.Dispose ();
                lblTitle = null;
            }

            if (webViewPDF != null) {
                webViewPDF.Dispose ();
                webViewPDF = null;
            }
        }
    }
}