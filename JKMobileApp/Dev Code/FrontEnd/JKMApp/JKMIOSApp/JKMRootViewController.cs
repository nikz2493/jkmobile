using Foundation;
using System;
using UIKit;

namespace JKMIOSApp
{
    /// <summary>
    /// Controller Name : JKMRootViewController
    /// Author          : Hiren Patel
    /// Creation Date   : 16 JAN 2018
    /// Purpose         : For initial screen as root view controller 
    /// Revision        : 
    /// </summary>
    [System.ComponentModel.DesignTimeVisible(false)]
    public partial class JKMRootViewController : UIViewController
    {
        public JKMRootViewController(IntPtr handle) : base(handle)
        {
        }
        public override void ViewDidLoad()
        {

            foreach (var family in UIFont.FamilyNames)
            {
                System.Diagnostics.Debug.WriteLine($"{family}");

                foreach (var names in UIFont.FontNamesForFamilyName(family))
                {
                    System.Diagnostics.Debug.WriteLine($"{names}");
                }
            }
            EmailViewController encounterViewController = this.Storyboard.InstantiateViewController("EmailView") as EmailViewController;
            this.View.AddSubview(encounterViewController.View);
        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}