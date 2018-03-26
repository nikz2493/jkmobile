using Ninject;
using System;

namespace JKMWindowsService
{
    static class Program
    {
        private readonly static StandardKernel kernel;
        private readonly static IWindowsService windowsService;

        static Program()
        {
            kernel = Utility.General.StandardKernel();
            kernel = Utility.General.LoadStandardKernel();
            windowsService = Utility.General.Resolve<IWindowsService>();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //For debugging the service code
            if (Environment.UserInteractive)
            {
                 windowsService.TestStartupAndStop(args);
            }
        }
    }
}
