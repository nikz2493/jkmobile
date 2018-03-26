using System;
using JKMPCL.Model;

namespace JKMPCL.Services
{
    /// <summary>
    /// Class Name      : MyAccountValidateService.
    /// Author          : Hiren Patel
    /// Creation Date   : 5 Dec 2017
    /// Purpose         : Validate my account model model. 
    /// Revision        : 
    /// </summary> 
    public class MyAccountValidateService
    {
        /// <summary>
        /// Method Name     : ValidatePrivacyPolicyModel
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Feb 2018
        /// Purpose         : Validates the privacy policy model.
        /// Revision        : 
        /// </summary>

        public string ValidatePrivacyPolicyModel(PrivacyPolicyModel privacyPolicyModel)
        {
            string errorMessage = string.Empty;

            if (UtilityPCL.IsNullOrEmptyOrWhiteSpace(privacyPolicyModel.Phone))
            {
                errorMessage = Resource.msgPhoneNumberIsRequired;
            }
            else if(privacyPolicyModel.Phone.Length<10)
            {
                errorMessage = Resource.msgPleaseEnterValidPhoneNo;
            }

            return errorMessage;
        }
    }
}
