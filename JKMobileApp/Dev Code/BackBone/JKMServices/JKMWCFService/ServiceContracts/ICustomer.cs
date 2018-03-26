using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace JKMWCFService
{
    /// <summary>
    /// Interface Name      : ICustomer
    /// Author              : Ranjana Singh
    /// Creation Date       : 27 Nov 2017
    /// Purpose             : Contains all information of Customers. 
    /// Revision            :
    /// </summary>
    /// <returns></returns>
    [ServiceContract]
    public interface ICustomer
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json,
             UriTemplate = "/{email}")]
        string GetCustomerID(string email);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/{customerID}/profile")]
        string GetCustomerProfileData(string customerID);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/{customerID}/verification")]
        string GetCustomerVerificationData(string customerID);

        [OperationContract]
        [WebInvoke(Method = "POST",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json,
          UriTemplate = "/{customerID}/profile")]
        string PostCustomerProfileData(string customerID);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/{customerID}/verification")]
        string PostCustomerVerificationData(string customerID);

        [OperationContract]
        [WebInvoke(Method = "PUT",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         UriTemplate = "/{customerID}/profile")]
        string PutCustomerProfileData(string customerID, CustomerProfile customerProfile);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/{customerID}/verification")]
        string PutCustomerVerificationData(string customerID, CustomerVerification customerVerification);
    }

    [DataContract]
    public class CustomerProfile
    {
        // Customer Profile fields 
        [DataMember]
        public string CustomerId { get; set; }

        [DataMember]
        public bool TermsAgreed { get; set; }

        // My Account fields 
        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string PreferredContact { get; set; }

        [DataMember]
        public string ReceiveNotifications { get; set; }

    }

    [DataContract]
    public class CustomerVerification
    {
        [DataMember]
        public string CustomerId { get; set; }

        [DataMember]
        public string PasswordHash { get; set; }

        [DataMember]
        public string PasswordSalt { get; set; }
    }
}
