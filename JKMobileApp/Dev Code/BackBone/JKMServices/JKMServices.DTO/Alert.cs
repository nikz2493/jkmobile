namespace JKMServices.DTO
{
    using System;

    /// <summary>
    /// Class Name      : Alert
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 28 Dec 2017
    /// Purpose         : DTO class
    /// Revision        : By Pratik Soni on 01 Feb 2018: Updated fields to map it to CRM's Activity Entity
    /// </summary>
    public partial class Alert
    {
        public string AlertID { get; set; }             // activityid
        public string CustomerID { get; set; }          // regardingobjectid
        public string AlertTitle { get; set; }          // subject
        public string AlertDescription { get; set; }    // description

        public DateTime StartDate { get; set; }         // actualstart
        public DateTime EndDate { get; set; }           // actualend

        public string NotificationType { get; set; }           // onerivet_notificationtype
        public string IsActive { get; set; }              // statecode
    }
}
