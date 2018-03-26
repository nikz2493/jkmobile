namespace JKMWindowsService.AlertJSONGenerator
{
    public interface IFinalPaymentMadeGenerator
    {
        string GenerateJSON(string customerID, string alertDescription);
    }
}