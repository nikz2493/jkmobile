using JKMPCL.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace JKMPCL.Services
{
    /// <summary>
    /// Class Name      : Move
    /// Author          : Hiren Patel
    /// Creation Date   : 05 Dec 2017
    /// Purpose         : For Customer Login & Password 
    /// Revision        : 
    /// </summary>
    public class Move
    {
        private readonly MoveAPIService moveAPIService;

        public Move()
        {
            moveAPIService = new MoveAPIService();
        }

        /// <summary>
        /// Method Name     : GetMoveConactList
        /// Author          : Hiren Patel
        /// Creation Date   : 27 Dec 2017
        /// Purpose         : Get move conact list
        /// Revision        : 
        /// </summary>
        /// <returns>The move data.</returns>
        public async Task<APIResponse<GetContactListForMoveResponse>> GetContactListForMove()
        {
            APIResponse<GetContactListForMoveResponse> apiResponse = new APIResponse<GetContactListForMoveResponse>();
            if (UtilityPCL.CustomerMoveData is null)
            {
                apiResponse.DATA = GetDefaultContactListForMove();
                apiResponse.STATUS = true;
            }
            else
            {
                apiResponse = await moveAPIService.GetContactListForMove(UtilityPCL.CustomerMoveData.MoveNumber);
                if (apiResponse.STATUS)
                {
                    SetMoveContactInfo(apiResponse);
                }
            }

            return apiResponse;
        }

        /// <summary>
        /// Method Name     : SetMoveContactInfo
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Refatoring : sub method to set contact info into response
        /// Revision        :
        /// </summary>
        /// <param name="apiResponse"></param>
        private void SetMoveContactInfo(APIResponse<GetContactListForMoveResponse> apiResponse)
        {
            apiResponse.STATUS = true;
            if (!string.IsNullOrEmpty(apiResponse.DATA.internalemailaddress))
            {
                apiResponse.DATA.internalemailaddress = apiResponse.DATA.internalemailaddress.ToLower();
            }
            else
            {
                apiResponse.DATA.internalemailaddress = GetDefaultContactListForMove().internalemailaddress;
            }
            if (!string.IsNullOrEmpty(apiResponse.DATA.address1_telephone1))
            {
                apiResponse.DATA.address1_telephone1 = UtilityPCL.DisplayPhoneFormat(apiResponse.DATA.address1_telephone1);
            }
            else
            {
                apiResponse.DATA.address1_telephone1 = GetDefaultContactListForMove().address1_telephone1;
            }
        }

        /// <summary>
        /// Method Name     : GetDefaultContactListForMove
        /// Author          : Hiren Patel
        /// Creation Date   : 22 JAN 2018
        /// Purpose         : Get default move conact list
        /// Revision : 
        /// </summary>
        /// <returns>The move data.</returns>
        public GetContactListForMoveResponse GetDefaultContactListForMove()
        {
            GetContactListForMoveResponse contactListForMoveResponse = new GetContactListForMoveResponse();
            contactListForMoveResponse.internalemailaddress = Resource.contactUsDefaultEmailId.ToLower();
            contactListForMoveResponse.address1_telephone1 = Resource.contactUsDefaultPhoneNo.ToLower();

            return contactListForMoveResponse;
        }

        /// <summary>
        /// Method Name     : GetMoveData
        /// Author          : Hiren Patel
        /// Creation Date   : 27 Dec 2017
        /// Purpose         : Get move data
        /// Revision : 
        /// </summary>
        /// <returns>The move data.</returns>
        /// <param name="customerId">Move identifier.</param>
        public async Task<APIResponse<MoveDataModel>> GetMoveData(string customerId)
        {
            APIResponse<MoveDataModel> apiResponse = new APIResponse<MoveDataModel> {STATUS=false,IsNoMove=false };
            APIResponse<GetMoveIDModel> getMoveIDResponse = await moveAPIService.GetMoveID(customerId);

            if (getMoveIDResponse.STATUS)
            {
                APIResponse<GetMoveDataResponse> responseGetMoveData = await moveAPIService.GetMoveData(getMoveIDResponse.DATA.MoveNumber);

                if (responseGetMoveData.STATUS)
                {
                    apiResponse = GetMoveDataResponse(responseGetMoveData);
                }
                else
                {
                    apiResponse.Message = responseGetMoveData.Message;
                }
            }
            else
            {
                if (getMoveIDResponse.Message == Resource.msgNoMoveForCustomer)
                {
                    apiResponse.IsNoMove = true;
                }
                apiResponse.Message = getMoveIDResponse.Message;
            }
            return apiResponse;
        }

        /// <summary>
        /// Method Name     : GetMoveDataResponse
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Refactoring : sub method to set response based on input
        /// Revision        :
        /// </summary>
        /// <param name="responseGetMoveData"></param>
        /// <returns></returns>
        private APIResponse<MoveDataModel> GetMoveDataResponse(APIResponse<GetMoveDataResponse> responseGetMoveData)
        {
            APIResponse<MoveDataModel> apiResponse = new APIResponse<MoveDataModel>();
            if (responseGetMoveData.DATA != null)
            {
                apiResponse.STATUS = true;
                apiResponse.DATA = GetMoveData(responseGetMoveData.DATA);

                if (apiResponse.DATA.IsActive is null || apiResponse.DATA.IsActive == "1")
                {
                    apiResponse.Message = Resource.msgNoMoveForCustomer;
                }
            }
            else
            {
                apiResponse.STATUS = false;
                apiResponse.Message = Resource.msgGetMoveDataNotFound;
            }

            return apiResponse;
        }

        

        /// <summary>
        /// Method Name     : GetMoveData
        /// Author          : Hiren Patel
        /// Creation Date   : 30 Dec 2017
        /// Purpose         : To get Move data from services  
        /// Revision        : 
        /// </summary>
        private MoveDataModel GetMoveData(GetMoveDataResponse moveData)
        {
            UtilityPCL.SetCustomerMoveData(moveData);
            MoveDataModel CustomerMoveData = SetMoveDataModel(moveData);

            SetCity(CustomerMoveData, moveData);

            SetDestinationAddress(CustomerMoveData, moveData);

            SetOriginAddress(CustomerMoveData, moveData);

            SetDays(CustomerMoveData, moveData);

            SetMoveDetails(CustomerMoveData, moveData);

            CustomerMoveData.WhatMattersMost = moveData.WhatMattersMost;

            SetValuation(CustomerMoveData, moveData);

            SetStatusCode(CustomerMoveData, moveData);

            SetServiceCode(CustomerMoveData, moveData);

            return CustomerMoveData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moveData"></param>
        /// <returns></returns>
        private MoveDataModel SetMoveDataModel(GetMoveDataResponse moveData)
        {
            MoveDataModel customerMoveData = new MoveDataModel()
            {
                MoveId = moveData.MoveId,
                MoveNumber = moveData.MoveNumber,
                IsActive = moveData.IsActive
            };

            return customerMoveData;
        }

        /// <summary>
        /// Method Name     : SetCity
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : set origion and destination city
        /// Revision        :
        /// </summary>
        /// <param name="customerMoveData"></param>
        /// <param name="moveData"></param>
        private void SetCity(MoveDataModel customerMoveData, GetMoveDataResponse moveData)
        {
            if (!string.IsNullOrEmpty(moveData.Destination_City) && moveData.Destination_City.Length > 3)
            {
                customerMoveData.Destination_City = moveData.Destination_City.Substring(0, 3).ToUpper();
            }

            if (!string.IsNullOrEmpty(moveData.Origin_City) && moveData.Origin_City.Length > 3)
            {
                customerMoveData.Origin_City = moveData.Origin_City.Substring(0, 3).ToUpper();
            }
        }

        /// <summary>
        /// Method Name     : SetOriginAddress
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : set origion address
        /// Revision        :
        /// </summary>
        /// <param name="customerMoveData"></param>
        /// <param name="moveData"></param>
        private void SetOriginAddress(MoveDataModel customerMoveData, GetMoveDataResponse moveData)
        {
            if (!string.IsNullOrEmpty(moveData.CustomOriginAddress))
            {
                if (moveData.CustomOriginAddress.Length < 30)
                {
                    customerMoveData.CustomOriginAddress = moveData.CustomOriginAddress;
                }
                else
                {
                    customerMoveData.CustomOriginAddress = moveData.CustomOriginAddress.Substring(0, 30);
                }
            }
        }

        /// <summary>
        /// Method Name     : SetDestinationAddress
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : set destination address
        /// Revision        :
        /// </summary>
        /// <param name="CustomerMoveData"></param>
        /// <param name="moveData"></param>
        private void SetDestinationAddress(MoveDataModel CustomerMoveData, GetMoveDataResponse moveData)
        {
            if (!string.IsNullOrEmpty(moveData.CustomDestinationAddress))
            {
                if (moveData.CustomDestinationAddress.Length < 30)
                {
                    CustomerMoveData.CustomDestinationAddress = moveData.CustomDestinationAddress;
                }
                else
                {
                    CustomerMoveData.CustomDestinationAddress = moveData.CustomDestinationAddress.Substring(0, 30);
                }
            }
        }

        /// <summary>
        /// Method Name     : SetDays
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : calculate days left & set date format
        /// Revision        :
        /// </summary>
        /// <param name="customerMoveData"></param>
        /// <param name="moveData"></param>
        private void SetDays(MoveDataModel customerMoveData, GetMoveDataResponse moveData)
        {
            DateTime tempDateObject;
            if (!string.IsNullOrEmpty(moveData.MoveStartDate))
            {
                tempDateObject = UtilityPCL.ConvertDateTimeInUSFormat(moveData.MoveStartDate);
                customerMoveData.daysLeft = UtilityPCL.CalulateMoveDays(tempDateObject).ToString();
                customerMoveData.MoveStartDate = GetDateForDiplay(moveData.MoveStartDate);
            }
            if ((!string.IsNullOrEmpty(moveData.MoveEndDate)) && moveData.MoveEndDate != Resource.DateTBD)
            {
                customerMoveData.MoveEndDate = GetDateForDiplay(moveData.MoveEndDate);
            }
            else
            {
                customerMoveData.MoveEndDate = Resource.DateTBD;
            }
        }

        /// <summary>
        /// Method Name     : SetDays
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : calculate days left & set date format
        /// Revision        :
        /// </summary>
        /// <param name="customerMoveData"></param>
        /// <param name="moveData"></param>
        private void SetMoveDetails(MoveDataModel customerMoveData, GetMoveDataResponse moveData)
        {
            if (!string.IsNullOrEmpty(moveData.MoveDetails_PackStartDate))
            {
                customerMoveData.MoveDetails_PackStartDate = GetDateForDiplay(moveData.MoveDetails_PackStartDate);
            }
            if (!string.IsNullOrEmpty(moveData.MoveDetails_LoadStartDate))
            {
                customerMoveData.MoveDetails_LoadStartDate = GetDateForDiplay(moveData.MoveDetails_LoadStartDate);
            }
            if (!string.IsNullOrEmpty(moveData.MoveDetails_DeliveryStartDate))
            {
                customerMoveData.MoveDetails_DeliveryStartDate = GetDateForDiplay(moveData.MoveDetails_DeliveryStartDate);
            }
            if (!string.IsNullOrEmpty(moveData.MoveDetails_DeliveryEndDate))
            {
                customerMoveData.MoveDetails_DeliveryEndDate = GetDateForDiplay(moveData.MoveDetails_DeliveryEndDate);
            }
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
        /// Method Name     : SetValuation
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Set specific currency formats for  valuation fields
        /// Revision        :
        /// </summary>
        /// <param name="customerMoveData"></param>
        /// <param name="moveData"></param>
        private void SetValuation(MoveDataModel customerMoveData, GetMoveDataResponse moveData)
        {
            if (!string.IsNullOrEmpty(moveData.ExcessValuation))
            {
                customerMoveData.ExcessValuation = UtilityPCL.CurrencyFormat(moveData.ExcessValuation);
            }

            if (!string.IsNullOrEmpty(moveData.ValuationDeductible))
            {
                customerMoveData.ValuationDeductible = UtilityPCL.GetMoveDataDisplayValue(moveData.ValuationDeductible, MoveDataDisplayResource.msgValuationDeductible);
            }

            if (!string.IsNullOrEmpty(moveData.ValuationCost))
            {
                customerMoveData.ValuationCost = UtilityPCL.CurrencyFormat(moveData.ValuationCost);
            }
        }

        /// <summary>
        /// Method Name     : SetServiceCode
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : set list of services from comma seperated string
        /// Revision        :
        /// </summary>
        /// <param name="customerMoveData"></param>
        /// <param name="moveData"></param>
        private void SetServiceCode(MoveDataModel customerMoveData, GetMoveDataResponse moveData)
        {
            if (!string.IsNullOrEmpty(moveData.ServiceCode))
            {
                String[] myServicecode = moveData.ServiceCode.Split(',');
                customerMoveData.MyServices = new List<MyServicesModel>();

                foreach (string serviceCode in myServicecode)
                {
                    customerMoveData.MyServices.Add(new MyServicesModel()
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
        /// Purpose         : Set Status code using mapping set in MoveDataDisplayResource resource file
        /// Revision        :
        /// </summary>
        /// <param name="customerMoveData"></param>
        /// <param name="moveData"></param>
        private void SetStatusCode(MoveDataModel customerMoveData, GetMoveDataResponse moveData)
        {

            if (!string.IsNullOrEmpty(moveData.StatusReason))
            {
                customerMoveData.StatusReason = UtilityPCL.GetMoveDataDisplayValue(moveData.StatusReason, MoveDataDisplayResource.msgMoveCode);
            }
        }

        /// <summary>
        /// Method Name     : PutMoveData
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : update move status in crm
        /// Revision        :
        /// </summary>
        /// <param name="moveDataModel"></param>
        /// <param name="moveID"></param>
        /// <returns></returns>
        public async Task<APIResponse<MoveDataModel>> PutMoveData(MoveDataModel moveDataModel, string moveID)
        {
            APIResponse<MoveDataModel> apiResponse = new APIResponse<MoveDataModel>();
            APIResponse<MoveDataModel> putMoveIDResponse = await moveAPIService.PutMoveData(moveDataModel, moveID);

            if (putMoveIDResponse.STATUS)
            {
                apiResponse.STATUS = true;
            }
            else
            {
                apiResponse.Message = putMoveIDResponse.Message;
            }

            return apiResponse;
        }
    }
}
