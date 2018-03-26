using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace JKMPCL
{
    /// <summary>
    /// Class Name      : ClientHelper
    /// Author          : Hiren M Patel
    /// Creation Date   : 18 Dec 2017
    /// Purpose         : For getting HttpClient 
    /// Revision        : 
    /// </summary>
    public class ClientHelper
    {
        /// <summary>
        /// Method Name     : GetAuthenticateClient
        /// Author          : Hiren Patel
        /// Creation Date   : 18 Dec 2017
        /// Purpose         : Gets the authenticate HttpClient.
        /// Revision        : 
        /// </summary>
        /// <returns>The authenticate Httpclient.</returns>
        public HttpClient GetAuthenticateClient()
        {
            string username = Resource.ApiAuthenticationUserName;
            string password = Resource.ApiAuthenticationPassword;
                
            var authValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));

            var client = new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = authValue }
            };

            return client;
        }
    }
}
