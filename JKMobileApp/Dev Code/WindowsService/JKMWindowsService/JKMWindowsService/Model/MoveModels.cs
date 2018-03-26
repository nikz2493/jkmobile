using System.Collections.Generic;

namespace JKMWindowsService.Model
{
    /// <summary>
    /// Class Name      : GetMoveIDList
    /// Author          : Pratik Soni
    /// Creation Date   : 15 Feb 2018
    /// Purpose         : 
    /// Revision        : 
    /// </summary> 
    public class MoveModel
    {
        public string MoveId { get; set; }
        public string MoveNumber { get; set; }
        public string IsActive { get; set; }
        public string StatusReason { get; set; }
        public string ContactOfMoveId { get; set; }

        public string MoveStartDate { get; set; }
        private string LoadEndDateValue;
        public string MoveEndDate
        {
            get
            {
                return LoadEndDateValue;
            }
            set
            {
                if (value is null || value == string.Empty)
                {
                    LoadEndDateValue = "TBD";
                }
                else
                {
                    LoadEndDateValue = value;
                }
            }
        }
        public List<AlertModel> MyAlertlist { get; set; }

        //Move Details
        public string MoveDetails_PackStartDate { get; set; }               //jkmoving_packfrom
        public string MoveDetails_LoadStartDate { get; set; }               //jkmoving_loadfrom
        public string MoveDetails_DeliveryStartDate { get; set; }           //jkmoving_deliveryfrom
        public string MoveDetails_DeliveryEndDate { get; set; }             //jkmoving_deliveryto
    }
}
