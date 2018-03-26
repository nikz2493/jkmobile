using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace JKMWCFService
{
    /// <summary>
    /// Interface Name      : IMove
    /// Author              : Ranjana Singh
    /// Creation Date       : 27 Nov 2017
    /// Purpose             : Contains all information of Move. 
    /// Revision            :
    /// </summary>
    /// <returns></returns>
    [ServiceContract]
    public interface IMove
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json,
             UriTemplate = "/{moveID}")]
        string GetMoveData(string moveID);

        [OperationContract]
        [WebInvoke(Method = "GET",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "/{customerID}/move")]
        string GetMoveID(string customerID);

        [OperationContract]
        [WebInvoke(Method = "GET",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "/{moveID}/contact")]
        string GetContactForMove(string moveID);

        [OperationContract]
        [WebInvoke(Method = "GET",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "/list/{statusReason}")]
        string GetMoveList(string statusReason);

        [OperationContract]
        [WebInvoke(Method = "PUT",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         UriTemplate = "/{moveID}")]
        string PutMoveData(string moveID, MoveDetail moveDetail);
    }

    [DataContract]
    public class MoveDetail
    {
        [DataMember]
        public string StatusReason { get; set; }
    }
}
