using JKMWindowsService.Model;
using JKMWindowsService.Utility;
using System.Collections.Generic;

namespace JKMWindowsService.UnitTest.MockData
{
    public class PreMoveConfirmationData
    {
        public List<MoveModel> GetMoveModelList()
        {
            MoveModel move1 = new MoveModel { MoveId = "1", MoveNumber = "RM6548" };
            MoveModel move2 = new MoveModel { MoveId = "2", MoveNumber = "RM4526" };
            List<MoveModel> moveModelList = new List<MoveModel>
            {
                move1,
                move2
            };
            return moveModelList;
        }

        public string GetAlertModelJSON()
        {
            AlertModel alertModel = new AlertModel
            {
                AlertID = null,
                CustomerID = "RC012",
                AlertTitle = "Are You Ready?",
                AlertDescription = "First Pre-Move Confirmation Notification - 5 Days to go",
                NotificationType = "676,860,002",
                StartDate = "12-05-2018",
                EndDate = "13-05-2018",
                IsActive = true
            };

            return General.ConvertToJson<AlertModel>(alertModel);
        }
    }
}
