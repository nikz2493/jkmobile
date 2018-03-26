using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace JKMWCFService
{
    /// <summary>
    /// Interface Name      : IAlert
    /// Author              : Ranjana Singh
    /// Creation Date       : 27 Nov 2017
    /// Purpose             : Gets the list of all alerts. 
    /// Revision            :
    /// </summary>
    /// <returns></returns>
    [ServiceContract]
    public interface IAlert
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/{CustomerId}/{*startDate}")]
        string GetAlertList(string customerID, string startDate = null);

        [OperationContract]
        [WebInvoke(Method = "PUT",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "/{alertID}")]
        string PutAlertList(string alertID, AlertDetail alertDetails);

        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         UriTemplate = "/{customerID}")]
        string PostAlertList(string customerID, AlertDetail alertDetails);

    }

    [DataContract]
    public class AlertDetail
    {
        [DataMember]
        public string AlertID { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string AlertTitle { get; set; }

        [DataMember]
        public string AlertDescription { get; set; }

        [DataMember]
        public string StartDate { get; set; }

        [DataMember]
        public string EndDate { get; set; }

        [DataMember]
        public int NotificationType { get; set; }

        [DataMember]
        public string IsActive { get; set; }
    }

    [DataContract]
    public class AlertId
    {
        [DataMember]
        public string AlertID { get; set; }
    }
}