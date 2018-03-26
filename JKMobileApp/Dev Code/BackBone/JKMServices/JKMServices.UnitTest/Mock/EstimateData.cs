using JKMServices.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests.Mock
{
    public class EstimateData
    {
        public List<Estimate> GetEstimateIdList()
        {
            var estimateIdList = new List<Estimate>
            {
                new Estimate
                {
                    MoveNumber = "RM0035658"
                },
                new Estimate
                {
                    MoveNumber = "RM0035659"
                }
            };

            return estimateIdList;
        }

        public ServiceResponse<Estimate> GetMockEstimateData_NoContentTest()
        {
            List<ServiceResponse<Estimate>> serviceResponse;
            serviceResponse = new List<ServiceResponse<Estimate>>();
            serviceResponse.Add(new ServiceResponse<Estimate> { Information = "204 - No Content" });
            return serviceResponse.FirstOrDefault();
        }

        public ServiceResponse<Estimate> GetMockEstimateData_ErrorTest()
        {
            List<ServiceResponse<Estimate>> serviceResponse;
            serviceResponse = new List<ServiceResponse<Estimate>>();
            serviceResponse.Add(new ServiceResponse<Estimate> { Message = "ERROR" });
            return serviceResponse.FirstOrDefault();
        }

        public ServiceResponse<List<Estimate>> GetMockEstimateDataList_PositiveTest()
        {
            ServiceResponse<List<Estimate>> serviceResponse;
            List<Estimate> estimateList;
            serviceResponse = new ServiceResponse<List<Estimate>>();


            estimateList = new List<Estimate>();

            estimateList.Add(new Estimate
            {
                MoveId = "05451-56531-656323-451212",
                MoveNumber = "MV00154",

                EstimatedLineHaul = "456",
                Deposit = "650",

                IsActive = "0",
                StatusReason = "1000001",

                Origin_Street1 = "11, Green Zone",
                Origin_Street2 = "",
                Origin_City = "San Jose",
                Origin_State = "San Fransisco",
                Origin_PostalCode = "",
                Origin_Country = "USA",
                Origin_ContactName = "",
                Origin_Telephone1 = "",
                Origin_Telephone3 = "",
                Origin_Telephone2 = "",

                Destination_Street1 = "54, Park Avenue",
                Destination_Street2 = "",
                Destination_City = "Oakland",
                Destination_State = "Las Vegas",
                Destination_PostalCode = "",
                Destination_Country = "USA",
                Destination_ContactName = "",
                Destination_Telephone1 = "",
                Destination_Telephone3 = "",
                Destination_Telephone2 = "",

                PackStartDate = "",
                LoadStartDate = "",
                MoveStartDate = "",

                WhatMattersMost = "Delivery should be on time.",
                ExcessValuation = "44563",
                ValuationDeductible = "02",
                ValuationCost = "656",
                ServiceCode = "stg_Packing,stg_Loading,stg_UnLoading"
            });

            estimateList.Add(new Estimate
            {
                MoveId = "05451-43534-86788-12331",
                MoveNumber = "MV00154",

                EstimatedLineHaul = "645",
                Deposit = "650",

                IsActive = "0",
                StatusReason = "1000001",

                Origin_Street1 = "11, Green Zone",
                Origin_Street2 = "",
                Origin_City = "San Jose",
                Origin_State = "San Fransisco",
                Origin_PostalCode = "",
                Origin_Country = "USA",
                Origin_ContactName = "",
                Origin_Telephone1 = "",
                Origin_Telephone3 = "",
                Origin_Telephone2 = "",

                Destination_Street1 = "54, Park Avenue",
                Destination_Street2 = "",
                Destination_City = "Oakland",
                Destination_State = "San Fransisco",
                Destination_PostalCode = "",
                Destination_Country = "USA",
                Destination_ContactName = "",
                Destination_Telephone1 = "",
                Destination_Telephone3 = "",
                Destination_Telephone2 = "",

                PackStartDate = "",
                LoadStartDate = "",
                MoveStartDate = "",

                WhatMattersMost = "Delivery should be on time.",
                ExcessValuation = "44563",
                ValuationDeductible = "02",
                ValuationCost = "656",
                //------- Below data is mocked as of now --------
                ServiceCode = "stg_Packing,stg_Loading,stg_UnLoading"
            });

            serviceResponse = new ServiceResponse<List<Estimate>>
            {
                Data = estimateList
            };

            return serviceResponse;
        }

        public List<Customer> GetRegisteredCustomer()
        {
            var estimateDetails = new List<Customer>();
            estimateDetails.Add(new Customer
            {
                IsCustomerRegistered = true
            });
            return estimateDetails;
        }

        public List<Customer> GetCustomer()
        {
            var estimateDetails = new List<Customer>();
            return estimateDetails;
        }

        public ServiceResponse<List<Estimate>> GetEstimateIdNoContent()
        {
            return null;
        }

        public ServiceResponse<List<Estimate>> CustomerNotRegistered()
        {
            ServiceResponse<List<Estimate>> serviceResponse;
            serviceResponse = new ServiceResponse<List<Estimate>>
            {
                Information = "Customer is not registered."
            };

            return serviceResponse;
        }
        public ServiceResponse<List<Estimate>> GetEstimateBadRequest()
        {
            ServiceResponse<List<Estimate>> serviceResponse;
            serviceResponse = new ServiceResponse<List<Estimate>>
            {
                Information = "No estimate exist for customer."
            };
            return serviceResponse;
        }


        public ServiceResponse<Estimate> GetEstimateDataPositive()
        {
            List<ServiceResponse<Estimate>> serviceResponse;
            serviceResponse = new List<ServiceResponse<Estimate>>();
            serviceResponse.Add(new ServiceResponse<Estimate>
            {
                Data = new Estimate
                {
                    EstimatedLineHaul = "544.00",
                    Deposit = "200.00",
                    StatusReason = "Estimate",
                    Destination_Street1 = "2888, Nelm Street, Washington.",
                    Destination_Street2 = "",
                    Destination_City = "",
                    Destination_State = "",
                    Destination_PostalCode = "",
                    Destination_Country = "",
                    Destination_ContactName = "",
                    Destination_Telephone1 = "",
                    Destination_Telephone3 = "",
                    Destination_Telephone2 = "986354547",
                    Origin_Street1 = "1888, cccLuke Lane, Oklahoma Ci",
                    Origin_Street2 = "",
                    Origin_City = "",
                    Origin_State = "",
                    Origin_PostalCode = "",
                    Origin_Country = "",
                    Origin_ContactName = "",
                    Origin_Telephone1 = "022656599",
                    Origin_Telephone3 = "",
                    Origin_Telephone2 = "",
                    PackStartDate = "02/16/2018",
                    LoadStartDate = "02/17/2018",
                    MoveStartDate = "02/17/2018",
                    WhatMattersMost = "There are many glass products which needs be taken care of.",
                    ExcessValuation = "5555.0000",
                    ValuationDeductible = "100000001",
                    ValuationCost = "5005.5000",
                    ServiceCode = "stg_Packing,stg_Loading,stg_UnLoading"
                }
            });
            return serviceResponse.FirstOrDefault();
        }
        public Dictionary<string, string> GetMockEstimateData_DictionaryWithError()
        {
            Dictionary<string, string> mockErrorResponse = new Dictionary<string, string> { { "ERROR", "ERROR" } };
            return mockErrorResponse;
        }

        //Need refactoring to pass proper data
        public Dictionary<string, string> GetMockEstimateData_DictionaryWithNoContent()
        {
            Dictionary<string, string> mockErrorResponse = new Dictionary<string, string>();
            mockErrorResponse.Add("STATUS", "OK");
            mockErrorResponse.Add("CONTENT", "");//containing CONTENT values

            return mockErrorResponse;
        }

        //Need refactoring to pass proper data
        public Dictionary<string, string> GetMockEstimateData_DictionaryWithSuccess()
        {
            Dictionary<string, string> mockErrorResponse = new Dictionary<string, string> { { "ERROR", "ERROR" } };
            return mockErrorResponse;
        }

        public ServiceResponse<Estimate> GetEstimateIdDataPositive()
        {
            List<ServiceResponse<Estimate>> serviceResponse;
            serviceResponse = new List<ServiceResponse<Estimate>>();
            serviceResponse.Add(new ServiceResponse<Estimate>
            {
                Data = new Estimate
                {
                    MoveNumber = "237510",
                    MoveId = "bba5729a-86d9-e611-80c7-369e029457bb"
                }
            });
            return serviceResponse.FirstOrDefault();
        }

        public Estimate GetEstimateDTO()
        {
            return new Estimate();
        }

        public string MockedRequestBody_positive()
        {
            StringBuilder requestBody = new StringBuilder();
            requestBody.Append("{");
            requestBody.Append("\"OriginAddress\":\"302, Classic Park, Manhatten, USA\",");
            requestBody.Append("\"DestinationAddress\":\"101, Park Avenue, Las Vegas, 201-563, USA\",");
            requestBody.Append("\"jkmoving_actual_packfirstday\":\"2018-01-26T03:00:00.000Z\",");
            requestBody.Append("\"jkmoving_actual_loadfirstday\":\"2018-01-27T04:00:00.000Z\",");
            requestBody.Append("\"jkmoving_actual_movefirstday\":\"2018-01-28T05:00:00.000Z\",");
            requestBody.Append("\"jkmoving_declaredpropertyvalue\":\"\",");
            requestBody.Append("\"jkmoving_valuationdeductible\":\"\",");
            requestBody.Append("\"jkmoving_valuationcost\":\"\",");
            requestBody.Append("\"jkmoving_servicecode\":\"\",");
            requestBody.Append("\"WhatMaterMost\":\"\",");
            requestBody.Append("\"IsServiceDate\":true,");
            requestBody.Append("\"IsAddressEdited\":true,");
            requestBody.Append("\"IsValuationEdited\":false,");
            requestBody.Append("\"IsWhatMatterMostEdited\":false");
            requestBody.Append("}");

            return requestBody.ToString();
        }
    }
}