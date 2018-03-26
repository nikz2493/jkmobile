using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
namespace JKMWindowsService
{
    public class Binding : NinjectModule
    {
        public override void Load()
        {
            // Resource Manager
            Bind<IResourceManagerFactory>().To<ResourceManagerFactory>();

            // Managers
            Bind<AlertManager.IPreMoveConfirmationNotifications>().To<AlertManager.PreMoveConfirmationNotifications>();
            Bind<MoveManager.IMoveDetails>().To<MoveManager.MoveDetails>();

            // Utility
            Bind<Utility.IClientHelper>().To<Utility.ClientHelper>();
            Bind<Utility.IAPIHelper>().To<Utility.APIHelper>();

            // Utility > Log
            Bind<IWindowsService>().To<WindowsService>();
            Bind<Utility.Log.ILogger>().To<Utility.Log.Logger>();
            Bind<Utility.Log.ILoggerStackTrace>().To<Utility.Log.LoggerStackTrace>();
        }
    }
}
