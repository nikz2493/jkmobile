namespace JKMServices.BLL.Interface
{
    public interface IAlertDetails
    {
        string GetAlertList(string customerID, string startDate = null);
        string PostAlertList(string customerID, string requestContent);
        string PutAlertList(string alertID, string requestContent);
    }
}