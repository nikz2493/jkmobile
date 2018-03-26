using JKMServices.BLL.EmailEngine;
using JKMServices.BLL.Interface;
using JKMServices.BLL.Model;
using JKMServices.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Utility;
using Utility.Logger;

namespace JKMServices.BLL
{
    /// <summary>
    /// Class Name      : EstimateDetails
    /// Author          : Pratik Soni
    /// Creation Date   : 10 Jan 2018
    /// Purpose         : Class for Estimate details. 
    /// Revision        :
    /// </summary>
    /// <returns></returns>
    public class EstimateDetails : ServiceBase, IEstimateDetails
    {
        private readonly DAL.CRM.IEstimateDetails crmEstimateDetails;
        private readonly DAL.CRM.IMoveDetails crmMoveDetails;
        private readonly IResourceManagerFactory resourceManager;
        private readonly DAL.CRM.ICustomerDetails crmCustomerDetails;
        private readonly IEmailHandler emailHandler;
        private readonly ILogger logger;
        private readonly ISharepointConsumer sharepointConsumer;

        //Constructor
        public EstimateDetails(DAL.CRM.ICustomerDetails crmCustomerDetails,
                               DAL.CRM.IEstimateDetails crmEstimateDetails,
                               DAL.CRM.IMoveDetails crmMoveDetails,
                               IEmailHandler emailHandler,
                               ILogger logger,
                               IResourceManagerFactory resourceManager,
                               ISharepointConsumer sharepointConsumer)
        {
            this.crmCustomerDetails = crmCustomerDetails;
            this.crmEstimateDetails = crmEstimateDetails;
            this.crmMoveDetails = crmMoveDetails;
            this.resourceManager = resourceManager;
            this.emailHandler = emailHandler;
            this.logger = logger;
            this.sharepointConsumer = sharepointConsumer;
        }

        /// <summary>
        /// Method Name     : GetEstimateData
        /// Author          : Pratik Soni
        /// Creation Date   : 10 Jan 2018
        /// Purpose         : Gets the estimate data for the Customer id.
        /// Revision        :
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns> Service response in form of json string </returns>
        /// 
        public string GetEstimateData(string customerId)
        {
            List<string> estimateIdList = new List<string>();
            try
            {
                string errorResponse = CheckForValidCustomer(customerId);
                if (errorResponse != null)
                {
                    return errorResponse;
                }

                var getMoveIdResponse = crmEstimateDetails.GetEstimateList(customerId);
                if (getMoveIdResponse.Data == null)
                {
                    logger.Info(resourceManager.GetString("msgNoEstimateExistsForCustomer"));
                    return GenerateServiceResponse<List<Estimate>>(INFORMATION, resourceManager.GetString("msgNoEstimateExistsForCustomer"));
                }
                foreach (var estimate in getMoveIdResponse.Data)
                {
                    estimateIdList.Add(estimate.MoveNumber);
                }
                return General.ConvertToJson<ServiceResponse<List<Estimate>>>(crmEstimateDetails.GetEstimateData(estimateIdList));
            }
            catch
            {
                logger.Error(resourceManager.GetString("msgServiceUnavailable"));
                return GenerateServiceResponse<List<Estimate>>(MESSAGE, resourceManager.GetString("msgServiceUnavailable"));
            }
        }

        /// <summary>
        /// Method Name     : CheckForValidCustomer
        /// Author          : Pratik Soni
        /// Creation Date   : 12 Jan 2018
        /// Purpose         : To check if the customer is valid and registered.
        /// Revision        :
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns> Service response in form of json string </returns>
        /// 
        private string CheckForValidCustomer(string customerId)
        {
            if (string.IsNullOrEmpty(customerId) || string.IsNullOrWhiteSpace(customerId))
            {
                return General.GenerateBadRequestMessage<DTO.Estimate>();
            }

            if (!crmCustomerDetails.CheckCustomerRegistered(customerId))
            {
                logger.Info(resourceManager.GetString("msgUnregisteredCustomer"));
                return GenerateServiceResponse<Estimate>(INFORMATION, resourceManager.GetString("msgUnregisteredCustomer"));
            }
            return null;
        }

        /// <summary>
        /// Method Name     : GetEstimatePDF
        /// Author          : Ranjana Singh
        /// Creation Date   : 15 Jan 2018
        /// Purpose         : Gets the estimate PDF file for the moveId in the request body.
        /// Revision        :
        /// </summary>
        /// <returns></returns>
        public string GetEstimatePDF(string moveId)
        {
            string documentpath, fileFullPath, applicationPath = string.Empty;
            if (string.IsNullOrEmpty(moveId) || string.IsNullOrWhiteSpace(moveId))
            {
                return General.GenerateBadRequestMessage<DTO.Estimate>();
            }
            //documentpath = crmEstimateDetails.GetEstimatePDF(moveId); (mocked as the new code will replace)
            if (!string.IsNullOrEmpty(General.GetConfigValue("mockedPdfPath")))
            {
                applicationPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;

                documentpath = General.GetConfigValue("mockedPdfPath");
                fileFullPath = Path.Combine(applicationPath, documentpath);

                if (!string.IsNullOrEmpty(fileFullPath))
                {
                    return GetSharepointDocument(fileFullPath);
                }
                else
                {
                    return General.GenerateBadRequestMessage<DTO.Estimate>();
                }
            }
            else
            {
                return GenerateServiceResponse<Estimate>(MESSAGE, resourceManager.GetString("msgNoDocumentFound"));
            }
        }

        /// <summary>
        /// Method Name     : GetSharepointDocument
        /// Author          : Ranjana Singh
        /// Creation Date   : 16 Jan 2018
        /// Purpose         : Gets the estimate PDF file for the moveId in the request body from Sharepoint.
        /// Revision        : By Pratik Soni on 13 Feb 2018: To use the new common method from ServiceBase class which 
        ///                   returns the formatted response based on input bytes[]
        /// </summary>
        /// <returns></returns>
        private string GetSharepointDocument(string filePath)
        {
            byte[] fileBytes;
            fileBytes = System.IO.File.ReadAllBytes(filePath);
            //fileBytes = sharepointConsumer.HandleResult(filePath); //Sharepoint method call, which is being mocked for now.

            return GetConditionalResponseForDocument(fileBytes);
        }

        /// <summary>
        /// Method Name     : PutEstimateData
        /// Author          : Pratik Soni
        /// Creation Date   : 12 Jan 2018
        /// Purpose         : Updates estimate for the moveID in the request body
        /// Revision        :
        /// </summary>
        /// <returns></returns>
        public string PutEstimateData(string moveId, string requestBody)
        {
            ServiceResponse<Estimate> serviceResponse;
            ServiceResponse<DTO.Move> coordinatorForMove;
            List<string> modifiedFields = new List<string>();
            string jsonFormattedData;
            logger.Info("PutEstimateData encountered.");
            try
            {
                jsonFormattedData = GenerateJsonFormattedData(requestBody, ref modifiedFields);
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgBadRequest"), ex);
                return General.ConvertToJson<DTO.ServiceResponse<DTO.Estimate>>(new ServiceResponse<Estimate> { BadRequest = resourceManager.GetString("msgBadRequest") });
            }

            logger.Info("RequestContent created.");
            if (jsonFormattedData == "{}")
            {
                logger.Error(resourceManager.GetString("msgBadRequest"));
                return General.ConvertToJson<DTO.ServiceResponse<DTO.Estimate>>(new ServiceResponse<Estimate> { BadRequest = resourceManager.GetString("msgBadRequest") });
            }

            serviceResponse = crmEstimateDetails.PutEstimateData(moveId, jsonFormattedData);

            if (serviceResponse.Information == "204 - NoContent")
            {
                //Throw the error if service is unable to send the mail.
                coordinatorForMove = crmMoveDetails.GetContactForMove(moveId); //get the contact of move co-ordinator for current move.
                if ((coordinatorForMove.Data == null) || !SendEmail(coordinatorForMove.Data.MoveCoordinator_EmailAddress, moveId, modifiedFields))
                {
                    logger.Error(resourceManager.GetString("msgFailToSave"));
                    return GenerateServiceResponse<Estimate>(MESSAGE, resourceManager.GetString("msgFailToSave"));
                }
                logger.Info("Successfully executed.");
            }
            return GetFinalResponseForPostService<Estimate>(serviceResponse);
        }

        ///<summary>
        /// Method Name     : SendEmail
        /// Author          : Pratik Soni
        /// Creation Date   : 12 Jan 2018
        /// Purpose         : To send email
        /// <param name="emailId"/>
        /// <returns>Customers Verification Data in the form of JObject.</returns>
        /// </summary>
        private bool SendEmail(string emailId, string moveNumber, List<string> updatedFields)
        {
            emailHandler.Add<EstimateDetailsUpdatedEmailModel>(emailId,
                                                               "0002",
                                                               new EstimateDetailsUpdatedEmailModel
                                                               {
                                                                   MoveCoOrdinatorEmailId = emailId,
                                                                   MoveNumber = moveNumber,
                                                                   FieldList = updatedFields
                                                               });
            emailHandler.Send();
            return true;
        }

        /// <summary>
        /// Method Name     : GenerateJsonFormattedData
        /// Author          : Pratik Soni
        /// Creation Date   : 12 Jan 2018
        /// Purpose         : To generate jsonString only for changed fields
        /// Revision        :
        /// </summary>
        /// <returns> formatted json in string format </returns>
        private string GenerateJsonFormattedData(string requestBody, ref List<string> modifiedFields)
        {
            JObject jsonFormattedData;
            JObject requestData = JObject.Parse(requestBody);

            jsonFormattedData = new JObject();
            if ((bool)((JValue)requestData["IsServiceDate"]).Value)
            {
                modifiedFields.Add("Service Dates");
                GenerateServiceDateFields(ref jsonFormattedData, requestData);
            }
            if ((bool)((JValue)requestData["IsAddressEdited"]).Value)
            {
                modifiedFields.Add("Address fields");
                GenerateAddressFields(ref jsonFormattedData, requestData);
            }
            if ((bool)((JValue)requestData["IsValuationEdited"]).Value)
            {
                modifiedFields.Add("Valuation fields");
                GenerateValuationFields(ref jsonFormattedData, requestData);
            }
            if ((bool)((JValue)requestData["IsWhatMatterMostEdited"]).Value)
            {
                modifiedFields.Add("What matters most");
                jsonFormattedData.Add("WhatMattersMost", requestData["WhatMattersMost"]);
            }
            return jsonFormattedData.ToString();
        }

        /// <summary>
        /// Method Name     : GenerateValuationFields
        /// Author          : Pratik Soni
        /// Creation Date   : 12 Jan 2018
        /// Purpose         : To generate jsonString only for Valuation fields
        /// Revision        :
        /// </summary>
        /// <returns> formatted json in string format </returns>
        private void GenerateValuationFields(ref JObject jsonFormattedData, JObject requestData)
        {
            if (!Validations.IsValid(requestData["ExcessValuation"].ToString()) ||
                !Validations.IsValid(requestData["ValuationDeductible"].ToString()) ||
                !Validations.IsValid(requestData["ValuationCost"].ToString()))
            {
                logger.Error(resourceManager.GetString("logValuationFieldsCannotEmpty"));
                jsonFormattedData = new JObject();
                return;
            }
            jsonFormattedData.Add("ExcessValuation", Convert.ToDecimal(requestData["ExcessValuation"]));
            jsonFormattedData.Add("ValuationDeductible", MapValuationDeductibleValue(requestData["ValuationDeductible"].ToString()));
            jsonFormattedData.Add("ValuationCost", Convert.ToDecimal(requestData["ValuationCost"]));
        }

        /// <summary>
        /// Method Name     : MapValuationDeductibleValue
        /// Author          : Pratik Soni
        /// Creation Date   : 16 Feb 2018
        /// Purpose         : To map the jkmoving codes to custom codes to save proper values in app's custom field
        /// Revision        :
        /// </summary>
        /// <returns> SUCCESS - mapped code, FAIL - empty string  </returns>
        private int MapValuationDeductibleValue(string valuationDeductible)
        {
            const int crm_onerivet_minimum = 676860000;
            const int crm_onerivet_0 = 676860001;
            const int crm_onerivet_100 = 676860002;
            const int crm_onerivet_250 = 676860003;
            const int crm_onerivet_500 = 676860004;

            const string crm_jkmoving_minimum = "100000000";
            const string crm_jkmoving_0 = "100000001";
            const string crm_jkmoving_100 = "100000002";
            const string crm_jkmoving_250 = "100000003";
            const string crm_jkmoving_500 = "100000004";

            switch (valuationDeductible)
            {
                case crm_jkmoving_minimum:
                    return crm_onerivet_minimum;
                case crm_jkmoving_0:
                    return crm_onerivet_0;
                case crm_jkmoving_100:
                    return crm_onerivet_100;
                case crm_jkmoving_250:
                    return crm_onerivet_250;
                case crm_jkmoving_500:
                    return crm_onerivet_500;
            }
            return 0;
        }

        /// <summary>
        /// Method Name     : GenerateAddressFields
        /// Author          : Pratik Soni
        /// Creation Date   : 12 Jan 2018
        /// Purpose         : To generate jsonString only for address fields
        /// Revision        :
        /// </summary>
        /// <returns> formatted json in string format </returns>
        private void GenerateAddressFields(ref JObject jsonFormattedData, JObject requestData)
        {
            if (!Validations.IsValid(requestData["CustomOriginAddress"].ToString()) ||
                !Validations.IsValid(requestData["CustomDestinationAddress"].ToString()))
            {
                logger.Error(resourceManager.GetString("logAddressFieldsCannotEmpty"));
                jsonFormattedData = new JObject();
                return;
            }

            try
            {
                jsonFormattedData.Add("CustomOriginAddress", requestData["CustomOriginAddress"].ToString());
                jsonFormattedData.Add("CustomDestinationAddress", requestData["CustomDestinationAddress"].ToString());
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("Error occured while generating JSON for address fields.") + ": " + ex.ToString());
                jsonFormattedData = new JObject();
                return;
            }
        }

        /// <summary>
        /// Method Name     : GenerateServiceDateFields
        /// Author          : Pratik Soni
        /// Creation Date   : 12 Jan 2018
        /// Purpose         : To generate jsonString only for service date fields
        /// Revision        :
        /// </summary>
        /// <returns> formatted json in string format </returns>
        private void GenerateServiceDateFields(ref JObject jsonFormattedData, JObject requestData)
        {
            if (!Validations.IsValid(Convert.ToString(requestData["PackStartDate"])) ||
                !Validations.IsValid(Convert.ToString(requestData["LoadStartDate"])) ||
                !Validations.IsValid(Convert.ToString(requestData["MoveStartDate"])))
            {
                logger.Error(resourceManager.GetString("logDateFieldsCannotEmpty"));
                jsonFormattedData = new JObject();
                return;
            }

            try
            {
                jsonFormattedData.Add("PackStartDate", General.ConvertDateStringInUTCFormat(requestData["PackStartDate"].ToString(), "MM/dd/yyyy"));
                jsonFormattedData.Add("LoadStartDate", General.ConvertDateStringInUTCFormat(requestData["LoadStartDate"].ToString(), "MM/dd/yyyy"));
                jsonFormattedData.Add("MoveStartDate", General.ConvertDateStringInUTCFormat(requestData["MoveStartDate"].ToString(), "MM/dd/yyyy"));
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("logDateConversionError") + ": " + ex.ToString());
            }
        }
    }
}