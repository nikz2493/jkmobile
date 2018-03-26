using JKMPCL.Model;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace JKMPCL.Services
{
    /// <summary>
    /// Model Name      : MoveAPIService
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 27 Dec 2017
    /// Purpose         : To get Move Id from service
    /// </summary>
    public class MoveAPIService
    {
        private readonly APIHelper apiHelper;
        private static string baseAPIUrl = Resource.BaseAPIUrl + Resource.MoveService;

        public MoveAPIService()
        {
            apiHelper = new APIHelper();
        }

        /// <summary>
        /// Method Name     : GetMoveID
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 27 Dec 2017
        /// Purpose         : To get Move Id from service
        /// Revision : 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>Get Move ID in response</returns>
        public async Task<APIResponse<GetMoveIDModel>> GetMoveID(string customerId)
        {
            APIResponse<GetMoveIDModel> apiResponse = new APIResponse<GetMoveIDModel>() { STATUS = false };
            try
            {
                string apiURL = string.Format(Resource.MoveIDUrl, baseAPIUrl, customerId);

                if (string.IsNullOrEmpty(customerId))
                {
                    apiResponse.Message = Resource.msgInvalidCustomer;
                }
                else
                {
                    HttpResponseMessage responseMessage = await GetHTTPResponseMessage(apiURL);
                    apiResponse = await SetMoveIdResponseMessage(responseMessage, apiResponse);
                }
                return apiResponse;
            }
            catch
            {
                apiResponse.Message = Resource.msgNoMoveForCustomer;
                return apiResponse;
            }
        }

        /// <summary>
        /// Method Name     : GetHTTPResponseMessage
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : to get Http Response from url
        /// Revision : 
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> GetHTTPResponseMessage(string apiUrl)
        {
            return await apiHelper.InvokeGetAPI(apiUrl);
        }

        /// <summary>
        /// Method Name     : SetMoveIdResponseMessage
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : set response base on http response
        /// Revision : 
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<APIResponse<GetMoveIDModel>> SetMoveIdResponseMessage(HttpResponseMessage httpResponseMessage, APIResponse<GetMoveIDModel> response)
        {
            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                response = await SerializeHttpResponse<APIResponse<GetMoveIDModel>>.Deserialize(httpResponseMessage);
                response.STATUS = true;
                response.Message = Resource.msgGetMoveService;
            }
            else
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
                {
                    response.Message = Resource.msgNoMoveForCustomer;
                }
                else
                {
                    response.Message = apiHelper.GetAPIResponseStatusCodeMessage(httpResponseMessage);
                }
            }
            return response;
        }

        /// <summary>
        /// Method Name     : GetMoveData
        /// Author          : Hiren Patel
        /// Creation Date   : 27 Dec 2017
        /// Purpose         : Get move data
        /// Revision : 
        /// </summary>
        /// <returns>The move data.</returns>
        /// <param name="moveId">Move identifier.</param>
        public async Task<APIResponse<GetMoveDataResponse>> GetMoveData(string moveId)
        {
            APIResponse<GetMoveDataResponse> response = new APIResponse<GetMoveDataResponse>() { STATUS = false };

            HttpResponseMessage httpResponseMessage = await apiHelper.InvokeGetAPI(baseAPIUrl + moveId);

            switch (httpResponseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    response = await SerializeHttpResponse<APIResponse<GetMoveDataResponse>>.Deserialize(httpResponseMessage);
                    response.STATUS = true;
                    response.Message = Resource.msgGetMoveDataOk;
                    break;
                case HttpStatusCode.NotFound:
                    response.Message = Resource.msgGetMoveDataNotFound;
                    break;
                default:
                    response.Message = apiHelper.GetAPIResponseStatusCodeMessage(httpResponseMessage);
                    break;
            }

            return response;
        }

        /// <summary>
        /// Method Name     : GetContactListForMoveResponse
        /// Author          : Hiren Patel
        /// Creation Date   : 27 Dec 2017
        /// Purpose         : To Get contact list for move.
        /// Revision : 
        /// </summary>
        /// <returns>The move data.</returns>
        /// <param name="moveId">Move identifier.</param>
        public async Task<APIResponse<GetContactListForMoveResponse>> GetContactListForMove(string moveId)
        {
            APIResponse<GetContactListForMoveResponse> response = new APIResponse<GetContactListForMoveResponse>() { STATUS = false };

            string apiURL = string.Format(Resource.MoveContactUrl, baseAPIUrl, moveId);
            HttpResponseMessage responseMessage = await apiHelper.InvokeGetAPI(apiURL);

            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    response = await SerializeHttpResponse<APIResponse<GetContactListForMoveResponse>>.Deserialize(responseMessage);
                    response.STATUS = true;
                    response.Message = Resource.msgGetContactListForMoveOK;
                    break;
                case HttpStatusCode.NoContent:
                    response.Message = Resource.msgGetContactListForMoveNoContent;
                    break;
                case HttpStatusCode.NotFound:
                    response.Message = Resource.msgGetContactListForMoveNotFound;
                    break;
                default:
                    response.Message = apiHelper.GetAPIResponseStatusCodeMessage(responseMessage);
                    break;
            }
            return response;
        }

        /// <summary>
        /// Method Name     : PutMoveData
        /// Author          : Hiren Patel
        /// Creation Date   : 27 Dec 2017
        /// Purpose         : To Puts the move data.
        /// Revision        : By Vivek Bhavsar on 11-01-2018 : Replace Move Model with T class due to multiple model can be pass for same method.
        /// </summary>
        /// <returns>The move data.</returns>
        /// <param name="moveDataModel" or "EstimateModel">Move/Estimate Data Model.</param>
        public async Task<APIResponse<T>> PutMoveData<T>(T moveDataModel, string moveID)
        {
            APIResponse<T> response = new APIResponse<T>() { STATUS = false };
            HttpResponseMessage httpResponseMessage = await apiHelper.InvokePutAPI<T>(baseAPIUrl+ moveID, moveDataModel);

            SetPutMoveDataResponse(response, httpResponseMessage);

            return response;
        }

        /// <summary>
        /// Method Name     : SetPutMoveDataResponse
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Refactoring : seperate response through switch case based on http response input
        /// Revision        :
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="httpResponseMessage"></param>
        private void SetPutMoveDataResponse<T>(APIResponse<T> response, HttpResponseMessage httpResponseMessage)
        {
            switch (httpResponseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    response.STATUS = true;
                    response.Message = Resource.msgPutMoveDataOK;
                    break;
                case HttpStatusCode.BadRequest:
                    response.Message = Resource.msgBadRequest;
                    break;
                case HttpStatusCode.NotFound:
                case HttpStatusCode.NoContent:
                    response.Message = Resource.msgPutMoveDataNotFound;
                    break;
                default:
                    response.Message = apiHelper.GetAPIResponseStatusCodeMessage(httpResponseMessage);
                    break;
            }
        }

    }
}
