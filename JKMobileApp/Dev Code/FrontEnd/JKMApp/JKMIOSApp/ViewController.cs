using Foundation;
using System;
using UIKit;

namespace JKMIOSApp
{
    [System.ComponentModel.DesignTimeVisible(false)]
    public partial class ViewController : UIViewController
    {
        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationItem.Title = "Custom Title";

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.     
        }

		public override bool ShouldAutorotate()
		{
			return false;

		}
        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return UIInterfaceOrientationMask.Portrait;
		}
    }
}