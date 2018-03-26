using JKMPCL.Model;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace JKMPCL.Services.Payment
{
    /// <summary>
    /// Class Name      : PaymentAPIService
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 25 Jan 2018
    /// Purpose         : to post payment transaction details in crm agaings
    /// Revision        :  
    /// </summary>
    public class PaymentAPIService
    {
        //Variables declaration
        private readonly APIHelper apiHelper;

        enum RequestType
        {
            Get,
            Put
        }

        //Constructor
        public PaymentAPIService()
        {
            apiHelper = new APIHelper();
        }

        /// <summary>
        /// Method Name     : PostTransactionHistoryAsync
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 25 Jan 2018
        /// Purpose         : save transactionid(reference no generated through payment gateway) for payment against particular estimate
        /// Revision        :  
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        public async Task<APIResponse<PaymentModel>> PostTransactionHistoryAsync(List<PaymentModel> paymentModel)
        {
            string apiUrl = string.Format(Resource.PostPaymentTransactionUrl, Resource.BaseAPIUrl);

            HttpResponseMessage httpResponseMessage = await apiHelper.InvokePostAPI<List<PaymentModel>>(apiUrl, paymentModel);

            return GetPostTransactionResponse(httpResponseMessage);
        }

        /// <summary>
        /// Method Name     : GetPostTransactionResponse
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 25 Jan 2018
        /// Purpose         : Sub method to generate response based on input http response status
        /// Revision        :  
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        private APIResponse<PaymentModel> GetPostTransactionResponse(HttpResponseMessage httpResponseMessage)
        {
            APIResponse<PaymentModel> apiResponse = new APIResponse<PaymentModel>() { STATUS = false };

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                apiResponse.STATUS = true;
                apiResponse.Message = Resource.msgPostPaymentTransaction;
            }
            else
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    apiResponse.Message = Resource.msgBadRequest;
                }
                else
                {
                    apiResponse.Message = apiHelper.GetAPIResponseStatusCodeMessage(httpResponseMessage);
                }

            }

            return apiResponse;
        }

        /// <summary>
        /// Method Name     : GetDepositAmount
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 20 Feb 2018
        /// Purpose         : to get amounts related to deposit & payment
        /// Revision        :  
        /// </summary>
        /// <param name="paymentModel"></param>
        /// <returns></returns>
        public async Task<APIResponse<PaymentModel>> GetPaymentAmount(PaymentModel paymentModel)
        {
            string apiUrl = string.Format(Resource.BaseAPIUrl + Resource.GetAmountServiceUrl, paymentModel.MoveID);

            APIResponse<PaymentModel> apiResponse = new APIResponse<PaymentModel>() { STATUS = false };
            HttpResponseMessage responseMessage = await apiHelper.InvokeGetAPI(apiUrl);
            apiResponse = await SetResponseMessage(responseMessage, apiResponse, RequestType.Get);

            return apiResponse;
        }

        /// <summary>
        /// Method Name     : SetResponseMessage
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 20 Feb 2018
        /// Purpose         : set response for httpresponse message base on input
        /// Revision        :  
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<APIResponse<PaymentModel>> SetResponseMessage(HttpResponseMessage httpResponseMessage, APIResponse<PaymentModel> response, RequestType requestType)
        {
            switch (httpResponseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    if (requestType == RequestType.Get)
                    {
                        response = await SerializeHttpResponse<APIResponse<PaymentModel>>.Deserialize(httpResponseMessage);
                        response.Message = Resource.msgGetDepositAmount;
                    }
                    else
                    {
                        response.Message = Resource.msgPutAmount;
                    }
                    response.STATUS = true;
                    break;
                case HttpStatusCode.BadRequest:
                    response.Message = Resource.msgBadRequest;
                    break;
                case HttpStatusCode.NotFound:
                    response.Message = Resource.msgUnregisteredCustomer;
                    break;
                default:
                    response.Message = apiHelper.GetAPIResponseStatusCodeMessage(httpResponseMessage);
                    break;
            }
            return response;
        }

        /// <summary>
        /// Method Name     : PutPaymentAmount
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 20 Feb 2018
        /// Purpose         : update payment amount in CRM
        /// Revision        :  
        /// </summary>
        /// <param name="paymentModel"></param>
        /// <returns></returns>
        public async Task<APIResponse<PaymentModel>> PutPaymentAmount(PaymentModel paymentModel)
        {
            APIResponse<PaymentModel> apiResponse = new APIResponse<PaymentModel>() { STATUS = false };
            try
            {
                HttpResponseMessage responseMessage = await apiHelper.InvokePutAPI<PaymentModel>(Resource.BaseAPIUrl + Resource.PutAmountServiceUrl, paymentModel);
                apiResponse = await SetResponseMessage(responseMessage, apiResponse, RequestType.Put);

                return apiResponse;
            }
            catch
            {
                apiResponse.Message = Resource.msgDefaultServieMessage;
                return apiResponse;
            }

        }


    }
}
