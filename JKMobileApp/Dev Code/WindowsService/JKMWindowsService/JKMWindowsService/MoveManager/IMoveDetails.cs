using System.Collections.Generic;
using JKMWindowsService.Model;

namespace JKMWindowsService.MoveManager
{
    public interface IMoveDetails
    {
        List<MoveModel> GetMoveList(string statusReason);
    }
}