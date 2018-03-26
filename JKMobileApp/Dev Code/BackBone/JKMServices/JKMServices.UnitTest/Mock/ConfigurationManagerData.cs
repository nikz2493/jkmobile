using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Mock
{
    public class ConfigurationManagerData
    {
        public List<JKMServices.DTO.Customer> GetData()
        {
            var customerDetails = new List<JKMServices.DTO.Customer>();
            customerDetails.Add(new JKMServices.DTO.Customer
            {
                CustomerId = "RC004",
                EmailId = "sanketprajapati@mailinator.com",
                PasswordHash = "test",
                PasswordSalt = "test",
                VerificationCode = 265742,
                CodeValidTill = new DateTime(),
                TermsAgreed = false,
            });

            return customerDetails;
        }
    }
}
