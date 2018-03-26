namespace JKMServices.DAL.CRM.common
{
    public interface IDTOToCRMMapper
    {
        string MapCustomerDTOToCRM(string requestBody);
        string MapMoveDTOToCRM(string requestBody);
        string MapEstimateDTOToCRM(string requestBody);
        string MapPaymentDTOToCRM(string requestBody);
        string MapAlertDTOToCRM(string requestBody);
    }
}