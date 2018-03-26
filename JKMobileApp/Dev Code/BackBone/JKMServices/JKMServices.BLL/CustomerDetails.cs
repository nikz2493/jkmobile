using JKMServices.BLL.EmailEngine;
using JKMServices.BLL.Interface;
using JKMServices.BLL.Model;
using JKMServices.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using Utility;
using Utility.Logger;

/// <summary>
/// Class Name      : CustomerDetails
/// Author          : Vivek Bhavsar
/// Creation Date   : 28 Nov 2017
/// Purpose         : Class to access customer details like customerID,verification code etc from DAL(CRM)
/// Revision        : 
/// </summary>
namespace JKMServices.BLL
{
    public class CustomerDetails : ServiceBase, ICustomerDetails
    {
        private readonly DAL.CRM.ICustomerDetails crmCustomerDetails;
        private readonly Utility.IGenerator codeGenerator;
        private readonly IResourceManagerFactory resourceManager;
        private readonly ILogger logger;
        private readonly IEmailHandler emailHandler;

        //Constructor
        public CustomerDetails(DAL.CRM.ICustomerDetails crmCustomerDetails,
                               Utility.IGenerator codeGenerator,
                               IResourceManagerFactory resourceManager,
                               ILogger logger,
                               IEmailHandler emailHandler = null)
        {
            this.codeGenerator = codeGenerator;
            this.crmCustomerDetails = crmCustomerDetails;
            this.resourceManager = resourceManager;
            this.emailHandler = emailHandler;
            this.logger = logger;
        }

        /// <summary>
        /// Method Name     : GetCustomerID
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 28 Nov 2017
        /// Purpose         : Method to get customerID using DAL
        /// Revision        : By Pratik Soni on 05th Dec 2017 : Implemented code to get Customer ID.
        /// </summary>
        public string GetCustomerID(string emailID)
        {
            try
            {
                if (!Validations.ValidateEmail(emailID))
                {
                    logger.Info(resourceManager.GetString("msgInvalidEmail"));
                    return GenerateServiceResponse<ServiceResponse<Customer>>(MESSAGE, resourceManager.GetString("msgInvalidEmail"));
                }
                return General.ConvertToJson<DTO.ServiceResponse<DTO.Customer>>(crmCustomerDetails.GetCustomerIDAsync(emailID));
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgErrorOccuredInGettingCustomerID"), ex);
                return GenerateServiceResponse<ServiceResponse<Customer>>(MESSAGE, resourceManager.GetString("msgErrorOccuredInGettingCustomerID"));
            }
        }

        /// Method Name     : GetCustomerProfileData
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 28 Nov 2017
        /// Purpose         : Method to get customer profile data from SQL using DAL
        /// Revision        : By Pratik Soni on 12 Dec 2017: Modified code to validate DataTable using common validation function from Validations.cs
        /// <param name="customerID"></param>
        /// <returns></returns>
        /// </summary>
        public string GetCustomerProfileData(string customerID)
        {
            try
            {
                if (!Validations.IsValid(customerID))
                {
                    logger.Error(resourceManager.GetString("msgInvalidCustomer"));
                    ServiceResponse<Customer> serviceResponse = new ServiceResponse<Customer> { Message = resourceManager.GetString("msgInvalidCustomer") };
                    return serviceResponse.ToString();
                }
                return General.ConvertToJson<DTO.ServiceResponse<DTO.Customer>>(crmCustomerDetails.GetCustomerProfileData(customerID));
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgServiceUnavailable"), ex);
                return GenerateServiceResponse<ServiceResponse<Customer>>(MESSAGE, resourceManager.GetString("msgServiceUnavailable"));
            }
        }

        ///<summary>
        /// Method Name     : GetCustomerVerificationData
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 28 Nov 2017
        /// Purpose         : Method to get customer verification code from SQL using DAL
        /// Revision        : By Pratik Soni on 21 Dec 2017 : Modified code to solved logic issue for sending Verification code to customer and update record in database.
        /// <param name="customerID"></param>
        /// <returns>Customers Verification Data in the form of JObject.</returns>
        /// </summary>
        public string GetCustomerVerificationData(string customerID)
        {
            ServiceResponse<Customer> serviceResponse;
            string customerEmailAddress;
            int verificationCode;

            try
            {
                serviceResponse = crmCustomerDetails.GetCustomerVerificationData(customerID);

                if (serviceResponse.Message != null)
                {
                    logger.Error(serviceResponse.Message);
                    return GenerateServiceResponse<ServiceResponse<Customer>>(MESSAGE, serviceResponse.Message);
                }
                if (serviceResponse.Information != null)
                {
                    if (serviceResponse.Information == resourceManager.GetString("msgInvalidCustomer"))
                    {
                        return General.ConvertToJson<ServiceResponse<Customer>>(serviceResponse);
                    }
                    serviceResponse = UpdateCustomerVerificationData(customerID);

                    if (!Validations.IsValid(serviceResponse.Data))
                    {
                        return General.ConvertToJson<ServiceResponse<Customer>>(serviceResponse);
                    }
                }
                customerEmailAddress = crmCustomerDetails.GetCustomerEmail(customerID);
                verificationCode = serviceResponse.Data.VerificationCode ?? 0;
                if (SendEmail(customerEmailAddress, verificationCode))
                {
                    return General.ConvertToJson<ServiceResponse<Customer>>(serviceResponse);
                }
                return GenerateServiceResponse<ServiceResponse<Customer>>(MESSAGE, resourceManager.GetString("msgOTPGenerationError"));
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgOTPGenerationError"), ex);
                return GenerateServiceResponse<ServiceResponse<Customer>>(MESSAGE, resourceManager.GetString("msgOTPGenerationError"));
            }
        }

        /// Method Name     : UpdateCustomerVerificationData
        /// Author          : Pratik Soni
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : To update the verification code and codevalidtill date in CRM
        /// Revision        : 
        /// </summary>
        /// <returns> </returns>
        private ServiceResponse<Customer> UpdateCustomerVerificationData(string customerID)
        {
            ServiceResponse<Customer> updateResponse;
            DTO.CustomerVerification dtoCustomerVerification;
            DTO.Customer dtoCustomer;
            int verificationCode = codeGenerator.GetVerificationCode(6);

            dtoCustomerVerification = GetUpdateVerificationRequestBody(verificationCode);
            updateResponse = crmCustomerDetails.PutCustomerVerificationData(customerID, General.ConvertToJson<DTO.CustomerVerification>(dtoCustomerVerification));
            if (updateResponse.Message != null)
            {
                return updateResponse;
            }

            if (Validations.IsValid(updateResponse.Information) && updateResponse.Information == resourceManager.GetString("CRM_STATUS_204"))
            {
                dtoCustomer = General.ConvertFromJson<Customer>(General.ConvertToJson<DTO.CustomerVerification>(dtoCustomerVerification));
                return new ServiceResponse<Customer> { Data = dtoCustomer };
            }
            return updateResponse;
        }

        /// Method Name     : GetUpdateVerificationRequestBody
        /// Author          : Pratik Soni
        /// Creation Date   : 24 Jan 2018
        /// Purpose         : To generate the request body for updating verification data in CRM
        /// Revision        : 
        /// </summary>
        /// <returns> json STRING with verification code and code valid till date.</returns>
        private DTO.CustomerVerification GetUpdateVerificationRequestBody(int verificationCode)
        {
            CustomerVerification dtoCustomer = new CustomerVerification();
            string currentDateTimeFormat;
            int OTPValidTill;

            OTPValidTill = int.Parse(ConfigurationManager.AppSettings["OTPValidTill"]);
            currentDateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;

            dtoCustomer.VerificationCode = verificationCode;
            dtoCustomer.CodeValidTill = DateTime.Parse(General.ConvertDateStringInUTCFormat(DateTime.UtcNow.AddDays(OTPValidTill).ToString(), currentDateTimeFormat));

            logger.Info(resourceManager.GetString("msgRequestContentCreated"));
            return dtoCustomer;
        }

        ///<summary>
        /// Method Name     : SendEmail
        /// Author          : Pratik Soni
        /// Creation Date   : 21 Dec 2017
        /// Purpose         : To send email
        /// <param name="emailId"/>
        /// <param name="verificationCode"/>
        /// <returns>Service response with customer DTO having latest verification code if mail successfully sent.</returns>
        /// </summary>
        private bool SendEmail(string emailId, int verificationCode)
        {
            try
            {
                emailHandler.Add<VerificationEmailModel>(emailId, "0001", new VerificationEmailModel { verificationCode = verificationCode });
                emailHandler.Send();
                logger.Info("Email sent succesfully");
                return true;
            }
            catch (Exception ex)
            {
                logger.Info(resourceManager.GetString("msgUnableToSendEmail") + ": " + ex.ToString());
                return false;
            }
        }

        ///<summary>
        /// Method Name     : PostCustomerProfileData
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 28 Nov 2017
        /// Purpose         : Method to Post customer profile data from CRM using DAL
        /// Revision        : 
        /// </summary>
        public string PostCustomerProfileData(string customerID)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                logger.Error(ex.InnerException);
                return null;
            }
        }

        ///<summary>
        /// Method Name     : PutCustomerVerificationData
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 28 Nov 2017
        /// Purpose         : Method to Post customer verification code from CRM using DAL
        /// Revision        : 
        /// </summary>
        public string PostCustomerVerificationData(string customerID)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                logger.Error(ex.InnerException);
                return null;
            }
        }

        /// <summary>
        /// Method Name     : PutCustomerProfileData
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 28 Nov 2017
        /// Purpose         : Method to Put customer profile data from CRM using DAL
        /// Revision        : By Pratik Soni on 24th Jan 2018 : Code erefactored to PUT data in CRM instead SQL.
        /// </summary>
        public string PutCustomerProfileData(string customerID, string requestCustomerProfile)
        {
            ServiceResponse<Customer> putResponse;
            try
            {
                string jsonFormattedData;
                logger.Info("PutCustomerData encountered.");

                jsonFormattedData = GenerateJsonFormattedData(requestCustomerProfile);

                if (jsonFormattedData == resourceManager.GetString("emptyJson"))
                {
                    logger.Error(resourceManager.GetString("msgBadRequest"));
                    return General.ConvertToJson<DTO.ServiceResponse<DTO.Customer>>(new ServiceResponse<Customer> { BadRequest = resourceManager.GetString("msgBadRequest") });
                }

                putResponse = crmCustomerDetails.PutCustomerProfileData(customerID, jsonFormattedData);

                if (putResponse.Information == resourceManager.GetString("CRM_STATUS_204"))
                {
                    return GenerateServiceResponse<Customer>(DATA, resourceManager.GetString("msgSavedSuccessfully"));
                }
                return General.ConvertToJson<ServiceResponse<Customer>>(putResponse);
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgFailToSave"), ex);
                return GenerateServiceResponse<ServiceResponse<Customer>>(MESSAGE, resourceManager.GetString("msgFailToSave"));
            }
        }

        /// <summary>
        /// Method Name     : ValidatePUTProfileRequestBody
        /// Author          : Pratik Soni
        /// Creation Date   : 24 Jan 2018
        /// Purpose         : Method to validate that PUT request body content should not be null
        /// Revision        : 
        /// </summary>
        private bool ValidatePutProfileRequestBody(string requestCustomerProfile)
        {
            JObject requestContent = JObject.Parse(requestCustomerProfile);
            foreach (JProperty property in requestContent.Properties())
            {
                if (!Validations.IsValid(property.Value))
                {
                    return false;
                }
            }
            return true;
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
                if ((bool)((JValue)requestData["TermsAgreed"]).Value)
                {
                    GenerateProfileFields(ref jsonFormattedData, requestData);
                }
                if (!string.IsNullOrEmpty((string)((JValue)requestData["PreferredContact"]).Value))
                {
                    GenerateMyAccountFields(ref jsonFormattedData, requestData);
                }
                return jsonFormattedData.ToString();
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgBadRequest"), ex);
                return resourceManager.GetString("emptyJson");
            }
        }

        /// <summary>
        /// Method Name     : GenerateProfileFields
        /// Author          : Ranjana Singh
        /// Creation Date   : 20 Feb 2018
        /// Purpose         : To generate jsonString only for Valuation fields
        /// Revision        :
        /// </summary>
        /// <returns> formatted json in string format </returns>
        private void GenerateProfileFields(ref JObject jsonFormattedData, JObject requestData)
        {
            if (!Validations.IsValid(requestData["CustomerId"].ToString()) ||
                !Validations.IsValid(requestData["TermsAgreed"].ToString()))
            {
                logger.Error(resourceManager.GetString("logProfileFieldsCannotEmpty"));
                jsonFormattedData = new JObject();
                return;
            }
            jsonFormattedData.Add("CustomerId", requestData["CustomerId"]);
            jsonFormattedData.Add("TermsAgreed", requestData["TermsAgreed"]);
        }

        /// <summary>
        /// Method Name     : GenerateProfileFields
        /// Author          : Ranjana Singh
        /// Creation Date   : 20 Feb 2018
        /// Purpose         : To generate jsonString only for Valuation fields
        /// Revision        :
        /// </summary>
        /// <returns> formatted json in string format </returns>
        private void GenerateMyAccountFields(ref JObject jsonFormattedData, JObject requestData)
        {
            if (!Validations.IsValid(requestData["Phone"].ToString()) ||
                !Validations.IsValid(requestData["PreferredContact"].ToString()) ||
                !Validations.IsValid(requestData["ReceiveNotifications"].ToString()) ||
                !Validations.ValidatePhoneNumber(requestData["Phone"].ToString()) ||
                !(requestData["PreferredContact"].ToString() == resourceManager.GetString("PreferredContactMethodCode_Email") ||
                requestData["PreferredContact"].ToString() == resourceManager.GetString("PreferredContactMethodCode_Phone")))
            {
                logger.Error(resourceManager.GetString("logMyAccountFieldsCannotEmpty"));
                jsonFormattedData = new JObject();
                return;
            }

            jsonFormattedData.Add("PreferredContact", requestData["PreferredContact"].ToString());
            jsonFormattedData.Add("Phone", requestData["Phone"]);
            jsonFormattedData.Add("ReceiveNotifications", requestData["ReceiveNotifications"]);
        }

        /// <summary>
        /// Method Name     : PutCustomerVerificationData
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 28 Nov 2017
        /// Purpose         : Method to Put Customer verification code from SQL using DAL
        /// Revision        : By Pratik Soni on 24 Jan 2018 : Code refactored for remove SQL calls and use CRM instead.
        /// </summary>
        public string PutCustomerVerificationData(string customerID, string requestCustomerVerification)
        {
            ServiceResponse<Customer> putResponse;
            try
            {
                if (!ValidatePutProfileRequestBody(requestCustomerVerification))
                {
                    return General.ConvertToJson<ServiceResponse<Customer>>(new ServiceResponse<Customer> { BadRequest = resourceManager.GetString("msgBadRequest") });
                }

                putResponse = crmCustomerDetails.PutCustomerVerificationData(customerID, requestCustomerVerification);
                if (putResponse.Information == resourceManager.GetString("CRM_STATUS_204"))
                {
                    return GenerateServiceResponse<Customer>(DATA, resourceManager.GetString("msgSavedSuccessfully"));
                }
                return General.ConvertToJson<ServiceResponse<Customer>>(putResponse);
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgFailToSave"), ex);
                return GenerateServiceResponse<ServiceResponse<Customer>>(MESSAGE, resourceManager.GetString("msgFailToSave"));
            }
        }
    }
}