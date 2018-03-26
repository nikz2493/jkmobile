using System;
using System.Resources;

namespace Utility
{
    /// <summary>
    /// Class Name      : Generator
    /// Author          : Ranjana Singh
    /// Creation Date   : 28 Nov 2017
    /// Purpose         : Generates Verification code between range of 4 to 9 and also generates Verification html code.
    /// Revision        :
    /// </summary>
    /// <returns></returns>
    public sealed class Generator : IGenerator
    {
        private readonly ResourceManager resourceManager;
        private string divHtml = string.Empty;

        public Generator()
        {
            resourceManager = new System.Resources.ResourceManager("Utility.Resource", System.Reflection.Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Method Name     : GetVerificationCode
        /// Author          : Ranjana Singh
        /// Creation Date   : 28 Nov 2017
        /// Purpose         : Gets the Verification code between range of 4 to 9.
        /// Revision        :
        /// </summary>
        /// <param name="noOfDigits">Total number of Digits for verification code.</param>
        /// <returns>Verification code in the form of integer.</returns>
        public int GetVerificationCode(int noOfDigits)
        {
            Random randomNumber;
            int verificationCode;
            try
            {
                if (noOfDigits >= int.Parse(resourceManager.GetString("CodeMinRange"))
                    && noOfDigits <= int.Parse(resourceManager.GetString("CodeMaxRange")))
                {
                    verificationCode = 0;
                    randomNumber = new Random();
                    for (int digit = 0; digit < noOfDigits; digit++)
                    {
                        //Code for calculating verification code
                        verificationCode = verificationCode * 10 +
                            randomNumber.Next(1, int.Parse(resourceManager.GetString("CodeMaxRange")));
                    }
                    return verificationCode;
                }
                else
                    return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Method Name     : GetVerificationHtml
        /// Author          : Ranjana Singh
        /// Creation Date   : 28 Nov 2017
        /// Purpose         : Gets the Verification code in html format.
        /// Revision        :
        /// </summary>
        /// <param name="totalDigits">Digits for verification code.</param>
        /// <returns>Verification html to be used in Email.</returns>
        public string GetVerificationHtml(int totalDigits, int verificationCode = 0)
        {
            string verificationHtml = resourceManager.GetString("VerificationHtml");
            try
            {
                if (!string.IsNullOrEmpty(verificationHtml))
                {
                    divHtml = verificationHtml;
                    if (totalDigits >= int.Parse(resourceManager.GetString("CodeMinRange"))
                        && totalDigits <= int.Parse(resourceManager.GetString("CodeMaxRange")))
                    {
                        if (verificationCode == 0)
                        {
                            verificationCode = GetVerificationCode(totalDigits);
                        }
                        return GenerateHtml(verificationCode);
                    }
                    else
                        verificationCode = 0;
                    return divHtml;
                }
                else
                    return resourceManager.GetString("VerificationHtmlError");
            }
            catch (Exception)
            {
                return resourceManager.GetString("VerificationCodeError");
            }
        }

        /// <summary>
        /// Method Name     : GenerateHtml
        /// Author          : Ranjana Singh
        /// Creation Date   : 30 Nov 2017
        /// Purpose         : Generates the Verification Html.
        /// Revision        :
        /// </summary>
        /// <param name="verificationCode">Verification code for generating html code.</param>
        /// <returns> Generates Verification html code.</returns>
        public string GenerateHtml(int verificationCode)
        {
            string defaultHtml = resourceManager.GetString("DefaultHtml");
            string androidUrl = resourceManager.GetString("AndroidUrl");
            string iosUrl = resourceManager.GetString("IOSUrl");

            if (verificationCode == 0 && !string.IsNullOrEmpty(defaultHtml))
                return defaultHtml;
            else
            {
                divHtml = divHtml.Replace("{0}", verificationCode.ToString());

                if (!string.IsNullOrEmpty(androidUrl))
                    divHtml = divHtml.Replace("{1}", androidUrl);
                if (!string.IsNullOrEmpty(iosUrl))
                    divHtml = divHtml.Replace("{2}", iosUrl);
                return divHtml;
            }
        }
    }
}

