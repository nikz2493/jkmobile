using JKMWindowsService.Model;
using JKMWindowsService.Utility;
using System.Collections.Generic;

namespace JKMWindowsService.UnitTest.MockData
{
    public class FinalPaymentMadeData
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
                AlertTitle = "Final Payment Made",
                AlertDescription = @"Thank you for your business!  We will be sending you a survey within the next 2-3 business days 
                                    regarding your experience.  Please take a moment to complete to tell us how we did.",
                NotificationType = "676,860,005",
                StartDate = "12-05-2018",
                EndDate = "13-05-2018",
                IsActive = true
            };

            return General.ConvertToJson<AlertModel>(alertModel);
        }
    }
}
