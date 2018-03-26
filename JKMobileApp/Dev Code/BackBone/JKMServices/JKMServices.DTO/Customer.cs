namespace JKMServices.DTO
{
    using System;

    public partial class Customer
    {
        public string CustomerId { get; set; }

        public bool TermsAgreed { get; set; } = false;

        private string _CustomerFullName { get; set; }
        public string CustomerFullName
        {
            get { return _CustomerFullName ?? null; }
            set { _CustomerFullName = value; }
        }

        private string _EmailId { get; set; }
        public string EmailId
        {
            get { return _EmailId ?? null; }
            set { _EmailId = value; }
        }

        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public int? VerificationCode { get; set; }
        public DateTime? CodeValidTill { get; set; }

        public bool IsCustomerRegistered { get; set; }

        private string _Phone { get; set; }
        public string Phone
        {
            get { return _Phone ?? null; }
            set { _Phone = value; }
        }

        public bool ReceiveNotifications { get; set; } = true;

        private string _PreferredContact { get; set; }
        public string PreferredContact
        {
            get { return _PreferredContact ?? null; }
            set { _PreferredContact = value; }
        }
    }

    public partial class CustomerVerification
    {
        public int? VerificationCode { get; set; }
        public DateTime CodeValidTill { get; set; }
    }
}
