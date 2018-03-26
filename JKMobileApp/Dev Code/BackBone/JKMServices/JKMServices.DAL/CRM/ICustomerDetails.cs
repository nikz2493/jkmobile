using System.Collections.Generic;

namespace JKMServices.DAL.CRM
{
    /// Interface Name      : ICustomerDetails
    /// Author              : Pratik Soni
    /// Creation Date       : 1 Dec 2017
    /// Purpose             : To perform operations on Customer entity
    /// Revision            : 
    /// </summary>
    public interface ICustomerDetails
    {
        Dictionary<string, string> GetCustomerGUID(string customerID);
        string GetCustomerEmail(string customerID);
        bool CheckCustomerRegistered(string customerID);

        DTO.ServiceResponse<DTO.Customer> GetCustomerIDAsync(string emailID);
        DTO.ServiceResponse<DTO.Customer> GetCustomerProfileData(string customerID);
        DTO.ServiceResponse<DTO.Customer> GetCustomerVerificationData(string customerID);
        DTO.ServiceResponse<DTO.Customer> PutCustomerVerificationData(string customerID, string jsonFormattedData);
        DTO.ServiceResponse<DTO.Customer> PutCustomerProfileData(string customerID, string jsonFormattedData);
    }
}