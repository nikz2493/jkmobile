namespace JKMWindowsService.AlertJSONGenerator
{
    public interface IBookYourMoveGenerator
    {
        string GenerateJSON(string customerID, string alertDescription);
    }
}