using Newtonsoft.Json.Linq;
using Ninject;
using System.ServiceModel.Web;

namespace JKMWCFService
{
    /// <summary>
    /// Class Name      : Document
    /// Author          : Ranjana Singh
    /// Creation Date   : 27 Nov 2017
    /// Purpose         : Contains all information of documents.
    /// Revision        :
    /// </summary>
    /// <returns></returns>
    public class Document : IDocument
    {
        private readonly JKMServices.BLL.Interface.IDocumentDetails bllDocumentDetails;
        private readonly StandardKernel kernel;

        public Document()
        {
            log4net.Config.XmlConfigurator.Configure();
            StandardKernelLoader standardKernelLoader = new StandardKernelLoader();
            kernel = Utility.General.StandardKernel();
            kernel = standardKernelLoader.LoadStandardKernel();
            bllDocumentDetails = Utility.General.Resolve<JKMServices.BLL.Interface.IDocumentDetails>();
        }

        /// <summary>
        /// Method Name     : GetDocumentList
        /// Author          : Ranjana Singh
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Gets the list of documents to be displayed in My Documents Page. If moveId is not provided, service will return common documents.
        /// Revision        :
        /// </summary>
        /// <param name="moveId"></param>
        /// <returns></returns>
        public string GetDocumentList(string moveId)
        {
            string serviceResponse = bllDocumentDetails.GetDocumentList(moveId).Replace("\"", "'");
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }

        /// <summary>
        /// Method Name     : GetDocumentPDF
        /// Author          : Ranjana Singh
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Gets the pdf file related to the document
        /// Revision        :
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public string GetDocumentPDF(string relativeFilePath)
        {
            string serviceResponse = bllDocumentDetails.GetDocumentPDF(relativeFilePath).Replace("\"", "'");
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }
    }
}
