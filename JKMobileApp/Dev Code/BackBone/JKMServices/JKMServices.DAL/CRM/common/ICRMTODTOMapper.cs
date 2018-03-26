using JKMServices.DTO;
using System.Collections.Generic;

namespace JKMServices.DAL.CRM.common
{
    public interface ICRMTODTOMapper
    {
        ServiceResponse<T> ValidateResponse<T>(Dictionary<string, string> crmResponse);

        ServiceResponse<Customer> MapCustomerResponseToDTO(Dictionary<string, string> crmResponse);
        ServiceResponse<Move> MapMoveDetailsResponseToDTO(Dictionary<string, string> crmResponse);
        ServiceResponse<List<Estimate>> MapEstimateDataResponseToDTO(Dictionary<string, string> crmResponse);
        ServiceResponse<List<Estimate>> MapEstimateIdResponseToDTO(Dictionary<string, string> crmResponse);
        ServiceResponse<List<Estimate>> MapEstimatePdfResponseToDTO(Dictionary<string, string> crmResponse);
        ServiceResponse<Move> MapMoveCustomerResponseToDTO(Dictionary<string, string> crmResponse);
        ServiceResponse<Payment> MapPaymentResponseToDTO(Dictionary<string, string> crmResponse);
        ServiceResponse<List<Document>> MapDocumentListResponseToDTO(Dictionary<string, string> crmResponse);
        ServiceResponse<List<Alert>> MapAlertResponseToDTO(Dictionary<string, string> crmResponse);
        ServiceResponse<List<Move>> MapMoveListResponseToDTO(Dictionary<string, string> crmResponse);
    }
}