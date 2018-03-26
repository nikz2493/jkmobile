using JKMServices.BLL.Interface;
using JKMServices.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utility;
using Utility.Logger;

namespace JKMServices.BLL
{
    public class DocumentDetails : ServiceBase, IDocumentDetails
    {
        private readonly DAL.CRM.IDocumentDetails crmDocumentDetails;
        private readonly ISharepointConsumer sharepointConsumer;
        private readonly IResourceManagerFactory resourceManager;
        private readonly ILogger logger;

        //Constructor
        public DocumentDetails(DAL.CRM.IDocumentDetails crmDocumentDetails,
                               ISharepointConsumer sharepointConsumer,
                               IResourceManagerFactory resourceManager,
                               ILogger logger)
        {
            this.crmDocumentDetails = crmDocumentDetails;
            this.sharepointConsumer = sharepointConsumer;
            this.resourceManager = resourceManager;
            this.logger = logger;
        }

        /// <summary>
        /// Method Name     : GetDocumentList
        /// Author          : Ranjana Singh
        /// Creation Date   : 22 Dec 2017
        /// Purpose         : Gets the list of documents to be displayed in My Documents Page. If Customerid is not provided, service will return common documents.
        /// Revision        : 
        /// </summary>
        public string GetDocumentList(string moveId)
        {
            ServiceResponse<List<Document>> getDocumentResponse;
            List<string> documentList = new List<string>();
            try
            {
                if (!Validations.IsValid(moveId))
                {
                    logger.Error(resourceManager.GetString("msgBadRequest"));
                    return General.ConvertToJson<ServiceResponse<List<Document>>>(new ServiceResponse<List<Document>> { BadRequest = resourceManager.GetString("msgBadRequest") });
                }
                getDocumentResponse = crmDocumentDetails.GetDocumentList(moveId);
                if (getDocumentResponse.Data == null)
                {
                    logger.Info(resourceManager.GetString("msgNoDataFoundForMove"));
                    return GenerateServiceResponse<Document>(INFORMATION, resourceManager.GetString("msgNoDataFoundForMove"));
                }
                foreach (var dtoDocument in getDocumentResponse.Data)
                {
                    documentList.Add(dtoDocument.RelativeUrl);
                }
                return General.ConvertToJson<ServiceResponse<List<Document>>>(getDocumentResponse);
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgNoDataFoundForMove"), ex);
                return GenerateServiceResponse<Document>(MESSAGE, resourceManager.GetString("msgNoDataFoundForMove"));
            }
        }

        /// <summary>
        /// Method Name     : GetDocumentPDF
        /// Author          : Pratik Soni
        /// Creation Date   : 13 Feb 2018
        /// Purpose         : Gets the pdf file related to the document title
        /// Revision        : 
        /// </summary>
        /// <remarks> Add below code instead of code written in warning. That code is mocked for testing purpose.
        /// const Char relativeFilePathDelimiter = '|';
        /// string[] substrings = relativeFilePath.Split(relativeFilePathDelimiter);
        ///        if (substrings.Count<string>() != 2)
        ///        {
        ///logger.Error(resourceManager.GetString("logInvalidRequestContent"));
        ///            return General.ConvertToJson<ServiceResponse<Document>>(new ServiceResponse<Document> { BadRequest = resourceManager.GetString("logInvalidRequestContent") });
        ///}
        ///
        ///string formattedFilePath = Path.Combine("jkmoving_move", substrings[0], substrings[1]);
        ///fileContentInByteArray = sharepointConsumer.HandleResult(formattedFilePath); //Sharepoint method call, which is being mocked for now.
        /// </remarks>
        /// <param name="relativeFilePath">It should be the combination of DTO.Document.RelativePath + "|" + DTO.Document.DocumentTitle</param>
        /// <returns>Success = Service Response with base64 string in DATA node.</returns>
        public string GetDocumentPDF(string relativeFilePath)
        {
            try
            {
                byte[] fileContentInByteArray;

                #warning Include code from remarks from comment section in here when mocking will not be required. And remove below 4 lines
                if(!Validations.IsValid(relativeFilePath.Trim()))
                {
                    logger.Error(resourceManager.GetString("msgBadRequest"));
                    return General.ConvertToJson<ServiceResponse<Document>>(new ServiceResponse<Document> { BadRequest = resourceManager.GetString("msgBadRequest") });
                }
                string documentpath, fileFullPath, applicationPath = string.Empty;
                applicationPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
                documentpath = General.GetConfigValue("mockedPdfPath");
                fileFullPath = Path.Combine(applicationPath, documentpath);
                fileContentInByteArray = System.IO.File.ReadAllBytes(fileFullPath);
                //End of mocked code

                logger.Info("GetConditionalResponseForDocument method encountered to return formatted service response.");
                return GetConditionalResponseForDocument(fileContentInByteArray);
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgServiceUnavailable"), ex);
                return General.ConvertToJson<ServiceResponse<Document>>(new ServiceResponse<Document> { Message = resourceManager.GetString("msgServiceUnavailable") });
            }
        }
    }
}