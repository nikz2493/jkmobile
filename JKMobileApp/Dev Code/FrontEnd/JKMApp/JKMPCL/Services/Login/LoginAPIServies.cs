using JKMPCL.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace JKMPCL.Services
{

    /// <summary>
    /// Class Name      : LoginAPIServices
    /// Author          : Hiren Patel
    /// Creation Date   : 5 Dec 2017
    /// Purpose         : To acess API servies. 
    /// Revision        : 
    /// </summary> 
    public class LoginAPIServies
    {
        private readonly APIHelper apiHelper;
        private static string baseAPIURL = Resource.BaseAPIUrl + Resource.CustomerService;
        private static string privacyPolicyAPIURL = Resource.BaseAPIUrl + Resource.DocumentService + Resource.PrivacyPolicyPdfUrl;

        /// <summary>
        /// Constructor Name        : LoginAPIServies
        /// Author                  : Hiren Patel
        /// Creation Date           : 18 Dec 2017
        /// Purpose                 : To create instant of APIHelper class
        /// Revision                : 
        /// </summary>
        public LoginAPIServies()
        {
            apiHelper = new APIHelper();
        }


        /// <summary>
        /// Method Name     : GetCustomerID
        /// Author          : Hiren Patel
        /// Creation Date   : 13 Dec 2017
        /// Purpose         : To get customer id by from API
        /// Revision : 
        /// </summary>
        /// <returns>The customer profile data.</returns>
        /// <param name="emailModel">Email model.</param>
        public async Task<APIResponse<CustomerModel>> GetCustomerID(EmailModel emailModel)
        {
            APIResponse<CustomerModel> apiResponse = new APIResponse<CustomerModel>() { STATUS = false };

            string apiURL = baseAPIURL + emailModel.EmailID; 
            HttpResponseMessage responseMessage = await apiHelper.InvokeGetAPI(apiURL);

            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                apiResponse = await GetCustomerResponseAsync(responseMessage);
            }
            else
            {
                if (responseMessage.StatusCode == HttpStatusCode.NoContent)
                {
                    apiResponse.Message = Resource.msgNotExistEmailId;
                }
                else
                {
                    apiResponse.Message = apiHelper.GetAPIResponseStatusCodeMessage(responseMessage);
                }
            }

            return apiResponse;
        }

        /// <summary>
        /// Method Name     : GenerateCustomerResponseAsync
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : sub method to generate response based on input http response message 
        /// Revision        : 
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        private async Task<APIResponse<CustomerModel>> GetCustomerResponseAsync(HttpResponseMessage responseMessage)
        {
            APIResponse<CustomerModel> apiResponse;
            string apiResponseString = await responseMessage.Content.ReadAsStringAsync();
            apiResponseString = JToken.Parse(apiResponseString).ToString();
            apiResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<APIResponse<CustomerModel>>(apiResponseString);

            if (apiResponse.DATA is null)
            {
                apiResponse.STATUS = false;
                apiResponse.Message = Resource.msgDefaultServieMessage;
            }
            else
            {
                apiResponse.STATUS = true;
                apiResponse.Message = Resource.msgGetCustomerService;
            }

            return apiResponse;
        }

        /// <summary>
        /// Method Name     : GetCustomerProfileData
        /// Author          : Hiren Patel
        /// Creation Date   : 18 Dec 2017
        /// Purpose         : Gets the customer profile data.
        /// Revision : 
        /// </summary>
        /// <returns>The customer profile data.</returns>
        /// <param name="customerModel">Customer model.</param>
        public async Task<APIResponse<CustomerModel>> GetCustomerProfileData(CustomerModel customerModel)
        {
            string apiURL = string.Format(Resource.CustomerProfileUrl, baseAPIURL, customerModel.CustomerId);
            APIResponse<CustomerModel> apiResponse = new APIResponse<CustomerModel>() { STATUS = false };

            HttpResponseMessage httpResponseMessage = await apiHelper.InvokeGetAPI(apiURL);
            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                apiResponse = await GetCustomerProfileResponseAsync(customerModel.CustomerId, httpResponseMessage);
            }
            else
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
                {
                    apiResponse.Message = Resource.msgUnregisteredCustomer;
                }
                else
                {
                    apiResponse.Message = apiHelper.GetAPIResponseStatusCodeMessage(httpResponseMessage);
                }
            }

            return apiResponse;
        }

        /// <summary>
        /// Method Name     : GenerateCustomerProfileResponseAsync
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : sub method to generate  response based on input http response message for customer profile
        /// Revision        : 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        private async Task<APIResponse<CustomerModel>> GetCustomerProfileResponseAsync(string customerId, HttpResponseMessage httpResponseMessage)
        {
            APIResponse<CustomerModel> apiResponse;
            string httpResponseString = await httpResponseMessage.Content.ReadAsStringAsync();
            httpResponseString = JToken.Parse(httpResponseString).ToString();
            apiResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<APIResponse<CustomerModel>>(httpResponseString);

            if (apiResponse.DATA is null)
            {
                apiResponse.STATUS = false;
                apiResponse.Message = Resource.msgDefaultServieMessage;
            }
            else
            {
                apiResponse.DATA.CustomerId = customerId;
                apiResponse.STATUS = true;
                apiResponse.Message = Resource.msgGetCustomerService;

                UtilityPCL.SetLoginCustomerProfileData(apiResponse.DATA);
            }

            return apiResponse;
        }

        /// <summary>
        /// Method Name     : GetCustomerVerificationData
        /// Author          : Hiren Patel
        /// Creation Date   : 18 Dec 2017
        /// Purpose         : Gets the customer verification data.
        /// Revision : 
        /// </summary>
        /// <returns>The customer verification data.</returns>
        /// <param name="customerModel">Customer model.</param>
        public async Task<APIResponse<CustomerModel>> GetCustomerVerificationData(CustomerModel customerModel)
        {
            string apiURL = string.Format(Resource.CustomerVerificationUrl, baseAPIURL, customerModel.CustomerId);
            APIResponse<CustomerModel> apiResponse = new APIResponse<CustomerModel>() { STATUS = false };

            HttpResponseMessage httpResponseMessage = await apiHelper.InvokeGetAPI(apiURL);
            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                apiResponse = await GetCustomerVerificationResponseAsync(customerModel.CustomerId, httpResponseMessage);
            }
            else
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
                {
                    apiResponse.Message = Resource.msgUnregisteredCustomer;
                }
                else
                {
                    apiResponse.Message = apiHelper.GetAPIResponseStatusCodeMessage(httpResponseMessage);
                }
            }

            return apiResponse;
        }

        /// <summary>
        /// Method Name     : GetCustomerVerificationResponseAsync
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : sub method to generate  response based on input http response message for customer verification data
        /// Revision        :  
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        private async Task<APIResponse<CustomerModel>> GetCustomerVerificationResponseAsync(string customerId, HttpResponseMessage httpResponseMessage)
        {
            APIResponse<CustomerModel> apiResponse = new APIResponse<CustomerModel> { STATUS = true };
            string httpResponseString = await httpResponseMessage.Content.ReadAsStringAsync();
            httpResponseString = JToken.Parse(httpResponseString).ToString();

            apiResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<APIResponse<CustomerModel>>(httpResponseString);

            if (apiResponse.DATA is null)
            {
                apiResponse.STATUS = false;
                apiResponse.Message = Resource.msgDefaultServieMessage;
            }
            else
            {
                SetCustomerLoginData(customerId, apiResponse);
                apiResponse.DATA.CustomerId = customerId;
                apiResponse.STATUS = true;
                apiResponse.Message = Resource.msgGetCustomerService;
            }

            return apiResponse;
        }

        /// <summary>
        /// Method Name     : SetCustomerLoginData
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : sub method to store customer login data
        /// Revision        : 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="apiResponse"></param>
        private void SetCustomerLoginData(string customerId, APIResponse<CustomerModel> apiResponse)
        {
            UtilityPCL.LoginCustomerData.CustomerId = customerId;
            UtilityPCL.LoginCustomerData.EmailId = apiResponse.DATA.EmailId;
            UtilityPCL.LoginCustomerData.PasswordHash = apiResponse.DATA.PasswordHash;
            UtilityPCL.LoginCustomerData.PasswordSalt = apiResponse.DATA.PasswordSalt;
            UtilityPCL.LoginCustomerData.VerificationCode = apiResponse.DATA.VerificationCode;
            UtilityPCL.LoginCustomerData.CodeValidTill = apiResponse.DATA.CodeValidTill;
            UtilityPCL.LoginCustomerData.IsCustomerRegistered = apiResponse.DATA.IsCustomerRegistered;
        }

        /// <summary>
        /// Method Name     : PostCustomerProfileData
        /// Author          : Hiren Patel
        /// Creation Date   : 18 Dec 2017
        /// Purpose         : Posts the customer profile data.
        /// Revision : 
        /// </summary>
        /// <returns>The customer profile data.</returns>
        /// <param name="customerModel">Customer model.</param>
        public async Task<APIResponse<GetCustomerVerificationDataResponse>> PostCustomerProfileData(CustomerModel customerModel)
        {
            string apiURL = string.Format(Resource.CustomerProfileUrl, baseAPIURL, customerModel.CustomerId);
            APIResponse<GetCustomerVerificationDataResponse> apiResponse = new APIResponse<GetCustomerVerificationDataResponse>() { STATUS = false };

            HttpResponseMessage responseMessage = await apiHelper.InvokeGetAPI(apiURL);
            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                apiResponse.STATUS = true;
                apiResponse.Message = Resource.msgPostCustomerService;
            }
            else
            {
                if (responseMessage.StatusCode == HttpStatusCode.NoContent)
                {
                    apiResponse.Message = Resource.msgInvalidCustomer;
                }
                else
                {
                    apiResponse.Message = apiHelper.GetAPIResponseStatusCodeMessage(responseMessage);
                }
            }

            return apiResponse;
        }

        /// <summary>
        /// Method Name     : PostCustomerVerificationData
        /// Author          : Hiren Patel
        /// Creation Date   : 18 Dec 2017
        /// Purpose         : Posts the customer verification data.
        /// Revision : 
        /// </summary>
        /// <returns>The customer verification data.</returns>
        /// <param name="verificationCodeModel">Verification code model.</param>
        public async Task<APIResponse<CustomerModel>> PostCustomerVerificationData(VerificationCodeModel verificationCodeModel)
        {
            string apiURL = string.Format(Resource.CustomerVerificationUrl, baseAPIURL, verificationCodeModel.CustomerId);
            APIResponse<CustomerModel> response = new APIResponse<CustomerModel> { STATUS = false };

            HttpResponseMessage responseMessage = await apiHelper.InvokePostAPI<VerificationCodeModel>(apiURL, verificationCodeModel);
            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                response.STATUS = true;
                response.Message = Resource.msgPostCustomerService;
            }
            else
            {
                if (responseMessage.StatusCode == HttpStatusCode.NoContent)
                {
                    response.Message = Resource.msgInvalidCustomer;
                }
                else
                {
                    response.Message = apiHelper.GetAPIResponseStatusCodeMessage(responseMessage);
                }

            }
            return response;
        }
        /// <summary>
        /// Method Name     : PutCustomerProfileData
        /// Author          : Hiren Patel
        /// Creation Date   : 18 Dec 2017
        /// Purpose         : Puts the customer profile data.
        /// Revision : 
        /// </summary>
        /// <returns>The customer profile data.</returns>
        /// <param name="privacyPolicyModel">Customer model.</param>
        public async Task<APIResponse<CustomerModel>> PutCustomerProfileData(PrivacyPolicyModel privacyPolicyModel)
        {
            string apiURL = string.Format(Resource.CustomerProfileUrl, baseAPIURL, privacyPolicyModel.CustomerId);
            APIResponse<CustomerModel> response = new APIResponse<CustomerModel>() { STATUS = false };

            HttpResponseMessage responseMessage = await apiHelper.InvokePutAPI<PrivacyPolicyModel>(apiURL, privacyPolicyModel);
            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                response.STATUS = true;
                response.Message = Resource.msgPutCustomerService;
            }
            else
            {
                if (responseMessage.StatusCode == HttpStatusCode.NoContent)
                {
                    response.Message = Resource.msgInvalidCustomer;
                }
                else
                {
                    response.Message = apiHelper.GetAPIResponseStatusCodeMessage(responseMessage);
                }
            }
            return response;
        }

        /// <summary>
        /// Method Name     : PutCustomerProfileData
        /// Author          : Hiren Patel
        /// Creation Date   : 18 Dec 2017
        /// Purpose         : Puts the customer verification data.
        /// Revision : 
        /// </summary>
        /// <returns>The customer verification data.</returns>
        /// <param name="putCreatePasswordModel">Customer model.</param>
        public async Task<APIResponse<CustomerModel>> PutCustomerVerificationData(PutCreatePasswordModel putCreatePasswordModel)
        {
            string apiURL = string.Format(Resource.CustomerVerificationUrl, baseAPIURL, putCreatePasswordModel.CustomerId);
            APIResponse<CustomerModel> apiResponse = new APIResponse<CustomerModel>() { STATUS = false };

            HttpResponseMessage httpResponseMessage = await apiHelper.InvokePutAPI<PutCreatePasswordModel>(apiURL, putCreatePasswordModel);

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                apiResponse.STATUS = true;
                apiResponse.Message = Resource.msgPutCustomerService;
            }
            else
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
                {
                    apiResponse.Message = Resource.msgInvalidCustomer;
                }
                else
                {
                    apiResponse.Message = apiHelper.GetAPIResponseStatusCodeMessage(httpResponseMessage);
                }
            }

            return apiResponse;
        }

        /// <summary>
        /// Method Name     : GetPrivacyPolicyPDF
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Dec 2017
        /// Purpose         : Get privacy policy pdf file as byte array.
        /// Revision : 
        /// </summary>
        /// <returns>The customer verification data.</returns>
        /// <param name="customerModel">Customer model.</param>
        public async Task<string> GetPrivacyPolicyPDF(CustomerModel customerModel)
        {
            APIResponse<GetPrivacyPolicyPDFResponse> apiResponse = new APIResponse<GetPrivacyPolicyPDFResponse>() { STATUS = false };
            HttpResponseMessage httpResponseMessage = await apiHelper.InvokeGetAPI(privacyPolicyAPIURL);

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                apiResponse = await GetPrivacyPolicyResponseAsync(httpResponseMessage, apiResponse);
            }
            else
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
                {
                    apiResponse.Message = Resource.msgInvalidCustomer;
                }
                else
                {
                    apiResponse.Message = apiHelper.GetAPIResponseStatusCodeMessage(httpResponseMessage);
                }
            }

            return apiResponse.DATA.DATA;
        }

        /// <summary>
        /// Method Name     : GetPrivacyPolicyResponseAsync
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : sub method to generate response for privacy policy based on input http response message
        /// Revision        : 
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <param name="apiResponse"></param>
        /// <returns></returns>
        private async Task<APIResponse<GetPrivacyPolicyPDFResponse>> GetPrivacyPolicyResponseAsync(HttpResponseMessage httpResponseMessage, APIResponse<GetPrivacyPolicyPDFResponse> apiResponse)
        {
            string pdfApiResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            pdfApiResponse = JToken.Parse(pdfApiResponse).ToString();

            GetPrivacyPolicyPDFResponse pdfBytesArry = Newtonsoft.Json.JsonConvert.DeserializeObject<GetPrivacyPolicyPDFResponse>(pdfApiResponse);
            apiResponse.DATA = pdfBytesArry;
            apiResponse.STATUS = true;
            apiResponse.Message = Resource.msgPutCustomerService;

            return apiResponse;
        }

        /// <summary>
        /// Method Name     : CheckPassword
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Dec 2017
        /// Purpose         : check password and generate response  
        /// Revision        : 
        /// </summary>
        /// <param name="passwordModel"></param>
        /// <param name="customerModel"></param>
        /// <returns></returns>
        public APIResponse<CustomerModel> CheckPassword(PasswordModel passwordModel, CustomerModel customerModel)
        {
            APIResponse<CustomerModel> apiResponse = new APIResponse<CustomerModel>()
            {
                STATUS = (passwordModel.CustomerPasssword == customerModel.PasswordHash)
            };

            if (!apiResponse.STATUS)
            {
                apiResponse.Message = Resource.msgCorrectPassword;
            }

            apiResponse.DATA = customerModel;
            return apiResponse;
        }

        /// <summary>
        /// Method Name     : CheckVerificationCode
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Dec 2017
        /// Purpose         : validate verification code base on code valid till date with current date
        /// Revision        :  
        /// </summary>
        /// <param name="verifyCodeModel"></param>
        /// <param name="customerModel"></param>
        /// <returns></returns>
        public APIResponse<CustomerModel> CheckVerificationCode(VerificationCodeModel verifyCodeModel, CustomerModel customerModel)
        {
            APIResponse<CustomerModel> apiResponse = new APIResponse<CustomerModel>();
            DateTime codeValidTill = Convert.ToDateTime(customerModel.CodeValidTill);

            if (codeValidTill < DateTime.Today)
            {
                apiResponse.Message = Resource.msgVerificationcodeisexpired;
            }
            else
            {
                apiResponse.STATUS = (verifyCodeModel.VerificationCode == customerModel.VerificationCode);
                apiResponse.DATA = customerModel;

                if (!apiResponse.STATUS)
                {
                    apiResponse.Message = Resource.msgCorrectVerificationCode;
                }
            }

            return apiResponse;
        }

    }
}
