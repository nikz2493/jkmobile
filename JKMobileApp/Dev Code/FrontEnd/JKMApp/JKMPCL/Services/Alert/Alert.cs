using JKMPCL.Model;
using JKMPCL.Services.Alert;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JKMPCL
{
    /// <summary>
    /// Method Name     : Alert
    /// Author          : Sanket Prajapati
    /// Creation Date   : 29 Dec 2017
    /// Purpose         : To perform activities of Alert details
    /// Revision        : 
    /// </summary>
    public class Alert
    {
        private readonly AlertAPIServices alertAPIServices;
        string errorMessage = string.Empty;

        //Constructor
        public Alert()
        {
            alertAPIServices = new AlertAPIServices();
        }

        /// <summary>
        /// Method Name     : GetAlertList
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : For Getting Alert List  
        /// Revision        : 
        /// </summary>
        public async Task<APIResponse<List<AlertModel>>> GetAlertList(CustomerModel customerModel)
        {
            APIResponse<List<AlertModel>> serviceResponse = new APIResponse<List<AlertModel>> { STATUS = false };
            AlertModel alertModel;

            try
            {
                if (customerModel is null)
                {
                    serviceResponse.Message = errorMessage;
                    return serviceResponse;
                }
                else
                {
                    alertModel = new AlertModel();
                    {
                        alertModel.CustomerID = customerModel.CustomerId;
                        alertModel.CustomerLastLoginDate = Convert.ToDateTime(customerModel.LastLoginDate);
                    }

                    serviceResponse = await GetALertListResponse(alertModel);
                }
                return serviceResponse;

            }
            catch
            {
                serviceResponse.Message = Resource.msgDefaultServieMessage;
                return serviceResponse;
            }
        }

        /// <summary>
        /// Method Name     : GetALertListResponse
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : For Getting Alert List  
        /// Revision        : 
        /// </summary>
        /// <param name="alertModel"></param>
        /// <returns></returns>
        private async Task<APIResponse<List<AlertModel>>> GetALertListResponse(AlertModel alertModel)
        {
            APIResponse<List<AlertModel>> apiResponse = new APIResponse<List<AlertModel>>() { STATUS = false };
            try
            {
                apiResponse = await alertAPIServices.GetAlertList(alertModel);

                if (apiResponse.STATUS)
                {
                    if ((apiResponse.DATA is null) || (apiResponse.DATA != null && apiResponse.DATA.Count <= 0))
                    {
                        errorMessage = Resource.msgAlertNotfound;
                        apiResponse.STATUS = false;
                    }
                }
                else
                {
                    errorMessage = apiResponse.Message;
                }

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
