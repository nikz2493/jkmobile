using JKMPCL.Model;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace JKMPCL.Services
{
    /// <summary>
    /// Class Name      : MyDocumentAPIService
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 07 Feb 2018
    /// Purpose         : for managing documents for customer
    /// Revision        :  
    /// </summary>
    public class MyDocumentAPIService
    {
        private readonly APIHelper apiHelper;

        //Constructor
        public MyDocumentAPIService()
        {
            apiHelper = new APIHelper();
        }

        /// <summary>
        /// Method Name     : GetDocumentList
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 07 Feb 2018
        /// Purpose         : get list of documents for customer
        /// Revision        :   
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<APIResponse<List<DocumentModel>>> GetDocumentList(string customerId)
        {
            APIResponse<List<DocumentModel>> response = new APIResponse<List<DocumentModel>>() { STATUS = false };
            try
            {
                if (string.IsNullOrEmpty(customerId))
                {
                    response.Message = Resource.msgInvalidCustomer;
                }
                else
                {
                    string apiUrl = string.Format(Resource.BaseAPIUrl + Resource.MyDocumentService, customerId);
                    HttpResponseMessage responseMessage = await apiHelper.InvokeGetAPI(apiUrl);
                    response = await SetDocumentResponseMessage(responseMessage, response);
                }
                return response;
            }
            catch
            {
                response.Message = Resource.msgDefaultServieMessage;
                return response;
            }
        }

        /// <summary>
        /// Method Name     : SetDocumentResponseMessage
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 07 Feb 2018
        /// Purpose         : set response message as per http response
        /// Revision : 
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<APIResponse<List<DocumentModel>>> SetDocumentResponseMessage(HttpResponseMessage httpResponseMessage, APIResponse<List<DocumentModel>> response)
        {
            switch (httpResponseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    response = await SerializeHttpResponse<APIResponse<List<DocumentModel>>>.Deserialize(httpResponseMessage);
                    response.STATUS = true;
                    response.Message = Resource.msgGetDocumentService;
                    break;
                case HttpStatusCode.NotFound:
                    response.Message = Resource.msgUnregisteredCustomer;
                    break;
                case HttpStatusCode.NoContent:
                    response.Message = Resource.msgNoDocumentFound;
                    break;
                default:
                    response.Message = apiHelper.GetAPIResponseStatusCodeMessage(httpResponseMessage);
                    break;
            }
            return response;
        }

        /// <summary>
        /// Method Name     : GetDocumentPDF
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 07 Feb 2018
        /// Purpose         : get document pdf 
        /// Revision :  
        /// </summary>
        /// <param name="documentID"></param>
        /// <returns></returns>
        public async Task<APIResponse<GetDocumentPDF>> GetDocumentPDF(string documentID)
        {
            APIResponse<GetDocumentPDF> response = new APIResponse<GetDocumentPDF>() { STATUS = false };
            try
            {
                if (string.IsNullOrEmpty(documentID))
                {
                    response.Message = Resource.msgInvalidDocumentID;
                }
                else
                {
                    string apiUrl = string.Format(Resource.BaseAPIUrl + Resource.MyDocumentPdfService, documentID);
                    HttpResponseMessage responseMessage = await apiHelper.InvokeGetAPI(apiUrl);
                    await GetResponse(response, responseMessage);
                }

                return response;
            }
            catch
            {
                response.Message = Resource.msgDefaultServieMessage;
                return response;
            }
        }

        /// <summary>
        /// Method Name     : GetResponse
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 07 Feb 2018
        /// Purpose         : set  response as per http response message
        /// Revision : 
        /// </summary>
        /// <param name="response"></param>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        private async Task GetResponse(APIResponse<GetDocumentPDF> response, HttpResponseMessage httpResponseMessage)
        {
            switch (httpResponseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    response = await SerializeHttpResponse<APIResponse<GetDocumentPDF>>.Deserialize(httpResponseMessage);
                    response.STATUS = true;
                    response.Message = Resource.msgGetDocumentService;
                    break;
                case HttpStatusCode.BadRequest:
                    response.Message = Resource.msgBadRequest;
                    break;
                case HttpStatusCode.NotFound:
                    response.Message = Resource.msgInvalidDocumentID;
                    break;
                default:
                    response.Message = apiHelper.GetAPIResponseStatusCodeMessage(httpResponseMessage);
                    break;
            }
        }
    }
}
