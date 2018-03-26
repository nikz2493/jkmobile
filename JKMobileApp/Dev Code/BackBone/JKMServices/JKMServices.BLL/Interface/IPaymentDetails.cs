namespace JKMServices.BLL.Interface
{
    public interface IPaymentDetails
    {
        string GetDeviceID();
        string PostTransactionHistory(string requestData);
        string GetAmount(string moveID);
        string PutAmount(string requestContent);
    }
}