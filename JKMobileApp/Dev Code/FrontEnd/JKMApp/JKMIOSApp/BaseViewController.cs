using System;
using UIKit;
using JKMPCL.Services;
namespace JKMIOSApp
{
    /// <summary>
    /// Controller Name : BaseViewController
    /// Author          : Hiren Patel
    /// Creation Date   : 29 Dec 2017
    /// Purpose         : To share some common function and object or service object
    /// Revision        : 
    /// </summary>
    public class BaseViewController : UIViewController
    {
        public readonly Login loginServices;

        public BaseViewController()
        {
            
        }
        public BaseViewController(IntPtr handle) : base(handle)
        {
            loginServices = new Login();
        }
        public override void ViewDidLoad()
        {
            // will use for creating object of services
        }
    }
}
