using JKMServices.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using Utility.Logger;

namespace JKMServices.DAL.CRM.common
{
    public class CRMUtilities : ICRMUtilities
    {
        private readonly IResourceManagerFactory resourceManager;
        private readonly ILogger logger;

        //Constructor
        public CRMUtilities(IResourceManagerFactory resourceManager, ILogger logger)
        {
            this.resourceManager = resourceManager;
            this.logger = logger;
        }

        /// Method Name     : SetUpCRMConnection
        /// Author          : Pratik Soni
        /// Creation Date   : 01 Dec 2017
        /// Purpose         : Method to SetUp CRM Connection
        /// Revision        : By Pratik Soni on 06th Dec 2017 : Modified code to remove throw exception and code optimization.
        /// </summary>
        private HttpClient SetUpCRMConnection()
        {
            const string UserName = "UserName";
            const string Password = "Password";
            const string DomainName = "DomainName";
            const string BaseAddress = "BaseAddress";

            Dictionary<string, string> configValues = new Dictionary<string, string>
            {
                { UserName, ConfigurationManager.AppSettings["UserName"] },
                { Password, ConfigurationManager.AppSettings["Password"] },
                { DomainName, ConfigurationManager.AppSettings["DomainName"] },
                { BaseAddress, ConfigurationManager.AppSettings["BaseAddress"] }
            };

            if (configValues.ContainsValue(null) || configValues.ContainsValue(string.Empty))
            {
                return null;
            }

            return GetNewHttpClient(configValues[UserName], configValues[Password], configValues[DomainName], configValues[BaseAddress]);
        }

        /// Method Name     : GetNewHttpClient
        /// Author          : Pratik Soni
        /// Creation Date   : 1 Dec 2017
        /// Purpose         : To create object of CRM's HttpClient and return that object for further use
        /// Revision        : 
        /// </summary>
        private HttpClient GetNewHttpClient(string userName, string password, string domainName, string webAPIBaseAddress)
        {
            HttpClient httpClient = new HttpClient(new HttpClientHandler() { Credentials = new NetworkCredential(userName, password, domainName) })
            {
                BaseAddress = new Uri(webAPIBaseAddress)
            };
            return httpClient;
        }

        /// Method Name     : GenerateRequestURL
        /// Author          : Pratik Soni
        /// Creation Date   : 1 Dec 2017
        /// Purpose         : Creates URL for all HTTP verbs i.e. GET, POST, PUT, DELETE
        /// Input Parameters: "retriveFieldList" - List of fields needed in a query result (MUST FOR GET REQUEST)
        ///                 : "filterString" should contain proper string having fieldName filterType and filterValue e.g. emailaddress1 eq jk.moving@gmail.com ((MUST FOR GET REQUEST))
        ///                 : "orderBy" and "orderingType" can be empty
        /// Revision        : 
        /// </summary>
        private string GenerateRequestURL(string entityName, string retriveFieldList = "", string filterString = "", string orderBy = "", string orderingType = "", string expandValue = "")
        {
            const string querySelect = "?$select=";
            const string queryfilter = "&$filter=";
            const string queryOrderBy = "&$orderBy=";
            const string queryDefaultOrderType = " asc";
            const string queryExpand = "&$expand=";

            StringBuilder requestURL = new StringBuilder();
            requestURL.Append(ConfigurationManager.AppSettings["BaseAddress"]);
            requestURL.Append(entityName);
            if (!string.IsNullOrWhiteSpace(filterString))
            {
                requestURL.Append(querySelect);
                requestURL.Append(retriveFieldList);
                requestURL.Append(queryfilter);
                requestURL.Append(filterString);

                if (!string.IsNullOrWhiteSpace(orderBy))
                {
                    requestURL.Append(queryOrderBy);
                    requestURL.Append(orderBy);
                    if (!string.IsNullOrWhiteSpace(orderingType))
                    {
                        requestURL.Append(orderingType);
                    }
                    else
                    {
                        requestURL.Append(queryDefaultOrderType);
                    }
                }
                if (!string.IsNullOrEmpty(expandValue))
                {
                    requestURL.Append(queryExpand + expandValue);
                }
            }
            return requestURL.ToString();
        }

        /// Method Name     : ExecuteGetRequest
        /// Author          : Pratik Soni
        /// Creation Date   : 1 Dec 2017
        /// Purpose         : To execute HttpMethod - GET and return Dictionary object with result
        /// Input Parameters: "orderBy" and "orderingType" parameters can be null
        ///                 : "retriveFieldList" - List of fields needed in a query result
        ///                 : "filterString" should contain proper string having fieldName filterType and filterValue e.g. emailaddress1 eq jk.moving@gmail.com
        /// Revision        : 
        /// </summary>
        public Dictionary<string, string> ExecuteGetRequest(string entityName, string retriveFieldList, string filterString, string orderBy = "", string orderingType = "", string expandValue = "")
        {
            string requestURL = GenerateRequestURL(entityName, retriveFieldList, filterString, orderBy, orderingType, expandValue);
            return DoRequest(requestURL, null, HttpMethod.Get);
        }

        /// Method Name     : ExecutePostRequest
        /// Author          : Pratik Soni
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : To execute HttpMethod - POST and return Dictionary object with result
        /// Revision        : 
        /// </summary>
        public Dictionary<string, string> ExecutePostRequest(string entityName, string jsonFormattedData)
        {
            string requestURL = GenerateRequestURL(entityName);
            HttpContent content = new StringContent(jsonFormattedData, Encoding.UTF8, "application/json");
            return DoRequest(requestURL.ToString(), content, HttpMethod.Post);
        }

        /// Method Name     : ExecutePutRequest
        /// Author          : Pratik Soni
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : To execute HttpMethod - PUT and return Dictionary object with result
        /// Revision        : 
        /// </summary>
        public Dictionary<string, string> ExecutePutRequest(string entityName, string strGuID, string jsonFormattedData)
        {
            StringBuilder requestURL;

            requestURL = new StringBuilder();
            requestURL.Append(GenerateRequestURL(entityName));
            requestURL.Append("(" + strGuID + ")");
           
            HttpContent content = new StringContent(jsonFormattedData, Encoding.UTF8, "application/json");
            return DoRequest(requestURL.ToString(), content, new HttpMethod("PATCH"));
        }

        /// Method Name     : ExecuteDeleteRequest
        /// Author          : Pratik Soni
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : To execute HttpMethod - DELETE and return Dictionary object with result
        /// Revision        : 
        /// </summary>
        public Dictionary<string, string> ExecuteDeleteRequest(string entityName, string jsonFormattedData)
        {
            HttpContent content = new StringContent(jsonFormattedData, Encoding.UTF8, "application/json");
            string requestURL = GenerateRequestURL(entityName);
            return DoRequest(requestURL, content, HttpMethod.Delete);
        }

        /// Method Name     : DoRequest
        /// Author          : Pratik Soni
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : To execute all requests (GET, POST, PUT, DELETE) and return HttpResponseMessage
        /// Input Parameters: "url"         - URL required for HttpMethods
        ///                 : "content"     - HttpContent required for POST, PUT & DELETE
        ///                 : "HttpMethod"  - Type of HttpMethod for execution - GET / POST / PUT / DELETEs
        /// Revision        : 
        /// </summary>
        private Dictionary<string, string> DoRequest(string requestUrl, HttpContent content, HttpMethod httpMethod)
        {
            Dictionary<string, string> returnValue;
            HttpResponseMessage responseMessage;
            returnValue = new Dictionary<string, string>();
            try
            {
                HttpClient httpClient = SetUpCRMConnection();
                HttpRequestMessage request;
                request = new HttpRequestMessage(httpMethod, requestUrl);
                switch (httpMethod.ToString())
                {
                    case "PATCH":
                    case "POST":
                    case "PUT":
                        request.Content = content;
                        break;
                    case "DELETE":
                        break;
                }
                responseMessage = httpClient.SendAsync(request).Result;
                return GetFormattedResponse(responseMessage);
            }
            catch (UriFormatException ex)
            {
                logger.Error(ex.InnerException);
                returnValue.Add("ERROR", "Invalid URL generated: " + ex.InnerException.ToString());
                return returnValue;
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("CRM_ConnectionLost"), ex);
                returnValue.Add("ERROR", resourceManager.GetString("CRM_ConnectionLost"));
                return returnValue;
            }
        }

        /// Method Name     : GetFormattedResponse
        /// Author          : Pratik Soni
        /// Creation Date   : 07 Dec 2017
        /// Purpose         : To generate proper message with the HttpStatusCode
        /// Input Parameters: "httpResponseMessage" - HttpResponseMessage received from request execution
        /// Revision        : 
        /// </summary>
        private Dictionary<string, string> GetFormattedResponse(HttpResponseMessage httpResponseMessage)
        {
            Dictionary<string, string> returnValue = new Dictionary<string, string>();

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                returnValue.Add(resourceManager.GetString("CRM_RESPONSE_STATUS"), httpResponseMessage.StatusCode.ToString());
                returnValue.Add(resourceManager.GetString("CRM_RESPONSE_CONTENT"), httpResponseMessage.Content.ReadAsStringAsync().Result);
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
            {
                returnValue.Add(resourceManager.GetString("CRM_RESPONSE_STATUS"), httpResponseMessage.StatusCode.ToString());
                returnValue.Add(resourceManager.GetString("CRM_RESPONSE_INFORMATION"), resourceManager.GetString("CRM_STATUS_204"));
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.ServiceUnavailable || httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                returnValue.Add(resourceManager.GetString("CRM_RESPONSE_STATUS"), httpResponseMessage.StatusCode.ToString());
                returnValue.Add("ERROR", "msgServiceUnavailableUnauthorized");
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                returnValue.Add(resourceManager.GetString("CRM_RESPONSE_STATUS"), httpResponseMessage.StatusCode.ToString());
                returnValue.Add("BADREQUEST", "BAD REQUEST");
            }
            return returnValue;
        }

        /// Method Name     : IsValidResponse
        /// Author          : Pratik Soni
        /// Creation Date   : 12 Jan 2018
        /// Purpose         : To check if CRM response isvalid or not
        /// Input Parameters: 
        /// Revision        : 
        /// </summary>
        public bool IsValidResponse(Dictionary<string, string> response)
        {
            if (!response.ContainsKey("CONTENT"))
            {
                return false;
            }
            return true;
        }

        /// Method Name     : GetFormattedResponse
        /// Author          : Pratik Soni
        /// Creation Date   : 12 Jan 2018
        /// Purpose         : To check of CRM response contains value or not
        /// Input Parameters: 
        /// Revision        : 
        /// </summary>
        public bool ContainsNullValue(Dictionary<string, string> response)
        {
            if (IsValidResponse(response))
            {
                JObject jsonObject = Utility.General.ConvertToJObject(response["CONTENT"].ToString());
                if (jsonObject["value"] == null || ((Newtonsoft.Json.Linq.JContainer)jsonObject["value"]).Count == 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// Method Name     : GetFormattedResponseForDTO
        /// Author          : Pratik Soni
        /// Creation Date   : 16 Jan 2018s
        /// Purpose         : To format the CRM response
        /// Revision        : 
        /// </summary>
        /// <returns> returns ServiceResponse with DTO </returns>
        /// 
        public ServiceResponse<T> GetFormattedResponseToDTO<T>(Dictionary<string, string> crmResponse)
        {
            if (crmResponse["STATUS"] == HttpStatusCode.BadRequest.ToString())
            {
                logger.Error(resourceManager.GetString("msgFailToSave"));
                return new ServiceResponse<T> { BadRequest = resourceManager.GetString("msgFailToSave") };
            }
            else if (crmResponse["STATUS"] == HttpStatusCode.ServiceUnavailable.ToString() || crmResponse["STATUS"] == HttpStatusCode.Unauthorized.ToString())
            {
                logger.Info(crmResponse["ERROR"]);
                return new ServiceResponse<T> { Information = crmResponse["ERROR"] };
            }
            else if (crmResponse["STATUS"] == HttpStatusCode.NoContent.ToString())
            {
                logger.Info(crmResponse["INFORMATION"]);
                return new ServiceResponse<T> { Information = crmResponse["INFORMATION"] };
            }

            logger.Info(resourceManager.GetString("msgSavedSuccessfully"));
            return new ServiceResponse<T> { Information = resourceManager.GetString("msgSavedSuccessfully") };
        }

        /// Method Name     : GetSpecificAttributeFromResponse
        /// Author          : Pratik Soni
        /// Creation Date   : 24 Jan 2018
        /// Purpose         : To fetch specific field from the "value" node of CRM response on 0th index
        /// Revision        : 
        /// </summary>
        public string GetSpecificAttributeFromResponse(Dictionary<string, string> crmResponse, string requiredField)
        {
            string returnValue;
            JObject jsonObject = Utility.General.ConvertToJObject(crmResponse["CONTENT"].ToString());
            if (jsonObject["value"] == null || ((Newtonsoft.Json.Linq.JContainer)jsonObject["value"]).Count == 0)
            {
                return jsonObject.ToString(Newtonsoft.Json.Formatting.None);
            }
            JObject valueObject = JObject.Parse(jsonObject["value"][0].ToString(Newtonsoft.Json.Formatting.None));
            returnValue = valueObject[requiredField].ToString();
            return returnValue;
        }
    }
}