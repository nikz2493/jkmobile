using JKMServices.DAL.CRM.common;
using JKMServices.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Utility;
using Utility.Logger;

namespace JKMServices.DAL.CRM
{
    /// <summary>
    /// Class Name      : EstimateDetails
    /// Author          : Pratik Soni
    /// Creation Date   : 10 Jan 2018
    /// Purpose         : Class for EstimateDetails details to interact with CRM. 
    /// Revision        :
    /// </summary>
    /// <returns></returns>
    public class EstimateDetails : IEstimateDetails
    {
        private readonly ICRMUtilities objCrmUtilities;
        private const string contactEntityName = "contacts";
        private const string moveEntityName = "jkmoving_moves";
        private readonly JKMServices.DAL.CRM.IMoveDetails crmMoveDetails;
        private readonly IResourceManagerFactory resourceManager;
        private readonly ICRMTODTOMapper objCRMToDTOMapper;
        private readonly IDTOToCRMMapper objDTOToCRMMapper;
        private readonly ICustomerDetails customerDetails;
        private readonly ILogger logger;

        //Constructor
        public EstimateDetails(ICustomerDetails customerDetails,
                               ICRMUtilities objCrmUtilities,
                               JKMServices.DAL.CRM.IMoveDetails crmMoveDetails,
                               ICRMTODTOMapper objCRMToDTOMapper,
                               IDTOToCRMMapper objDTOToCRMMapper,
                               IResourceManagerFactory resourceManager,
                               ILogger logger)
        {
            this.customerDetails = customerDetails;
            this.objCrmUtilities = objCrmUtilities;
            this.crmMoveDetails = crmMoveDetails;
            this.resourceManager = resourceManager;
            this.objCRMToDTOMapper = objCRMToDTOMapper;
            this.objDTOToCRMMapper = objDTOToCRMMapper;
            this.logger = logger;
        }

        /// <summary>
        /// Method Name     : GetEstimateList
        /// Author          : Pratik Soni
        /// Creation Date   : 11 Jan 2018
        /// Purpose         : Gets the list of estimate id for the Customer number.
        /// Revision        :
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public ServiceResponse<List<Estimate>> GetEstimateList(string customerId)
        {
            Dictionary<string, string> customerResponse, moveResponse;
            string retrieveFieldList;
            StringBuilder filterString;

            filterString = new StringBuilder();

            customerResponse = objCrmUtilities.ExecuteGetRequest(contactEntityName, "contactid", "jkmoving_customernumber eq '" + customerId + "'");
            if (objCrmUtilities.ContainsNullValue(customerResponse))
            {
                logger.Info(resourceManager.GetString("msgUnregisteredCustomer"));
                return new ServiceResponse<List<Estimate>> { Message = resourceManager.GetString("msgUnregisteredCustomer") };
            }

            retrieveFieldList = "jkmoving_movenumber,_jkmoving_contactofmoveid_value,statecode";

            filterString.Append("_jkmoving_contactofmoveid_value eq " + General.GetSpecificAttributeFromCRMResponse(customerResponse, "contactid") + " and statecode eq 0");
            filterString.Append(" and statuscode eq " + " 676860000");

            moveResponse = objCrmUtilities.ExecuteGetRequest(moveEntityName, retrieveFieldList, filterString.ToString());
            return objCRMToDTOMapper.MapEstimateIdResponseToDTO(moveResponse);
        }

        /// <summary>
        /// Method Name     : GetEstimateData
        /// Author          : Pratik Soni
        /// Creation Date   : 10 Jan 2018
        /// Purpose         : Gets the list of estimate data for the Move number.
        /// Revision        :
        /// </summary>
        /// <param name="moveId"></param>
        /// <returns></returns>
        public ServiceResponse<List<Estimate>> GetEstimateData(List<string> estimateIdList)
        {
            StringBuilder selectFields;
            Dictionary<string, string> crmResponse;
            StringBuilder filterString;
            string formattedFilterString;

            selectFields = new StringBuilder();
            selectFields.Append("jkmoving_moveid, statecode, statuscode, jkmoving_movenumber");
            selectFields.Append(",jkmoving_estimatedlinehaul,jkmoving_deposit");
            selectFields.Append(",jkmoving_packfrom,jkmoving_loadfrom,jkmoving_deliveryfrom");
            selectFields.Append("," + resourceManager.GetString("moveDestinationAddressFields"));
            selectFields.Append("," + resourceManager.GetString("moveOriginAddressFields"));
            selectFields.Append("," + "jkmoving_whatmattersmost,jkmoving_declaredpropertyvalue,jkmoving_valuationdeductible,jkmoving_valuationcost ");

            filterString = new StringBuilder();
            for (int index = 0; index < estimateIdList.Count; index++)
            {
                filterString.Append(" jkmoving_movenumber eq '" + estimateIdList[index] + "' or");
            }

            formattedFilterString = (filterString.ToString().Length > 0) ?
                                        filterString.ToString().Substring(0, filterString.ToString().Length - 3) : filterString.ToString();
            crmResponse = objCrmUtilities.ExecuteGetRequest(moveEntityName, selectFields.ToString(), formattedFilterString);

            return objCRMToDTOMapper.MapEstimateDataResponseToDTO(crmResponse);
        }

        /// <summary>
        /// Method Name     : GetEstimatePDF
        /// Author          : Ranjana Singh
        /// Creation Date   : 15 Jan 2018
        /// Purpose         : Gets estimate PDF file from CRM. 
        /// Revision        :
        /// </summary>
        /// <param name="estimateId"></param>
        /// <returns></returns>
        public string GetEstimatePDF(string moveId)
        {
            Dictionary<string, string> crmResponse;
            try
            {
                crmResponse = objCrmUtilities.ExecuteGetRequest(moveEntityName, "jkmoving_moveid", "jkmoving_movenumber eq '" + moveId + "'");
                if (!crmResponse.ContainsKey("CONTENT"))
                {
                    return string.Empty;
                }
                JObject jsonObject = Utility.General.ConvertToJObject(crmResponse["CONTENT"].ToString());
                if (jsonObject["value"] == null || ((Newtonsoft.Json.Linq.JContainer)jsonObject["value"]).Count == 0)
                {
                    return null;
                }
                JObject valueObject = JObject.Parse(jsonObject["value"][0].ToString(Newtonsoft.Json.Formatting.None));
                return valueObject["jkmoving_moveid"].ToString();
            }
            catch(Exception ex)
            {
                logger.Error(resourceManager.GetString("msgServiceUnavailable"),ex);
                return resourceManager.GetString("msgServiceUnavailable");
            }
        }

        /// <summary>
        /// Method Name     : PutEstimateData
        /// Author          : Pratik Soni
        /// Creation Date   : 12 Jan 2018
        /// Purpose         : To update estimate details. 
        /// Revision        :
        /// </summary>
        /// <param name="moveId"> </param>
        /// <param name="jsonFormattedData"> Contains the estimate details to be saved </param>
        /// <returns></returns>
        public ServiceResponse<Estimate> PutEstimateData(string moveId, string jsonFormattedData)
        {
            Dictionary<string, string> crmResponse;
            try
            {
                logger.Info("PutEstimateData encountered");
                string moveGUID = General.GetSpecificAttributeFromCRMResponse(crmMoveDetails.GetMoveGUID(moveId), "jkmoving_moveid");

                crmResponse = objCrmUtilities.ExecutePutRequest("jkmoving_moves", moveGUID, objDTOToCRMMapper.MapEstimateDTOToCRM(jsonFormattedData));

                return GetCRMResponse(crmResponse);
            }
            catch(Exception ex)
            {
                logger.Error(resourceManager.GetString("msgServiceUnavailable"),ex);
                return new ServiceResponse<Estimate> { Message = resourceManager.GetString("msgServiceUnavailable") };
            }
        }

        /// <summary>
        /// Method Name     : GetCRMResponse
        /// Author          : Pratik Soni
        /// Creation Date   : 12 Jan 2018
        /// Purpose         : To generate formatted response.
        /// Revision        :
        /// </summary>
        /// <param name="crmResponse"> </param>
        /// <returns></returns>
        public ServiceResponse<Estimate> GetCRMResponse(Dictionary<string, string> crmResponse)
        {
            if (crmResponse["STATUS"] == HttpStatusCode.BadRequest.ToString())
            {
                logger.Info(resourceManager.GetString("msgBadRequest"));
                return new ServiceResponse<Estimate> { BadRequest = resourceManager.GetString("CRM_STATUS_400") };
            }
            else if (crmResponse["STATUS"] == HttpStatusCode.ServiceUnavailable.ToString() || crmResponse["STATUS"] == HttpStatusCode.Unauthorized.ToString())
            {
                logger.Info(crmResponse["ERROR"]);
                return new ServiceResponse<Estimate> { Information = crmResponse["ERROR"] };
            }
            else if (crmResponse["STATUS"] == HttpStatusCode.NoContent.ToString())
            {
                logger.Info(crmResponse["INFORMATION"]);
                return new ServiceResponse<Estimate> { Information = crmResponse["INFORMATION"] };
            }

            logger.Error(resourceManager.GetString("msgServiceUnavailable"));
            return new ServiceResponse<Estimate> { Information = resourceManager.GetString("msgSavedSuccessfully") };
        }
    }
}