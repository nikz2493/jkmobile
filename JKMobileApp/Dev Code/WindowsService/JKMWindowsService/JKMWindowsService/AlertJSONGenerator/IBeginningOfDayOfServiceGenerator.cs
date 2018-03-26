namespace JKMWindowsService.AlertJSONGenerator
{
    public interface IBeginningOfDayOfServiceGenerator
    {
        string GenerateJSON(string customerID, string startDate);
    }
}