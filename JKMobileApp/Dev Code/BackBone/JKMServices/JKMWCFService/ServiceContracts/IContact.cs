using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace JKMWCFService
{
    /// <summary>
    /// Interface Name      : IContact
    /// Author              : Ranjana Singh
    /// Creation Date       : 27 Nov 2017
    /// Purpose             : Gets all information of Contacts. 
    /// Revision            :
    /// </summary>
    /// <returns></returns>
    [ServiceContract]
    public interface IContact
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/{userId}")]
        string GetContactList(string userId);

        [OperationContract]
        [WebInvoke(Method = "GET",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "/{moveId}/move")]
        string GetContactListForMove(string moveId);

        [OperationContract]
        [WebInvoke(Method = "PUT",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         UriTemplate = "/")]
        string PutContactList();

        [OperationContract]
        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "/{userId}")]
        string PostContactList(string userId);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "/")]
        string DeleteContactList();
    }
}
