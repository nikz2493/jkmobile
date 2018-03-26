using System.IO;

namespace JKMServices.BLL.Interface
{
    public interface ICustomerDetails
    {
        string GetCustomerID(string emailID);
        string GetCustomerProfileData(string customerID);
        string GetCustomerVerificationData(string customerID);
        string PostCustomerProfileData(string customerID);
        string PostCustomerVerificationData(string customerID);
        string PutCustomerProfileData(string customerID, string requestCustomerProfile);
        string PutCustomerVerificationData(string customerID, string requestCustomerVerification);
    }
}