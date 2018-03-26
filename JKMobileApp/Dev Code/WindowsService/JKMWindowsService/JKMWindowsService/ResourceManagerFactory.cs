using System.Resources;

namespace JKMWindowsService
{
    /// <summary>
    /// Class Name      : ResourceManagerFactory
    /// Author          : Ranjana Singh
    /// Creation Date   : 30 Dec 2017
    /// Purpose         : 
    /// Revision        : 
    /// </summary>
    public class ResourceManagerFactory:IResourceManagerFactory
    {
        private readonly ResourceManager resourceManager;
        
        //Constructor
        public ResourceManagerFactory()
        {
            resourceManager = new ResourceManager("JKMWindowsService.Resource", System.Reflection.Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Property Name   : GetString
        /// Author          : Ranjana Singh
        /// Creation Date   : 30 Dec 2017
        /// Purpose         : 
        /// Revision        : 
        /// </summary>
        public string GetString(string queryName)
        {
            return resourceManager.GetString(queryName);
        }
    }
}
