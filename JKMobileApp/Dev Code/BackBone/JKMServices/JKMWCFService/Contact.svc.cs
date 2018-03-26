using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace JKMWCFService
{
    /// <summary>
    /// Class Name      : Contact
    /// Author          : Ranjana Singh
    /// Creation Date   : 27 Nov 2017
    /// Purpose         : Contains all information of contacts and alerts. 
    /// Revision        :
    /// </summary>
    /// <returns></returns>
    public class Contact : IContact
    {
        /// <summary>
        /// Method Name     : GetContactList
        /// Author          : Ranjana Singh
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Gets the list of contacts for the specified user ID. 
        /// Revision        :
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetContactList(string userId)
        {
            return userId;
        }

        /// <summary>
        /// Method Name     : GetContactListForMove
        /// Author          : Ranjana Singh
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Gets the list of contacts for the specified move ID. 
        ///                   This is the default use of Contact service, as it returns the move team. 
        /// Revision        :
        /// </summary>
        /// <param name="moveId"></param>
        /// <returns></returns>
        public string GetContactListForMove(string moveId)
        {
            return moveId;
        }

        /// <summary>
        /// Method Name     : DeleteContactList
        /// Author          : Ranjana Singh
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Gets the contact data for the user id.
        /// Revision        :
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string DeleteContactList()
        {
            return null;
        }

        /// <summary>
        /// Method Name     : PutContactList
        /// Author          : Ranjana Singh
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Deletes all the contacts in the contact list sent in request body. 
        /// Revision        :
        /// </summary>
        /// <returns></returns>
        public string PutContactList()
        {
            return null;
        }

        /// <summary>
        /// Method Name     : PostContactList
        /// Author          : Ranjana Singh
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Creates new contacts for the user ID specified.
        /// Revision        :
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string PostContactList(string userId)
        {
            return userId;
        }
    }
}
