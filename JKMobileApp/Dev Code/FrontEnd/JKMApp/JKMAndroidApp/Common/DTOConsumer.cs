using JKMPCL.Model;
using JKMPCL.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JKMAndroidApp.Common
{

    /// <summary>
    /// Method Name     : DTOConsumer
    /// Author          : Sanket Prajapati
    /// Creation Date   : 22 jan 2018
    /// Purpose         : Featch Move data and Estimed data For Estimed wizerd and dashboard
    /// Revision        : 
    /// </summary>
    public static class DTOConsumer
    {
        public static MoveDataModel dtoMoveData { get; set; }
        public static List<EstimateModel> dtoEstimateData { get; set; }
        public static EstimateModel estimateModel { get; set; }

        /// <summary>
        /// Method Name     : BindMoveDataAsync
        /// Author          : Sanket Prajapati
        /// Creation Date   : 22 jan 2018
        /// Purpose         : Binding move for data 
        /// Revision        : 
        /// </summary>
        public static async Task BindMoveDataAsync()
        {
            string retMessage = string.Empty;
            dtoEstimateData = new List<EstimateModel>();
            dtoMoveData = new MoveDataModel();
            try
            {
                Move moveService = new Move();
                APIResponse<MoveDataModel> response = await moveService.GetMoveData(UtilityPCL.LoginCustomerData.CustomerId);
                if (response is null)
                {
                    retMessage = StringResource.msgDashboardNotLoad;
                }
                else
                {
                   retMessage = MoveDataResponse(retMessage, response);
                    if (response.IsNoMove)
                    {
                        await GetEstimateList();
                    }
                }
            }
            catch (Exception ex)
            {
                retMessage = ex.Message;
                dtoMoveData = null;
            }
            finally
            {
                if (!string.IsNullOrEmpty(retMessage))
                {
                    dtoMoveData = new MoveDataModel() { response_status = false, message = retMessage };
                }
            }
        }

        /// <summary>
        /// Method Name     : MoveDataResponse
        /// Author          : Sanket Prajapati
        /// Creation Date   : 22 jan 2018
        /// Purpose         : Get move data response
        /// Revision        : 
        /// </summary>
        private static string MoveDataResponse(string retMessage , APIResponse<MoveDataModel> response)
        {
            if (response.STATUS)
            {
                if (response.DATA is null)
                {
                    retMessage = StringResource.msgDashboardNotLoad;
                }
                else
                {
                    if (response.DATA.IsActive == "1")
                    {
                        retMessage = response.Message;
                    }
                    else
                    {
                        dtoMoveData = response.DATA;
                        dtoMoveData.response_status = true;
                    }
                }
            }
            else
            {
                retMessage = response.Message;
               
            }
            return retMessage;
        }

        /// <summary>
        /// Method Name     : GetEstimateList
        /// Author          : Sanket Prajapati
        /// Creation Date   : 22 jan 2018
        /// Purpose         : Binding Estimted data list
        /// Revision        : 
        /// </summary>
        public static async Task GetEstimateList()
        {
            string retMessage = string.Empty;
            dtoEstimateData = null;
            try
            {
                JKMPCL.Services.Estimate.Estimate estimateService = new JKMPCL.Services.Estimate.Estimate();
                APIResponse<List<EstimateModel>> response = await estimateService.GetEstimateData(UtilityPCL.LoginCustomerData.CustomerId);
                if (response is null)
                {
                    retMessage = StringResource.msgDashboardNotLoad;
                }
                else
                {
                    EstimateResponse(retMessage, response);
                }
            }
            catch (Exception ex)
            {
                retMessage = ex.Message;
                dtoEstimateData = null;
            }
            finally
            {
                if (!string.IsNullOrEmpty(retMessage))
                {
                    dtoEstimateData = new List<EstimateModel>();
                    dtoEstimateData.Add(new EstimateModel() { Response_status = false, message = retMessage });
                }
            }
        }

        /// <summary>
        /// Method Name     : EstimateResponse
        /// Author          : Sanket Prajapati
        /// Creation Date   : 22 jan 2018
        /// Purpose         : Get estimed response
        /// Revision        : 
        /// </summary>
        private static void EstimateResponse(string retMessage, APIResponse<List<EstimateModel>> response)
        {
            if (response.STATUS)
            {
                if (response.DATA is null)
                {
                    retMessage = StringResource.msgDashboardNotLoad;
                }
                else
                {
                    dtoEstimateData = response.DATA;
                }
            }
            else
            {
                retMessage = response.Message;
            }
        }

        /// <summary>
        /// Method Name     : GetCustomerProfileData
        /// Author          : Hiren Patel
        /// Creation Date   : 22 jan 2018
        /// Purpose         : Get customer profile data
        /// Revision        : 
        /// </summary>
        public static async Task<APIResponse<CustomerModel>> GetCustomerProfileData()
        {
            APIResponse<CustomerModel> response = new APIResponse<CustomerModel> { STATUS = false };

            try
            {
                Login loginServices = new Login();
                response = await loginServices.GetCustomerProfileData(UtilityPCL.LoginCustomerData);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}