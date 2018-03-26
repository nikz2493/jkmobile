using System;

namespace JKMPCL.Model
{
    /// <summary>
    /// Class Name      : Customer Model
    /// Author          : Hiren Patel
    /// Creation Date   : 05 Dec 2017
    /// Purpose         : 
    /// Revision        : 
    /// </summary>
    public class CustomerModel
    {
        public string CustomerId { get; set; }
        public string EmailId { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Phone { get; set; }
        public DateTime LastLoginDate { get; set; }
        public int? VerificationCode { get; set; }
        public string CodeValidTill { get; set; }
        public int OTPValidTill { get; set; }
        public bool TermsAgreed { get; set; }
        public string PreferredContact { get; set; }
        public bool ReceiveNotifications { get; set; }
        public bool IsCustomerRegistered { get; set; }
        public string CustomerFullName { get; set; }
    }

    /// <summary>
    /// Class Name      : GetCustomerProfileDataResponse
    /// Author          : Hiren Patel
    /// Creation Date   : 05 Dec 2017
    /// Purpose         : Get customer profile data response.
    /// Revision        : 
    /// </summary>
    public class GetCustomerProfileDataResponse
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public bool IsCustomerRegistered { get; set; }
    }


    /// <summary>
    /// Class Name      : GetCustomerProfileDataResponse
    /// Author          : Hiren Patel
    /// Creation Date   : 05 Dec 2017
    /// Purpose         : Get customer verification data response.
    /// Revision        : 
    /// </summary>
    public class GetCustomerVerificationDataResponse
    {
        public string CustomerId { get; set; }
        public string VerificationCode { get; set; }

    }

    /// <summary>
    /// Class Name      : GetPrivacyPolicyPDFResponse
    /// Author          : Hiren Patel
    /// Creation Date   : 05 Dec 2017
    /// Purpose         : use for Privacy Policy display text
    /// Revision        : 
    /// </summary>
    public class GetPrivacyPolicyPDFResponse
    {
        public string DATA { get; set; }
    }
}
