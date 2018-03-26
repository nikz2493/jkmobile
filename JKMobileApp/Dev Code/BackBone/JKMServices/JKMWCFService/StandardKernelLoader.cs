using Ninject;
using System;
using System.Configuration;
using System.Reflection;

namespace JKMWCFService
{
    public class StandardKernelLoader
    {
        public StandardKernel LoadStandardKernel()
        {
            StandardKernel kernel = Utility.General.StandardKernel();
            Assembly assembly = Assembly.GetExecutingAssembly();

            string KernelModuleName = ConfigurationManager.AppSettings["KernelModuleName"];

            if (!string.IsNullOrEmpty(KernelModuleName) && !kernel.HasModule("JKMWCFService.Binding"))
            {
                kernel.Load(assembly);
            }
            return kernel;
        }
    }
}