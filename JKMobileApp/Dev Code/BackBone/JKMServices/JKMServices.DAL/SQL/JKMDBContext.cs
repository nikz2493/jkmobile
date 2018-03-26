using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core;

namespace JKMServices.DAL.SQL
{
    /// <summary>
    /// Class Name      : JKMDBContext
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 20 Dec 2017
    /// Purpose         : SQL Connection using entity framework DBContext 
    /// Revision        : 
    /// </summary>
    public class JKMDBContext : DbContext,IJKMDBContext
    {
        private readonly IResourceManagerFactory resourceManager;

        public JKMDBContext(IResourceManagerFactory resourceManager) : base()
        {
            this.resourceManager = resourceManager;
        }

        public virtual List<T> GetData<T>(string queryName, params object[] paramters)
        {
            var dataList = this.Database.SqlQuery<T>(resourceManager.GetString(queryName), paramters).ToList();

            return dataList;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual int SaveData<T>(List<T> entity) where T : class
        {
            try
            {
                int retValue = SaveChanges();
                return retValue;
            }
            catch
            {
                return 0;
            }
           
        }
    }
}
