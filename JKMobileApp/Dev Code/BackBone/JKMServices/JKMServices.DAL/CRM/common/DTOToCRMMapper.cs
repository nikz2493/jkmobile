using Newtonsoft.Json.Linq;
using System;

namespace JKMServices.DAL.CRM.common
{
    /// <summary>
    /// Class Name      : DTOToCRMMapper
    /// Author          : Pratik Soni
    /// Creation Date   : 19 Jan 2018
    /// Purpose         : Class contains methods to map DTO properties to respective CRM field name.
    /// Revision        : 
    /// </summary>
    public class DTOToCRMMapper : IDTOToCRMMapper
    {
        const string UTCFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";

        /// Method Name     : GetDateForInsertInCRM
        /// Author          : Pratik Soni
        /// Creation Date   : 03 Feb 2018
        /// Purpose         : To get the DateOffset to save proper date in CRM.
        /// Revision        : 
        /// </summary>
        /// <returns> "string" of dateoffset with time zone</returns>
        /// 
        private string GetDateForInsertInCRM(string utcFormattedDate)
        {
            DateTime packstartDate = DateTime.ParseExact(utcFormattedDate, UTCFormat, null);
            DateTimeOffset dateOffset = new DateTimeOffset(packstartDate, TimeZoneInfo.Local.GetUtcOffset(packstartDate));
            return dateOffset.ToString("o");
        }

        /// Method Name     : MapMoveDTOToCRM
        /// Author          : Pratik Soni
        /// Creation Date   : 19 Jan 2018s
        /// Purpose         : To Map Move DTO's properties to CRM fields
        /// Revision        : 
        /// </summary>
        /// <returns> "string" of json formatted requestBody where DTO property will be replaced with respective CRM field name </returns>
        /// 
        public string MapMoveDTOToCRM(string requestBody)
        {
            JObject unformattedRequestJObject;
            JObject formattedRequestJObject = new JObject();

            unformattedRequestJObject = JObject.Parse(requestBody);

            foreach (JProperty property in unformattedRequestJObject.Properties())
            {
                // Each field of request body for Move DTO should be added as a "case".
                switch (property.Name)
                {
                    case "StatusReason":
                        formattedRequestJObject.Add("statuscode", property.Value);
                        break;
                }
            }
            return formattedRequestJObject.ToString(Newtonsoft.Json.Formatting.None);
        }

        /// Method Name     : MapEstimateDTOToCRM
        /// Author          : Pratik Soni
        /// Creation Date   : 20 Jan 2018s
        /// Purpose         : To Map Estimate DTO's properties to CRM fields
        /// Revision        : 
        /// </summary>
        /// <returns> "string" of json formatted requestBody where DTO property will be replaced with respective CRM field name </returns>
        /// 
        public string MapEstimateDTOToCRM(string requestBody)
        {
            JObject unformattedRequestJObject;
            JObject formattedRequestJObject = new JObject();
            string formattedDateString;

            unformattedRequestJObject = JObject.Parse(requestBody);

            foreach (JProperty property in unformattedRequestJObject.Properties())
            {
                // Each field of request body for Estimate DTO should be added as a "case".
                switch (property.Name)
                {
                    case "CustomOriginAddress":
                        formattedRequestJObject.Add("onerivet_originaddress", property.Value);
                        break;
                    case "CustomDestinationAddress":
                        formattedRequestJObject.Add("onerivet_destinationaddress", property.Value);
                        break;
                    case "PackStartDate":
                        formattedDateString = GetDateForInsertInCRM(property.Value.ToObject<DateTime>().ToString(UTCFormat));
                        formattedRequestJObject.Add("onerivet_packfrom", formattedDateString);
                        break;
                    case "LoadStartDate":
                        formattedDateString = GetDateForInsertInCRM(property.Value.ToObject<DateTime>().ToString(UTCFormat));
                        formattedRequestJObject.Add("onerivet_loadfrom", formattedDateString);
                        break;
                    case "MoveStartDate":
                        formattedDateString = GetDateForInsertInCRM(property.Value.ToObject<DateTime>().ToString(UTCFormat));
                        formattedRequestJObject.Add("onerivet_deliveryfrom", formattedDateString);
                        break;
                    case "ExcessValuation":
                        formattedRequestJObject.Add("onerivet_declaredpropertyvalue", property.Value);
                        break;
                    case "ValuationDeductible":
                        formattedRequestJObject.Add("onerivet_valuationdeductible", property.Value);
                        break;
                    case "WhatMattersMost":
                        formattedRequestJObject.Add("onerivet_whatmattersmost", property.Value);
                        break;
                }
            }
            return formattedRequestJObject.ToString(Newtonsoft.Json.Formatting.None);
        }

        /// Method Name     : MapCustomerDTOToCRM
        /// Author          : Pratik Soni
        /// Creation Date   : 24 Jan 2018
        /// Purpose         : To Map Customer DTO's properties to CRM fields
        /// Revision        : 
        /// </summary>
        /// <returns> "string" of json formatted requestBody where DTO property will be replaced with respective CRM field name </returns>
        public string MapCustomerDTOToCRM(string requestBody)
        {
            JObject unformattedRequestJObject;
            JObject formattedRequestJObject = new JObject();
            string formattedDateString;

            unformattedRequestJObject = JObject.Parse(requestBody);

            foreach (JProperty property in unformattedRequestJObject.Properties())
            {
                // Each field of request body for Move DTO should be added as a "case".
                switch (property.Name)
                {
                    case "TermsAgreed":
                        formattedRequestJObject.Add("onerivet_termsagreed", property.Value);
                        break;
                    case "PasswordHash":
                        formattedRequestJObject.Add("onerivet_passwordhash", property.Value);
                        break;
                    case "PasswordSalt":
                        formattedRequestJObject.Add("onerivet_passwordsalt", property.Value);
                        break;
                    case "CodeValidTill":
                        formattedDateString = GetDateForInsertInCRM(property.Value.ToObject<DateTime>().ToString(UTCFormat));
                        formattedRequestJObject.Add("onerivet_codevalidtill", formattedDateString);
                        break;
                    case "VerificationCode":
                        formattedRequestJObject.Add("onerivet_verificationcode", property.Value.ToString());
                        break;
                    case "IsCustomerRegistered":
                        formattedRequestJObject.Add("onerivet_iscustomerregistered", property.Value.ToString());
                        break;
                    case "Phone":
                        formattedRequestJObject.Add("telephone1", property.Value.ToString());
                        break;
                    case "PreferredContact":
                        formattedRequestJObject.Add("preferredcontactmethodcode", property.Value.ToString());
                        break;
                    case "ReceiveNotifications":
                        formattedRequestJObject.Add("onerivet_receivenotification", property.Value.ToString());
                        break;
                }
            }
            return formattedRequestJObject.ToString(Newtonsoft.Json.Formatting.None);
        }

        /// Method Name     : MapPaymentDTOToCRM
        /// Author          : Ranjana Singh
        /// Creation Date   : 25 Jan 2018
        /// Purpose         : To Map Payment DTO's properties to CRM fields.
        /// Revision        : By Pratik Soni on 30 Jan 2018: Updated method to generate valid JSON for lookup field
        /// </summary>
        /// <returns> "string" of json formatted requestBody where DTO property will be replaced with respective CRM field name </returns>
        /// 
        public string MapPaymentDTOToCRM(string requestBody)
        {
            JObject unformattedRequestJObject;
            JObject formattedRequestJObject = new JObject();

            unformattedRequestJObject = JObject.Parse(requestBody);

            foreach (JProperty property in unformattedRequestJObject.Properties())
            {
                // Each field of request body for Payment DTO should be added as a "case".
                switch (property.Name)
                {
                    case "TransactionNumber":
                        formattedRequestJObject.Add("onerivet_transactionnumber", property.Value);
                        break;
                    case "TransactionAmount":
                        formattedRequestJObject.Add("onerivet_transactionamount", property.Value);
                        break;
                    case "TransactionDate":
                        string utcDateString = Utility.General.ConvertDateStringInUTCFormat(property.Value.ToString(), "MM-dd-yyyy HH:mm:ss");
                        formattedRequestJObject.Add("onerivet_transactiondate", utcDateString);
                        break;
                    case "CustomerID":
                        formattedRequestJObject.Add("onerivet_contactid@odata.bind", "/contacts(" + property.Value + ")");
                        break;
                    case "MoveID":
                        formattedRequestJObject.Add("onerivet_MoveID@odata.bind", "/jkmoving_moves(" + property.Value + ")");
                        break;
                    case "TotalPaid":
                        formattedRequestJObject.Add("jkmoving_totalpaid", Decimal.Parse(property.Value.ToString()));
                        break;
                }
            }
            return formattedRequestJObject.ToString(Newtonsoft.Json.Formatting.None);
        }

        /// Method Name     : MapAlertDTOToCRM
        /// Author          : Pratik Soni
        /// Creation Date   : 01 Feb 2018
        /// Purpose         : To Map Alert DTO's properties to CRM fields.
        /// Revision        : 
        /// </summary>
        /// <returns> "string" of json formatted requestBody where DTO property will be replaced with respective CRM field name </returns>
        /// 
        public string MapAlertDTOToCRM(string requestBody)
        {
            JObject unformattedRequestJObject;
            JObject formattedRequestJObject = new JObject();

            unformattedRequestJObject = JObject.Parse(requestBody);

            foreach (JProperty property in unformattedRequestJObject.Properties())
            {
                // Each field of request body for Payment DTO should be added as a "case".
                switch (property.Name)
                {
                    case "AlertID":
                        formattedRequestJObject.Add("activityid", property.Value);
                        break;
                    case "CustomerID":
                        formattedRequestJObject.Add("regardingobjectid_contact@odata.bind", "/contacts(" + Convert.ToString(property.Value) + ")");
                        break;
                    case "AlertTitle":
                        formattedRequestJObject.Add("subject", Convert.ToString(property.Value));
                        break;
                    case "AlertDescription":
                        formattedRequestJObject.Add("description", Convert.ToString(property.Value));
                        break;
                    case "StartDate":
                        string utcStartDateString = Utility.General.ConvertDateStringInUTCFormat(property.Value.ToString(), "MM-dd-yyyy HH:mm:ss");
                        formattedRequestJObject.Add("actualstart", utcStartDateString);
                        break;
                    case "EndDate":
                        string utcEndDateString = Utility.General.ConvertDateStringInUTCFormat(property.Value.ToString(), "MM-dd-yyyy HH:mm:ss");
                        formattedRequestJObject.Add("actualend", utcEndDateString);
                        break;
                    case "NotificationType":
                        formattedRequestJObject.Add("onerivet_notificationtype", property.Value.ToString());
                        break;
                    case "IsActive":
                        formattedRequestJObject.Add("statecode", property.Value);
                        break;
                }
            }
            return formattedRequestJObject.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
