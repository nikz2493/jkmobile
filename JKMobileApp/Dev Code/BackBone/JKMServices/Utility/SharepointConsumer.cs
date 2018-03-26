using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Resources;
using System.Security;
using Utility.Logger;

namespace Utility
{
    public class SharepointConsumer : ISharepointConsumer
    {
        private readonly ILogger logger;
        private readonly ResourceManager resourceManagerObject;

        public SharepointConsumer(ILogger logger)
        {
            this.logger = logger;
            log4net.Config.XmlConfigurator.Configure();
            resourceManagerObject = new ResourceManager("Utility.Resource", System.Reflection.Assembly.GetExecutingAssembly());
        }
        /// <summary>
        /// Method Name     : GetClientContext
        /// Author          : Jitendra Garg
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : Creates a clientcontext object using base url and the credentials object. 
        /// </summary>
        /// <returns>returns null=Failure,ClientContext Object=Success</returns>
        private ClientContext GetClientContext()
        {
            //Definitions
            string baseUrl;
            ICredentials credentialsObject;
            ClientContext clientContextObject;

            //Get Base URL
            baseUrl = GetBaseUrl();

            //Check for errors in the last function call
            if (baseUrl == String.Empty)
            {
                logger.Info(resourceManagerObject.GetString("logBaseUrl"));
            }

            //Get Sharepoint Credentials
            credentialsObject = GetSharePointCredentials();

            //Check for errors in the last function call
            if (credentialsObject is null)
            {
                logger.Info(resourceManagerObject.GetString("logCredentialsObject"));
            }

            //Create ClientContext object
            clientContextObject = new ClientContext(baseUrl);
            clientContextObject.Credentials = credentialsObject;

            return clientContextObject;
        }

        /// <summary>
        /// Method Name     : GetBaseUrl
        /// Author          : Jitendra Garg
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : Gets base url from resources
        /// </summary>
        /// <returns>returns String.Empty=Failure,baseURL=Success</returns>
        private string GetBaseUrl()
        {
            //Definitions
            string baseUrl;

            //If null, then return empty string. 
            if (resourceManagerObject is null)
            {
                logger.Info(resourceManagerObject.GetString("logResourceManagerObject"));
                return string.Empty;
            }
            else  //Resource file exists and is accessible.
            {
                //If resource key is modified, this line should reflect the new key.
                baseUrl = resourceManagerObject.GetString("sharepointBaseUrl");

                //If string is empty or null, return empty string.
                if (baseUrl is null || baseUrl == String.Empty)
                {
                    logger.Info(resourceManagerObject.GetString("logBaseUrl"));
                    return String.Empty;
                }
                else  //BaseURL found in resource. Return baseURL to the calling method.
                {
                    return baseUrl;
                }
            }
        }

        /// <summary>
        /// Method Name     : GetSharePointCredentials
        /// Author          : Jitendra Garg
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : Gets credential object based on the username and password stored in configuration.
        /// </summary>
        /// <returns>returns null=Failure,ICredential Object=Success</returns>
        private ICredentials GetSharePointCredentials()
        {
            //Definitions
            string userNameString;
            string passwordInsecureString;
            SecureString securePasswordString;
            ICredentials credentialsObject;

            //If null, then return null. 
            if (resourceManagerObject is null)
            {
                logger.Info(resourceManagerObject.GetString("logResourceManagerObject"));
                return null;
            }
            else  //Resource file exists and is accessible.
            {
                //If resource key is modified, these two lines should reflect the new key.
                userNameString = resourceManagerObject.GetString("sharepointUsername");
                passwordInsecureString = resourceManagerObject.GetString("sharepointPassword");

                //If strings are empty or null, return null.
                if (userNameString is null || userNameString == String.Empty || passwordInsecureString is null || passwordInsecureString == String.Empty)
                {
                    logger.Info(resourceManagerObject.GetString("logUsernamePassword"));
                    return null;
                }
                else  //Resource found. Create object of ICredentials.
                {
                    //Create secure string of password to be used for credentials.
                    securePasswordString = new SecureString();
                    foreach (char singlePasswordCharacter in passwordInsecureString)
                    {
                        securePasswordString.AppendChar(singlePasswordCharacter);
                    }

                    //Create credential object based on username and password
                    credentialsObject = new NetworkCredential(userNameString, securePasswordString);
                    return credentialsObject;
                }
            }
        }

        /// <summary>
        /// Method Name     : GetDocument
        /// Author          : Ranjana Singh
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : Gets the document from Sharepoint and returns it.
        /// </summary>
        /// <param name="filePath">Path of the sharepoint document.</param>
        /// <returns>returns null=Failure,FileInformation Object=Success</returns>
        private FileInformation GetDocument(string filePath)
        {
            try
            {
                //Gets the Client Context to be used for accessing file from Sharepoint.
                ClientContext clientContext = GetClientContext();

                //Gets the file from Sharepoint based on the file path.
                Uri filePathURI = new Uri(Path.Combine(clientContext.Url, filePath));
                FileInformation file = Microsoft.SharePoint.Client.File.OpenBinaryDirect(clientContext, filePathURI.AbsolutePath);
                clientContext.ExecuteQuery();

                return file;
            }
            catch (Exception ex)
            {
                logger.Error(resourceManagerObject.GetString("logDcoumentExists"), ex);
                return null;
            }
        }

        /// <summary>
        /// Method Name     : HandleResult
        /// Author          : Ranjana Singh
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : Gets the Sharepoint document and converts it into byte[] -> Base64 string.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>returns null=Failure,byte[]=Success</returns>
        public byte[] HandleResult(string filePath)
        {
            int count;
            try
            {
                FileInformation file = GetDocument(filePath);

                byte[] bytes = new byte[16384];
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    while ((count = file.Stream.Read(bytes, 0, bytes.Length)) > 0)
                    {
                        memoryStream.Write(bytes, 0, count);
                    }
                    return memoryStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                logger.Error(resourceManagerObject.GetString("logDcoumentExists"), ex);
                return null;
            }
        }

        /// <summary>
        /// Method Name     : GetDocumentList
        /// Author          : Ranjana Singh
        /// Creation Date   : 09 Feb 2018
        /// Purpose         : Gets the list of all the Sharepoint document.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>returns null=Failure,list<string>=Success</returns>
        public List<string> GetDocumentList(string filePath)
        {
            try
            {
                ClientContext clientContext = GetClientContext();

                Uri filePathURI = new Uri(Path.Combine(clientContext.Url, filePath));
                List list = clientContext.Web.Lists.GetByTitle(filePathURI.AbsolutePath);

                clientContext.Load(list);
                clientContext.Load(list.RootFolder);
                clientContext.Load(list.RootFolder.Folders);
                clientContext.Load(list.RootFolder.Files);
                clientContext.ExecuteQuery();
                FolderCollection fcol = list.RootFolder.Folders;
                List<string> lstFile = new List<string>();
                foreach (Folder f in fcol)
                {
                    if (f.Name == filePath)
                    {
                        clientContext.Load(f.Files);
                        clientContext.ExecuteQuery();
                        FileCollection fileCol = f.Files;
                        foreach (Microsoft.SharePoint.Client.File file in fileCol)
                        {
                            lstFile.Add(file.Name);
                        }
                        logger.Info(resourceManagerObject.GetString("logDocumentFetchedSuccessfully"));
                        return lstFile;
                    }
                }
                return new List<string>();
            }
            catch (Exception ex)
            {
                logger.Error(resourceManagerObject.GetString("logErrorWhileFetchingDocuments"), ex);
                #warning mocked the data in below line. Remove when not required and return "new List<string>()" instead.
                return new List<string> { "Sharepoint Mocked Doc 1", "Sharepoint Mocked Doc 2", "Sharepoint Mocked Doc 3" };
            }
        }
    }
}
