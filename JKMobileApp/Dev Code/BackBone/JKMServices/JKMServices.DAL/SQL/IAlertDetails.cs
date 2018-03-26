using JKMServices.DTO;
using System.Collections.Generic;

namespace JKMServices.DAL.SQL
{
    public interface IAlertDetails
    {
        List<Alert> GetAlertList(string customerId, string startDate);
        int DeleteAlertDetails(List<DTO.Alert> alertIdList);
        int PostAlertDetails(string customerID, List<DTO.Alert> alertDetails);
        int PutAlertDetails(List<DTO.Alert> alertDetails);
    }
}