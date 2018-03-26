namespace JKMWindowsService.AlertJSONGenerator
{
    public interface IEndOfServiceCheckInGenerator
    {
        string GenerateJSON(string customerID, string startDate);
    }
}