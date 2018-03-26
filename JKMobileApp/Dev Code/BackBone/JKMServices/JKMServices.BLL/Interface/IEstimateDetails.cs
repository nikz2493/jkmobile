namespace JKMServices.BLL.Interface
{
    public interface IEstimateDetails
    {
        string GetEstimateData(string customerId);
        string GetEstimatePDF(string moveId);
        string PutEstimateData(string moveId, string requestBody);
    }
}