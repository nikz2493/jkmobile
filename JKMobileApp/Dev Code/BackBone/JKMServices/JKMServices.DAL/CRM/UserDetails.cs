using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using JKMServices.DAL.CRM.common;

namespace JKMServices.DAL.CRM
{
    public class UserDetails : IUserDetails
    {
        /// Method Name     : GetUserID
        /// Author          : Pratik Soni
        /// Creation Date   : 1 Dec 2017
        /// Purpose         : To get UserID from the given emailID
        /// Revision        : 
        /// </summary>
        public Dictionary<string, string> GetUserIDAsync(string emailID)
        {
            CRMUtilities crmUtilities = new CRMUtilities();
            return crmUtilities.ExecuteGetRequest("contacts ", "", emailID);
        }

        /// Method Name     : GetUserProfileData
        /// Author          : Pratik Soni
        /// Creation Date   : 1 Dec 2017
        /// Purpose         : To get User Profile data from the given UserID
        /// Revision        : 
        /// </summary>
        public string GetUserProfileData(string userID)
        {
            throw new NotImplementedException();
        }

        /// Method Name     : GetUserVerificationData
        /// Author          : Pratik Soni
        /// Creation Date   : 1 Dec 2017
        /// Purpose         : To get User Verification data from the given UserID
        /// Revision        : 
        /// </summary>
        public string GetUserVerificationData(string userID)
        {
            throw new NotImplementedException();
        }

        /// Method Name     : PostUserProfileData
        /// Author          : Pratik Soni
        /// Creation Date   : 1 Dec 2017
        /// Purpose         : To insert new user profile data for the given UserID
        /// Revision        : 
        /// </summary>
        public string PostUserProfileData(string userID)
        {
            throw new NotImplementedException();
        }

        /// Method Name     : PostUserVerificationData
        /// Author          : Pratik Soni
        /// Creation Date   : 1 Dec 2017
        /// Purpose         : To insert new user verification data for the given UserID
        /// Revision        : 
        /// </summary>
        public string PostUserVerificationData(string userID)
        {
            throw new NotImplementedException();
        }

        /// Method Name     : PutUserProfileData
        /// Author          : Pratik Soni
        /// Creation Date   : 1 Dec 2017
        /// Purpose         : To update exisiting user profile data for the given UserID
        /// Revision        : 
        /// </summary>
        public string PutUserProfileData(string userID)
        {
            throw new NotImplementedException();
        }

        /// Method Name     : PutUserVerificationData
        /// Author          : Pratik Soni
        /// Creation Date   : 1 Dec 2017
        /// Purpose         : To update exisiting user verification data for the given UserID
        /// Revision        : 
        /// </summary>
        public string PutUserVerificationData(string userID)
        {
            throw new NotImplementedException();
        }
    }
}
