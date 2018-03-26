using System;
using System.Collections.Generic;
using Utility;
using Utility.Logger;
using JKMServices.BLL.Interface;
using JKMServices.DTO;
using Newtonsoft.Json.Linq;

namespace JKMServices.BLL
{
    /// <summary>
    /// Class Name          : AlertDetails
    /// Author              : Pratik Soni
    /// Creation Date       : 28 Dec 2017
    /// Purpose             : Validation for data and call to DAL 
    /// Revision            :
    /// </summary>
    /// <returns></returns>
    public class AlertDetails : ServiceBase, IAlertDetails
    {
        private readonly DAL.CRM.IAlertDetails crmAlertDetails;
        private readonly DAL.CRM.ICustomerDetails crmCustomerDetails;
        private readonly IResourceManagerFactory resourceManager;
        private readonly ILogger logger;

        //Constructor
        public AlertDetails(
                            DAL.CRM.IAlertDetails crmAlertDetails,
                            DAL.CRM.ICustomerDetails crmCustomerDetails,
                            IResourceManagerFactory resourceManager,
                            ILogger logger
                            )
        {
            this.crmAlertDetails = crmAlertDetails;
            this.crmCustomerDetails = crmCustomerDetails;
            this.resourceManager = resourceManager;
            this.logger = logger;
        }

        /// <summary>
        /// Method Name     : GetAlertList
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 28 Nov 2017
        /// Purpose         : Method to get customerID using DAL
        /// Revision        : 
        /// </summary>
        public string GetAlertList(string customerID, string startDate = null)
        {
            try
            {
                if (!Validations.IsValid(customerID) || !crmCustomerDetails.CheckCustomerRegistered(customerID))
                {
                    logger.Info(resourceManager.GetString("msgCustomerNotExistsOrInvalid"));
                    return GenerateServiceResponse<DTO.Alert>(MESSAGE, resourceManager.GetString("msgCustomerNotExistsOrInvalid"));
                }
                var crmResponse = crmAlertDetails.GetAlertList(customerID, startDate);

                if (crmResponse.Message != null)
                {
                    logger.Info(resourceManager.GetString("msgErrorOccuredInGettingAlertList"));
                    return GenerateServiceResponse<DTO.Alert>(MESSAGE, resourceManager.GetString("msgErrorOccuredInGettingAlertList"));
                }
                else if (crmResponse.Information != null)
                {
                    logger.Info(resourceManager.GetString("msgNoAlertFound"));
                    return GenerateServiceResponse<DTO.Alert>(INFORMATION, resourceManager.GetString("msgNoAlertFound"));
                }
                else if (crmResponse.BadRequest != null)
                {
                    logger.Info(resourceManager.GetString("msgBadRequest"));
                    return GenerateServiceResponse<DTO.Alert>(BADREQUEST, resourceManager.GetString("msgBadRequest"));
                }
                return GenerateServiceResponse<List<DTO.Alert>>(crmResponse.Data);
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgErrorOccuredInGettingAlertList"), ex);
                return GenerateServiceResponse<DTO.Alert>(MESSAGE, resourceManager.GetString("msgErrorOccuredInGettingAlertList"));
            }
        }

        /// <summary>
        /// Method Name      : PostAlertList
        /// Author           : Pratik Soni
        /// Creation Date    : 28 Dec2017
        /// Purpose          : 
        /// Revision         : 
        /// </summary>
        /// <param name="customerID"/>
        /// <param name="requestContent"/>
        /// <returns></returns>
        public string PostAlertList(string customerID, string requestContent)
        {
            DTO.Alert requestDTOAlertList;
            ServiceResponse<Alert> validationServiceResponse;
            try
            {
                requestDTOAlertList = General.ConvertFromJson<DTO.Alert>(requestContent);

                //Validation for request content
                validationServiceResponse = ValidateRequestContent(requestDTOAlertList, customerID);
                if (Validations.IsValid(validationServiceResponse.BadRequest) || Validations.IsValid(validationServiceResponse.Message))
                {
                    return General.ConvertToJson<ServiceResponse<Alert>>(validationServiceResponse);
                }
                return ExecutePostAlertList(requestDTOAlertList, customerID);
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgFailToSave"), ex);
                return GenerateServiceResponse<DTO.Alert>(MESSAGE, resourceManager.GetString("msgFailToSave"));
            }
        }

        /// <summary>
        /// Method Name      : ExecutePostAlertList
        /// Author           : Pratik Soni
        /// Creation Date    : 05 Feb 2018
        /// Purpose          : Executes the CRM post service for each Alert from the list
        /// Revision         : 
        /// </summary>
        /// <param name="requestObject"/>
        /// <param name="customerID"/>
        /// <returns></returns>
        private string ExecutePostAlertList(DTO.Alert requestObject, string customerID)
        {
            ServiceResponse<Alert> crmResponse;
            Dictionary<string, string> customerResponse;
            try
            {
                if (!crmCustomerDetails.CheckCustomerRegistered(customerID))
                {
                    logger.Error(resourceManager.GetString("msgInvalidCustomer"));
                    return GenerateServiceResponse<ServiceResponse<Alert>>(BADREQUEST, resourceManager.GetString("msgInvalidCustomer"));
                }

                customerResponse = crmCustomerDetails.GetCustomerGUID(customerID);
                requestObject.CustomerID = General.GetSpecificAttributeFromCRMResponse(customerResponse, "contactid");
                crmResponse = crmAlertDetails.PostAlertDetails(customerID, General.ConvertToJson<Alert>(requestObject));

                if (!Validations.IsValid(crmResponse.Information))
                {
                    logger.Error(resourceManager.GetString("msgErrorInInsertingNewAlert") + requestObject.AlertTitle);
                    return General.ConvertToJson<ServiceResponse<Alert>>(crmResponse);
                }

                logger.Info(resourceManager.GetString("msgSavedSuccessfully"));
                return General.ConvertToJson<ServiceResponse<Alert>>(new ServiceResponse<Alert> { });
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgFailToSave"), ex);
                return GenerateServiceResponse<DTO.Alert>(MESSAGE, resourceManager.GetString("msgFailToSave"));
            }
        }

        /// <summary>
        /// Method Name      : ValidateRequestContent
        /// Author           : Pratik Soni
        /// Creation Date    : 05 Feb 2018
        /// Purpose          : To validate request content DTO for POST alert method
        /// Revision         :
        /// </summary>
        /// <returns>BadRequest if validation fails, else returns NULL ServiceResponse </returns>
        private ServiceResponse<Alert> ValidateRequestContent(DTO.Alert requestObject, string customerID)
        {
            try
            {
                if (!IsRequestDataValid(requestObject, true, customerID))
                {
                    logger.Info(resourceManager.GetString("msgBadRequest"));
                    return new ServiceResponse<Alert> { BadRequest = resourceManager.GetString("msgBadRequest") };
                }
                return new ServiceResponse<Alert> { };
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgServiceUnavailable"), ex);
                return new ServiceResponse<Alert> { Message = resourceManager.GetString("msgServiceUnavailable") };
            }
        }

        /// <summary>
        /// Method Name      : IsRequestDataValid
        /// Author           : Pratik Soni
        /// Creation Date    : 02 Jan 2018
        /// Purpose          : To validate request data
        /// Revision         :
        /// </summary>
        /// <param name="requestObject"></param>
        /// <param name="checkForPostRequest">if it is false, it will execute for PUT request </param>
        /// <param name="customerID"> We don't need customerID for PUT request </param>
        /// <returns></returns>
        private bool IsRequestDataValid(DTO.Alert requestObject, bool checkForPostRequest = false, string customerID = "")
        {
            string customerIDValue = string.Empty;
            if (checkForPostRequest)
            {
                customerIDValue = customerID;
            }
            else
            {
                customerIDValue = requestObject.CustomerID;
            }
            if (string.IsNullOrEmpty(customerIDValue) || string.IsNullOrEmpty(requestObject.AlertTitle) || string.IsNullOrEmpty(Convert.ToString(requestObject.StartDate)) ||
                string.IsNullOrEmpty(Convert.ToString(requestObject.EndDate)))
            {
                logger.Info(resourceManager.GetString("msgValidateRequestContentFail"));
                return false;
            }
            logger.Info(resourceManager.GetString("msgValidateRequestContent"));
            return true;
        }


        /// <summary>
        /// Method Name      : PutAlertList
        /// Author           : Ranjana Singh
        /// Creation Date    : 24 Feb 2018
        /// Purpose          : For updating information of Alerts.
        /// Revision         : 
        /// </summary>
        /// <param name="alertID"/>
        /// <param name="requestContent"/>
        /// <returns></returns>
        public string PutAlertList(string alertID, string requestContent)
        {
            ServiceResponse<Alert> putResponse;
            try
            {
                string jsonFormattedData;

                jsonFormattedData = GenerateJsonFormattedData(requestContent);

                if (jsonFormattedData == resourceManager.GetString("emptyJson"))
                {
                    logger.Error(resourceManager.GetString("msgBadRequest"));
                    return General.ConvertToJson<DTO.ServiceResponse<Alert>>(new ServiceResponse<Alert> { BadRequest = resourceManager.GetString("msgBadRequest") });
                }

                putResponse = crmAlertDetails.PutAlertDetails(alertID, jsonFormattedData);

                if (putResponse.Information == resourceManager.GetString("CRM_STATUS_204"))
                {
                    return GenerateServiceResponse<Alert>(DATA, resourceManager.GetString("msgSavedSuccessfully"));
                }
                return General.ConvertToJson<ServiceResponse<Alert>>(putResponse);
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgFailToSave"), ex);
                return GenerateServiceResponse<DTO.Alert>(MESSAGE, resourceManager.GetString("msgFailToSave"));
            }
        }

        /// <summary>
        /// Method Name     : GenerateJsonFormattedData
        /// Author          : Ranjana Singh
        /// Creation Date   : 20 Feb 2018
        /// Purpose         : To generate jsonString only for changed fields
        /// Revision        :
        /// </summary>
        /// <returns> formatted json in string format </returns>
        private string GenerateJsonFormattedData(string requestBody)
        {
            JObject jsonFormattedData;
            try
            {
                JObject requestData = JObject.Parse(requestBody);
                jsonFormattedData = new JObject();
                GenerateRequiredFields(ref jsonFormattedData, requestData);
                return jsonFormattedData.ToString();
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgBadRequest"), ex);
                return resourceManager.GetString("emptyJson");
            }
        }

        /// <summary>
        /// Method Name     : GenerateRequiredFields
        /// Author          : Ranjana Singh
        /// Creation Date   : 20 Feb 2018
        /// Purpose         : To generate jsonString only for Valuation fields
        /// Revision        :
        /// </summary>
        /// <returns> formatted json in string format </returns>
        private void GenerateRequiredFields(ref JObject jsonFormattedData, JObject requestData)
        {
            if (!Validations.IsValid(requestData["StartDate"].ToString()) ||
                !Validations.IsValid(requestData["EndDate"].ToString()))
            {
                jsonFormattedData.Add("StartDate", DateTime.MinValue);
                jsonFormattedData.Add("EndDate", DateTime.MinValue);
                jsonFormattedData.Add("IsActive", requestData["IsActive"].ToString());
                return;
            }

            jsonFormattedData.Add("StartDate", requestData["StartDate"].ToString());
            jsonFormattedData.Add("EndDate", requestData["EndDate"].ToString());
            jsonFormattedData.Add("IsActive", requestData["IsActive"].ToString());
        }
    }
}