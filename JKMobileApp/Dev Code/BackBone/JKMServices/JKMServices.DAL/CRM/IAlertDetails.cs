using JKMServices.DTO;
using System.Collections.Generic;

namespace JKMServices.DAL.CRM
{
    public interface IAlertDetails
    {
        ServiceResponse<List<DTO.Alert>> GetAlertList(string customerID, string startDate = null);
        ServiceResponse<Alert> PostAlertDetails(string customerID, string jsonFormattedData);
        ServiceResponse<Alert> PutAlertDetails(string alertID, string jsonFormattedData);
    }
}