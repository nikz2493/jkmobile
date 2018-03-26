using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using Utility;
using Utility.Logger;

namespace JKMServices.DAL.SQL
{
    public class AlertDetails : IAlertDetails
    {
        private readonly SQL.IJKMDBContext jKMDBContext;
        private List<DTO.Alert> dtoAlertList;
        private readonly ILogger logger;
        private static readonly int QUERYFAIL = -1;

        // Constructor
        public AlertDetails(SQL.IJKMDBContext jKMDBContext, ILogger logger)
        {
            this.jKMDBContext = jKMDBContext;
            this.logger = logger;
        }

        /// <summary>
        /// Method Name     : GetAlertList
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : Method to get list of alerts from database
        /// Revision        : 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="startDate"></param>
        /// <returns>List of Alert DTO</returns>
        public List<DTO.Alert> GetAlertList(string customerId, string startDate)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[2];
                sqlParameter[0] = new SqlParameter { ParameterName = "@CustomerID", SqlValue = customerId };

                if (string.IsNullOrEmpty(startDate))
                {
                    sqlParameter[1] = new SqlParameter { ParameterName = "@StartDate", SqlValue = string.Empty };
                }
                else
                {
                    //expected format : mm-dd-yyyy
                    sqlParameter[1] = new SqlParameter { ParameterName = "@StartDate", SqlValue = Convert.ToDateTime(startDate, CultureInfo.CreateSpecificCulture("en-US")) };
                }

                dtoAlertList = jKMDBContext.GetData<DTO.Alert>("getAlertList", sqlParameter);

                return dtoAlertList;
            }
            catch (MissingManifestResourceException ex)
            {
                logger.Error(ex.InnerException.ToString());
                return null;
            }
            catch (Exception ex)
            {
                logger.Error(ex.InnerException.ToString());
                return null;
            }

        }

        /// <summary>
        /// Method Name      : DeleteAlertDetails
        /// Author           : Pratik Soni
        /// Creation Date    : 29 Dec2017
        /// Purpose          : Delete the given list of Alerts in "Alerts" table in SQL.
        /// Revision         :
        /// </summary>
        /// <param name="alertIdList"></param>
        /// <returns></returns>
        public int DeleteAlertDetails(List<DTO.Alert> alertIdList)
        {
            int queryResult;
            Dictionary<string, dynamic> parameterList = new Dictionary<string, dynamic>();
            try
            {
                queryResult = 0;
                foreach (var alertDetail in alertIdList)
                {
                    parameterList.Add("@alertIdList", alertDetail.AlertID.ToString());

                    queryResult = ConnectionManager.ModifyData("deleteAlertList", parameterList);

                    if (queryResult == QUERYFAIL)
                    {
                        return QUERYFAIL;
                    }
                    parameterList.Clear();
                }
                return queryResult;
            }
            catch (Exception ex)
            {
                logger.Error(ex.InnerException.ToString());
                return QUERYFAIL;
            }
        }

        /// <summary>
        /// Method Name      : PostAlertList
        /// Author           : Pratik Soni
        /// Creation Date    : 29 Dec2017
        /// Purpose          : Insert the given list of Alerts in "Alerts" table in SQL.
        /// Revision         :
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="alertDetails"/>
        /// <returns></returns>
        public int PostAlertDetails(string customerID, List<DTO.Alert> alertDetails)
        {
            int queryResult = 0;
            int lastAlertId;
            try
            {
                lastAlertId = RetriveLastAlertId();
                if (lastAlertId == -1)
                {
                    return QUERYFAIL;
                }

                foreach (var dtoAlert in alertDetails)
                {
                    lastAlertId += 1;
                    queryResult = ConnectionManager.ModifyData("insertAlertDetails", ConvertDTOToDictionary(dtoAlert, lastAlertId, customerID, true));

                    if (queryResult == QUERYFAIL)
                    {
                        return QUERYFAIL;
                    }
                }
                return queryResult;
            }
            catch (Exception ex)
            {
                logger.Error(ex.InnerException.ToString());
                return QUERYFAIL;
            }
        }

        /// <summary>
        /// Method Name      : PostAlertList
        /// Author           : Pratik Soni
        /// Creation Date    : 29 Dec 2017
        /// Purpose          : Update the given list of Alerts in "Alerts" table in SQL.
        /// Revision         :
        /// </summary>
        ///<param name="alertDetails"/>
        /// <returns></returns>
        public int PutAlertDetails(List<DTO.Alert> alertDetails)
        {
            Dictionary<string, dynamic> dictionaryObject;
            string updateValues;
            int queryResult = 0;
            try
            {
                dictionaryObject = new Dictionary<string, dynamic>();
                foreach (var dtoAlert in alertDetails)
                {
                    updateValues = Utility.General.DictionaryToCommaSeparatedString(ConvertDTOToDictionary(dtoAlert));
                    dictionaryObject.Add("UpdateValues", updateValues);
                    dictionaryObject.Add("WhereConditionValue", dtoAlert.AlertID.ToString());
                    queryResult = ConnectionManager.ModifyData("updateAlertData", dictionaryObject, true);

                    if (queryResult == QUERYFAIL)
                    {
                        return QUERYFAIL;
                    }
                    dictionaryObject.Clear();
                }
                return queryResult;
            }
            catch (Exception ex)
            {
                logger.Error(ex.InnerException.ToString());
                return QUERYFAIL;
            }
        }

        /// <summary>
        /// Method Name      : ConvertDTOToDictionary
        /// Author           : Pratik Soni
        /// Creation Date    : 02 Jan 2018
        /// Purpose          : Update the given list of Alerts in "Alerts" table in SQL.
        /// Revision         :
        /// </summary>
        /// <param name="alertID"/>
        /// <param name="alertTable"/>
        /// <param name="customerID"/>
        /// <param name="prepareForInsert"/> -- if there parameter is set to true only then alertID and customerID params will get used.
        /// <returns></returns>
        private Dictionary<string, dynamic> ConvertDTOToDictionary(DTO.Alert alertTable, int alertID = -1, string customerID = "", bool prepareForInsert = false)
        {
            Dictionary<string, dynamic> alertDetails = new Dictionary<string, dynamic>();
            if (prepareForInsert)
            {
                alertDetails.Add("AlertID", alertID);
                alertDetails.Add("CustomerID", customerID);
            }
            else
            {
                alertDetails.Add("AlertID", alertTable.AlertID.ToString());
                alertDetails.Add("CustomerID", alertTable.CustomerID.ToString());
            }
            alertDetails.Add("AlertTitle", alertTable.AlertTitle.ToString());
            alertDetails.Add("AlertDescription", (!string.IsNullOrEmpty(alertTable.AlertDescription) ? alertTable.AlertDescription : string.Empty));
            alertDetails.Add("StartDate", General.GetSQLDateFormat(alertTable.StartDate.ToString()));
            alertDetails.Add("EndDate", General.GetSQLDateFormat(alertTable.EndDate.ToString()));
            alertDetails.Add("NotificationType", alertTable.NotificationType.ToString());
            alertDetails.Add("IsActive", alertTable.IsActive.ToString());

            return alertDetails;
        }

        /// <summary>
        /// Method Name      : GetLastAlertID
        /// Author           : Pratik Soni
        /// Creation Date    : 02 Jan 2018
        /// Purpose          : To get the ID of last inserted record.
        /// Revision         :
        /// </summary>
        private DTO.Alert GetLastAlertID()
        {
            try
            {
                List<DTO.Alert> alert = new List<DTO.Alert>();
                object[] param = new object[]
                   {
                        null
                   };
                alert = jKMDBContext.GetData<DTO.Alert>("getLastPrimaryKeyField", param);
                if (alert.FirstOrDefault() == null)
                {
                    return new DTO.Alert { AlertID = "" };
                }
                return alert.FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.Error("Error occured while fetching data from SQL", ex);
                return null;
            }
        }

        /// <summary>
        /// Method Name      : RetriveLastAlertId
        /// Author           : Pratik Soni
        /// Creation Date    : 02 Jan 2018
        /// Purpose          : To get the ID of last inserted record.
        /// Revision         :
        /// </summary>
        /// <returns></returns>
        private int RetriveLastAlertId()
        {
            DTO.Alert alert = new DTO.Alert();
            alert = GetLastAlertID();
            if (alert == null)
            {
                logger.Error("Unable to get Last alert ID");
                return -1;
            }
            return 0;// modified to return 0 if AlertID not found or NULL found
        }
    }
}