using System.Collections.Generic;
using System.Data.Entity;

namespace JKMServices.DAL.SQL
{
    public interface IJKMDBContext
    {
        List<T> GetData<T>(string queryName, params object[] paramters);
        int SaveData<T>(List<T> entity) where T : class;  
    }
}