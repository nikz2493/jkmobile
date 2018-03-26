using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Reflection;

namespace JKMServices.DAL.SQL
{
    public class GenericRepository<T> where T : class
    {
        private readonly JKMServices.DAL.SQL.JKMDBContext jKMDBContext;
        public GenericRepository(JKMServices.DAL.SQL.JKMDBContext jKMDBContext)
        {
            this.jKMDBContext = jKMDBContext;
        }

        public DbSet Set()
        {
            return jKMDBContext.Set<T>();
        }
    }
}
