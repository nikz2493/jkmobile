using JKMServices.DTO;
using System.Collections.Generic;

namespace JKMServices.DAL.CRM.common
{
    public interface ICRMUtilities
    {
        Dictionary<string, string> ExecuteDeleteRequest(string entityName, string jsonFormattedData);
        Dictionary<string, string> ExecuteGetRequest(string entityName, string retriveFieldList, string filterString, string orderBy = "", string orderingType = "", string expandValue = "");
        Dictionary<string, string> ExecutePostRequest(string entityName, string jsonFormattedData);
        Dictionary<string, string> ExecutePutRequest(string entityName, string strGuID, string jsonFormattedData);

        bool IsValidResponse(Dictionary<string, string> response);
        bool ContainsNullValue(Dictionary<string, string> response);
        ServiceResponse<T> GetFormattedResponseToDTO<T>(Dictionary<string, string> crmResponse);
        string GetSpecificAttributeFromResponse(Dictionary<string, string> crmResponse, string requiredField);
    }
}