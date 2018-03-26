using JKMServices.BLL.Interface;
using JKMServices.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Utility;
using Utility.Logger;

namespace JKMServices.BLL
{
    public class PaymentDetails : ServiceBase, IPaymentDetails
    {
        private readonly DAL.CRM.IPaymentDetails crmPaymentDetails;
        private readonly IResourceManagerFactory resourceManager;
        private readonly ILogger logger;

        //Constructor
        public PaymentDetails(DAL.CRM.IPaymentDetails crmPaymentDetails,
                              IResourceManagerFactory resourceManager,
                              ILogger logger)
        {
            this.crmPaymentDetails = crmPaymentDetails;
            this.resourceManager = resourceManager;
            this.logger = logger;
        }

        /// <summary>
        /// Method Name      : PostTransactionHistory
        /// Author           : Ranjana Singh
        /// Creation Date    : 23 Jan 2018
        /// Purpose          : Creates new records for transaction history.
        /// Revision         : By Pratik Soni on 27 Jan 2018: Updated whole method to set it in proper flow and for single transaction instead of list
        /// </summary>
        /// <param name="customerID"/>
        /// <param name="requestData"/>
        /// <returns></returns>
        public string PostTransactionHistory(string requestData)
        {
            ServiceResponse<DTO.Payment> crmResponse;
            DTO.Payment transactionHistory;
            try
            {
                //If any error occures while assigning requestData to DTO
                try
                {
                    transactionHistory = General.ConvertFromJson<DTO.Payment>(requestData);
                    if (IsInvalidRequestContentForTransaction(transactionHistory))
                    {
                        return GenerateServiceResponse<DTO.Payment>(BADREQUEST, resourceManager.GetString("msgBadRequest"));
                    }
                }
                catch (Exception)
                {
                    logger.Error(resourceManager.GetString("msgBadRequest") + ": " + resourceManager.GetString("msgErrorOccuredWhileAssigningDTO"));
                    return GenerateServiceResponse<DTO.Payment>(BADREQUEST, resourceManager.GetString("msgBadRequest"));
                }

                crmResponse = ExecutePostForTransaction(transactionHistory);
                return GetFinalResponseForPostService(crmResponse);
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgFailToSave"), ex);
                return GenerateServiceResponse<DTO.Payment>(MESSAGE, resourceManager.GetString("msgFailToSave"));
            }
        }

        /// <summary>
        /// Method Name      : IsInvalidRequestContentForTransaction
        /// Author           : Pratik Soni
        /// Creation Date    : 31 Jan 2018
        /// Purpose          : Validate request content.
        /// Revision         :
        /// </summary>
        /// <param name="transactionHistory"> contains list of transaction history</param>
        /// <returns>TRUE - if content is invalid, else FALSE</returns>
        private bool IsInvalidRequestContentForTransaction(DTO.Payment transactionHistory)
        {
            if (!Validations.IsValid(transactionHistory.CustomerID) ||
                !Validations.IsValid(transactionHistory.MoveID) ||
                !Validations.IsValid(transactionHistory.TransactionDate) ||
                !Validations.IsValid(transactionHistory.TransactionAmount) ||
                !Validations.IsValid(transactionHistory.TransactionNumber))
            {
                logger.Info(resourceManager.GetString("CRM_STATUS_400"));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method Name      : ExecutePostForTransaction
        /// Author           : Pratik Soni
        /// Creation Date    : 30 Jan 2018
        /// Purpose          : To loop through Transaction history and to POST detail in CRM.
        /// Revision         :
        /// </summary>
        /// <param name="transactionHistory"> contains list of transaction history</param>
        /// <returns>response of type - ServiceResponse<DTO.Payment></returns>
        private ServiceResponse<DTO.Payment> ExecutePostForTransaction(DTO.Payment transactionHistory)
        {
            ServiceResponse<DTO.Payment> crmResponse, invalidResponse;

            crmResponse = crmPaymentDetails.PostTransactionHistory(transactionHistory);

            //Validates if the CRM execution is success or not
            invalidResponse = CheckForErrorOrBadRequest<DTO.Payment>(crmResponse);
            if (invalidResponse.Message != null || invalidResponse.BadRequest != null)
            {
                logger.Error(resourceManager.GetString("msgFailToSave") + " OR " + resourceManager.GetString("msgBadRequest"));
                return invalidResponse;
            }

            logger.Info(resourceManager.GetString("msgSavedSuccessfully"));
            return crmResponse;
        }

        /// <summary>
        /// Method Name      : CheckForErrorOrBadRequest
        /// Author           : Pratik Soni
        /// Creation Date    : 30 Jan 2018
        /// Purpose          : To validate if CRM response contains Error message or Bad Request message.
        /// Revision         :
        /// </summary>
        /// <param name="serviceResponse"> contains service response received from DAL method after POST</param>
        /// <returns>service response if Message or Bad Request found; else returns NULL</returns>
        private ServiceResponse<T> CheckForErrorOrBadRequest<T>(ServiceResponse<T> serviceResponse)
        {
            if (Validations.IsValid(serviceResponse.Message))
            {
                logger.Info(resourceManager.GetString("msgServiceUnavailable") + ": " + serviceResponse.Message);
                return new ServiceResponse<T> { Message = serviceResponse.Message };
            }
            else if (Validations.IsValid(serviceResponse.BadRequest))
            {
                logger.Info(resourceManager.GetString("msgBadRequest"));
                return new ServiceResponse<T> { BadRequest = serviceResponse.BadRequest };
            }
            return new ServiceResponse<T> { };
        }

        /// <summary>
        /// Method Name      : GetDeviceID
        /// Author           : Pratik Soni
        /// Creation Date    : 07 Feb 2018
        /// Purpose          : To get device ID from CRM
        /// Revision         :
        /// </summary>
        public string GetDeviceID()
        {
            ServiceResponse<Payment> crmResponse = crmPaymentDetails.GetDeviceID();
            return General.ConvertToJson<ServiceResponse<Payment>>(crmResponse);
        }

        /// <summary>
        /// Method Name      : GetAmount
        /// Author           : Pratik Soni
        /// Creation Date    : 19 Feb 2018
        /// Purpose          : To get customer's payment details.
        /// Revision         :
        /// </summary>
        public string GetAmount(string moveID)
        {
            try
            {
                if (!Validations.IsValid(moveID.Trim()))
                {
                    logger.Error("msgBadRequest");
                    General.GenerateBadRequestMessage<Payment>();
                }
                return General.ConvertToJson<ServiceResponse<Payment>>(crmPaymentDetails.GetAmount(moveID));
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgServiceUnavailable"), ex);
                return General.ConvertToJson<ServiceResponse<Payment>>(new ServiceResponse<Payment> { Message = resourceManager.GetString("msgServiceUnavailable") });
            }
        }

        /// <summary>
        /// Method Name      : PutAmount
        /// Author           : Pratik Soni
        /// Creation Date    : 20 Feb 2018
        /// Purpose          : To update customer's payment details.
        /// Revision         :
        /// </summary>
        public string PutAmount(string requestContent)
        {
            ServiceResponse<DTO.Payment> crmResponse;
            Payment requestBody;
            try
            {
                //If any error occures while assigning requestContent to DTO
                try
                {
                    requestBody = General.ConvertFromJson<DTO.Payment>(requestContent);
                    if (!IsInvalidRequestBodyForAmount(requestBody))
                    {
                        return GenerateServiceResponse<DTO.Payment>(BADREQUEST, resourceManager.GetString("msgBadRequest"));
                    }
                }
                catch (Exception)
                {
                    logger.Error(resourceManager.GetString("msgBadRequest") + ": " + resourceManager.GetString("msgErrorOccuredWhileAssigningDTO"));
                    return GenerateServiceResponse<DTO.Payment>(BADREQUEST, resourceManager.GetString("msgBadRequest"));
                }
                crmResponse = crmPaymentDetails.PutAmount(requestContent);
                return GetFinalResponseForPostService(crmResponse);
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgServiceUnavailable"), ex);
                return General.ConvertToJson<ServiceResponse<Payment>>(new ServiceResponse<Payment> { Message = resourceManager.GetString("msgServiceUnavailable") });
            }
        }

        /// <summary>
        /// Method Name      : IsInvalidRequestBodyForAmount
        /// Author           : Pratik Soni
        /// Creation Date    : 31 Jan 2018
        /// Purpose          : Validate request content.
        /// Revision         :
        /// </summary>
        /// <param name="transactionHistory"> contains list of transaction history</param>
        /// <returns>TRUE - if content is invalid, else FALSE</returns>
        private bool IsInvalidRequestBodyForAmount(Payment payment)
        {
            if (!Validations.IsValid(payment.MoveID) || !Validations.IsValid(payment.TotalPaid))
            {
                return false;
            }
            return true;
        }
    }
}
