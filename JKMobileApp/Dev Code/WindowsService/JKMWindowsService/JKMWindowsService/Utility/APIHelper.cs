using JKMWindowsService.Utility.Log;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JKMWindowsService.Utility
{
    public class APIHelper : IAPIHelper
    {
        private readonly ClientHelper clientHelper;
        private readonly IResourceManagerFactory resourceManager;
        private readonly ILogger logger;

        /// <summary>
        /// Constructor Name        : APIHelper
        /// Author                  : Pratik Soni
        /// Creation Date           : 14 Feb 2018
        /// Purpose                 : To create instant of ClientHelper class
        /// Revision                : 
        /// </summary>
        public APIHelper(ClientHelper clientHelper,
                         IResourceManagerFactory resourceManager,
                         ILogger logger)
        {
            this.clientHelper = clientHelper;
            this.resourceManager = resourceManager;
            this.logger = logger;
        }

        /// <summary>
        /// Method Name     : InvokeGetAPI
        /// Author          : Pratik Soni
        /// Creation Date   : 14 Feb 2018
        /// Purpose         : Invokes the get API request.
        /// Revision : 
        /// </summary>
        /// <param name="apiName">API name.</param>
        public HttpResponseMessage InvokeGetAPI(string apiName)
        {
            HttpClient httpClient = clientHelper.GetAuthenticateClient();
            HttpRequestMessage request;
            request = new HttpRequestMessage(HttpMethod.Get, apiName);
                        
            HttpResponseMessage httpResponseMessage = httpClient.SendAsync(request).Result;
            return httpResponseMessage;

        }

        /// <summary>
        /// Method Name     : InvokePutAPi
        /// Author          : Pratik Soni
        /// Creation Date   : 14 Feb 2018
        /// Purpose         : Invokes the put API request for update.
        /// Revision : 
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        /// <param name="apiName">API name.</param>
        /// <param name="body">Body.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public async Task<HttpResponseMessage> InvokePutAPI<T>(string apiName, T body)
        {
            using (var httpClient = clientHelper.GetAuthenticateClient())
            {
                logger.Info("Put request invoked");
                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(body), Encoding.UTF8, resourceManager.GetString("msgAppJson"));
                return await httpClient.PutAsync(apiName, content);
            }
        }

        /// <summary>
        /// Method Name     : InvokePostAPI
        /// Author          : Pratik Soni
        /// Creation Date   : 23 Feb 2018
        /// Purpose         : Invokes the post API request for insert.
        /// Revision : 
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        /// <param name="apiName">API name.</param>
        /// <param name="body">Body.</param>
        public HttpResponseMessage InvokePostAPI(string apiName, string requestBody)
        {
            HttpRequestMessage request;
            HttpResponseMessage httpResponseMessage;
            HttpClient httpClient = clientHelper.GetAuthenticateClient();

            var content = new StringContent(requestBody, Encoding.UTF8, resourceManager.GetString("msgAppJson"));
            logger.Info("Post request invoked");

            request = new HttpRequestMessage(HttpMethod.Post, apiName)
            {
                Content = content
            };

            httpResponseMessage = httpClient.SendAsync(request).Result;
            return httpResponseMessage;
        }

        /// <summary>
        /// Method Name     : InvokeDeleteAPI
        /// Author          : Pratik Soni
        /// Creation Date   : 14 Feb 2018
        /// Purpose         : Invoke API Request for delete
        /// Revision : 
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        /// <param name="apiName">API name.</param>
        public async Task<HttpResponseMessage> InvokeDeleteAPI(string apiName)
        {
            using (var httpClient = clientHelper.GetAuthenticateClient())
            {
                logger.Info("Delete request invoked");
                return await httpClient.DeleteAsync(apiName);
            }
        }

        /// <summary>
        /// Method Name     : GetAPIResponseStatusCodeMessage
        /// Author          : Pratik Soni
        /// Creation Date   : 14 Feb 2018
        /// Purpose         : common method to return response message common for selected status codes
        /// Revision : 
        /// Gets the API Response status code.
        /// <returns>The APIR esponse status code.</returns>
        /// <param name="responseMessage">Response message.</param>
        public string GetAPIResponseStatusCodeMessage(HttpResponseMessage responseMessage)
        {
            string message = string.Empty;
            if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable || responseMessage.StatusCode == HttpStatusCode.InternalServerError || responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                message = resourceManager.GetString("msgServiceUnavailable");
            }
            else
            {
                message = resourceManager.GetString("msgDefaultServieMessage");
            }
            return message;
        }

    }
}
