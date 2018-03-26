using JKMServices.DTO;
using System;
using System.Collections.Generic;
using Utility;

namespace UnitTests.Mock
{
    /// <summary>
    /// Class Name      : AlertData
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 30 Nov 2017
    /// Purpose         : Mock data for moq unit test
    /// Revision        : 
    /// </summary>
    public class AlertData
    {
        public JKMServices.DTO.ServiceResponse<List<JKMServices.DTO.Alert>> GetMockAlertDataList_PositiveTest()
        {
            ServiceResponse<List<Alert>> serviceResponse;
            List<Alert> alertList;


            alertList = new List<Alert>();

            alertList.Add(new Alert
            {
                AlertID = "0001",
                CustomerID = "RC0064218",
                AlertTitle = "Send mail for load date.",
                AlertDescription = string.Empty,
                StartDate = Convert.ToDateTime("2018-01-26T09: 23:27"),
                EndDate = Convert.ToDateTime("2018-01-26T09: 23:27"),
                NotificationType = "1",
                IsActive = "0"
            });

            alertList.Add(new Alert
            {
                AlertID = "0001",
                CustomerID = "RC0064218",
                AlertTitle = "Send mail for load date.",
                AlertDescription = string.Empty,
                StartDate = Convert.ToDateTime("2018-01-26T09: 23:27"),
                EndDate = Convert.ToDateTime("2018-01-26T09: 23:27"),
                NotificationType = "1",
                IsActive = "0"
            });

            serviceResponse = new ServiceResponse<List<Alert>>
            {
                Data = alertList
            };

            return serviceResponse;
        }


        //moq return null to create test case if method returns null
        public List<JKMServices.DTO.Alert> GetAlertListFailData()
        {
            return null;
        }
        internal List<JKMServices.DTO.Alert> RequestBodyForPost()
        {
            var alertDetails = new List<JKMServices.DTO.Alert>
            {
                new JKMServices.DTO.Alert
                {
                    AlertID = "0001",
                    CustomerID = "RC0064218",
                    AlertTitle = "Send mail for load date.",
                    AlertDescription = string.Empty,
                    StartDate = Convert.ToDateTime("2018-01-26T09: 23:27"),
                    EndDate = Convert.ToDateTime("2018-01-26T09: 23:27"),
                    NotificationType = "1",
                    IsActive = "0"
                },

                new JKMServices.DTO.Alert
                {
                    AlertID = "0002",
                    CustomerID = "RC0064218",
                    AlertTitle = "Send mail for approval.",
                    AlertDescription = string.Empty,
                    StartDate = Convert.ToDateTime("2018-01-26T09: 23:27"),
                    EndDate = Convert.ToDateTime("2018-01-26T09: 23:27"),
                    NotificationType = "2",
                    IsActive = "0"
                }
            };
            return alertDetails;
        }

        internal string RequestBodyForPut()
        {
            JKMServices.DTO.ServiceResponse<JKMServices.DTO.Alert> serviceResponse;
            serviceResponse = new ServiceResponse<Alert>
            {
                Data = new JKMServices.DTO.Alert
                {
                    AlertID = "0002",                  
                    IsActive = "0"
                }
            };
            return General.ConvertToJson<ServiceResponse<Alert>>(serviceResponse);
        }

        internal List<Alert> RequestBodyForDelete()
        {
            var alertDetails = new List<JKMServices.DTO.Alert>
            {
                new JKMServices.DTO.Alert
                {
                    AlertID = "0001"
                },

                new JKMServices.DTO.Alert
                {
                    AlertID = "0002"
                }
            };
            return alertDetails;
        }

        public JKMServices.DTO.ServiceResponse<JKMServices.DTO.Alert> MockPostAlertData()
        {
            JKMServices.DTO.ServiceResponse<JKMServices.DTO.Alert> serviceResponse;
            serviceResponse = new JKMServices.DTO.ServiceResponse<JKMServices.DTO.Alert>();
            return new ServiceResponse<Alert> { };
        }

        public JKMServices.DTO.ServiceResponse<JKMServices.DTO.Alert> GetCRMAlertListData()
        {
            JKMServices.DTO.ServiceResponse<JKMServices.DTO.Alert> serviceResponse;
            serviceResponse = new ServiceResponse<Alert>
            {
                Data = new JKMServices.DTO.Alert
                {
                    AlertID = "0002",
                    CustomerID = "RC0064218",
                    AlertTitle = "Update - Send mail for approval.",
                    AlertDescription = string.Empty,
                    StartDate = DateTime.ParseExact("2018/01/01", "yyyy/MM/dd", null),
                    EndDate = DateTime.ParseExact("2018/01/04", "yyyy/MM/dd", null),
                    NotificationType = "2",
                    IsActive = "0"
                }
            };
            return serviceResponse;
        }

        public string GetBLLAlertListData()
        {
            JKMServices.DTO.ServiceResponse<JKMServices.DTO.Alert> serviceResponse;
            serviceResponse = new ServiceResponse<Alert>
            {
                Data = new JKMServices.DTO.Alert
                {
                    AlertID = "0002",
                    CustomerID = "RC0064218",
                    AlertTitle = "Update - Send mail for approval.",
                    AlertDescription = string.Empty,
                    StartDate = Convert.ToDateTime("2018-01-26T09: 23:27"),
                    EndDate = Convert.ToDateTime("2018-01-26T09: 23:27"),
                    NotificationType = "2",
                    IsActive = "0"
                }
            };
            return General.ConvertToJson<ServiceResponse<Alert>>(serviceResponse);
        }

        public bool CheckCustomerRegistered()
        {
            if (Utility.Validations.IsValid("RC0064218"))
            {
                return true;
            }
            return false;
        }

        public string PostAlertData()
        {
            JKMServices.DTO.ServiceResponse<JKMServices.DTO.Alert> serviceResponse;
            serviceResponse = new ServiceResponse<Alert>
            {
                Message = "204 - NoContent"
            };
            return General.ConvertToJson<ServiceResponse<Alert>>(serviceResponse);
        }
    }
}
