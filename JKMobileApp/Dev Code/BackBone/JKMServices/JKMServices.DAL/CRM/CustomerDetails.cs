using JKMServices.DAL.CRM.common;
using JKMServices.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Utility;
using Utility.Logger;

namespace JKMServices.DAL.CRM
{
    public class CustomerDetails : ICustomerDetails
    {
        private readonly ICRMUtilities crmUtilities;
        private const string contactEntityName = "contacts";
        private readonly IResourceManagerFactory resourceManager;
        private readonly ICRMTODTOMapper crmTODTOMapper;
        private readonly IDTOToCRMMapper dtoToCRMMapper;
        private readonly ILogger logger;

        //constructor
        public CustomerDetails(ICRMTODTOMapper crmTODTOMapper,
                               ICRMUtilities crmUtilities,
                               IDTOToCRMMapper dtoToCRMMapper,
                               IResourceManagerFactory resourceManager,
                               ILogger logger)
        {
            this.crmUtilities = crmUtilities;
            this.crmTODTOMapper = crmTODTOMapper;
            this.dtoToCRMMapper = dtoToCRMMapper;
            this.resourceManager = resourceManager;
            this.logger = logger;
        }

        /// Method Name     : GetCustomerGUID
        /// Author          : Pratik Soni
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : To get Customer GUID from the CRM for given customerID
        /// Revision        : 
        /// </summary>
        public Dictionary<string, string> GetCustomerGUID(string customerID)
        {
            string retrieveFieldList;
            string filterString;
            retrieveFieldList = resourceManager.GetString("crm_contact_customerId");
            filterString = resourceManager.GetString("crm_contact_customerNumber") + " eq '" + customerID + "'";

            logger.Info("Get Request encountered");
            return crmUtilities.ExecuteGetRequest(contactEntityName, retrieveFieldList, filterString);
        }

        /// Method Name     : GetCustomerID
        /// Author          : Pratik Soni
        /// Creation Date   : 1 Dec 2017
        /// Purpose         : To get CustomerID from the given emailID
        /// Revision        : 
        /// </summary>
        public DTO.ServiceResponse<DTO.Customer> GetCustomerIDAsync(string emailID)
        {
            StringBuilder retrieveFieldList = new StringBuilder();
            string filterString;

            retrieveFieldList.Append(resourceManager.GetString("crm_contact_customerNumber"));
            retrieveFieldList.Append("," + resourceManager.GetString("crm_contact_customerFullName"));
            retrieveFieldList.Append("," + resourceManager.GetString("crm_contact_iscustomerregistered"));

            filterString = resourceManager.GetString("crm_contact_customerPrimaryEmail") + " eq '" + emailID + "'";

            logger.Info("Get CustomerID async Request encountered");
            var crmResponse = crmUtilities.ExecuteGetRequest(contactEntityName, retrieveFieldList.ToString(), filterString);
            var validatedResponse = crmTODTOMapper.ValidateResponse<Customer>(crmResponse);
            if (!string.IsNullOrEmpty(validatedResponse.Message) || !string.IsNullOrEmpty(validatedResponse.Information))
            {
                return validatedResponse;
            }
            return ReturnServiceResponse(crmResponse);
        }

        /// Method Name     : PutCustomerVerificationData
        /// Author          : Pratik Soni
        /// Creation Date   : 1 Dec 2017
        /// Purpose         : To update exisiting customer verification data for the given CustomerID
        /// Revision        : 
        /// </summary>
        public ServiceResponse<Customer> PutCustomerVerificationData(string customerID, string jsonFormattedData)
        {
            Dictionary<string, string> crmPutResponse;
            string contactID;
            try
            {
                logger.Info("Put Customer Request encountered");

                contactID = GetSpecificAttributeFromResponse(GetCustomerGUID(customerID), resourceManager.GetString("crm_contact_customerId"));

                JObject requestContent = JObject.Parse(jsonFormattedData);

                if (Validations.IsValid(requestContent["VerificationCode"]))
                {
                    crmPutResponse = crmUtilities.ExecutePutRequest(contactEntityName, contactID, dtoToCRMMapper.MapCustomerDTOToCRM(requestContent.ToString()));
                }
                else
                {
                    requestContent["IsCustomerRegistered"] = "true";
                    crmPutResponse = crmUtilities.ExecutePutRequest(contactEntityName, contactID, dtoToCRMMapper.MapCustomerDTOToCRM(requestContent.ToString()));
                }
                return crmUtilities.GetFormattedResponseToDTO<Customer>(crmPutResponse);
            }
            catch (Exception ex)
            {
                logger.Error(ex.InnerException.ToString());
                return new ServiceResponse<Customer> { Message = ex.ToString() };
            }
        }       

        /// Method Name     : GetCustomerEmail
        /// Author          : Pratik Soni
        /// Creation Date   : 19 Dec 2017
        /// Purpose         : To get Customer Email ID from CRM
        /// Revision        : 
        /// </summary>
        public string GetCustomerEmail(string customerID)
        {
            string filterString, jsonString;
            JObject valueObject;

            filterString = resourceManager.GetString("crm_contact_customerNumber") + " eq '" + customerID + "'";

            Dictionary<string, string> crmResponse;
            logger.Info("Execute Get Request encountered");

            crmResponse = crmUtilities.ExecuteGetRequest(contactEntityName, resourceManager.GetString("crm_contact_customerPrimaryEmail"), filterString);
            jsonString = ValidateResponse(crmResponse);
            valueObject = JObject.Parse(jsonString);
            return valueObject["emailaddress1"].ToString();
        }

        /// Method Name     : ValidateResponse
        /// Author          : Pratik Soni
        /// Creation Date   : 24 Jan 2018
        /// Purpose         : To validate crm response and return string if "value is not null".
        /// Revision        : 
        /// </summary>
        private string ValidateResponse(Dictionary<string, string> crmResponse)
        {
            if (!crmResponse.ContainsKey("CONTENT"))
            {
                return string.Empty;
            }

            JObject jsonObject = Utility.General.ConvertToJObject(crmResponse["CONTENT"].ToString());
            if (jsonObject["value"] == null || ((JContainer)jsonObject["value"]).Count == 0)
            {
                return null;
            }
            return jsonObject["value"][0].ToString(Newtonsoft.Json.Formatting.None);
        }

        /// Method Name     : GetSpecificAttributeFromResponse
        /// Author          : Pratik Soni
        /// Creation Date   : 19 Dec 2017
        /// Purpose         : To fetch specific field from the "value" node of CRM response on 0th index
        /// Revision        : 
        /// </summary>
        private string GetSpecificAttributeFromResponse(Dictionary<string, string> crmResponse, string requiredField)
        {
            string returnValue;
            JObject jsonObject = Utility.General.ConvertToJObject(crmResponse["CONTENT"].ToString());
            if (jsonObject["value"] == null || ((Newtonsoft.Json.Linq.JContainer)jsonObject["value"]).Count == 0)
            {
                return jsonObject.ToString(Newtonsoft.Json.Formatting.None);
            }
            JObject valueObject = JObject.Parse(jsonObject["value"][0].ToString(Newtonsoft.Json.Formatting.None));
            returnValue = valueObject[requiredField].ToString();
            return returnValue;
        }

        /// Method Name     : GetCustomerProfileData
        /// Author          : Pratik Soni
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : To fetch customer profile data using Customer ID(jkmoving_customernumer)
        /// Revision        : 
        /// </summary>
        public ServiceResponse<Customer> GetCustomerProfileData(string customerID)
        {
            string retriveFieldList, filterString;

            retriveFieldList = resourceManager.GetString("contactProfileDataFields");
            filterString = resourceManager.GetString("crm_contact_customerNumber") + " eq '" + customerID + "'";
            logger.Info("Execute Get Request encountered");
            Dictionary<string, string> crmResponse = crmUtilities.ExecuteGetRequest(contactEntityName, retriveFieldList, filterString);
            ServiceResponse<Customer> validatedResponse = crmTODTOMapper.ValidateResponse<Customer>(crmResponse);
            if (!string.IsNullOrEmpty(validatedResponse.Message) || !string.IsNullOrEmpty(validatedResponse.Information))
            {
                return validatedResponse;
            }
            return ReturnServiceResponse(crmResponse);
        }

        /// Method Name     : GetCustomerVerificationData
        /// Author          : Pratik Soni
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : To fetch customer verification data using Customer ID(jkmoving_customernumer)
        /// Revision        : 
        /// </summary>
        public ServiceResponse<Customer> GetCustomerVerificationData(string customerID)
        {
            ServiceResponse<Customer> crmResponse;

            //Get the CRM response for customer ID and returns error message if the customer is not valid
            crmResponse = GetVerificationDataForCustomer(customerID);

            //Validates CRM response and returns the value accordingly.
            return ValidateVerificationData(crmResponse);
        }

        /// Method Name     : ValidateVerificationData
        /// Author          : Pratik Soni
        /// Creation Date   : 24 Jan 2018
        /// Purpose         : Validates crm response
        /// Revision        : 
        /// </summary>
        /// <returns> Service response of customer DTO if response is valid else pass reponse with information</returns>
        private ServiceResponse<Customer> ValidateVerificationData(ServiceResponse<Customer> crmResponse)
        {
            DateTime currentUTCDate;
            if (crmResponse.Message != null)
            {
                logger.Error(crmResponse.Message);
                return crmResponse;
            }
            else if (crmResponse.Information != null)
            {
                logger.Info(crmResponse.Information);
                return crmResponse;
            }
            currentUTCDate = Utility.General.GetCurrentDateInUSDateTimeFormat();
            if (string.IsNullOrEmpty(crmResponse.Data.CodeValidTill.ToString()) || crmResponse.Data.CodeValidTill < currentUTCDate)
            {
                logger.Info(resourceManager.GetString("msgNoVerificationCodeFound"));
                return new ServiceResponse<Customer> { Information = resourceManager.GetString("msgNoVerificationCodeFound") };
            }

            //if Valid verification code found for customer
            return crmResponse;
        }

        /// Method Name     : GetVerificationDataForCustomer
        /// Author          : Pratik Soni
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : To check if customer ID exist in CRM.
        /// Revision        : 
        /// </summary>
        /// <returns> true if respective customer GUID found for customer id, else returne false
        private ServiceResponse<Customer> GetVerificationDataForCustomer(string customerID)
        {
            string retriveFieldList, filterString;
            Dictionary<string, string> crmResponse;
            ServiceResponse<Customer> validatedResponse;

            retriveFieldList = resourceManager.GetString("contactVerificationDataFields");
            filterString = resourceManager.GetString("crm_contact_customerNumber") + " eq '" + customerID + "'";

            crmResponse = crmUtilities.ExecuteGetRequest(contactEntityName, retriveFieldList, filterString);
            validatedResponse = crmTODTOMapper.ValidateResponse<Customer>(crmResponse);
            if (validatedResponse.Message != null)
            {
                return new ServiceResponse<Customer> { Message = resourceManager.GetString("msgServiceUnavailable") };
            }
            else if (validatedResponse.Information != null)
            {
                logger.Error(resourceManager.GetString("msgInvalidCustomer"));
                return new ServiceResponse<Customer> { Information = resourceManager.GetString("msgInvalidCustomer") };
            }

            return crmTODTOMapper.MapCustomerResponseToDTO(crmResponse);
        }

        /// Method Name     : PutCustomerProfileData
        /// Author          : Pratik Soni
        /// Creation Date   : 24 Jan 2018
        /// Purpose         : PUT request for customer
        /// Revision        : 
        /// </summary>
        /// <returns> true if respective customer GUID found for customer id, else returne false
        public ServiceResponse<Customer> PutCustomerProfileData(string customerID, string jsonFormattedData)
        {
            Dictionary<string, string> crmResponse;
            string customerGUID;
            try
            {
                customerGUID = GetSpecificAttributeFromResponse(GetCustomerGUID(customerID), resourceManager.GetString("crm_contact_customerId"));
                if (string.IsNullOrEmpty(customerGUID))
                {
                    logger.Info(resourceManager.GetString("msgInvalidCustomer"));
                    return new ServiceResponse<Customer> { Information = resourceManager.GetString("msgInvalidCustomer") };
                }

                crmResponse = crmUtilities.ExecutePutRequest(resourceManager.GetString("crm_contact_entityName"), customerGUID, dtoToCRMMapper.MapCustomerDTOToCRM(jsonFormattedData));
                return crmUtilities.GetFormattedResponseToDTO<Customer>(crmResponse);
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgServiceUnavailableUnauthorized"), ex);
                return new ServiceResponse<Customer> { Message = resourceManager.GetString("msgServiceUnavailableUnauthorized") };
            }
        }

        /// Method Name     : CheckCustomerRegistered
        /// Author          : Pratik Soni
        /// Creation Date   : 29 Jan 2018
        /// Purpose         : Check if customer is registered or not
        /// Revision        : 
        /// </summary>
        /// <returns> true if respective customer GUID found for customer id, else returne false
        public bool CheckCustomerRegistered(string customerID)
        {
            string retrieveFieldList;
            StringBuilder filterString = new StringBuilder();
            Dictionary<string, string> crmResponse;
            string registeredCustomer;
            try
            {
                retrieveFieldList = resourceManager.GetString("crm_contact_customerId");
                filterString.Append(resourceManager.GetString("crm_contact_customerNumber") + " eq '" + customerID + "'");
                filterString.Append(" and " + resourceManager.GetString("crm_contact_iscustomerregistered") + " eq true");

                crmResponse = crmUtilities.ExecuteGetRequest(contactEntityName, retrieveFieldList, filterString.ToString());
                registeredCustomer = Utility.General.GetSpecificAttributeFromCRMResponse(crmResponse, resourceManager.GetString("crm_contact_customerId"));

                if (Utility.Validations.IsValid(registeredCustomer))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return false;
            }
        }

        /// Method Name     : ReturnServiceResponse
        /// Author          : Pratik Soni
        /// Creation Date   : 07 Feb 2018
        /// Purpose         : To return service response from CRM's dictionary response
        /// Revision        : 
        /// </summary>
        private ServiceResponse<DTO.Customer> ReturnServiceResponse(Dictionary<string, string> crmResponse)
        {
            if (crmUtilities.ContainsNullValue(crmResponse))
            {
                logger.Info(resourceManager.GetString("CRM_STATUS_204"));
                return new ServiceResponse<Customer> { Message = resourceManager.GetString("CRM_STATUS_204") };
            }
            var validatedResponse = crmTODTOMapper.ValidateResponse<Customer>(crmResponse);
            if (validatedResponse.Message != null)
            {
                logger.Error(resourceManager.GetString("msgServiceUnavailable"));
                return new ServiceResponse<Customer> { Message = resourceManager.GetString("msgServiceUnavailable") };
            }
            else if (validatedResponse.Information != null)
            {
                logger.Error(resourceManager.GetString("CRM_STATUS_204"));
                return new ServiceResponse<Customer> { Information = resourceManager.GetString("CRM_STATUS_204") };
            }
            return crmTODTOMapper.MapCustomerResponseToDTO(crmResponse);
        }
    }
}
