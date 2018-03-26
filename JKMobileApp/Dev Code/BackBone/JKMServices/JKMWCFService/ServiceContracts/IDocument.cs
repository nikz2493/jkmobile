using System.ServiceModel;
using System.ServiceModel.Web;

namespace JKMWCFService
{
    /// <summary>
    /// Interface Name      : IDocument
    /// Author              : Ranjana Singh
    /// Creation Date       : 27 Nov 2017
    /// Purpose             : Gets all information of Document. 
    /// Revision            :
    /// </summary>
    /// <returns></returns>
    [ServiceContract]
    public interface IDocument
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/list/{moveId}")]
        string GetDocumentList(string moveId);

        [OperationContract]
        [WebInvoke(Method = "GET",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         UriTemplate = "{relativeFilePath}/pdf")]
        string GetDocumentPDF(string relativeFilePath);
    }
}
