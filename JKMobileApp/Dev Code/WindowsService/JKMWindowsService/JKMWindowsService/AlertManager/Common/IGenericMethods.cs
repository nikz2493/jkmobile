using System.Collections.Generic;
using JKMWindowsService.Model;

namespace JKMWindowsService.AlertManager.Common
{
    public interface IGenericMethods
    {
        List<MoveModel> GetMoveModelList(string statusReason);
    }
}