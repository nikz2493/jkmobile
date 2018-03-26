using JKMPCL.Model;
using System;
using System.Threading.Tasks;

namespace JKMPCL.Services
{
    /// <summary>
    /// Class Name      : Login
    /// Author          : Sanket prajapati
    /// Creation Date   : 05 Dec 2017
    /// Purpose         : For Customer Login & Password 
    /// Revision        : 
    /// </summary>
    public class Login
    {
        private readonly LoginAPIServies loginAPIServies;
        private readonly LoginValidateServices loginValidateServices;

        //Constructor
        public Login()
        {
            loginAPIServies = new LoginAPIServies();
            loginValidateServices = new LoginValidateServices();
        }

        /// <summary>
        /// Method Name     : GetPassword
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : For Customer Login  
        /// Revision        : 
        /// </summary>
      

        public async Task<ServiceResponse> GetCustomerLogin(string strEmail)
        {
            ServiceResponse serviceResponse = new ServiceResponse { Status = false };
            EmailModel emailModel;

            string errorMessage = string.Empty;
            emailModel = new EmailModel { EmailID = strEmail };
            errorMessage = loginValidateServices.ValidateEmailModel(emailModel);
            serviceResponse.Message = errorMessage;
            if (string.IsNullOrEmpty(errorMessage))
            {
                serviceResponse = await GetCustomerIDAndVerificationData(emailModel);
            }
            return serviceResponse;
        }

        /// <summary>
        /// Method Name     : GetCustomerIDAndVerificationData
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Check & Verify customer id 
        /// Revision        : 
        /// </summary>
        /// <param name="emailModel"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> GetCustomerIDAndVerificationData(EmailModel emailModel)
        {
            string errorMessage = string.Empty;
            bool isRegistered = false;

            APIResponse<CustomerModel> response = await loginAPIServies.GetCustomerID(emailModel);
            if (response.STATUS)
            {
                CheckCustomerRegistered(response);

                if (Convert.ToBoolean(response.DATA.IsCustomerRegistered))
                {
                    isRegistered = true;
                }
                else
                {
                    errorMessage = await GetVerificationData();
                    if (string.IsNullOrEmpty(errorMessage))
                    {
                        isRegistered = false;
                    }
                }
            }
            else
            {
                errorMessage = response.Message;
            }

            return getServiceResponse(errorMessage, isRegistered);
        }

        /// <summary>
        /// Method Name     : getServiceResponse
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : sub method to set service response 
        /// Revision        : 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private ServiceResponse getServiceResponse(string message, bool status)
        {
            ServiceResponse serviceResponse;
            serviceResponse = new ServiceResponse
            {
                Message = message,
                Status = status
            };

            return serviceResponse;
        }

        /// <summary>
        /// Method Name     : CheckCustomerRegistered
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : sub method to check & set login customer id & is_registered flag
        /// Revision        : 
        /// </summary>
        /// <param name="response"></param>
        private void CheckCustomerRegistered(APIResponse<CustomerModel> response)
        {
            if (UtilityPCL.LoginCustomerData is null)
            {
                UtilityPCL.LoginCustomerData = new CustomerModel();
            }

            UtilityPCL.LoginCustomerData.CustomerId = response.DATA.CustomerId;
            UtilityPCL.LoginCustomerData.IsCustomerRegistered = response.DATA.IsCustomerRegistered;
        }

        /// <summary>
        /// Method Name     : GetVerificationData
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 22 Jan 2018
        /// Purpose         : get customer verification details
        /// Revision        : 
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetVerificationData()
        {
            APIResponse<CustomerModel> response = await loginAPIServies.GetCustomerVerificationData(UtilityPCL.LoginCustomerData);
            if (!response.STATUS)
            {
                return response.Message;
            }

            return string.Empty;
        }

        /// <summary>
        /// Method Name     : GetPassword
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : For password verification   
        /// Revision        : By Vivek Bhavsar on 23 Jan 2018 for code refactoring add sub methods
        /// </summary>
        public async Task<ServiceResponse> GetPassword(string passsword)
        {
            string errorMessage = string.Empty;
            bool isTermAgree = false;

            PasswordModel passwordModel = GetPasswordModel(passsword);
            errorMessage = loginValidateServices.ValidatePasswordModel(passwordModel);

            if (string.IsNullOrEmpty(errorMessage))
            {
                APIResponse<CustomerModel> response = await CheckPasswordAsync(passwordModel);
                errorMessage = response.Message;
                if (response.STATUS)
                {
                    isTermAgree = Convert.ToBoolean(response.DATA.TermsAgreed);
                    errorMessage = string.Empty;
                }
            }

            return getServiceResponse(errorMessage, isTermAgree);
        }

        /// <summary>
        /// Method Name     : CheckPassword
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : validate password from service.
        /// Revision        : 
        /// </summary>
        /// <param name="passwordModel"></param>
        /// <returns></returns>
        private async Task<APIResponse<CustomerModel>> CheckPasswordAsync(PasswordModel passwordModel)
        {
            APIResponse<CustomerModel> apiResponse = await loginAPIServies.GetCustomerProfileData(UtilityPCL.LoginCustomerData);
            if (apiResponse.STATUS)
            {
                UtilityPCL.LoginCustomerData = apiResponse.DATA;
                UtilityPCL.LoginCustomerData.CustomerId = apiResponse.DATA.CustomerId;

                apiResponse = loginAPIServies.CheckPassword(passwordModel, UtilityPCL.LoginCustomerData);
                if (!apiResponse.STATUS)
                {
                    apiResponse.Message = Resource.msgCorrectPassword;
                }
            }

            return apiResponse;
        }

        /// <summary>
        /// Method Name     : GetPasswordModel
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Generate Password Model
        /// Revision        :
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private PasswordModel GetPasswordModel(string password)
        {
            return new PasswordModel
            {
                CustomerId = UtilityPCL.LoginCustomerData.CustomerId,
                CustomerPasssword = password
            };
        }

        /// <summary>
        /// Method Name     : GetVerifyCode
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : For use Verify code
        /// Revision        : 
        /// </summary>
        public ServiceResponse GetVerifyCode(int? strVerifyCode)
        {
            string errorMessage = string.Empty;
            bool isVerified = false;

            VerificationCodeModel verificationCodeModel = new VerificationCodeModel { VerificationCode = strVerifyCode };
            errorMessage = loginValidateServices.ValidateVerificationCodeModel(verificationCodeModel);

            if (string.IsNullOrEmpty(errorMessage))
            {
                APIResponse<CustomerModel> response = loginAPIServies.CheckVerificationCode(verificationCodeModel, UtilityPCL.LoginCustomerData);
                errorMessage = response.Message;

                if (response.STATUS)
                {
                    isVerified = true;
                    errorMessage = string.Empty;
                }
            }

            return getServiceResponse(errorMessage, isVerified);
        }

        /// <summary>
        /// Method Name     : CreatePassword
        /// Author          : Sanket Prajapati
        /// Creation Date   : 5 Dec 2017
        /// Purpose         : For Save Customer Password
        /// Revision        : 
        /// </summary>
        public async Task<ServiceResponse> CreatePassword(string StrCreatePassword, string StrVerifyPassword)
        {
            string errorMessage = string.Empty;
            APIResponse<CustomerModel> response;
            bool isTermAgree = false;

            CreatePasswordModel createPasswordModel = new CreatePasswordModel { CreatePassword = StrCreatePassword.Trim(), VerifyPassword = StrVerifyPassword.Trim() };

            errorMessage = loginValidateServices.ValidateCreatePasswordModel(createPasswordModel);
            if (string.IsNullOrEmpty(errorMessage))
            {
                response = await PutCustomerVerificationDataAsync(createPasswordModel);
                errorMessage = response.Message;

                if (response.STATUS)
                {
                    await GetCustomerProfileData(UtilityPCL.LoginCustomerData);
                    isTermAgree = Convert.ToBoolean(UtilityPCL.LoginCustomerData.TermsAgreed);
                    errorMessage = string.Empty;
                }
            }

            return getServiceResponse(errorMessage, isTermAgree);
        }

        /// <summary>
        /// Method Name     : PutCustomerVerificationDataAsync
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Sub method created(code refactoring) to update verification data 
        /// Revision        : 
        /// </summary>
        /// <param name="createPasswordModel"></param>
        /// <returns></returns>
        private async Task<APIResponse<CustomerModel>> PutCustomerVerificationDataAsync(CreatePasswordModel createPasswordModel)
        {
            APIResponse<CustomerModel> response;

            PutCreatePasswordModel putCreatePasswordModel = new PutCreatePasswordModel();
            putCreatePasswordModel.PasswordHash = createPasswordModel.CreatePassword;
            putCreatePasswordModel.PasswordSalt = createPasswordModel.CreatePassword;
            putCreatePasswordModel.CustomerId = UtilityPCL.LoginCustomerData.CustomerId;
            response = await loginAPIServies.PutCustomerVerificationData(putCreatePasswordModel);

            return response;
        }

        /// <summary>
        /// Method Name     : PrivacyPolicyService
        /// Author          : Sanket Prajapati
        /// Creation Date   : 5 Dec 2017
        /// Purpose         : For Save Customer Password
        /// Revision        : 
        /// </summary>
        public async Task<ServiceResponse> PrivacyPolicyService(bool isAgree)
        {
            string errorMessage = string.Empty;
            bool isTermAgree = false;

            PrivacyPolicyModel privacyPolicyModel = GetPrivacyPolicyModel(isAgree);
            APIResponse<CustomerModel> response = await loginAPIServies.PutCustomerProfileData(privacyPolicyModel);
            if (response.STATUS)
            {
                isTermAgree = isAgree;
            }
            else
            {
                isTermAgree = false;
                errorMessage = response.Message;
            }
            UtilityPCL.LoginCustomerData.TermsAgreed = isTermAgree;

            return getServiceResponse(errorMessage, isTermAgree);
        }

        /// <summary>
        /// Method Name     : GetPrivacyPolicyModel
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : generate privacy policy model
        /// Revision        :  
        /// </summary>
        /// <param name="isAgree"></param>
        /// <returns></returns>
        private PrivacyPolicyModel GetPrivacyPolicyModel(bool isAgree)
        {
            PrivacyPolicyModel privacyPolicyModel;
            privacyPolicyModel = new PrivacyPolicyModel();
            privacyPolicyModel.CustomerId = UtilityPCL.LoginCustomerData.CustomerId;
            privacyPolicyModel.TermsAgreed = isAgree;

            return privacyPolicyModel;
        }

        /// <summary>
        /// Method Name     : GetPassword
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : For Customer Login  
        /// Revision        : 
        /// </summary>
        public async Task<APIResponse<CustomerModel>> GetCustomerProfileData(CustomerModel customerModel)
        {
            APIResponse<CustomerModel> response = await loginAPIServies.GetCustomerProfileData(customerModel);
            if (response.STATUS)
            {
                UtilityPCL.LoginCustomerData.CustomerId = response.DATA.CustomerId;
            }
            return response;
        }
    }
}
