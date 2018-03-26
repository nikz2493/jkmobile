using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
namespace UnitTests
{
    public class Binding : NinjectModule
    {
        public override void Load()
        {
            // Resource Manager
            Bind<JKMServices.BLL.Interface.IResourceManagerFactory>().To<JKMServices.BLL.ResourceManagerFactory>();
            Bind<JKMServices.DAL.IResourceManagerFactory>().To<JKMServices.DAL.ResourceManagerFactory>();

            // BLLs
            Bind<JKMServices.BLL.Interface.ICustomerDetails>().To<JKMServices.BLL.CustomerDetails>();
            Bind<JKMServices.BLL.Interface.IMoveDetails>().To<JKMServices.BLL.MoveDetails>();
            Bind<JKMServices.BLL.Interface.IDocumentDetails>().To<JKMServices.BLL.DocumentDetails>();

            // DAL > SQLs
            Bind<JKMServices.DAL.SQL.IJKMDBContext>().To<JKMServices.DAL.SQL.JKMDBContext>().InSingletonScope();
            Bind<JKMServices.DAL.CRM.IDocumentDetails>().To<JKMServices.DAL.CRM.DocumentDetails>();

            // DAL > CRM > Common
            Bind<JKMServices.DAL.CRM.common.ICRMTODTOMapper>().To<JKMServices.DAL.CRM.common.CRMTODTOMapper>();
            Bind<JKMServices.DAL.CRM.common.ICRMUtilities>().To<JKMServices.DAL.CRM.common.CRMUtilities>();

            // DAL > CRM 
            Bind<JKMServices.DAL.CRM.ICustomerDetails>().To<JKMServices.DAL.CRM.CustomerDetails>();
            Bind<JKMServices.DAL.CRM.IMoveDetails>().To<JKMServices.DAL.CRM.MoveDetails>();

            Bind<Utility.IGenerator>().To<Utility.Generator>();
        }
    }
}
