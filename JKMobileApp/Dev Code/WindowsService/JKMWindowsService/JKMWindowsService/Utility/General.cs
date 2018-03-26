using Newtonsoft.Json;
using Ninject;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace JKMWindowsService.Utility
{
    public static class General
    {
        private static readonly StandardKernel standardKernel = new StandardKernel();
        
        public static StandardKernel StandardKernel()
        {
            return standardKernel;
        }

        public static T Resolve<T>(params Ninject.Parameters.IParameter[] param)
        {
            return standardKernel.Get<T>(param);
        }

        public static T Resolve<T>(Type type, params Ninject.Parameters.IParameter[] param)
        {
            return (T)standardKernel.Get(type, param);
        }

        public static StandardKernel LoadStandardKernel()
        {
            StandardKernel kernel = StandardKernel();
            Assembly assembly = Assembly.GetExecutingAssembly();

            string KernelModuleName = ConfigurationManager.AppSettings["KernelModuleName"];

            if (!string.IsNullOrEmpty(KernelModuleName) && !kernel.HasModule("JKMWindowsService.Binding"))
            {
                kernel.Load(assembly);
            }
            return kernel;
        }

        /// <summary>
        /// Method Name     : ConvertToJson
        /// Author          : Pratik Soni
        /// Creation Date   : 22 Feb 2018
        /// Purpose         : Convert to json object from any type of object
        /// Revision        : 
        /// </summary>
        public static string ConvertToJson<T>(T val)
        {
            return JsonConvert.SerializeObject(val);
        }
        /// <summary>
        /// Method Name     : ConvertFromJson
        /// Author          : Pratik Soni
        /// Creation Date   : 22 Feb 2018
        /// Purpose         : Convert to any type of object from json
        /// Revision        : 
        /// </summary>
        public static T ConvertFromJson<T>(string jsonValue)
        {
            return (T)JsonConvert.DeserializeObject(jsonValue, typeof(T));
        }


        /// <summary>
        /// Method Name     : WriteDTOListToXMLFile
        /// Author          : Pratik Soni
        /// Creation Date   : 26 Feb 2018
        /// Purpose         : Create XML file for List of Object (here model of DTO).
        /// Revision        : 
        /// </summary>
        /// <param name="modelList"></param>
        /// <param name="fileName">Name of XML fileName from which data needs to be read</param>
        public static bool WriteDTOListToXMLFile<T>(T modelList, string fileName)
        {
            try
            {
                XmlSerializer writer = new XmlSerializer(typeof(T));

                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
                FileStream file = File.Create(path);

                writer.Serialize(file, modelList);
                file.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Method Name     : ReadDTOListFromXML
        /// Author          : Pratik Soni
        /// Creation Date   : 26 Feb 2018
        /// Purpose         : To read XML and convert it into T object (here in List of Model)
        /// Revision        : 
        /// </summary>
        /// <param name="fileName">Name of XML fileName from which data needs to be read</param>
        /// <returns>IF Error - null; ELSE object</returns>
        public static T ReadDTOListFromXML<T>(string fileName)
        {
            try
            {
                // Now we can read the serialized Model ...  
                XmlSerializer reader = new XmlSerializer(typeof(T));
                StreamReader file = new StreamReader(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName)); 
                T modelList = (T)reader.Deserialize(file);
                file.Close();
                return modelList;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Method Name     : UpdateConfigValueForCount
        /// Author          : Pratik Soni
        /// Creation Date   : 26 Feb 2018
        /// Purpose         : To update the Count in config value
        /// Revision        : 
        /// </summary>
        /// <param name="reset"> Boolean input parameter to check if we need to reset the config value or increment required. </param>
        /// <returns>IF Error - null; ELSE object</returns>
        public static string UpdateConfigValueForCount(bool reset = false)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var count = int.Parse(config.AppSettings.Settings["Count"].Value);
            if (reset)
            {
                count = 0;
            }
            else
            {
                count = count + 1;
            }
            config.AppSettings.Settings["Count"].Value = count.ToString();
            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
            return config.AppSettings.Settings["Count"].Value;
        }

        /// <summary>
        /// Method Name     : GetAPIPath
        /// Author          : Pratik Soni
        /// Creation Date   : 27 Feb 2018
        /// Purpose         : To create API path
        /// Revision        : 
        /// </summary>
        /// <param name="serviceName">Contains the service name (e.g. Move.svc) </param>
        /// <param name="uriPath">Contains the tailing detail to call api method (e.g. list) </param>
        public static string GetAPIPath(string serviceName, string uriPath)
        {
            string baseURL = "http://13.92.188.152:88/";
            string apiPath = string.Empty;
            apiPath = baseURL + serviceName + uriPath;
            return apiPath;
        }
    }
}
