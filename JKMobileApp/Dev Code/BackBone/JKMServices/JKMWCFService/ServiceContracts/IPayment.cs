using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace JKMWCFService
{
    /// <summary>
    /// Interface Name      : IPayment
    /// Author              : Ranjana Singh
    /// Creation Date       : 27 Nov 2017
    /// Purpose             : Contains all information of Payment. 
    /// Revision            : By Pratik Soni on 31 Jan 2018: Removing LIST object for PostTransactionHistory
    /// </summary>
    /// <returns></returns>
    [ServiceContract]
    public interface IPayment
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/{customerID}/transaction")]
        string GetTransactionHistory(string customerID);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "{moveID}/amount")]
        string GetAmount(string moveID);

        [OperationContract]
        [WebInvoke(Method = "GET",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "/deviceid")]
        string GetDeviceID();

        [OperationContract]
        [WebInvoke(Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/amount")]
        string PutAmount(PaymentDetails requestContent);

        [OperationContract]
        [WebInvoke(Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "/transaction")]
        string PostTransactionHistory(TransactionDetail transactionDetails);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "/transaction")]
        string DeleteTransactionHistory();
    }

    [DataContract]
    public class TransactionDetail
    {
        [DataMember]
        public string TransactionNumber { get; set; }

        [DataMember]
        public string TransactionAmount { get; set; }

        [DataMember]
        public string TransactionDate { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string MoveID { get; set; }
    }

    [DataContract]
    public class PaymentDetails
    {
        [DataMember]
        public string TotalPaid { get; set; }
        
        [DataMember]
        public string MoveID { get; set; }
    }
}
