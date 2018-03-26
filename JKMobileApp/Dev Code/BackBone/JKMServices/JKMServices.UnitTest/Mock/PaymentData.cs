using JKMServices.DTO;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Mock
{
    public class PaymentData
    {
        public ServiceResponse<JKMServices.DTO.Payment> GetPaymentDataPositive()
        {
            ServiceResponse<JKMServices.DTO.Payment> serviceResponse;
            serviceResponse = new ServiceResponse<Payment>
            {
                Data = new JKMServices.DTO.Payment
                {

                }
            };
            return serviceResponse;
        }

        public string MockedRequestBody_PostTransaction()
        {
            StringBuilder requestBody = new StringBuilder();

            requestBody.Append("{");
            requestBody.Append("\"TransactionNumber\":\"123456789121\"");
            requestBody.Append(",");
            requestBody.Append("\"TransactionAmount\":\"550\"");
            requestBody.Append(",");
            requestBody.Append("\"TransactionDate\":\"10-25-2017 15:01:00\"");
            requestBody.Append(",");
            requestBody.Append("\"CustomerID\":\"RC0064218\"");
            requestBody.Append(",");
            requestBody.Append("\"MoveID\":\"RM0035665\"");
            requestBody.Append("}");
            requestBody.Append("\"DeviceID\":\"\"");
            requestBody.Append("}");

            return requestBody.ToString();
        }

        public JKMServices.DTO.ServiceResponse<JKMServices.DTO.Payment> PostTransactionData()
        {
            JKMServices.DTO.ServiceResponse<JKMServices.DTO.Payment> serviceResponse;
            serviceResponse = new ServiceResponse<Payment>
            {
                Message = "204 - NoContent"
            };
            return serviceResponse;
        }


        public Dictionary<string, string> GetCustomerGUID()
        {
            Dictionary<string, string> mockErrorResponse = new Dictionary<string, string>
            {
                { "STATUS", "OK" },
                { "CONTENT", @"{'@odata.context':'https://edenbeta.jkmoving.com/ClientApp/api/data/v8.2/$metadata#contacts(contactid)','value':
                                [
                                    {
                                        '@odata.etag':'W/\'48085111\'','contactid':'afb9e006-6be5-e711-80ed-bef806786223'
                                    }
                                ]
                               }"
                }
            };
            return mockErrorResponse;
        }

        public ServiceResponse<Payment> MockedResponseBodyForCRM_GetDeviceID()
        {
            ServiceResponse<Payment> serviceResponse = new ServiceResponse<Payment>
            {
                Data = new Payment
                {
                    DeviceID = "dbad401c-1682-7702-c62d-cbada737a737"
                }
            };

            return serviceResponse;
        }

        public ServiceResponse<Payment> MockedReponseBodyForCRM_GetAmount()
        {
            ServiceResponse<Payment> serviceResponse = new ServiceResponse<Payment>
            {
                Data = new Payment
                {
                    TransactionNumber = null,
                    TransactionAmount = 0,
                    TransactionDate = null,
                    CustomerID = null,
                    MoveID = null,
                    DeviceID = null,
                    Deposit = "500",
                    TotalCost = "6000",
                    TotalDue = "4500",
                    TotalPaid = "500",
                    MoveCoordinator_ContactNumber = null
                }
            };
            return serviceResponse;
        }

        public ServiceResponse<Payment> MockedReponseBodyForCRM_GetAmount_Negative()
        {
            ServiceResponse<JKMServices.DTO.Payment> serviceResponse;
            serviceResponse = new ServiceResponse<Payment>
            {
                Message = "ERROR"
            };
            return serviceResponse;
        }
    }
}
