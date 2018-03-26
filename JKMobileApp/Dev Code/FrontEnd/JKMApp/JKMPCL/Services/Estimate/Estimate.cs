using JKMPCL.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JKMPCL.Services.Estimate
{
    /// <summary>
    /// Class Name      : Estimate.
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 10 Jan 2018
    /// Purpose         : connect with EstimateAPIService for request & response
    /// Revision        : 
    /// </summary>
    public class Estimate
    {
        private readonly EstimateAPIServices estimateAPIServices;

        public Estimate()
        {
            estimateAPIServices = new EstimateAPIServices();
        }

        /// <summary>
        /// Method Name     : GetEstimateData
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 22 Jan 2018
        /// Purpose         : to get list of estimates
        /// Revision        : 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<APIResponse<List<EstimateModel>>> GetEstimateData(string customerId)
        {
            APIResponse<List<EstimateModel>> apiResponse;
            apiResponse = await estimateAPIServices.GetEstimateData(customerId);

            if (apiResponse.STATUS)
            {
                if (apiResponse.DATA != null && apiResponse.DATA.Count > 0)
                {
                    foreach (EstimateModel estimate in apiResponse.DATA)
                    {
                        SetValuation(estimate);
                        SetServiceCode(estimate);
                        SetStatusCode(estimate);
                        SetDates(estimate);
                    }
                }
                else
                {
                    apiResponse.STATUS = false;
                    apiResponse.Message = Resource.msgNoEstimateFound;
                }
            }

            return apiResponse;
        }

        /// <summary>
        /// Method Name     : SetValuation
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 22 Jan 2018
        /// Purpose         : sub method to set data for valuation fields
        /// Revision        : 
        /// </summary>
        /// <param name="dtoEstimate"></param>
        private void SetValuation(EstimateModel dtoEstimate)
        {
            if (!string.IsNullOrEmpty(dtoEstimate.ExcessValuation))
            {
                dtoEstimate.ExcessValuation = UtilityPCL.CurrencyFormat(dtoEstimate.ExcessValuation);
            }
            if (!string.IsNullOrEmpty(dtoEstimate.EstimatedLineHaul))
            {
                dtoEstimate.EstimatedLineHaul = UtilityPCL.CurrencyFormat(dtoEstimate.EstimatedLineHaul);
            }
            if (!string.IsNullOrEmpty(dtoEstimate.ValuationDeductible))
            {
                dtoEstimate.ValuationDeductible = UtilityPCL.GetMoveDataDisplayValue(dtoEstimate.ValuationDeductible, MoveDataDisplayResource.msgValuationDeductible);
            }
            if (!string.IsNullOrEmpty(dtoEstimate.ValuationCost))
            {
                dtoEstimate.ValuationCost = UtilityPCL.CurrencyFormat(dtoEstimate.ValuationCost);
            }
            if (!string.IsNullOrEmpty(dtoEstimate.Deposit))
            {
                dtoEstimate.Deposit = UtilityPCL.CurrencyFormat(dtoEstimate.Deposit);
            }
        }

        /// <summary>
        /// Method Name     : SetServiceCode
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 22 Jan 2018
        /// Purpose         : sub method to set list of services
        /// Revision        : 
        /// </summary>
        /// <param name="dtoEstimate"></param>
        private void SetServiceCode(EstimateModel dtoEstimate)
        {
            if(!string.IsNullOrEmpty(dtoEstimate.ServiceCode))
            {
                string[] myServicecode = dtoEstimate.ServiceCode.Split(',');
                dtoEstimate.MyServices = new List<MyServicesModel>();

                foreach (string serviceCode in myServicecode)
                {
                    dtoEstimate.MyServices.Add(new MyServicesModel()
                    {
                        ServicesCode = serviceCode
                    });
                }
            }
        }

        /// <summary>
        /// Method Name     : SetStatusCode
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : sub method to set status code
        /// Revision        : 
        /// </summary>
        /// <param name="dtoEstimate"></param>
        private void SetStatusCode(EstimateModel dtoEstimate)
        {
            if (!string.IsNullOrEmpty(dtoEstimate.StatusReason))
            {
                dtoEstimate.StatusReason = UtilityPCL.GetMoveDataDisplayValue(dtoEstimate.StatusReason, MoveDataDisplayResource.msgMoveCode);
            }
        }

        /// <summary>
        /// Method Name     : SetDates
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : sub method to set dates for estimate
        /// Revision        : 
        /// </summary>
        /// <param name="dtoEstimate"></param>
        private void SetDates(EstimateModel dtoEstimate)
        {
            dtoEstimate.LoadStartDate = GetDateForDiplay(dtoEstimate.LoadStartDate);
            dtoEstimate.PackStartDate = GetDateForDiplay(dtoEstimate.PackStartDate);
            dtoEstimate.MoveStartDate = GetDateForDiplay(dtoEstimate.MoveStartDate);
        }

        /// <summary>
        /// Method Name     : GetDateForDiplay
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : sub method to set dates as per require format for estimate
        /// Revision        : 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string GetDateForDiplay(string date)
        {
            string dateValue = string.Empty;
            if (!string.IsNullOrEmpty(date))
            {
                DateTime datetime = UtilityPCL.ConvertDateTimeInUSFormat(date);
                if (datetime != DateTime.MinValue)
                {
                    dateValue = UtilityPCL.DisplayDateFormatForEstimate(datetime,Resource.MMddyyyyDateFormat);
                }
            }

            return dateValue;
        }

        /// <summary>
        /// Method Name     : GetEstimatePDF
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 22 Jan 2018
        /// Purpose         : get byte string for pdf file
        /// Revision        : 
        /// </summary>
        /// <param name="estimateID"></param>
        /// <returns></returns>
        public async Task<APIResponse<GetEstimatePDF>> GetEstimatePDF(string estimateID)
        {
            APIResponse<GetEstimatePDF> response = await estimateAPIServices.GetEstimatePDF(estimateID);
            return response;
        }

        /// <summary>
        /// Method Name     : PutEstimateData
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 22 Jan 2018
        /// Purpose         : Update estimate data in CRM
        /// Revision        : 
        /// </summary>
        /// <param name="estimateModel"></param>
        /// <param name="estimateID"></param>
        /// <returns></returns>
        public async Task<APIResponse<EstimateModel>> PutEstimateData(EstimateModel estimateModel, string estimateID)
        {
            APIResponse<EstimateModel> apiResponse;
            apiResponse = await estimateAPIServices.PutEstimateData(estimateModel, estimateID);

            return apiResponse;
        }
    }
}
