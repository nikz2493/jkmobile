using JKMPCL.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace JKMPCL.Services.Payment
{
    /// <summary>
    /// Class Name      : TokenGenerator
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 30 Jan 2018
    /// Purpose         : class to generate token for Nodus Payfabric payment gateway transaction
    /// Revision        :  
    /// </summary>
    public class TokenGenerator
    {
        private readonly APIHelper apiHelper;

        //Constructor
        public TokenGenerator()
        {
            apiHelper = new APIHelper();
        }

        /// <summary>
        /// Method Name     : CreateToken
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 30 Jan 2018
        /// Purpose         : generate token for Nodus Payfabric payment gateway transaction used for processing transaction
        /// Revision        :  
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse<TokenResponseModel>> CreateToken()
        {
            APIResponse<TokenResponseModel> apiResponse = new APIResponse<TokenResponseModel> { STATUS = false };
            try
            {
                string deviceID =  await GetDeviceID();
                using (HttpWebResponse httpWebResponse = await apiHelper.GetWebResponseForSecurityToken(deviceID))
                {
                    SetResponseStatus(httpWebResponse, apiResponse);
                    if (apiResponse.STATUS)
                    {
                        SetTokenResponse(httpWebResponse, apiResponse);
                    }
                }
                return apiResponse;
            }
            catch (Exception ex)
            {
                apiResponse.Message = ex.Message;
                return apiResponse;
            }

        }

        /// <summary>
        /// Method Name     : GetDeviceID
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 08 Feb 2018
        /// Purpose         : get Device id from service
        /// Revision        :   
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetDeviceID()
        {
            try
            {
                string apiURL = Resource.BaseAPIUrl + Resource.DeviceIDService;

                HttpResponseMessage httpResponseMessage = await apiHelper.InvokeGetAPI(apiURL);
                if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    PaymentModel paymentModel =   await SerializeHttpResponse<PaymentModel>.Deserialize(httpResponseMessage);
                    return paymentModel.Data.DeviceID;

                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Method Name     : SetTokenResponse
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 30 Jan 2018
        /// Purpose         : read & set response base on input http web response 
        /// Revision        :   
        /// </summary>
        /// <param name="httpWebResponse"></param>
        /// <param name="apiResponse"></param>
        private void SetTokenResponse(HttpWebResponse httpWebResponse, APIResponse<TokenResponseModel> apiResponse)
        {
            string streamResult = string.Empty;
            using (Stream responseStream = httpWebResponse.GetResponseStream())
            {
                streamResult = (new StreamReader(responseStream)).ReadToEnd();
                if (!string.IsNullOrEmpty(streamResult))
                {
                    apiResponse.DATA = JsonConvert.DeserializeObject<TokenResponseModel>(streamResult);
                }
            }
        }

        /// <summary>
        /// Method Name     : SetResponseStatus
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 30 Jan 2018
        /// Purpose         : set response status & message based on input httpwebresponse
        /// Revision        : 
        /// </summary>
        /// <param name="httpWebResponse"></param>
        /// <param name="apiResponse"></param>
        private void SetResponseStatus(HttpWebResponse httpWebResponse, APIResponse<TokenResponseModel> apiResponse)
        {
            if (httpWebResponse.StatusCode == HttpStatusCode.OK)
            {
                apiResponse.STATUS = true;
            }
            else
            {
                if (httpWebResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    apiResponse.Message = Resource.msgBadRequest;
                }
                else
                {
                    apiResponse.Message = Resource.msgPaymentRequestFail;
                }
            }
        }

    }
}
