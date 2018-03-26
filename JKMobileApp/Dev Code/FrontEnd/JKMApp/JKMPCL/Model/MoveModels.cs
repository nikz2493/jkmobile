using System;
using System.Collections.Generic;

namespace JKMPCL.Model
{
    /// <summary>
    /// Class Name      : GetMoveIDModel.
    /// Author          : Hiren Patel
    /// Creation Date   : 12 Dec 2017
    /// Purpose         :  
    /// Revision        : 
    /// </summary> 
    public class GetMoveIDModel
    {
        public string CustomerId { get; set; }
        public string MoveNumber { get; set; }
    }

    /// <summary>
    /// Class Name      : GetMoveIDResponseModel.
    /// Author          : Hiren Patel
    /// Creation Date   : 27 Dec 2017
    /// Purpose         :  
    /// Revision        : 
    /// </summary> 
    public class GetMoveIDResponse
    {
        public string CustomerId { get; set; }
        public string MoveId { get; set; }
    }

    /// <summary>
    /// Class Name      : GetMoveDataModelResponse
    /// Author          : Hiren Patel
    /// Creation Date   : 27 Dec 2017
    /// Purpose         : 
    /// Revision        : 
    /// </summary> 
    public class GetMoveDataResponse
    {
        public string MoveId { get; set; }
        public string MoveNumber { get; set; }
        public string IsActive { get; set; }
        public string StatusReason { get; set; }
        public string Destination_City { get; set; }
        public string Destination_State { get; set; }
        public string Destination_Country { get; set; }
        public string Destination_ContactName { get; set; }
        public string CustomDestinationAddress { get; set; }
        public string Origin_City { get; set; }
        public string Origin_State { get; set; }
        public string Origin_Country { get; set; }
        public string Origin_ContactName { get; set; }
        public string CustomOriginAddress { get; set; }
        public string MoveStartDate { get; set; }
        private string LoadEndDateValue=string.Empty;
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
        public string WhatMattersMost { get; set; }
        public string ExcessValuation { get; set; }
        public string ValuationDeductible { get; set; }
        public string ValuationCost { get; set; }
        public string ServiceCode { get; set; }

        public bool response_status { get; set; }
        public string message { get; set; }

        public List<MyServices> MyServices { get; set; }
        public List<AlertModel> MyAlertlist { get; set; }

        //Move Details
        public string MoveDetails_PackStartDate { get; set; }               //jkmoving_packfrom
        public string MoveDetails_LoadStartDate { get; set; }               //jkmoving_loadfrom
        public string MoveDetails_DeliveryStartDate { get; set; }           //jkmoving_deliveryfrom
        public string MoveDetails_DeliveryEndDate { get; set; }             //jkmoving_deliveryto

    }

    /// <summary>
    /// Class Name      : MyServices
    /// Author          : Hiren Patel
    /// Creation Date   : 27 Dec 2017
    /// Purpose         :  
    /// Revision        : 
    /// </summary> 
    public class MyServices
    {
        public string ServicesCode { get; set; }
    }

    /// <summary>
    /// Class Name      : GetContactListForMoveResponse
    /// Author          : Hiren Patel
    /// Creation Date   : 27 Dec 2017
    /// Purpose         :  
    /// Revision        : 
    /// </summary> 
    public class GetContactListForMoveResponse
    {
        // Move Contacts
        public string address1_telephone1 { get; set; }
        public string internalemailaddress { get; set; }
        public string fullname { get; set; }
    }

    /// <summary>
    /// Class Name      : MoveDataModel
    /// Author          : Hiren Patel
    /// Creation Date   : 27 Dec 2017
    /// Purpose         :  
    /// Revision        : 
    /// </summary> 
    public class MoveDataModel
    {
        public string daysLeft { get; set; }
        public string MoveId { get; set; }
        public string MoveNumber { get; set; }
        public string IsActive { get; set; }
        public string StatusReason { get; set; }
        public string Destination_City { get; set; }
        public string CustomOriginAddress { get; set; }
        public string CustomDestinationAddress { get; set; }
        public string Origin_City { get; set; }
        public string WhatMattersMost { get; set; }
        public string ExcessValuation { get; set; }
        public string ValuationDeductible { get; set; }
        public string ValuationCost { get; set; }
        public string ServiceCode { get; set; }
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
        public bool response_status { get; set; }
        public string message { get; set; }

        public List<MyServicesModel> MyServices { get; set; }
        public List<AlertModel> MyAlertlist { get; set; }

        //Move Details
        public string MoveDetails_PackStartDate { get; set; }               //jkmoving_packfrom
        public string MoveDetails_LoadStartDate { get; set; }               //jkmoving_loadfrom
        public string MoveDetails_DeliveryStartDate { get; set; }           //jkmoving_deliveryfrom
        public string MoveDetails_DeliveryEndDate { get; set; }             //jkmoving_deliveryto
    }

    /// <summary>
    /// Class Name      : MyServicesModel
    /// Author          : Hiren Patel
    /// Creation Date   : 30 Dec 2017
    /// Purpose         :  
    /// Revision        : 
    /// </summary> 
    public class MyServicesModel
    {
        public string ServicesCode { get; set; }
    }
}
