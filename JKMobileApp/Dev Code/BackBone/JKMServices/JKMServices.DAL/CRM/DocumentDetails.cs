using JKMServices.DAL.CRM.common;
using JKMServices.DTO;
using System.Collections.Generic;
using Utility;
using Utility.Logger;

namespace JKMServices.DAL.CRM
{
    public class DocumentDetails : IDocumentDetails
    {
        private readonly ICRMUtilities objCrmUtilities;
        private const string documentEntityName = "sharepointdocumentlocations";
        private readonly ILogger logger;
        private readonly IResourceManagerFactory resourceManager;
        private readonly ICRMTODTOMapper objCRMToDTOMapper;
        private readonly ISharepointConsumer sharepointConsumer;

        public DocumentDetails(ICRMUtilities objCrmUtilities,
                                IResourceManagerFactory resourceManager,
                                ILogger logger,
                                ISharepointConsumer sharepointConsumer,
                                ICRMTODTOMapper objCRMToDTOMapper)
        {
            this.objCrmUtilities = objCrmUtilities;
            this.resourceManager = resourceManager;
            this.logger = logger;
            this.sharepointConsumer = sharepointConsumer;
            this.objCRMToDTOMapper = objCRMToDTOMapper;
        }

        /// <summary>
        /// Method Name     : GetDocumentList
        /// Author          : Ranjana Singh
        /// Creation Date   : 19 Dec 2017
        /// Purpose         : Gets the list of documents to be displayed in My Documents Page. If moveId is not provided, service will return common documents.
        /// Revision        :   
        /// </summary>
        public ServiceResponse<List<Document>> GetDocumentList(string moveId)
        {
            Dictionary<string, string> documentListResponse;
            string relativeURL;
            List<string> documentList;
            string retrieveFiledString = "relativeurl,name";

            #warning Mocked move number (Remove it before publishing)
            moveId = "RM002970";
            string filterString = "contains(relativeurl,'" + moveId + "' " + ")";

            documentListResponse = objCrmUtilities.ExecuteGetRequest(documentEntityName, retrieveFiledString, filterString);
            if (objCrmUtilities.ContainsNullValue(documentListResponse))
            {
                logger.Info(resourceManager.GetString("msgNoDocumentFound"));
                return new ServiceResponse<List<Document>> { Message = resourceManager.GetString("msgNoDocumentFound") };
            }

            relativeURL = GetRelativeUrl(documentListResponse);
            if (!Validations.IsValid(relativeURL))
            {
                return new ServiceResponse<List<Document>> { Information = resourceManager.GetString("CRM_STATUS_204") };
            }

            documentList = GetSharepointDocument(relativeURL);
            return GetDocumentListResponse(moveId, documentList, relativeURL);
        }

        /// <summary>
        /// Method Name     : GetDocumentListResponse
        /// Author          : Pratik Soni
        /// Creation Date   : 09 Feb 2018
        /// Purpose         : Gets formatted response based on parameteres passed to the method
        /// Revision        :   
        /// </summary>
        private ServiceResponse<List<Document>> GetDocumentListResponse(string moveId, List<string> documentList, string relativeURL)
        {
            List<Document> docList = new List<Document>();
            try
            {
                foreach (string documentName in documentList)
                {
                    docList.Add(new Document
                    {
                        DocumentTitle = documentName,
                        DocumentType = null,
                        RelativeUrl = relativeURL,
                        MoveId = moveId
                    });
                }
                if (docList.Count == 0)
                {
                    return new ServiceResponse<List<Document>> { Information = resourceManager.GetString("msgNoDocumentFound") };
                }
                return new ServiceResponse<List<Document>> { Data = docList };
            }
            catch (System.Exception ex)
            {
                logger.Error(resourceManager.GetString("msgServiceUnavailable"), ex);
                return new ServiceResponse<List<Document>> { Message = resourceManager.GetString("msgServiceUnavailable") };
                throw;
            }
        }

        /// <summary>
        /// Method Name     : GetRelativeUrl
        /// Author          : Ranjana Singh
        /// Creation Date   : 09 Feb 2018
        /// Purpose         : Gets the relative url of the document.
        /// Revision        :      
        /// </summary>
        public string GetRelativeUrl(Dictionary<string, string> documentListResponse)
        {
            return General.GetSpecificAttributeFromCRMResponse(documentListResponse, "relativeurl");
        }
       
        /// <summary>
        /// Method Name     : GetSharepointDocument
        /// Author          : Ranjana Singh
        /// Creation Date   : 16 Jan 2018
        /// Purpose         : Gets the estimate PDF file for the moveId in the request body from Sharepoint.
        /// Revision        :
        /// </summary>
        /// <returns></returns>
        private List<string> GetSharepointDocument(string filePath)
        {
            return sharepointConsumer.GetDocumentList(filePath);
        }
    }
}
