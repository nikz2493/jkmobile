using System.Collections.Generic;
using JKMServices.DTO;
using System;
using Utility;
using System.Text;

namespace UnitTests.Mock
{
    public class CustomerData
    {
        public List<Customer> GetCustomer()
        {
            var customerDetails = new List<Customer>
            {
                new Customer
                {
                    CustomerId = "RC004",
                    EmailId = "sanketprajapati@mailinator.com",
                    PasswordHash = "test",
                    PasswordSalt = "test",
                    VerificationCode = 265742,
                    CodeValidTill = new System.DateTime(),
                    TermsAgreed = false,
                }
            };

            return customerDetails;
        }

        public ServiceResponse<Customer> GetCustomerDataPositive()
        {
            ServiceResponse<Customer> serviceResponse;
            serviceResponse = new ServiceResponse<Customer>
            {
                Data = new Customer
                {
                    CustomerId = "RC123"
                }
            };
            return serviceResponse;
        }

        public ServiceResponse<Customer> GetCRMVerificationData()
        {
            ServiceResponse<Customer> serviceResponse;
            serviceResponse = new ServiceResponse<Customer>
            {
                Data = new Customer
                {
                    PasswordHash = "admin",
                    PasswordSalt = "admin",
                    VerificationCode = 120120,
                    CodeValidTill = Convert.ToDateTime("2018-01-26T09: 23:27")
                }
            };
            return serviceResponse;
        }

        public string GetBLLVerificationData()
        {
            ServiceResponse<Customer> serviceResponse;
            serviceResponse = new ServiceResponse<Customer>
            {
                Data = new Customer
                {
                    PasswordHash = "admin",
                    PasswordSalt = "admin",
                    VerificationCode = 120120,
                    CodeValidTill = Convert.ToDateTime("2018-01-26T09: 23:27")
                }
            };
            return General.ConvertToJson<ServiceResponse<Customer>>(serviceResponse);
        }


        public ServiceResponse<Customer> GetCRMProfileData()
        {
            ServiceResponse<JKMServices.DTO.Customer> serviceResponse;
            serviceResponse = new ServiceResponse<Customer>
            {
                Data = new Customer
                {
                    CustomerFullName = "Ranjana Singh",
                    PreferredContact = "2",
                    EmailId = "ranjana.singh@mailinator.com",
                    Phone = "9874561234",
                    PasswordHash = "admin",
                    PasswordSalt = "admin",
                    VerificationCode = 120120,
                    TermsAgreed = true,
                    IsCustomerRegistered = false,
                    CodeValidTill = Convert.ToDateTime("2018-01-26T09: 23:27")
                }
            };
            return serviceResponse;
        }

        public string PutBLLVerificationData()
        {
            ServiceResponse<JKMServices.DTO.Customer> serviceResponse;
            serviceResponse = new ServiceResponse<Customer>
            {
                Data = new Customer
                {
                    CustomerId = "RC0064215",
                    PasswordHash = "admin",
                    PasswordSalt = "admin"
                }
            };
            return General.ConvertToJson<ServiceResponse<Customer>>(serviceResponse);
        }

        public ServiceResponse<Customer> PutCRMProfileData()
        {
            ServiceResponse<Customer> serviceResponse;
            serviceResponse = new ServiceResponse<Customer>
            {
                Information = "204 - NoContent"
            };
            return serviceResponse;
        }

        public string PutBLLCustomerProfileData()
        {
            ServiceResponse<Customer> serviceResponse;
            serviceResponse = new ServiceResponse<Customer>
            {
                Data = new Customer
                {
                    CustomerId = "RC0064215",
                    TermsAgreed = true
                }
            };
            return General.ConvertToJson<ServiceResponse<Customer>>(serviceResponse);
        }

        public string PutBLLCustomerMyAccountData()
        {
            ServiceResponse<Customer> serviceResponse;
            serviceResponse = new ServiceResponse<Customer>
            {
                Data = new Customer
                {
                    CustomerId = "RC0064215",
                    TermsAgreed = true,
                    Phone="9876543212",
                    PreferredContact="2",
                    ReceiveNotifications=true
                }
            };
            return General.ConvertToJson<ServiceResponse<Customer>>(serviceResponse);
        }

        public string GetBLLProfileData()
        {
            ServiceResponse<Customer> serviceResponse;
            serviceResponse = new ServiceResponse<Customer>
            {
                Data = new Customer
                {
                    CustomerFullName = "Ranjana Singh",
                    PreferredContact = "2",
                    EmailId = "ranjana.singh@mailinator.com",
                    Phone = "9874561234",
                    PasswordHash = "admin",
                    PasswordSalt = "admin",
                    VerificationCode = 120120,
                    TermsAgreed = true,
                    IsCustomerRegistered = false,
                    CodeValidTill = Convert.ToDateTime("2018-01-26T09: 23:27")
                }
            };
            return General.ConvertToJson<ServiceResponse<Customer>>(serviceResponse);
        }

        public string MockedRequestBody_Profile()
        {
            StringBuilder requestBody = new StringBuilder();
            requestBody.Append("{");
            requestBody.Append("\"CustomerId\":\"RC0064215\"");
            requestBody.Append(",");
            requestBody.Append("\"TermsAgreed\":\"true\"");
            requestBody.Append("}");

            return requestBody.ToString();
        }

        public string MockedRequestBody_MyAccount()
        {
            StringBuilder requestBody = new StringBuilder();
            requestBody.Append("{");
            requestBody.Append("\"CustomerId\":\"RC0064215\"");
            requestBody.Append(",");
            requestBody.Append("\"TermsAgreed\":\"true\"");
            requestBody.Append(",");
            requestBody.Append("\"Phone\":\"9874589651\"");
            requestBody.Append(",");
            requestBody.Append("\"PreferredContact\":\"2\"");
            requestBody.Append(",");
            requestBody.Append("\"ReceiveNotifications\":\"true\"");
            requestBody.Append("}");

            return requestBody.ToString();
        }

        public string MockedRequestBody_Verification()
        {
            StringBuilder requestBody = new StringBuilder();
            requestBody.Append("{");
            requestBody.Append("\"CustomerId\":\"RC0064215\"");
            requestBody.Append(",");
            requestBody.Append("\"PasswordHash\":\"test\"");
            requestBody.Append(",");
            requestBody.Append("\"PasswordSalt\":\"test\"");
            requestBody.Append("}");

            return requestBody.ToString();
        }
    }
}
