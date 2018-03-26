using JKMPCL.Model;

namespace JKMPCL.Services
{
    /// <summary>
    /// Class Name      : Login validate services.
    /// Author          : Hiren Patel
    /// Creation Date   : 5 Dec 2017
    /// Purpose         : Validate model. 
    /// Revision        : 
    /// </summary> 
    public class LoginValidateServices
    {
        /// <summary>
        /// Method Name     : ValidateEmailModel
        /// Author          : Hiren Patel
        /// Creation Date   : 5 Dec 2017
        /// Purpose         : Validate email model. 
        /// Revision        : 
        /// </summary>
        /// <param name="emailModel"></param>
        /// <returns></returns>
        public string ValidateEmailModel(EmailModel emailModel)
        {
            string errorMessage = string.Empty;

            if (string.IsNullOrEmpty(emailModel.EmailID))
            {
                errorMessage = string.Format(Resource.msgFieldRequired, Resource.msgRequiredEmailId);
            }
            else if (!UtilityPCL.ValidateEmail(emailModel.EmailID)) // IsValid Email ID    
            {
                errorMessage = Resource.msgInvalidEmail;
            }

            return errorMessage;
        }

        /// <summary>
        /// Method Name     : ValidatePasswordModel
        /// Author          : Hiren Patel
        /// Creation Date   : 5 Dec 2017
        /// Purpose         : Validate password model. 
        /// Revision        : 
        /// </summary>
        /// <param name="passwordModel"></param>
        /// <returns></returns>
        public string ValidatePasswordModel(PasswordModel passwordModel)
        {
            string errorMessage = string.Empty;
            if (string.IsNullOrEmpty(passwordModel.CustomerPasssword))
            {
                errorMessage = string.Format(Resource.msgFieldRequired, Resource.msgRequiredPassword);
            }
            return errorMessage;
        }

        /// <summary>
        /// Method Name     : ValidateCreatePasswordModel
        /// Author          : Hiren Patel
        /// Creation Date   : 5 Dec 2017
        /// Purpose         : Validate Create Password model. 
        /// Revision        : 
        /// </summary>
        /// <param name="createPasswordModel"></param>
        /// <returns></returns>
        public string ValidateCreatePasswordModel(CreatePasswordModel createPasswordModel)
        {
            string errorMessage = string.Empty;
            if (string.IsNullOrEmpty(createPasswordModel.CreatePassword))
            {
                errorMessage = string.Format(Resource.msgFieldRequired, Resource.msgRequiredCreatePassword);
            }
            else if (string.IsNullOrEmpty(createPasswordModel.VerifyPassword))
            {
                errorMessage = string.Format(Resource.msgFieldRequired, Resource.msgRequiredConfirmPassword);
            }
            else if (createPasswordModel.VerifyPassword != createPasswordModel.CreatePassword)
            {
                errorMessage = Resource.msgPasswordMismatch;
            }
            return errorMessage;
        }

        /// <summary>
        /// Method Name     : ValidateVerificationCodeModel
        /// Author          : Hiren Patel
        /// Creation Date   : 5 Dec 2017
        /// Purpose         : Validate verification code model. 
        /// Revision        : 
        /// </summary>
        /// <param name="verificationCodeModel"></param>
        /// <returns></returns>
        public string ValidateVerificationCodeModel(VerificationCodeModel verificationCodeModel)
        {
            string errorMessage = string.Empty;
            if (verificationCodeModel.VerificationCode is null)
            {
                errorMessage = Resource.msgRequiredVerificationCode;
            }
            else if (verificationCodeModel.VerificationCode.ToString() == "0")
            {
                errorMessage = Resource.msgCorrectVerificationCode;
            }
            else if (verificationCodeModel.VerificationCode.ToString().Length < 6)
            {
                errorMessage = Resource.msgRequired6digitVerificationCode;
            }

            return errorMessage;
        }

        /// <summary>
        /// Method Name     : ValidateForgotPassowordModel
        /// Author          : Hiren Patel
        /// Creation Date   : 5 Dec 2017
        /// Purpose         : Validate forgot password model. 
        /// Revision        : 
        /// </summary>
        /// <param name="forgotPassowordModel"></param>
        /// <returns></returns>
        public string ValidateForgotPassowordModel(ForgotPassowordModel forgotPassowordModel)
        {
            string errorMessage = string.Empty;
            if (string.IsNullOrEmpty(forgotPassowordModel.EmailID))
            {
                errorMessage = string.Format(Resource.msgFieldRequired, Resource.msgRequiredEmailId);
            }

            return errorMessage;
        }
    }
}