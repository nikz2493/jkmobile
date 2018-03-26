using JKMWindowsService.Model;
using System.Collections.Generic;

namespace JKMWindowsService.AlertManager
{
    public interface IPreMoveConfirmationNotifications
    {
        void SendAlerts();
    }
}