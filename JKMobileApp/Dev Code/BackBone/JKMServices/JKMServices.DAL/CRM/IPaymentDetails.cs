using JKMServices.DTO;

namespace JKMServices.DAL.CRM
{
    public interface IPaymentDetails
    {
        ServiceResponse<DTO.Payment> PostTransactionHistory(DTO.Payment dtoPayment);
        ServiceResponse<DTO.Payment> GetDeviceID();
        ServiceResponse<DTO.Payment> GetAmount(string moveID);
        ServiceResponse<DTO.Payment> PutAmount(string jsonFormattedData);
    }
}