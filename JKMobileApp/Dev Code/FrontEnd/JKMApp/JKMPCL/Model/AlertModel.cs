using System;
using System.Collections.Generic;

namespace JKMPCL.Model
{
    /// <summary>
    /// Class Name      : AlertModel
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 28 Dec 2017
    /// Purpose         : DTO for Alert Details
    /// Revision : 
    /// </summary>
    public class AlertModel
    {
        public string AlertID { get; set; }
        public string CustomerID { get; set; }
        public string AlertTitle { get; set; }
        public string AlertDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? DisplayInCalendar { get; set; }
        public string ActionToTake { get; set; }
        public int AlertType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CustomerLastLoginDate { get; set; }
    }


    /// <summary>
    /// Class Name      : AlertList
    /// Author          : Hiren M Patel
    /// Creation Date   : 28 Dec 2017
    /// Purpose         : DTO for Alert list
    /// Revision : 
    /// </summary>
    public class AlertList
    {
        public List<AlertModel> AlertsList { get; set; }
    }

    /// <summary>
    /// Class Name      : AlertModel
    /// Author          : Hiren M Patel
    /// Creation Date   : 28 Dec 2017
    /// Purpose         : DTO for Alert Details list response
    /// Revision : 
    /// </summary>
    public class AlertModelResponse
    {
        public string CustomerLastLoginDate { get; set; }
        public string AlertID { get; set; }
        public string CustomerID { get; set; }
        public string AlertTitle { get; set; }
        public string AlertDescription { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool? DisplayInCalendar { get; set; }
        public string ActionToTake { get; set; }
        public int AlertType { get; set; }
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Class Name      : GetAlertListResponse
    /// Author          : Hiren M Patel
    /// Creation Date   : 28 Dec 2017
    /// Purpose         : DTO for Alert Details list response
    /// Revision : 
    /// </summary>
    public class GetAlertListResponse
    {
        public List<AlertModelResponse> AlertsList { get; set; }
    }


}
