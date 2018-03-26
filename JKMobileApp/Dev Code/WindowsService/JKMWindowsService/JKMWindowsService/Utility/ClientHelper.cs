using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace JKMWindowsService.Utility
{
    /// <summary>
    /// Class Name      : ClientHelper
    /// Author          : Hiren M Patel
    /// Creation Date   : 18 Dec 2017
    /// Purpose         : For getting HttpClient 
    /// Revision        : 
    /// </summary>
    public class ClientHelper : IClientHelper
    {
        
        /// <summary>
        /// Method Name     : GetAuthenticateClient
        /// Author          : Pratik Soni
        /// Creation Date   : 14 Feb 2018
        /// Purpose         : Gets the authenticate HttpClient.
        /// Revision        : 
        /// </summary>
        /// <returns>The authenticate Httpclient.</returns>
        public HttpClient GetAuthenticateClient()
        {
            string username = "admin";
            string password = "admin";
                
            var authValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));

            var client = new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = authValue }
            };

            return client;
        }
    }
}
