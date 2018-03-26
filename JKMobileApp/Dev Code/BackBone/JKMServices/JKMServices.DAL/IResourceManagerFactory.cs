namespace JKMServices.DAL
{
    /// <summary>
    /// Interface Name  : IResourceManagerFactory
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 20 Dec 2017
    /// Purpose         : interface for ResourceManagerFactory class use to inject in DBContext class
    /// Revision        : 
    /// </summary>
    public interface IResourceManagerFactory
    {
        string GetString(string queryName);
    }
}
