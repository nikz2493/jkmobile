using System.Globalization;
using System.Threading;
using UIKit;

namespace JKMIOSApp
{
    public static class Application
    {
        // This is the main entry point of the application.
        private static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.

            CultureInfo ci = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = ci;
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}