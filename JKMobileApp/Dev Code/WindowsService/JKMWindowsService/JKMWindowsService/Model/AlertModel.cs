using System;
using System.Collections.Generic;

namespace JKMWindowsService.Model
{
    /// <summary>
    /// Class Name      : AlertModel
    /// Author          : Pratik Soni
    /// Creation Date   : 14 Feb 2018
    /// Purpose         : DTO for Alert Details
    /// Revision : 
    /// </summary>
    public class AlertModel
    {
        public string AlertID { get; set; }
        public string CustomerID { get; set; }
        public string AlertTitle { get; set; }
        public string AlertDescription { get; set; }
        public string NotificationType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int AlertType { get; set; }
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Class Name      : AlertList
    /// Author          : Pratik Soni
    /// Creation Date   : 14 Feb 2018
    /// Purpose         : DTO for Alert list
    /// Revision : 
    /// </summary>
    public class AlertList
    {
        public List<AlertModel> AlertsList { get; set; }
    }
}
