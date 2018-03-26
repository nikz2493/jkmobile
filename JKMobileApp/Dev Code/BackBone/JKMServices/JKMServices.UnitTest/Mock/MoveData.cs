using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JKMServices.DTO;
using Utility;

namespace UnitTests.Mock
{
    public class MoveData
    {
        public List<JKMServices.DTO.Move> GetMove()
        {
            var moveDetails = new List<JKMServices.DTO.Move>
            {
                new JKMServices.DTO.Move
                {
                    MoveNumber = "",
                    Origin_City = "Valsad"
                }
            };

            return moveDetails;
        }

        public JKMServices.DTO.ServiceResponse<JKMServices.DTO.Move> GetMockMoveData_NoContentTest()
        {
            List<JKMServices.DTO.ServiceResponse<JKMServices.DTO.Move>> serviceResponse;
            serviceResponse = new List<JKMServices.DTO.ServiceResponse<JKMServices.DTO.Move>>
            {
                new ServiceResponse<Move> { Information = "204 - No Content" }
            };
            return serviceResponse.FirstOrDefault();
        }

        public JKMServices.DTO.ServiceResponse<JKMServices.DTO.Move> GetMockMoveData_ErrorTest()
        {
            List<JKMServices.DTO.ServiceResponse<JKMServices.DTO.Move>> serviceResponse;
            serviceResponse = new List<JKMServices.DTO.ServiceResponse<JKMServices.DTO.Move>>
            {
                new ServiceResponse<Move> { Message = "ERROR" }
            };
            return serviceResponse.FirstOrDefault();
        }

        public JKMServices.DTO.ServiceResponse<JKMServices.DTO.Move> GetMockMoveData_PositiveTest()
        {
            List<JKMServices.DTO.ServiceResponse<JKMServices.DTO.Move>> serviceResponse;
            serviceResponse = new List<ServiceResponse<Move>>
            {
                new ServiceResponse<Move>
                {
                    Data = new JKMServices.DTO.Move
                    {
                        MoveId = "",
                        MoveNumber = "236659",
                        IsActive = "0",
                        StatusReason = "100000001",
                        Origin_Street1 = "32/Park Avenue",
                        Origin_Street2 = "",
                        Origin_City = "New York City",
                        Origin_State = "",
                        Origin_PostalCode = "",
                        Origin_Country = "USA",
                        Origin_ContactName = "",
                        Origin_Telephone1 = "",
                        Origin_Telephone3 = "",
                        Origin_Telephone2 = "",

                        Destination_Street1 = "101, Symphony",
                        Destination_Street2 = "Opp. Starbucks",
                        Destination_City = "Las Vegas",
                        Destination_State = "",
                        Destination_PostalCode = "",
                        Destination_Country = "USA",
                        Destination_ContactName = "",
                        Destination_Telephone1 = "",
                        Destination_Telephone2 = "",
                        Destination_Telephone3 = "",
                        MoveStartDate = "12/24/2017",
                        MoveEndDate = "01/02/2017",
                        MoveDetails_PackStartDate = "",
                        MoveDetails_LoadStartDate = "",
                        MoveDetails_DeliveryStartDate = "",
                        MoveDetails_DeliveryEndDate = "",
                        WhatMattersMost = "",
                        //------- Below data is mocked as of now --------
                        ExcessValuation = "",
                        ValuationDeductible = "",
                        ValuationCost = "",
                        ServiceCode = ""
                    }
                }
            };
            return serviceResponse.FirstOrDefault();
        }

        public List<JKMServices.DTO.Customer> GetRegisteredCustomer()
        {
            var moveDetails = new List<JKMServices.DTO.Customer>
            {
                new JKMServices.DTO.Customer
                {
                    IsCustomerRegistered = true
                }
            };
            return moveDetails;
        }

        public List<JKMServices.DTO.Customer> GetCustomer()
        {
            var moveDetails = new List<JKMServices.DTO.Customer>();
            return moveDetails;
        }

        public JKMServices.DTO.ServiceResponse<JKMServices.DTO.Move> GetMoveDataPositive()
        {
            List<JKMServices.DTO.ServiceResponse<JKMServices.DTO.Move>> serviceResponse;
            serviceResponse = new List<ServiceResponse<Move>>
            {
                new ServiceResponse<Move>
                {
                    Data = new JKMServices.DTO.Move
                    {
                        MoveId = "",
                        MoveNumber = "236659",
                        IsActive = "0",
                        StatusReason = "100000001",
                        Origin_Street1 = "32/Park Avenue",
                        Origin_Street2 = "",
                        Origin_City = "New York City",
                        Origin_State = "",
                        Origin_PostalCode = "",
                        Origin_Country = "USA",
                        Origin_ContactName = "",
                        Origin_Telephone1 = "",
                        Origin_Telephone3 = "",
                        Origin_Telephone2 = "",

                        Destination_Street1 = "101, Symphony",
                        Destination_Street2 = "Opp. Starbucks",
                        Destination_City = "Las Vegas",
                        Destination_State = "",
                        Destination_PostalCode = "",
                        Destination_Country = "USA",
                        Destination_ContactName = "",
                        Destination_Telephone1 = "",
                        Destination_Telephone2 = "",
                        Destination_Telephone3 = "",
                        MoveStartDate = "12/24/2017",
                        MoveEndDate = "01/02/2017",
                        MoveDetails_PackStartDate = "",
                        MoveDetails_LoadStartDate = "",
                        MoveDetails_DeliveryStartDate = "",
                        MoveDetails_DeliveryEndDate = "",
                        WhatMattersMost = "",
                        //------- Below data is mocked as of now --------
                        ExcessValuation = "",
                        ValuationDeductible = "",
                        ValuationCost = "",
                        ServiceCode = "",
                        ContactOfMoveId = "9c7a332c-ffea-e711-80ed-bef806786223"
                    }
                }
            };
            return serviceResponse.FirstOrDefault();
        }

        public Dictionary<string, string> GetMockMoveData_DictionaryWithError()
        {
            Dictionary<string, string> mockErrorResponse = new Dictionary<string, string> { { "ERROR", "ERROR" } };
            return mockErrorResponse;
        }

        //Need refactoring to pass proper data
        public Dictionary<string, string> GetMockMoveData_DictionaryWithNoContent()
        {
            Dictionary<string, string> mockNoContentResponse = new Dictionary<string, string>
            {
                { "STATUS", "OK" },
                { "CONTENT", "" }//containing CONTENT values
            };

            return mockNoContentResponse;
        }

        //Need refactoring to pass proper data
        public Dictionary<string, string> GetMockMoveData_DictionaryWithSuccess()
        {
            Dictionary<string, string> mockErrorResponse = new Dictionary<string, string>
            {
                { "ERROR", "ERROR" }
            };

            return mockErrorResponse;
        }

        public JKMServices.DTO.ServiceResponse<JKMServices.DTO.Move> GetMoveIdDataPositive()
        {
            List<JKMServices.DTO.ServiceResponse<JKMServices.DTO.Move>> serviceResponse;
            serviceResponse = new List<ServiceResponse<Move>>
            {
                new ServiceResponse<Move>
                {
                    Data = new JKMServices.DTO.Move
                    {
                        MoveNumber = "237510",
                        ContactOfMoveId = "9c7a332c-ffea-e711-80ed-bef806786223",
                        MoveId = "bba5729a-86d9-e611-80c7-369e029457bb"
                    }
                }
            };
            return serviceResponse.FirstOrDefault();
        }

        public string MockedRequestBody_positive()
        {
            StringBuilder requestBody = new StringBuilder();
            requestBody.Append("{");
            requestBody.Append("\"StatusReason\":\"100000000\"");
            requestBody.Append("}");

            return requestBody.ToString();
        }
    }
}
