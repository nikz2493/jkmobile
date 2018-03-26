using JKMServices.DAL.CRM.common;
using JKMServices.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Utility.Logger;

namespace JKMServices.DAL.CRM
{
    /// <summary>
    /// Class Name      : MoveDetails
    /// Author          : Pratik Soni
    /// Creation Date   : 13 Dec 2017
    /// Purpose         : Class for MOVE details to interact with CRM. 
    /// Revision        :
    /// </summary>
    /// <returns></returns>
    public class MoveDetails : IMoveDetails
    {
        private readonly ICRMUtilities objCrmUtilities;
        private const string contactEntityName = "contacts";
        private const string moveEntityName = "jkmoving_moves";
        private readonly IResourceManagerFactory resourceManager;
        private readonly ICRMTODTOMapper objCRMToDTOMapper;
        private readonly IDTOToCRMMapper objDTOToCRMMapper;
        private readonly ILogger logger;

        //Constructor
        public MoveDetails(ICRMUtilities objCrmUtilities,
                           IResourceManagerFactory resourceManager,
                           ICRMTODTOMapper objCRMToDTOMapper,
                           IDTOToCRMMapper objDTOToCRMMapper,
                           ILogger logger)
        {
            this.objCrmUtilities = objCrmUtilities;
            this.resourceManager = resourceManager;
            this.objCRMToDTOMapper = objCRMToDTOMapper;
            this.objDTOToCRMMapper = objDTOToCRMMapper;
            this.logger = logger;
        }

        /// <summary>
        /// Method Name     : GetMoveId
        /// Author          : Ranjana Singh
        /// Creation Date   : 27 Dec 2017
        /// Purpose         : Gets move Id from customerID. 
        /// Revision        :
        /// </summary>
        /// <param name="moveID"></param>
        /// <returns></returns>
        public DTO.ServiceResponse<DTO.Move> GetMoveId(string customerID)
        {
            Dictionary<string, string> customerResponse, moveResponse;
            string retrieveFieldList;
            StringBuilder filterString;

            filterString = new StringBuilder();

            logger.Info("GetMoveId encountered");
            customerResponse = objCrmUtilities.ExecuteGetRequest(contactEntityName, "contactid", "jkmoving_customernumber eq '" + customerID + "'");

            retrieveFieldList = "jkmoving_movenumber,_jkmoving_contactofmoveid_value,statecode";
            filterString.Append("_jkmoving_contactofmoveid_value eq " + Utility.General.GetSpecificAttributeFromCRMResponse(customerResponse, "contactid"));
            filterString.Append(" and statecode eq 0");
            filterString.Append(" and statuscode ne " + resourceManager.GetString("Move_StatusReasonCode_Estimated"));

            moveResponse = objCrmUtilities.ExecuteGetRequest(moveEntityName, retrieveFieldList, filterString.ToString());
            var validatedResponse = objCRMToDTOMapper.ValidateResponse<Move>(moveResponse);
            if (!string.IsNullOrEmpty(validatedResponse.Message) || !string.IsNullOrEmpty(validatedResponse.Information))
            {
                return validatedResponse;
            }
            return ReturnServiceResponse(moveResponse);
        }

        /// Method Name     : ReturnServiceResponse
        /// Author          : Pratik Soni
        /// Creation Date   : 07 Feb 2018
        /// Purpose         : To return service response from CRM's dictionary response
        /// Revision        : 
        /// </summary>
        private ServiceResponse<DTO.Move> ReturnServiceResponse(Dictionary<string, string> crmResponse)
        {
            if (objCrmUtilities.ContainsNullValue(crmResponse))
            {
                logger.Info(resourceManager.GetString("CRM_STATUS_204"));
                return new ServiceResponse<Move> { Message = resourceManager.GetString("CRM_STATUS_204") };
            }
            var validatedResponse = objCRMToDTOMapper.ValidateResponse<Move>(crmResponse);
            if (validatedResponse.Message != null)
            {
                logger.Error(resourceManager.GetString("msgServiceUnavailable"));
                return new ServiceResponse<Move> { Message = resourceManager.GetString("msgServiceUnavailable") };
            }
            else if (validatedResponse.Information != null)
            {
                logger.Error(resourceManager.GetString("CRM_STATUS_204"));
                return new ServiceResponse<Move> { Information = resourceManager.GetString("CRM_STATUS_204") };
            }
            return objCRMToDTOMapper.MapMoveDetailsResponseToDTO(crmResponse);
        }

        /// <summary>
        /// Method Name     : GetMoveData
        /// Author          : Pratik Soni
        /// Creation Date   : 28 Dec 2017
        /// Purpose         : Get move data from move ID. 
        /// Revision        :
        /// </summary>
        /// <param name="moveID"></param>
        /// <returns></returns>
        public DTO.ServiceResponse<DTO.Move> GetMoveData(string moveID)
        {
            Dictionary<string, string> moveResponse;
            StringBuilder selectFields = new StringBuilder();

            selectFields.Append("jkmoving_moveid, statecode, statuscode, jkmoving_movenumber");
            selectFields.Append("," + "jkmoving_packfrom,jkmoving_loadfrom,jkmoving_deliveryfrom,jkmoving_deliveryto");
            selectFields.Append("," + resourceManager.GetString("moveDestinationAddressFields"));
            selectFields.Append("," + resourceManager.GetString("moveOriginAddressFields"));
            selectFields.Append("," + "jkmoving_whatmattersmost,jkmoving_declaredpropertyvalue,jkmoving_valuationdeductible,jkmoving_valuationcost ");

            moveResponse = objCrmUtilities.ExecuteGetRequest(moveEntityName, selectFields.ToString(), "jkmoving_movenumber eq '" + moveID + "'");
            var validatedResponse = objCRMToDTOMapper.ValidateResponse<Move>(moveResponse);
            if (!string.IsNullOrEmpty(validatedResponse.Message) || !string.IsNullOrEmpty(validatedResponse.Information))
            {
                return validatedResponse;
            }
            return ReturnServiceResponse(moveResponse);
        }

        /// <summary>
        /// Method Name     : GetContactListForMove
        /// Author          : Pratik Soni
        /// Creation Date   : 19 Dec 2017
        /// Purpose         : Get move contact for move ID. 
        /// Revision        :
        /// </summary>
        /// <param name="moveID"></param>
        /// <returns></returns>
        public DTO.ServiceResponse<DTO.Move> GetContactForMove(string moveID)
        {
            const string userEntity = "systemusers";

            List<DTO.ServiceResponse<DTO.Move>> serviceResponse;
            Dictionary<string, string> moveResponse;
            string retriveFieldList;

            serviceResponse = new List<DTO.ServiceResponse<DTO.Move>>();
            retriveFieldList = "ownerid";

            //Gets Move Co-ordinator's ID i.e. ownerid
            moveResponse = objCrmUtilities.ExecuteGetRequest(moveEntityName, retriveFieldList, "jkmoving_movenumber eq '" + moveID + "'", expandValue: retriveFieldList);

            if (moveResponse["CONTENT"] is null)
            {
                logger.Info(moveResponse["ERROR"]);
                serviceResponse.Add(new ServiceResponse<Move> { Message = moveResponse["ERROR"] });
                return serviceResponse.FirstOrDefault();
            }

            JObject jsonObject = Utility.General.ConvertToJObject(moveResponse["CONTENT"].ToString());
            if (((Newtonsoft.Json.Linq.JContainer)jsonObject["value"]).Count == 0 || jsonObject["value"][0] is null)
            {
                serviceResponse.Add(new ServiceResponse<Move> { Information = "No content found for move ID." });
                return serviceResponse.FirstOrDefault();
            }
            JObject valueObject = JObject.Parse(jsonObject["value"][0].ToString(Newtonsoft.Json.Formatting.None));
            JObject ownerIdObject = JObject.Parse(valueObject["ownerid"].ToString(Newtonsoft.Json.Formatting.None));

            //Gets email and phone number of move co-ordinator
            retriveFieldList = "internalemailaddress,address1_telephone1,firstname,lastname";
            Dictionary<string, string> crmResponse = objCrmUtilities.ExecuteGetRequest(userEntity, retriveFieldList, "systemuserid eq " + ownerIdObject["ownerid"].ToString());
            var validatedResponse = objCRMToDTOMapper.ValidateResponse<Move>(moveResponse);
            if (!string.IsNullOrEmpty(validatedResponse.Message) || !string.IsNullOrEmpty(validatedResponse.Information))
            {
                return validatedResponse;
            }
            return ReturnServiceResponse(crmResponse);
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
            if (jsonObject["value"] == null)
            {
                return jsonObject.ToString(Newtonsoft.Json.Formatting.None);
            }
            JObject valueObject = JObject.Parse(jsonObject["value"][0].ToString(Newtonsoft.Json.Formatting.None));
            returnValue = valueObject[requiredField].ToString();
            return returnValue;
        }

        /// Method Name     : PutMoveData
        /// Author          : Pratik Soni
        /// Creation Date   : 11 Jan 2018
        /// Purpose         : To update exisiting move data for the given MoveID
        /// Revision        : 
        /// </summary>
        public DTO.ServiceResponse<DTO.Move> PutMoveData(string moveID, string jsonFormattedData)
        {
            Dictionary<string, string> crmResponse;
            string moveGUID;
            try
            {
                moveGUID = GetSpecificAttributeFromResponse(GetMoveGUID(moveID), "jkmoving_moveid");
                if (string.IsNullOrEmpty(moveGUID))
                {
                    logger.Info(resourceManager.GetString("msgInvalidMove"));
                    return new ServiceResponse<Move> { Information = resourceManager.GetString("msgInvalidMove") };
                }

                crmResponse = objCrmUtilities.ExecutePutRequest(moveEntityName, moveGUID, objDTOToCRMMapper.MapMoveDTOToCRM(jsonFormattedData));
                return GetFormattedResponse(crmResponse);
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgServiceUnavailableUnauthorized"), ex);
                return new ServiceResponse<Move> { Message = resourceManager.GetString("msgServiceUnavailableUnauthorized") };
            }
        }

        /// Method Name     : GetFormattedResponse
        /// Author          : Pratik Soni
        /// Creation Date   : 16 Jan 2018
        /// Purpose         : To format the CRM response
        /// Revision        : 
        /// </summary>
        /// <returns> returns ServiceResponse with Move DTO </returns>
        /// 
        private ServiceResponse<Move> GetFormattedResponse(Dictionary<string, string> crmResponse)
        {
            if (crmResponse["STATUS"] == HttpStatusCode.BadRequest.ToString())
            {
                logger.Error(resourceManager.GetString("msgFailToSave"));
                return new ServiceResponse<Move> { Message = resourceManager.GetString("msgFailToSave") };
            }
            else if (crmResponse["STATUS"] == HttpStatusCode.ServiceUnavailable.ToString() || crmResponse["STATUS"] == HttpStatusCode.Unauthorized.ToString())
            {
                logger.Info(crmResponse["ERROR"]);
                return new ServiceResponse<Move> { Information = crmResponse["ERROR"] };
            }
            else if (crmResponse["STATUS"] == HttpStatusCode.NoContent.ToString())
            {
                logger.Info(crmResponse["INFORMATION"]);
                return new ServiceResponse<Move> { Information = crmResponse["INFORMATION"] };
            }

            logger.Info(resourceManager.GetString("msgSavedSuccessfully"));
            return new ServiceResponse<Move> { Information = resourceManager.GetString("msgSavedSuccessfully") };
        }

        /// Method Name     : GetMoveGUID
        /// Author          : Pratik Soni
        /// Creation Date   : 11 Jan 2018
        /// Purpose         : To fetch GUID from Move Number
        /// Revision        : 
        /// </summary>
        /// <returns> returns dictionary with CRM response </returns>
        /// 
        public Dictionary<string, string> GetMoveGUID(string moveNumber)
        {
            return objCrmUtilities.ExecuteGetRequest(moveEntityName, "jkmoving_moveid", "jkmoving_movenumber eq '" + moveNumber + "'");
        }

        /// Method Name     : GetMoveList
        /// Author          : Pratik Soni
        /// Creation Date   : 15 Feb 2018
        /// Purpose         : Get all active moves from JIM for windows service
        /// Revision        : 
        /// </summary>
        /// <returns> returns dictionary with CRM response </returns>
        public ServiceResponse<List<Move>> GetMoveList(string statusReason)
        {
            
            Dictionary<string, string> moveResponse;
            StringBuilder selectFields = new StringBuilder();
            string filterString = GetFilterString(statusReason);

            selectFields.Append("jkmoving_moveid,statecode,jkmoving_movenumber,_jkmoving_contactofmoveid_value");
            selectFields.Append("," + "jkmoving_packfrom,jkmoving_loadfrom,jkmoving_deliveryfrom,jkmoving_deliveryto");

            moveResponse = objCrmUtilities.ExecuteGetRequest(moveEntityName, selectFields.ToString(), filterString);
            var validatedResponse = objCRMToDTOMapper.ValidateResponse<List<Move>>(moveResponse);
            if (!string.IsNullOrEmpty(validatedResponse.Message) || !string.IsNullOrEmpty(validatedResponse.Information))
            {
                return validatedResponse;
            }
            if (validatedResponse.Message != null)
            {
                logger.Error(resourceManager.GetString("msgServiceUnavailable"));
                return new ServiceResponse<List<Move>> { Message = resourceManager.GetString("msgServiceUnavailable") };
            }
            else if (validatedResponse.Information != null)
            {
                logger.Error(resourceManager.GetString("CRM_STATUS_204"));
                return new ServiceResponse<List<Move>> { Information = resourceManager.GetString("CRM_STATUS_204") };
            }
            return objCRMToDTOMapper.MapMoveListResponseToDTO(moveResponse);
        }

        /// Method Name     : GetFilterString
        /// Author          : Pratik Soni
        /// Creation Date   : 23 Feb 2018
        /// Purpose         : Generate filter string based on status reason
        /// Revision        : 
        /// </summary>
        /// <returns> returns dictionary with CRM response </returns>
        private string GetFilterString(string statusReason)
        {
            string statusReasonCode = string.Empty;
            StringBuilder filterString = new StringBuilder();
            filterString.Append("statecode eq 0"); //for active moves

            switch (statusReason.ToUpper())
            {
                case "ESTIMATED":
                    statusReasonCode = "676860000";
                    break;
                case "NEEDSOVERRIDE":
                    statusReasonCode = "148050000";
                    break;
                case "PENDING":
                    statusReasonCode = "100000000";
                    break;
                case "BOOKED":
                    statusReasonCode = "100000001";
                    break;
                case "PACKED":
                    statusReasonCode = "100000002";
                    break;
                case "LOADED":
                    statusReasonCode = "100000003";
                    break;
                case "DELIVERED":
                    statusReasonCode = "100000004";
                    break;
                case "INTRANSIT":
                    statusReasonCode = "100000005";
                    break;
                case "INVOICED":
                    statusReasonCode = "100000006";
                    break;
            }
            if (!string.IsNullOrEmpty(statusReasonCode))
            {
                filterString.Append(" and statuscode eq " + statusReasonCode);
            }
            return filterString.ToString();
        }

        /// Method Name     : GetCustomerDetails
        /// Author          : Ranjana Singh
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : To get Customer Details from CRM.
        /// Revision        : 
        /// </summary>
        public ServiceResponse<DTO.Move> GetCustomerDetails(string moveID)
        {
            Dictionary<string, string> customerResponse, moveResponse;

            moveResponse = objCrmUtilities.ExecuteGetRequest(moveEntityName, "_jkmoving_contactofmoveid_value", "jkmoving_movenumber eq '" + moveID + "'");
            customerResponse = objCrmUtilities.ExecuteGetRequest(contactEntityName, "emailaddress1,telephone1", "contactid eq " + GetSpecificAttributeFromResponse(moveResponse, "_jkmoving_contactofmoveid_value") + "");

            return objCRMToDTOMapper.MapMoveCustomerResponseToDTO(customerResponse);
        }
    }
}