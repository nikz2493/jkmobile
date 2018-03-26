using JKMWindowsService.Model;

namespace JKMWindowsService.AlertJSONGenerator
{
    public interface IPreMoveConfirmationNotificationGenerator
    {
        string GenerateJSON(string customerID, string alertStartDate, string alertEndDate, string alertDescription);
    }
}