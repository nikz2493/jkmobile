using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace JKMServices.DAL.CRM
{
    /// Interface Name      : IUserDetails
    /// Author              : Pratik Soni
    /// Creation Date       : 1 Dec 2017
    /// Purpose             : To perform operations on User entity
    /// Revision            : 
    /// </summary>
    interface IUserDetails
    {
        Dictionary<string, string> GetUserIDAsync(string emailID);
        string GetUserProfileData(string userID);
        string GetUserVerificationData(string userID);

        string PostUserProfileData(string userID);
        string PostUserVerificationData(string userID);

        string PutUserProfileData(string userID);
        string PutUserVerificationData(string userID);
    }
}