using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JKMPCL
{
    public class APIHelper
    {
        private readonly ClientHelper clientHelper;

        /// <summary>
        /// Constructor Name        : APIHelper
        /// Author                  : Hiren Patel
        /// Creation Date           : 18 Dec 2017
        /// Purpose                 : To create instant of ClientHelper class
        /// Revision                : 
        /// </summary>
        public APIHelper()
        {
            clientHelper = new ClientHelper();
        }

        /// <summary>
        /// Method Name     : InvokeGetAPI
        /// Author          : Hiren Patel
        /// Creation Date   : 18 Dec 2017
        /// Purpose         : Invokes the get API request.
        /// Revision : 
        /// </summary>
        /// <param name="apiName">API name.</param>
        public async Task<HttpResponseMessage> InvokeGetAPI(string apiName)
        {
            using (var client = clientHelper.GetAuthenticateClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Resource.msgAppJson));
                return await client.GetAsync(apiName);
            }
        }

        /// <summary>
        /// Method Name     : InvokePutAPi
        /// Author          : Hiren Patel
        /// Creation Date   : 18 Dec 2017
        /// Purpose         : Invokes the put API request for update.
        /// Revision : 
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        /// <param name="apiName">API name.</param>
        /// <param name="body">Body.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public async Task<HttpResponseMessage> InvokePutAPI<T>(string apiName, T body)
        {
            using (var client = clientHelper.GetAuthenticateClient())
            {
                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(body), Encoding.UTF8, Resource.msgAppJson);
                return await client.PutAsync(apiName, content);
            }
        }

        /// <summary>
        /// Method Name     : InvokePostAPI
        /// Author          : Hiren Patel
        /// Creation Date   : 18 Dec 2017
        /// Purpose         : Invokes the post API request for insert.
        /// Revision : 
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        /// <param name="apiName">API name.</param>
        /// <param name="body">Body.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public async Task<HttpResponseMessage> InvokePostAPI<T>(string apiName, T body)
        {
            using (var client = clientHelper.GetAuthenticateClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, Resource.msgAppJson);
                return await client.PostAsync(apiName, content);
            }
        }

        /// <summary>
        /// Method Name     : InvokeDeleteAPI
        /// Author          : Hiren Patel
        /// Creation Date   : 18 Dec 2017
        /// Purpose         : Invoke API Request for delete
        /// Revision : 
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        /// <param name="apiName">API name.</param>
        public async Task<HttpResponseMessage> InvokeDeleteAPI(string apiName)
        {
            using (var client = clientHelper.GetAuthenticateClient())
            {
                return await client.DeleteAsync(apiName);
            }
        }

        /// <summary>
        /// Method Name     : GetAPIResponseStatusCodeMessage
        /// Author          : Hiren Patel
        /// Creation Date   : 18 Dec 2017
        /// Purpose         : common method to return response message common for selected status codes
        /// Revision : 
        /// Gets the APIR esponse status code.
        /// <returns>The APIR esponse status code.</returns>
        /// <param name="responseMessage">Response message.</param>
        public string GetAPIResponseStatusCodeMessage(HttpResponseMessage responseMessage)
        {
            string message = string.Empty;
            if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable || responseMessage.StatusCode == HttpStatusCode.InternalServerError || responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                message = Resource.msgServiceUnavailable;
            }
            else
            {
                message = Resource.msgDefaultServieMessage;
            }
            return message;
        }

        /// <summary>
        /// Method Name     : GetWebResponseForSecurityToken
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 30 Jan 2018
        /// Purpose         : generate request to get  response fro nodus for token generation
        /// Revision        : 
        /// </summary>
        /// <returns></returns>
        public async Task<HttpWebResponse> GetWebResponseForSecurityToken(string deviceID)
        {
            try
            {
                HttpWebRequest httpWebRequest = WebRequest.Create(Resource.NodusPayFabricTokenUrl) as HttpWebRequest;
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "GET";
                httpWebRequest.Headers["authorization"] = deviceID;

                return await httpWebRequest.GetResponseAsync() as HttpWebResponse;
            }
            catch 
            {
                return null;
            }
           
        }

        /// <summary>
        /// Method Name     : ProcessPaymentTransaction
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 30 Jan 2018
        /// Purpose         : process payment request to payfabric
        /// Revision        : 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="cardDetails"></param>
        /// <returns></returns>
        public async Task<HttpWebResponse> ProcessPaymentTransaction(string token, string cardDetails)
        {
            HttpWebRequest httpWebRequest = WebRequest.Create(Resource.NodusPayFabricTransactionProcessUrl) as HttpWebRequest;
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers["authorization"] = token;

            byte[] requestData = Encoding.UTF8.GetBytes(cardDetails);
            Stream stream = await httpWebRequest.GetRequestStreamAsync();
            stream.Write(requestData, 0, requestData.Length);
            stream.Dispose();

            return await httpWebRequest.GetResponseAsync() as HttpWebResponse;
        }
    }
}
