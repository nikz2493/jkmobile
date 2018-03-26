using JKMPCL.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace JKMPCL.Services.Alert
{
    public class AlertAPIServices
    {
        private readonly APIHelper apiHelper;

        /// <summary>
        /// Constructor Name        : AlertAPIServices
        /// Author                  : Sanket Prajapati
        /// Creation Date           : 27 Dec 2017
        /// Purpose                 : To create instant of APIHelper class
        /// Revision                : 
        /// </summary>
        public AlertAPIServices()
        {
            apiHelper = new APIHelper();
        }

        /// <summary>
        /// Method Name     : GetAlertList
        /// Author          : Sanket Prajapati
        /// Creation Date   : 27 Dec 2017
        /// Purpose         : To get Alert List by from API
        /// Revision        : 
        /// </summary>
        public async Task<APIResponse<List<AlertModel>>> GetAlertList(AlertModel alertModel)
        {
            APIResponse<List<AlertModel>> apiResponse = new APIResponse<List<AlertModel>>() { STATUS = false };

            if (alertModel is null)
            {
                apiResponse.Message = Resource.msgInvalidCustomer;
                return apiResponse;
            }
            else
            {
                string lastLoginDate = null;
                if(alertModel.CustomerLastLoginDate!=DateTime.MinValue)
                {
                    lastLoginDate = alertModel.CustomerLastLoginDate.ToString(Resource.MM_dd_yyyyDateFormat);
                }
                string apiURL = string.Format("{0}{1}/{2}", Resource.BaseAPIUrl + Resource.AlertService, alertModel.CustomerID, lastLoginDate);
                await GetAlertListAsync(apiResponse, apiURL);

                return apiResponse;
            }
        }

        /// <summary>
        /// Method Name     : GetAlertListAsync
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 22 Jan 2017
        /// Purpose         : sub method to get alert list for GetAlertList
        /// Revision        : 
        /// </summary>
        /// <param name="response"></param>
        /// <param name="apiURL"></param>
        /// <returns></returns>
        private async Task GetAlertListAsync(APIResponse<List<AlertModel>> response, string apiURL)
        {
            HttpResponseMessage httpResponseMessage = await apiHelper.InvokeGetAPI(apiURL);
            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                response = await GetAlertListResponseAsync(httpResponseMessage, response);
            }
            else
            {
                response.Message = ResponseMessage(httpResponseMessage);
            }
        }

        /// <summary>
        /// Method Name     : GetAlertListAsync
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2017
        /// Purpose         : sub method to get alert list for GetAlertList
        /// Revision        : 
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<APIResponse<List<AlertModel>>> GetAlertListResponseAsync(HttpResponseMessage httpResponseMessage, APIResponse<List<AlertModel>> response)
        {
            APIResponse<List<AlertModelResponse>> responseAlertList = new APIResponse<List<AlertModelResponse>>();

            string apiResponseString = await httpResponseMessage.Content.ReadAsStringAsync();
            apiResponseString = JToken.Parse(apiResponseString).ToString();

            responseAlertList = Newtonsoft.Json.JsonConvert.DeserializeObject<APIResponse<List<AlertModelResponse>>>(apiResponseString);
            response.STATUS = true;

            if (responseAlertList.DATA != null)
            {
                response.DATA = GetMoveAlertList(responseAlertList.DATA);
                responseAlertList.STATUS = true;
            }

            response.Message = Resource.msgGetListAlertService;

            return response;
        }

        /// <summary>
        /// Method Name     : ResponseMessage
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 22 Jan 2017
        /// Purpose         : set response based on input HttpResponseMessage
        /// Revision        : 
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        private string ResponseMessage(HttpResponseMessage responseMessage)
        {
            string response = string.Empty;

            if (responseMessage.StatusCode == HttpStatusCode.NoContent)
            {
                response = Resource.msgAlertNotfound;
            }
            else if (responseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                response = Resource.msgInvalidCustomerid;
            }
            else
            {
                response = apiHelper.GetAPIResponseStatusCodeMessage(responseMessage);
            }

            return response;
        }

        /// <summary>
        /// Method Name     : GetMoveAlertList
        /// Author          : Hiren Patel
        /// Creation Date   : 30 Dec 2017
        /// Purpose         : To get Move data from services  
        /// Revision        : 
        /// </summary>
        private List<AlertModel> GetMoveAlertList(List<AlertModelResponse> objGetAlertListResponse)
        {
            List<AlertModel> alertList = new List<AlertModel>();

            if (objGetAlertListResponse != null)
            {
                foreach (AlertModelResponse alertResponse in objGetAlertListResponse)
                {
                    AlertModel alertModel = new AlertModel();
                    SetAlertDetails(alertModel, alertResponse);
                    SetAlertDates(alertModel, alertResponse);
                    alertList.Add(alertModel);
                }
            }

            return alertList;
        }

        /// <summary>
        /// Method Name     : SetAlertDetails
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 22 Jan 2018
        /// Purpose         : sub method to set alert details
        /// Revision        : 
        /// </summary>
        /// <param name="alertModel"></param>
        /// <param name="alertResponse"></param>
        private void SetAlertDetails(AlertModel alertModel, AlertModelResponse alertResponse)
        {
            alertModel.ActionToTake = alertResponse.ActionToTake;
            alertModel.AlertDescription = alertResponse.AlertDescription;
            alertModel.AlertID = alertResponse.AlertID;
            alertModel.AlertTitle = alertResponse.AlertTitle;
            alertModel.CustomerID = alertResponse.CustomerID;
            alertModel.DisplayInCalendar = alertResponse.DisplayInCalendar;
            alertModel.IsActive = alertResponse.IsActive;
            alertModel.AlertType = alertResponse.AlertType;
        }

        /// <summary>
        /// Method Name     : SetAlertDates
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 22 Jan 2018
        /// Purpose         : sub method to set alert dates
        /// Revision        : 
        /// </summary>
        /// <param name="alertModel"></param>
        /// <param name="alertResponse"></param>
        private void SetAlertDates(AlertModel alertModel, AlertModelResponse alertResponse)
        {
            if (!string.IsNullOrEmpty(alertResponse.EndDate))
            {
                alertModel.EndDate = Convert.ToDateTime(alertResponse.EndDate);
            }
            if (!string.IsNullOrEmpty(alertResponse.StartDate))
            {
                alertModel.StartDate = Convert.ToDateTime(alertResponse.StartDate);
            }
            if (!string.IsNullOrEmpty(alertResponse.CustomerLastLoginDate))
            {
                alertModel.CustomerLastLoginDate = Convert.ToDateTime(alertResponse.CustomerLastLoginDate);
            }
        }
    }
}
