using System;
using System.Configuration;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace JKMWCFService
{
    /// <summary>
    /// Class Name          : RestAuthorizationManager
    /// Author              : Pratik Soni
    /// Creation Date       : 15 Dec 2017
    /// Purpose             : to check authorization for wcf servcie request
    /// Revision            :
    /// </summary>
    public class RestAuthorizationManager : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            //Extract the Authorization header, and parse out the credentials converting the Base64 string:
            var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
            if ((authHeader != null) && (authHeader != string.Empty))
            {
                var svcCredentials = System.Text.ASCIIEncoding.ASCII
                        .GetString(Convert.FromBase64String(authHeader.Substring(6)))
                        .Split(':');
                var user = new { Name = svcCredentials[0], Password = svcCredentials[1] };
                if ((user.Name == ConfigurationManager.AppSettings["ServiceAuthUsername"] && user.Password == ConfigurationManager.AppSettings["ServiceAuthPassword"]))
                {
                    //User is authorized and originating call will proceed
                    return true;
                }
                else
                {
                    //not authorized
                    return false;
                }
            }
            else
            {
                //No authorization header was provided, so challenge the client to provide before proceeding:
                WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"J K Moving Services\"");
                //Throw an exception with the associated HTTP status code equivalent to HTTP status 401
                throw new WebFaultException(HttpStatusCode.Unauthorized);
            }
        }
    }
}
