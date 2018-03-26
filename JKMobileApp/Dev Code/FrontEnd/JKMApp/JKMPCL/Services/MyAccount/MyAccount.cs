using JKMPCL.Model;
using System;
using System.Threading.Tasks;

namespace JKMPCL.Services
{
    /// <summary>
    /// Class Name      : MyAccount
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 20 Feb 2018
    /// Purpose         : to update my account profile data
    /// Revision        : 
    /// </summary>
    public class MyAccount
    {
        private readonly LoginAPIServies loginAPIServies;

        /// <summary>
        /// Construtor
        /// </summary>
        public MyAccount()
        {
            loginAPIServies = new LoginAPIServies();
        }

        /// <summary>
        /// Method Name     : PutMyAccountDetails
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 20 Feb 2018
        /// Purpose         : Update acount details like phone & preffered contact
        /// Revision        : 
        /// </summary>
        /// <param name="privacyPolicyModel"></param>
        /// <returns></returns>
        public async Task<APIResponse<PrivacyPolicyModel>> PutMyAccountDetails(PrivacyPolicyModel privacyPolicyModel)
        {
            APIResponse<PrivacyPolicyModel> apiResponse = new APIResponse<PrivacyPolicyModel> { STATUS = false };

            try
            {
                var response = await loginAPIServies.PutCustomerProfileData(privacyPolicyModel);

                if (response.STATUS)
                {
                    apiResponse.STATUS = true;
                    apiResponse.Message = Resource.msgAccountSuccessfullyUpdate;
                    UtilityPCL.LoginCustomerData.Phone = privacyPolicyModel.Phone;
                    UtilityPCL.LoginCustomerData.PreferredContact= privacyPolicyModel.PreferredContact;
                    UtilityPCL.LoginCustomerData.ReceiveNotifications= privacyPolicyModel.ReceiveNotifications;
                }
                else
                {
                    apiResponse.Message = response.Message;
                }

                return apiResponse;
            }
            catch
            {
                apiResponse.Message = Resource.msgDefaultServieMessage;
                return apiResponse;
            }

        }
    }
}
