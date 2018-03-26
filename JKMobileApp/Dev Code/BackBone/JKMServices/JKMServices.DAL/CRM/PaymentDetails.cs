using JKMServices.DAL.CRM.common;
using JKMServices.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Utility;
using Utility.Logger;

namespace JKMServices.DAL.CRM
{
    public class PaymentDetails : IPaymentDetails
    {
        private readonly ICRMUtilities crmUtilities;
        private const string transactionEntityNamePlural = "onerivet_transactions";
        private const string nodusPayFabricEntityNamePlural = "nodus_payfabricsecuritycodes";
        private const string moveEntityPluralName = "jkmoving_moves";
        private readonly IDTOToCRMMapper dtoToCRMMapper;
        private readonly ICRMTODTOMapper crmToDTOMapper;
        private readonly IMoveDetails dalMoveDetails;
        private readonly ICustomerDetails dalCustomerDetails;
        private readonly IResourceManagerFactory resourceManager;
        private readonly ILogger logger;

        //constructor
        public PaymentDetails(IDTOToCRMMapper dtoToCRMMapper,
                              ICRMTODTOMapper crmToDTOMapper,
                              ICRMUtilities crmUtilities,
                              IMoveDetails dalMoveDetails,
                              ICustomerDetails dalCustomerDetails,
                              IResourceManagerFactory resourceManager,
                              ILogger logger)
        {
            this.crmUtilities = crmUtilities;
            this.dtoToCRMMapper = dtoToCRMMapper;
            this.crmToDTOMapper = crmToDTOMapper;
            this.dalMoveDetails = dalMoveDetails;
            this.dalCustomerDetails = dalCustomerDetails;
            this.resourceManager = resourceManager;
            this.logger = logger;
        }

        /// Method Name     : PostTransactionHistory
        /// Author          : Ranjana Singh
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Creates new records for transaction history based on given customer ID.
        /// Revision        : By Pratik Soni on 27 Jan 2018: Modified to send proper request body for CRM POST method and return validated serviceResponse
        /// </summary>
        public DTO.ServiceResponse<DTO.Payment> PostTransactionHistory(DTO.Payment dtoPayment)
        {
            Dictionary<string, string> crmResponse;
            string jsonFormattedData;
            string moveGUID, customerGUID;

            try
            {
                moveGUID = General.GetSpecificAttributeFromCRMResponse(dalMoveDetails.GetMoveGUID(dtoPayment.MoveID), "jkmoving_moveid");
                customerGUID = General.GetSpecificAttributeFromCRMResponse(dalCustomerDetails.GetCustomerGUID(dtoPayment.CustomerID), "contactid");
                dtoPayment.MoveID = moveGUID;
                dtoPayment.CustomerID = customerGUID;

                jsonFormattedData = General.ConvertToJson<Payment>(dtoPayment);
                crmResponse = crmUtilities.ExecutePostRequest(transactionEntityNamePlural, dtoToCRMMapper.MapPaymentDTOToCRM(jsonFormattedData));

                return crmUtilities.GetFormattedResponseToDTO<Payment>(crmResponse);
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgServiceUnavailable"), ex);
                return new ServiceResponse<Payment> { Message = resourceManager.GetString("msgServiceUnavailable") };
            }
        }

        /// Method Name     : GetDeviceID
        /// Author          : Pratik Soni
        /// Creation Date   : 07 Feb 2018
        /// Purpose         : To Get device ID from CRM - for nodus payfabric
        /// Revision        : 
        /// </summary>
        /// <returns> ServiceResponse with Paymeny DTO having only DeviceID value. </returns>
        public ServiceResponse<DTO.Payment> GetDeviceID()
        {
            string retriveField = resourceManager.GetString("crm_nodus_payfabricsecuritycode_deviceID");
            string filterString = "statecode eq 0";
            Dictionary<string, string> crmResponse = crmUtilities.ExecuteGetRequest(nodusPayFabricEntityNamePlural, retriveField, filterString);
            return ReturnServiceResponse(crmResponse);
        }

        /// Method Name     : GetAmount
        /// Author          : Pratik Soni
        /// Creation Date   : 19 Feb 2018
        /// Purpose         : To retrive payment details from CRM for customer.
        /// Revision        : 
        /// </summary>
        /// <returns> ServiceResponse with Paymeny DTO having only DeviceID value. </returns>
        public ServiceResponse<Payment> GetAmount(string moveID)
        {
            StringBuilder filterString;
            Dictionary<string, string> crmResponse;
            try
            {
                logger.Info(resourceManager.GetString("logMethodEncountered"));
                string retriveFields = "jkmoving_deposit,jkmoving_actuallinehaul,jkmoving_estimatedlinehaul,jkmoving_totalremaining,jkmoving_totalpaid";
                string moveGUID = General.GetSpecificAttributeFromCRMResponse(dalMoveDetails.GetMoveGUID(moveID), "jkmoving_moveid");

                if (!Validations.IsValid(moveGUID))
                {
                    logger.Info(resourceManager.GetString("CRM_STATUS_204") + ": " + resourceManager.GetString("msgMoveNotExistsOrInvalid"));
                    return new ServiceResponse<Payment> { Information = resourceManager.GetString("CRM_STATUS_204") };
                }
                filterString = new StringBuilder();
                filterString.Append("jkmoving_moveid eq " + moveGUID);
                filterString.Append(" and " + resourceManager.GetString("crm_ActiveRecordFilter"));

                crmResponse = crmUtilities.ExecuteGetRequest(moveEntityPluralName, retriveFields, filterString.ToString());
                return ReturnServiceResponse(crmResponse);
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgServiceUnavailable"), ex);
                return new ServiceResponse<Payment> { Message = resourceManager.GetString("msgServiceUnavailable") };
            }
        }

        /// Method Name     : ReturnServiceResponse
        /// Author          : Pratik Soni
        /// Creation Date   : 07 Feb 2018
        /// Purpose         : To return service response from CRM's dictionary response
        /// Revision        : 
        /// </summary>
        private ServiceResponse<DTO.Payment> ReturnServiceResponse(Dictionary<string, string> crmResponse)
        {
            if (crmUtilities.ContainsNullValue(crmResponse))
            {
                logger.Info(resourceManager.GetString("CRM_STATUS_204"));
                return new ServiceResponse<Payment> { Message = resourceManager.GetString("CRM_STATUS_204") };
            }
            var validatedResponse = crmToDTOMapper.ValidateResponse<Payment>(crmResponse);
            if (validatedResponse.Message != null)
            {
                logger.Error(resourceManager.GetString("msgServiceUnavailable"));
                return new ServiceResponse<Payment> { Message = resourceManager.GetString("msgServiceUnavailable") };
            }
            else if (validatedResponse.Information != null)
            {
                logger.Error(resourceManager.GetString("CRM_STATUS_204"));
                return new ServiceResponse<Payment> { Information = resourceManager.GetString("CRM_STATUS_204") };
            }
            return crmToDTOMapper.MapPaymentResponseToDTO(crmResponse);
        }

        /// Method Name     : PutAmount
        /// Author          : Pratik Soni
        /// Creation Date   : 19 Feb 2018
        /// Purpose         : To retrive payment details from CRM for customer.
        /// Revision        : 
        /// </summary>
        /// <returns> ServiceResponse with Paymeny DTO having only DeviceID value. </returns>
        public ServiceResponse<Payment> PutAmount(string jsonFormattedData)
        {
            Dictionary<string, string> crmResponse;
            string moveGUID;
            string requestContentUsingCRMFields;
            try
            {
                var requestContent = (JObject)JsonConvert.DeserializeObject(jsonFormattedData);
                moveGUID = General.GetSpecificAttributeFromCRMResponse(dalMoveDetails.GetMoveGUID(requestContent.Property("MoveID").Value.ToString()), "jkmoving_moveid");
                if (!Validations.IsValid(moveGUID))
                {
                    logger.Info(resourceManager.GetString("msgInvalidMove"));
                    return new ServiceResponse<Payment> { Information = resourceManager.GetString("msgInvalidMove") };
                }
                requestContent.Property("MoveID").Remove(); // Deleting MoveID from requestContent as we are updating only TotalPaid amount.

                requestContentUsingCRMFields = dtoToCRMMapper.MapPaymentDTOToCRM(requestContent.ToString(Formatting.None));
                crmResponse = crmUtilities.ExecutePutRequest(moveEntityPluralName, moveGUID, requestContentUsingCRMFields);

                return crmUtilities.GetFormattedResponseToDTO<Payment>(crmResponse);
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgServiceUnavailable"), ex);
                return new ServiceResponse<Payment> { Message = resourceManager.GetString("msgServiceUnavailable") };
            }
        }
    }
}
