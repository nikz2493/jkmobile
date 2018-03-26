using System.Collections.Generic;

namespace JKMServices.DAL.CRM
{
    /// Interface Name      : IMoveDetails
    /// Author              : Pratik Soni
    /// Creation Date       : 13 Dec 2017
    /// Purpose             : To perform operations on Move entity
    /// Revision            : 
    /// </summary>
    public interface IMoveDetails
    {
        DTO.ServiceResponse<DTO.Move> GetMoveData(string moveID);
        DTO.ServiceResponse<DTO.Move> GetContactForMove(string moveID);
        DTO.ServiceResponse<DTO.Move> GetMoveId(string customerID);
        Dictionary<string, string> GetMoveGUID(string moveNumber);
        DTO.ServiceResponse<List<DTO.Move>> GetMoveList(string statusReason);
        DTO.ServiceResponse<DTO.Move> PutMoveData(string moveID, string jsonFormattedData);
        DTO.ServiceResponse<DTO.Move> GetCustomerDetails(string moveID);
    }
}
