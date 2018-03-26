using JKMServices.DAL.CRM.common;
using JKMServices.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using Utility;
using Utility.Logger;

namespace JKMServices.DAL.CRM
{
    public class AlertDetails : IAlertDetails
    {
        private readonly ICRMUtilities crmUtilities;
        private const string activityEntityName = "onerivet_appnotificationses";

        private readonly DAL.CRM.ICustomerDetails crmCustomerDetails;
        private readonly IResourceManagerFactory resourceManager;
        private readonly ICRMTODTOMapper crmTODTOMapper;
        private readonly IDTOToCRMMapper dtoToCRMMapper;
        private readonly ILogger logger;

        //constructor
        public AlertDetails(ICRMTODTOMapper crmTODTOMapper,
                            DAL.CRM.ICustomerDetails crmCustomerDetails,
                            ICRMUtilities crmUtilities,
                            IDTOToCRMMapper dtoToCRMMapper,
                            IResourceManagerFactory resourceManager,
                            ILogger logger)
        {
            this.crmUtilities = crmUtilities;
            this.crmCustomerDetails = crmCustomerDetails;
            this.crmTODTOMapper = crmTODTOMapper;
            this.dtoToCRMMapper = dtoToCRMMapper;
            this.resourceManager = resourceManager;
            this.logger = logger;
        }

        /// <summary>
        /// Method Name     : GetAlertList
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : Method to get list of alerts from database
        /// Revision        : 
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="startDate"></param>
        /// <returns>List of Alert DTO</returns>
        public ServiceResponse<List<DTO.Alert>> GetAlertList(string customerID, string startDate = null)
        {
            string retriveFieldList, customerGUID;
            StringBuilder filterString = new StringBuilder();
            Dictionary<string, string> customerResponse, alertResponse;
            try
            {
                //Get customer GUID
                customerResponse = crmCustomerDetails.GetCustomerGUID(customerID);
                customerGUID = crmUtilities.GetSpecificAttributeFromResponse(customerResponse, resourceManager.GetString("crm_contact_customerId"));

                retriveFieldList = resourceManager.GetString("activityGetAlertListFields");
                filterString.Append(resourceManager.GetString("crm_activity_regardingobjectidvalue") + " eq " + customerGUID);
                if (Validations.IsValid(startDate))
                {
                    DateTime.TryParse(startDate, new CultureInfo("en-US"), DateTimeStyles.None, out DateTime returnValue);
                    filterString.Append(" and " + resourceManager.GetString("crm_activity_actualstart") + " ge " + returnValue.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));
                }

                alertResponse = crmUtilities.ExecuteGetRequest(activityEntityName, retriveFieldList, filterString.ToString());
                return ValidateAndGenerateResponse(alertResponse);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message.ToString());
                return new ServiceResponse<List<DTO.Alert>> { Message = resourceManager.GetString("msgServiceUnavailable") };
            }
        }

        /// <summary>
        /// Method Name     : ValidateAndGenerateResponse
        /// Author          : Pratik Soni
        /// Creation Date   : 02 Feb 2018
        /// Purpose         : To validate and generate proper response based on details received from CRM
        /// Revision        : 
        /// </summary>
        /// <param name="alertResponse"></param>
        private ServiceResponse<List<Alert>> ValidateAndGenerateResponse(Dictionary<string, string> alertResponse)
        {
            ServiceResponse<List<Alert>> validatedResponse;
            validatedResponse = crmTODTOMapper.ValidateResponse<List<DTO.Alert>>(alertResponse);

            if (validatedResponse.Message != null)
            {
                return new ServiceResponse<List<Alert>> { Message = resourceManager.GetString("msgServiceUnavailable") };
            }
            else if (validatedResponse.Information != null)
            {
                logger.Error(resourceManager.GetString("msgInvalidCustomer"));
                return new ServiceResponse<List<Alert>> { Information = resourceManager.GetString("msgInvalidCustomer") };
            }
            return crmTODTOMapper.MapAlertResponseToDTO(alertResponse);
        }

        /// <summary>
        /// Method Name      : PostAlertList
        /// Author           : Pratik Soni
        /// Creation Date    : 29 Dec2017
        /// Purpose          : Insert the given list of Alerts in "Alerts" table in SQL.
        /// Revision         :
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="jsonFormattedData"/>
        /// <returns></returns>
        public ServiceResponse<Alert> PostAlertDetails(string customerID, string jsonFormattedData)
        {
            Dictionary<string, string> postCRMResponse;
            var requestContent = (JObject)JsonConvert.DeserializeObject(jsonFormattedData);
            string requestContentUsingCRMFields;
            try
            {
                if (!string.IsNullOrEmpty(requestContent.Property("AlertID").Name))
                {
                    requestContent.Property("AlertID").Remove();
                }
                requestContentUsingCRMFields = dtoToCRMMapper.MapAlertDTOToCRM(requestContent.ToString(Formatting.None));
                postCRMResponse = crmUtilities.ExecutePostRequest(activityEntityName, requestContentUsingCRMFields);

                return crmUtilities.GetFormattedResponseToDTO<Alert>(postCRMResponse);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Method Name      : PutAlertDetails
        /// Author           : Ranjana Singh
        /// Creation Date    : 24 Feb 2018
        /// Purpose          : Update the given list of Alerts in "Alerts" table in CRM.
        /// Revision         :
        /// </summary>
        /// <param name="alertID"></param>
        /// <param name="jsonFormattedData"/>
        /// <returns></returns>
        public ServiceResponse<Alert> PutAlertDetails(string alertID, string jsonFormattedData)
        {
            Dictionary<string, string> putCRMResponse;
            var requestContent = (JObject)JsonConvert.DeserializeObject(jsonFormattedData);
            string requestContentUsingCRMFields;
            try
            {
                if (!string.IsNullOrEmpty(requestContent.Property("StartDate").Name))
                {
                    requestContent.Property("StartDate").Remove();
                }
                if (!string.IsNullOrEmpty(requestContent.Property("EndDate").Name))
                {
                    requestContent.Property("EndDate").Remove();
                }
                requestContentUsingCRMFields = dtoToCRMMapper.MapAlertDTOToCRM(requestContent.ToString(Formatting.None));
                putCRMResponse = crmUtilities.ExecutePutRequest(activityEntityName, alertID, requestContentUsingCRMFields);

                return GetFormattedResponse(putCRMResponse);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message.ToString());
                return null;
            }
        }

        /// Method Name     : GetFormattedResponse
        /// Author          : Pratik Soni
        /// Creation Date   : 05 Feb 2018
        /// Purpose         : To format the CRM response
        /// Revision        : 
        /// </summary>
        /// <returns> returns ServiceResponse with Transaction DTO </returns>
        private ServiceResponse<Alert> GetFormattedResponse(Dictionary<string, string> crmResponse)
        {
            if (crmResponse["STATUS"] == HttpStatusCode.BadRequest.ToString())
            {
                logger.Error(resourceManager.GetString("msgFailToSave"));
                return new ServiceResponse<DTO.Alert> { BadRequest = resourceManager.GetString("CRM_STATUS_400") };
            }
            else if (crmResponse["STATUS"] == HttpStatusCode.ServiceUnavailable.ToString() || crmResponse["STATUS"] == HttpStatusCode.Unauthorized.ToString())
            {
                logger.Info(crmResponse["ERROR"]);
                return new ServiceResponse<Alert> { Information = crmResponse["ERROR"] };
            }
            else if (crmResponse["STATUS"] == HttpStatusCode.NoContent.ToString())
            {
                logger.Info(crmResponse["INFORMATION"]);
                return new ServiceResponse<Alert> { Information = crmResponse["INFORMATION"] };
            }

            logger.Info(resourceManager.GetString("msgSavedSuccessfully"));
            return new ServiceResponse<Alert> { Information = resourceManager.GetString("msgSavedSuccessfully") };
        }
    }
}