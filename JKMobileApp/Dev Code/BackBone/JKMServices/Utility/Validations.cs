using System;
using System.Data;
using System.Text.RegularExpressions;

namespace Utility
{
    /// <summary>
    /// Class Name		: Validations
    /// Author          : Pratik SOni
    /// Creation Date   : 12 Dec 2017
    /// Purpose         : Common class to contain all validation methods. 
    /// Revision        : 
    /// </summary>
    public static class Validations
    {
        /// <summary>
        /// Method Name     : IsDataTableValid
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Check datatable is not null & rows > 0
        /// Revision        : By Pratik Soni on 12 Dec 2017 : Moved function from General.cs to Validation.cs file.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static bool IsDataTableValid(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method Name     : ValidateEmail
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 28 Nov 2017
        /// Purpose         : Validate email id as per mail format
        /// Revision        : By Pratik Soni on 12 Dec 2017 : Moved function from General.cs to Validation.cs file.
        /// </summary>
        public static bool ValidateEmail(string emailID)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(emailID);

            return match.Success;
        }

        /// <summary>
        /// Method Name     : IsValid
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Method to check value for null,dbnull or empty and return replaced value if value is invalid
        /// Revision        : By Pratik Soni on 12 Dec 2017 : Moved function from General.cs to Validation.cs file.
        /// </summary>
        public static bool IsValid(object objVal)
        {
            if (objVal == null || objVal == DBNull.Value || string.IsNullOrEmpty(objVal.ToString()))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Method Name     : IsNull
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Method to check value for null,dbnull or emty and return replaced value if value is invalid
        /// Revision        : By Pratik Soni on 12 Dec 2017 : Moved function from General.cs to Validation.cs file.
        /// </summary>
        public static T IsNull<T>(object objVal, object retVal)
        {
            if (objVal == null || objVal == DBNull.Value || string.IsNullOrEmpty(objVal.ToString()))
                return (T)Convert.ChangeType(retVal, typeof(T));
            else
                return (T)Convert.ChangeType(objVal, typeof(T));
        }

        /// <summary>
        /// Method Name     : ValidatePhoneNumber
        /// Author          : Ranjana Singh
        /// Creation Date   : 20 Feb 2018
        /// Purpose         : Method to Validate phone number in US format
        /// Revision        : 
        /// </summary>
        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            Regex phoneRegex = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");

            if (phoneRegex.IsMatch(phoneNumber))
            {
                return true;
            }
            return false;
        }
    }
}
