using JKMServices.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Resources;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace Utility
{
    /// <summary>
    /// Class Name      : General
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 27 Nov 2017
    /// Purpose         : Common class to access common methods required to use in all projects
    /// Revision        : 
    /// </summary>
    public static class General
    {
        /// <summary>
        /// Method Name     : GetConfigValue
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : get value from config file base on key
        /// Revision        : 
        /// </summary>
        public static string GetConfigValue(string key)
        {
            if (!string.IsNullOrEmpty(key))
                return System.Configuration.ConfigurationManager.AppSettings[key].ToString();
            else
                return null;
        }

        /// <summary>
        /// Method Name     : GetDataTable
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Fetch data using query & sql connection and return data in datatable
        /// Revision        : 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static DataTable GetDataTable(string query, SqlConnection sqlConnection)
        {
            DataTable table;
            SqlDataAdapter sqlDataAdapter;

            sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
            table = new DataTable();
            sqlDataAdapter.Fill(table);
            return table;
        }

        /// <summary>
        /// Method Name     : ConvertToJson
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 28 Nov 2017
        /// Purpose         : Convert to json object from any type of object
        /// Revision        : 
        /// </summary>
        public static dynamic ConvertToJObject(string jsonValue)
        {
            return JObject.Parse(jsonValue);
        }
        public static string ConvertToJson<T>(T val)
        {
            return JsonConvert.SerializeObject(val);
        }
        /// <summary>
        /// Method Name     : ConvertFromJson
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 28 Nov 2017
        /// Purpose         : Convert to any type of object from json
        /// Revision        : 
        /// </summary>
        public static T ConvertFromJson<T>(string jsonValue)
        {
            return (T)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonValue, typeof(T));
        }

        /// <summary>
        /// Method Name     : SetCustomHttpStatusCode
        /// Author          : Pratik Soni
        /// Creation Date   : 07 Dec 2017
        /// Purpose         : Set custom status for the service response
        /// Revision        : 
        /// </summary>
        public static WebOperationContext SetCustomHttpStatusCode(JObject output)
        {
            ResourceManager resourceManagerObject;
            resourceManagerObject = new ResourceManager("Utility.Resource", System.Reflection.Assembly.GetExecutingAssembly());
            WebOperationContext customContext = null;
            if (!string.IsNullOrEmpty(output.ToString()))
            {
                if (!string.IsNullOrEmpty(output["Information"].ToString()))
                {
                    customContext = SetHttpStatusCode(HttpStatusCode.NoContent.ToString(), resourceManagerObject.GetString("msgNoContent"));
                }
                else if (!string.IsNullOrEmpty(output["Message"].ToString()))
                {
                    customContext = SetHttpStatusCode(HttpStatusCode.ServiceUnavailable.ToString(), resourceManagerObject.GetString("msgServiceUnavailable"));
                }
                else if (!string.IsNullOrEmpty(output["BadRequest"].ToString()))
                {
                    customContext = SetHttpStatusCode(HttpStatusCode.BadRequest.ToString(), resourceManagerObject.GetString("msgBadRequest"));
                }
            }
            return customContext;
        }

        /// <summary>
        /// Method Name     : SetHttpStatusCode
        /// Author          : Pratik Soni
        /// Creation Date   : 07 Dec 2017
        /// Purpose         : To get custom value based on status code 
        /// Revision        : 
        /// </summary>
        private static WebOperationContext SetHttpStatusCode(string httpStatusCode, string customResponseMessage)
        {
            WebOperationContext webOperationContext = WebOperationContext.Current;
            switch (httpStatusCode)
            {
                case "NoContent":
                    webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.NoContent;
                    break;
                case "Unauthorized":
                case "ServiceUnavailable":
                case "InternalServerError":
                    webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                    break;
                case "BadRequest":
                    webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.BadRequest;
                    break;
                default:
                    webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                    break;
            }
            webOperationContext.OutgoingResponse.StatusDescription = customResponseMessage;
            return webOperationContext;
        }

        /// <summary>
        /// Method Name     : DictionaryToCommaSeparatedString
        /// Author          : Pratik Soni
        /// Creation Date   : 12 Dec 2017
        /// Purpose         : To convert Dictionary(Key-Value) pair to a single comma separated string
        /// Revision        : 
        /// </summary>

        public static string DictionaryToCommaSeparatedString(Dictionary<string, dynamic> dictionaryObject)
        {
            var builder = dictionaryObject.Aggregate(new StringBuilder(), (sb, kvp) => sb.AppendFormat("{0} = '{1}', ", kvp.Key, kvp.Value));
            if (builder.Length > 0)
                builder.Remove(builder.Length - 2, 2);
            return builder.ToString();
        }

        /// <summary>
        /// Method Name     : ConfigureJObjectMessage
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 06 DEC 2017
        /// Purpose         : Common method to generate JObject
        /// Revision        : 
        /// </summary>
        public static JObject ConfigureJObjectMessage(string messageType, dynamic strMessage)
        {
            JObject returnValue = new JObject
            {
                [messageType] = strMessage
            };
            return returnValue;
        }

        /// <summary>
        /// Method Name     : ConvertDataTableToJObject
        /// Author          : Pratik Soni
        /// Creation Date   : 06 DEC 2017
        /// Purpose         : Common method to Convert datatable (with single row) to JObject
        /// Revision        : 
        /// </summary>
        public static JObject ConvertDataTableToJObject(DataTable customerDatatable)
        {
            try
            {
                string jsonResult = JsonConvert.SerializeObject(customerDatatable, Formatting.None);
                return JObject.Parse(jsonResult.Replace("[", "").Replace("]", ""));
            }
            catch
            {
                return new JObject();
            }
        }

        /// <summary>
        /// Method Name     : GetSQLDateFormat
        /// Author          : Pratik Soni
        /// Creation Date   : 29 DEC 2017
        /// Purpose         : Common method to Convert System date format to SQL date format
        /// Revision        : 
        /// </summary>
        public static string GetSQLDateFormat(string date)
        {
            DateTime dateVal = DateTime.ParseExact(date, CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern, CultureInfo.InvariantCulture);
            return dateVal.ToString("yyyy-MM-dd");
        }

        private static readonly StandardKernel standardKernel = new StandardKernel();

        public static StandardKernel StandardKernel()
        {
            return standardKernel;
        }
        public static T Resolve<T>(params Ninject.Parameters.IParameter[] param)
        {
            return standardKernel.Get<T>(param);
        }
        public static T Resolve<T>(Type type, params Ninject.Parameters.IParameter[] param)
        {
            return (T)standardKernel.Get(type, param);
        }

        /// <summary>
        /// Method Name     : GetBase64StringForImage
        /// Author          : Ranjana Singh
        /// Creation Date   : 05 Jan 2018
        /// Purpose         : Convert image into base64string 
        /// Revision        : 
        /// </summary>
        /// <param name="imgPath">Image Path</param>
        /// <returns>Base64string</returns>
        public static string GetBase64StringForImage(string imgPath)
        {
            string applicationPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            string path = Path.Combine(applicationPath, imgPath);
            byte[] imageBytes = System.IO.File.ReadAllBytes(path);
            string base64String = "data:image/png;base64," + Convert.ToBase64String(imageBytes);
            return base64String;
        }

        /// <summary>
        /// Method Name     : GenerateBadRequestMessage
        /// Author          : Pratik Soni
        /// Creation Date   : 12 Jan 2018
        /// Purpose         : Returns the service response of BAD REQUEST.
        /// Revision        :
        /// </summary>
        /// <returns></returns>
        public static string GenerateBadRequestMessage<T>()
        {
            ServiceResponse<T> serviceResponse;
            serviceResponse = new ServiceResponse<T>() { BadRequest = "BAD REQUEST." };

            return General.ConvertToJson<ServiceResponse<T>>(serviceResponse);
        }

        /// Method Name     : GetSpecificAttributeFromResponse
        /// Author          : Pratik Soni
        /// Creation Date   : 12 Jan 2018
        /// Purpose         : To fetch specific field from the "value" node of CRM response on 0th index
        /// Revision        : 
        /// </summary>
        public static string GetSpecificAttributeFromCRMResponse(Dictionary<string, string> crmResponse, string requiredField)
        {
            string returnValue;
            JObject jsonObject = Utility.General.ConvertToJObject(crmResponse["CONTENT"].ToString());
            if (jsonObject["value"] == null || ((Newtonsoft.Json.Linq.JContainer)jsonObject["value"]).Count == 0)
            {
                return string.Empty;
            }
            JObject valueObject = JObject.Parse(jsonObject["value"][0].ToString(Newtonsoft.Json.Formatting.None));
            returnValue = valueObject[requiredField].ToString();
            return returnValue;
        }

        /// Method Name     : GetDateStringInUTCFormat
        /// Author          : Pratik Soni
        /// Creation Date   : 12 Jan 2018
        /// Purpose         : To convert the US date format string in UTC format
        /// Revision        : 
        /// </summary>
        public static string ConvertDateStringInUTCFormat(string dateString, string dateStringFormat)
        {
            DateTime dateObject = DateTime.ParseExact(dateString, dateStringFormat, CultureInfo.CurrentCulture, DateTimeStyles.None);
            return dateObject.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
        }

        /// Method Name     : GetCurrentDateInUSDateTimeFormat
        /// Author          : Pratik Soni
        /// Creation Date   : 24 Jan 2018
        /// Purpose         : To get current date in UTC format
        /// Revision        : 
        /// </summary>
        public static DateTime GetCurrentDateInUSDateTimeFormat()
        {
            return DateTime.ParseExact(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }
    }
}
