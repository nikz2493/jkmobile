using JKMPCL.Model;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace JKMPCL.Services.Estimate
{
    /// <summary>
    /// Class Name      : EstimateAPIServices.
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 10 Jan 2018
    /// Purpose         : request & response mapping with WCF Service
    /// Revision        : 
    /// </summary>
    public class EstimateAPIServices
    {
        private readonly APIHelper apiHelper;
        private static string baseAPIUrl = Resource.BaseAPIUrl + Resource.EstimateService;

        //constructor
        public EstimateAPIServices()
        {
            apiHelper = new APIHelper();
        }

        /// <summary>
        /// Method Name     : GetEstimateDetails
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 10 Jan 2018
        /// Purpose         : set response base on http response
        /// Revision : 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>get list of estimates related to move</returns>
        public async Task<APIResponse<List<EstimateModel>>> GetEstimateData(string customerId)
        {
            APIResponse<List<EstimateModel>> response = new APIResponse<List<EstimateModel>>() { STATUS = false };

            if (string.IsNullOrEmpty(customerId))
            {
                response.Message = Resource.msgInvalidCustomer;
            }
            else
            {
                HttpResponseMessage responseMessage = await apiHelper.InvokeGetAPI(baseAPIUrl+customerId);
                response = await SetEstimateResponseMessage(responseMessage, response);
            }

            return response;
        }

        /// <summary>
        /// Method Name     : SetEstimateResponseMessage
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 10 Jan 2018
        /// Purpose         : set response base on http response
        /// Revision : 
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<APIResponse<List<EstimateModel>>> SetEstimateResponseMessage(HttpResponseMessage httpResponseMessage, APIResponse<List<EstimateModel>> response)
        {
            switch (httpResponseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    response = await SerializeHttpResponse<APIResponse<List<EstimateModel>>>.Deserialize(httpResponseMessage);
                    response.STATUS = true;
                    response.Message = Resource.msgGetEstimateService;
                    break;
                case HttpStatusCode.NotFound:
                    response.Message = Resource.msgUnregisteredCustomer;
                    break;
                case HttpStatusCode.NoContent:
                    response.Message = Resource.msgNoEstimateFound;
                    break;
                default:
                    response.Message = apiHelper.GetAPIResponseStatusCodeMessage(httpResponseMessage);
                    break;
            }

            return response;
        }

        /// <summary>
        /// Method Name     : PutEstimateData
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 10 Jan 2018
        /// Purpose         : Update Estimate Details
        /// Revision : 
        /// </summary>
        /// <param name="estimateModel"></param>
        /// <param name="estimateID"></param>
        /// <returns></returns>
        public async Task<APIResponse<EstimateModel>> PutEstimateData(EstimateModel estimateModel, string estimateID)
        {
            APIResponse<EstimateModel> response = new APIResponse<EstimateModel>() { STATUS = false };

            HttpResponseMessage responseMessage = await apiHelper.InvokePutAPI<EstimateModel>(baseAPIUrl+estimateID, estimateModel);
            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    response.STATUS = true;
                    response.Message = Resource.msgPutEstimateData;
                    break;
                case HttpStatusCode.NotFound:
                    response.Message = Resource.msgPutNoEstimateFound;
                    break;
                default:
                    response.Message = apiHelper.GetAPIResponseStatusCodeMessage(responseMessage);
                    break;
            }

            return response;
        }

        /// <summary>
        /// Method Name     : GetEstimatePDF
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 10 Jan 2018
        /// Purpose         : PDF for Estimate Details
        /// Revision        : 
        /// </summary>
        /// <param name="estimateId"></param>
        /// <returns>GetEstimatePDF class</returns>
        public async Task<APIResponse<GetEstimatePDF>> GetEstimatePDF(string estimateId)
        {
            APIResponse<GetEstimatePDF> response = new APIResponse<GetEstimatePDF>() { STATUS = false };
            if (string.IsNullOrEmpty(estimateId))
            {
                response.Message = Resource.msgPutNoEstimateFound;
            }
            else
            {
                HttpResponseMessage responseMessage = await apiHelper.InvokeGetAPI(baseAPIUrl + estimateId);
                await GetResponse(response, responseMessage);
            }

            return response;
        }


        /// <summary>
        /// Method Name     : GetResponse
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 22 Jan 2018
        /// Purpose         : Get response based on httpresponse & status codes
        /// Revision        : 
        /// </summary>
        /// <param name="response"></param>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        private async Task GetResponse(APIResponse<GetEstimatePDF> response, HttpResponseMessage httpResponseMessage)
        {
            switch (httpResponseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    response = await SerializeHttpResponse<APIResponse<GetEstimatePDF>>.Deserialize(httpResponseMessage);
                    response.STATUS = true;
                    response.Message = Resource.msgGetEstimatePDF;
                    break;
                case HttpStatusCode.BadRequest:
                    response.Message = Resource.msgBadRequest;
                    break;
                case HttpStatusCode.NotFound:
                    response.Message = Resource.msgPutNoEstimateFound;
                    break;
                default:
                    response.Message = apiHelper.GetAPIResponseStatusCodeMessage(httpResponseMessage);
                    break;
            }
        }

    }
}
