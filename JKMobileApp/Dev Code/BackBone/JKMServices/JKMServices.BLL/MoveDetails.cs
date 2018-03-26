using JKMServices.BLL.EmailEngine;
using JKMServices.BLL.Interface;
using JKMServices.BLL.Model;
using JKMServices.DTO;
using Newtonsoft.Json.Linq;
using System;
using Utility;
using Utility.Logger;

namespace JKMServices.BLL
{
    /// <summary>
    /// Class Name      : MoveDetails
    /// Author          : Pratik Soni
    /// Creation Date   : 13 Dec 2017
    /// Purpose         : Class for MOVE details. 
    /// Revision        :
    /// </summary>
    /// <returns></returns>
    public class MoveDetails : ServiceBase, IMoveDetails
    {
        private readonly DAL.CRM.IMoveDetails crmMoveDetails;
        private readonly IResourceManagerFactory resourceManager;
        private readonly DAL.CRM.ICustomerDetails crmCustomerDetails;
        private readonly ILogger logger;

        //Constructor
        private readonly IEmailHandler emailHandler;

        //Constructor
        public MoveDetails(DAL.CRM.ICustomerDetails crmCustomerDetails,
                           DAL.CRM.IMoveDetails crmMoveDetails,
                           IResourceManagerFactory resourceManager,
                           IEmailHandler emailHandler,
                           ILogger logger)
        {
            this.crmCustomerDetails = crmCustomerDetails;
            this.crmMoveDetails = crmMoveDetails;
            this.resourceManager = resourceManager;
            this.logger = logger;
            this.emailHandler = emailHandler;
        }

        /// <summary>
        /// Method Name     : GetMoveID
        /// Author          : Ranjana Singh
        /// Creation Date   : 27 Dec 2017
        /// Purpose         : Gets the specified move ID from CRM based on registered CustomerId. 
        /// Revision        :
        /// </summary>
        /// <returns></returns>
        public string GetMoveID(string customerID)
        {
            try
            {
                if (string.IsNullOrEmpty(customerID) || string.IsNullOrWhiteSpace(customerID))
                {
                    return General.GenerateBadRequestMessage<Move>();
                }

                if (!crmCustomerDetails.CheckCustomerRegistered(customerID))
                {
                    logger.Info(resourceManager.GetString("msgUnregisteredCustomer"));
                    return GenerateServiceResponse<Move>(INFORMATION, resourceManager.GetString("msgUnregisteredCustomer"));
                }
                return General.ConvertToJson(crmMoveDetails.GetMoveId(customerID));
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgServiceUnavailable"), ex);
                return GenerateServiceResponse<Move>(MESSAGE, resourceManager.GetString("msgServiceUnavailable"));
            }
        }

        /// <summary>
        /// Method Name     : GetMoveData
        /// Author          : Pratik Soni
        /// Creation Date   : 16 Dec 2017
        /// Purpose         : Gets the full move data, including addresses, dates, services selected, current status.  
        /// Revision        :
        /// </summary>
        /// <returns></returns>
        public string GetMoveData(string moveID)
        {
            if (string.IsNullOrEmpty(moveID) || string.IsNullOrWhiteSpace(moveID))
            {
                return General.GenerateBadRequestMessage<Move>();
            }
            return General.ConvertToJson(crmMoveDetails.GetMoveData(moveID));
        }

        /// <summary>
        /// Method Name     : GetContactForMove
        /// Author          : Pratik Soni
        /// Creation Date   : 16 Dec 2017
        /// Purpose         : Gets the list of contacts for the specified move ID. 
        /// Revision        :
        /// </summary>
        /// <returns></returns>
        public string GetContactForMove(string moveID)
        {
            if (string.IsNullOrEmpty(moveID) || string.IsNullOrWhiteSpace(moveID))
            {
                return General.GenerateBadRequestMessage<Move>();
            }
            return General.ConvertToJson(crmMoveDetails.GetContactForMove(moveID));
        }

        /// <summary>
        /// Method Name      : PutMoveData
        /// Author           : Ranjana Singh
        /// Creation Date    : 10 Jan 2018
        /// Purpose          : 
        /// Revision         :
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public string PutMoveData(string moveID, string requestData)
        {
            ServiceResponse<Move> crmResponse;
            try
            {
                crmResponse = crmMoveDetails.PutMoveData(moveID, requestData);

                if (crmResponse.Message != null)
                {
                    return General.ConvertToJson(crmResponse);
                }

                //Throw the error if service is unable to send the mail.
                if (!SendEmail(moveID, requestData))
                {
                    logger.Info(resourceManager.GetString("msgSavedSuccessfully") + resourceManager.GetString("msgUnableToSendEmailToMoveCoordinator")
                        + resourceManager.GetString("msgContactMoveCoordinator"));
                }

                logger.Info(resourceManager.GetString("msgSavedSuccessfully"));
                return GenerateServiceResponse<Move>(DATA, resourceManager.GetString("msgSavedSuccessfully"));
            }
            catch
            {
                logger.Error(resourceManager.GetString("msgFailToSave"));
                return GenerateServiceResponse<Move>(MESSAGE, resourceManager.GetString("msgFailToSave"));
            }
        }

        ///<summary>
        /// Method Name     : SendEmail
        /// Author          : Ranjana Singh
        /// Creation Date   : 21 Dec 2017
        /// Purpose         : To send email
        /// <param name="customerEmailId"/>
        /// <returns>Customers Verification Data in the form of JObject.</returns>
        /// </summary>
        private bool SendEmail(string moveNumber, string requestData)
        {
            ServiceResponse<Move> coordinatorForMove;
            ServiceResponse<Move> customerResponse;
            JObject jObject;
            string templateID;
            MoveStatusChangedEmailModel emailModel;

            //get the contact of move co-ordinator for current move.
            coordinatorForMove = crmMoveDetails.GetContactForMove(moveNumber);

            if ((coordinatorForMove.Data == null))
            {
                logger.Info(resourceManager.GetString("msgInvalidMove"));
                return false;
            }
            try
            {
                jObject = JObject.Parse(requestData);
                emailModel = new MoveStatusChangedEmailModel
                {
                    MoveCoOrdinatorEmailId = coordinatorForMove.Data.MoveCoordinator_EmailAddress,
                    MoveNumber = moveNumber
                };

                if (jObject["StatusReason"].ToString() == resourceManager.GetString("StatusCodeNeedsOverride"))//Checked status code for "Need Override"
                {
                    customerResponse = GetContactDetailsForMove(moveNumber);
                    emailModel.CustomerEmailAddress = customerResponse.Data.CustomerEmailAddress;
                    emailModel.CustomerContactNumber = customerResponse.Data.CustomerContactNumber;
                    templateID = "0004";
                }
                else
                {
                    templateID = "0003";
                }
                emailHandler.Add(coordinatorForMove.Data.MoveCoordinator_EmailAddress, templateID, emailModel);
                emailHandler.Send();
                return true;
            }
            catch
            {
                logger.Error(resourceManager.GetString("msgUnableToSendEmailToMoveCoordinator"));
                return false;
            }
        }

        /// <summary>
        /// Method Name     : GetContactDetailsForMove
        /// Author          : Ranjana Singh
        /// Creation Date   : 24 Jan 2018
        /// Purpose         : Gets the details of Customers for the specified move ID. 
        /// Revision        :
        /// </summary>
        /// <returns></returns>
        public ServiceResponse<Move> GetContactDetailsForMove(string moveID)
        {
            if (string.IsNullOrEmpty(moveID) || string.IsNullOrWhiteSpace(moveID))
            {
                ServiceResponse<Move> serviceResponse;
                serviceResponse = new ServiceResponse<Move>() { BadRequest = "BAD REQUEST." };

                return serviceResponse;
            }
            return crmMoveDetails.GetCustomerDetails(moveID);
        }

        /// <summary>
        /// Method Name     : GetMoveList
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 22 DEC 2017
        /// Purpose         : Get list of active moves
        /// Revision        : 
        /// </summary>
        public string GetMoveList(string statusReason)
        {
            var serviceResponse = crmMoveDetails.GetMoveList(statusReason);
            return General.ConvertToJson(serviceResponse);
        }
    }
}