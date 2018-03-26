using System.Resources;

namespace JKMServices.DAL
{
    /// <summary>
    /// Class Name      : ResourceManagerFactory
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 20 Dec 2017
    /// Purpose         : to create isntance of resource manager and get key values from resource file using instance.
    /// Revision        : 
    /// </summary>
    public class ResourceManagerFactory:IResourceManagerFactory
    {
        private readonly ResourceManager resourceManager;
        
        //Constructor
        public ResourceManagerFactory()
        {
            resourceManager = new ResourceManager("JKMServices.DAL.Resource", System.Reflection.Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Property Name   : GetQuery
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 20 Dec 2017
        /// Purpose         : get value as per key name from resource file
        /// Revision        : 
        /// </summary>
        public string GetString(string queryName)
        {
            return resourceManager.GetString(queryName);
        }
    }
}
