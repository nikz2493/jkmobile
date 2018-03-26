namespace JKMPCL.Model
{
    /// <summary>
    /// Class Name      : PrivacyPolicyModel
    /// Author          : Hiren Patel
    /// Creation Date   : 5 Dec 2017
    /// Purpose         :  
    /// Revision        : 
    /// </summary> 
    public class PrivacyPolicyModel
    {
        public string CustomerId { get; set; }
        public bool TermsAgreed { get; set; }
        
        //Added by Vivek Bhavsar on 20 Feb 2018 for MyAccount Page
        public string Phone { get; set; }
        public string PreferredContact { get; set; }//PreferredContactMethodCode: Email :  2 SMS: 3
        public bool ReceiveNotifications { get; set; }
    }
}
