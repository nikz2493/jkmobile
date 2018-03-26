using JKMServices.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Utility;
using Utility.Logger;

namespace JKMServices.DAL.CRM.common
{
    public class CRMTODTOMapper : ICRMTODTOMapper
    {
        private readonly IResourceManagerFactory resourceManager;
        private readonly ILogger logger;

        public CRMTODTOMapper(IResourceManagerFactory resourceManager, ILogger logger)
        {
            this.resourceManager = resourceManager;
            this.logger = logger;
        }

        /// <summary>
        /// Method Name     : GetDateInStringFormat
        /// Author          : Pratik Soni
        /// Creation Date   : 09 Feb 2018
        /// Purpose         : To pass date string and get date in MM-dd-yyyy format
        /// Revision        : 
        /// </summary>
        /// <returns> Empty string if unable to parse the date </returns>
        private string GetDateInStringFormat(string dateString)
        {
            try
            {
                DateTime loadStartDay = Convert.ToDateTime(dateString);
                return loadStartDay.ToString("MM-dd-yyyy");
            }
            catch (Exception ex)
            {
                logger.Error(resourceManager.GetString("msgErrorInDateConversion"), ex);
                return string.Empty;
            }
        }
        /// <summary>
        /// Method Name     : MapMoveResponseToDTO
        /// Author          : Pratik Soni
        /// Creation Date   : 22 Dec 2017
        /// Purpose         : Mapping CRM Response to Move DTO class.
        /// Revision        : 
        /// </summary>
        public ServiceResponse<Move> MapMoveResponseToDTO(Dictionary<string, string> crmResponse)
        {
            List<ServiceResponse<Move>> serviceResponse;
            serviceResponse = new List<ServiceResponse<Move>>();
            try
            {
                if (crmResponse.ContainsKey("STATUS"))
                {
                    if (crmResponse["CONTENT"] is null)
                    {
                        serviceResponse.Add(new ServiceResponse<Move> { Message = crmResponse["ERROR"] });
                        return serviceResponse.FirstOrDefault();
                    }
                    dynamic jsonObject = General.ConvertToJObject(crmResponse["CONTENT"].ToString());
                    if (!jsonObject["value"].HasValues)
                    {
                        serviceResponse.Add(new ServiceResponse<Move> { Information = resourceManager.GetString("CRM_STATUS_204") });
                    }
                    else
                    {
                        JObject fieldValues = jsonObject["value"][0];
                        string loadStartDate = string.Empty;

                        //Get conditionally Start date for Move
                        GetConditionalStartDateForMove(fieldValues, ref loadStartDate);
                        serviceResponse.Add(new ServiceResponse<Move>
                        {
                            Data = new Move
                            {
                                MoveId = fieldValues["jkmoving_moveid"].ToString(),
                                MoveNumber = fieldValues["jkmoving_movenumber"].ToString(),
                                IsActive = fieldValues["statecode"].ToString(),
                                StatusReason = fieldValues["statuscode"].ToString(),

                                Origin_Street1 = fieldValues["jkmoving_origin_line1"].ToString(),
                                Origin_Street2 = fieldValues["jkmoving_origin_line2"].ToString(),
                                Origin_City = fieldValues["jkmoving_origin_city"].ToString(),
                                Origin_State = fieldValues["jkmoving_origin_stateorprovince"].ToString(),
                                Origin_PostalCode = fieldValues["jkmoving_origin_postalcode"].ToString(),
                                Origin_Country = fieldValues["jkmoving_origin_country"].ToString(),
                                Origin_ContactName = fieldValues["jkmoving_origin_contactname"].ToString(),
                                Origin_Telephone1 = fieldValues["jkmoving_origin_telephone1"].ToString(),
                                Origin_Telephone3 = fieldValues["jkmoving_origin_telephone3"].ToString(),
                                Origin_Telephone2 = fieldValues["jkmoving_origin_telephone2"].ToString(),

                                Destination_Street1 = fieldValues["jkmoving_destination_line1"].ToString(),
                                Destination_Street2 = fieldValues["jkmoving_destination_line2"].ToString(),
                                Destination_City = fieldValues["jkmoving_destination_city"].ToString(),
                                Destination_State = fieldValues["jkmoving_destination_stateorprovince"].ToString(),
                                Destination_PostalCode = fieldValues["jkmoving_destination_postalcode"].ToString(),
                                Destination_Country = fieldValues["jkmoving_destination_country"].ToString(),
                                Destination_ContactName = fieldValues["jkmoving_destination_contactname"].ToString(),
                                Destination_Telephone1 = fieldValues["jkmoving_destination_telephone1"].ToString(),
                                Destination_Telephone2 = fieldValues["jkmoving_destination_telephone2"].ToString(),
                                Destination_Telephone3 = fieldValues["jkmoving_destination_telephone3"].ToString(),

                                MoveStartDate = loadStartDate,
                                MoveEndDate = fieldValues["jkmoving_deliveryfrom"].ToString(),

                                WhatMattersMost = fieldValues["jkmoving_whatmattersmost"].ToString(),
                                ExcessValuation = fieldValues["jkmoving_declaredpropertyvalue"].ToString(),
                                ValuationDeductible = fieldValues["jkmoving_valuationdeductible"].ToString(),
                                ValuationCost = fieldValues["jkmoving_valuationcost"].ToString(),
                                //------- Below data is mocked as of now --------
                                ServiceCode = "stg_Packing,stg_Loading,stg_UnLoading"
                            }
                        });
                    }
                }
                else
                {
                    serviceResponse.Add(new ServiceResponse<Move> { Message = crmResponse["ERROR"] });
                }
                logger.Info(resourceManager.GetString("logDTOMapped"));
                return serviceResponse.FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.Error(ex.InnerException);
                serviceResponse.Add(new ServiceResponse<Move>
                {
                    Message = ex.ToString()
                });
                return serviceResponse.FirstOrDefault();
            }
        }

        /// <summary>
        /// Method Name     : GetConditionalStartDateForEstimates
        /// Author          : Pratik Soni
        /// Creation Date   : 03 Feb 2018
        /// Purpose         : To Get conditionally Start and End date for Move
        /// Revision        : 
        /// </summary>
        private void GetConditionalStartDateForMove(JObject fieldValues, ref string loadStartDate)
        {
            if (Convert.ToString(fieldValues["jkmoving_packfrom"]) == string.Empty)
            {
                loadStartDate = GetDateInStringFormat(Convert.ToString(fieldValues["jkmoving_loadfrom"]));
            }
            else
            {
                loadStartDate = GetDateInStringFormat(fieldValues["jkmoving_packfrom"].ToString());
            }
        }

        /// <summary>
        /// Method Name     : MapMoveIdResponseToDTO
        /// Author          : Ranjana Singh
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : Mapping CRM Response to Move DTO class.
        /// Revision        : 
        /// </summary>
        public ServiceResponse<Move> MapMoveIdResponseToDTO(Dictionary<string, string> crmResponse)
        {
            List<ServiceResponse<Move>> serviceResponse;
            serviceResponse = new List<ServiceResponse<Move>>();
            try
            {
                if (crmResponse.ContainsKey("STATUS"))
                {
                    if (crmResponse["CONTENT"] is null)
                    {
                        serviceResponse.Add(new ServiceResponse<Move> { Message = crmResponse["ERROR"] });
                        return serviceResponse.FirstOrDefault();
                    }
                    dynamic jsonObject = General.ConvertToJObject(crmResponse["CONTENT"].ToString());
                    if (!jsonObject["value"].HasValues)
                    {
                        serviceResponse.Add(new ServiceResponse<Move> { Information = resourceManager.GetString("CRM_STATUS_204") });
                    }
                    else
                    {
                        JObject fieldValues = jsonObject["value"][0];
                        serviceResponse.Add(new ServiceResponse<Move>
                        {
                            Data = new Move
                            {
                                MoveId = fieldValues["jkmoving_moveid"].ToString(),
                                MoveNumber = fieldValues["jkmoving_movenumber"].ToString(),
                                ContactOfMoveId = fieldValues["_jkmoving_contactofmoveid_value"].ToString(),
                                IsActive = fieldValues["statecode"].ToString()
                            }
                        });
                    }
                }
                else
                {
                    serviceResponse.Add(new ServiceResponse<Move> { Message = crmResponse["ERROR"] });
                }
            logger.Info(resourceManager.GetString("logDTOMapped"));
                return serviceResponse.FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.Error(ex.InnerException);
                serviceResponse.Add(new ServiceResponse<Move> { Message = ex.ToString() });
                return serviceResponse.FirstOrDefault();
            }
        }

        /// <summary>
        /// Method Name     : MapEstimateDataResponseToDTO
        /// Author          : Pratik Soni
        /// Creation Date   : 11 Jan 2018
        /// Purpose         : Mapping CRM Response to Estimate DTO class.
        /// Revision        : 
        /// </summary>
        /// <returns>Returns service response with list of Estimates if exist OR returns respective MESSAGE / INFORMATION</returns>
        public ServiceResponse<List<Estimate>> MapEstimateDataResponseToDTO(Dictionary<string, string> crmResponse)
        {
            ServiceResponse<List<Estimate>> serviceResponse;
            List<Estimate> estimateList;
            serviceResponse = new ServiceResponse<List<Estimate>>();
            try
            {
                if (crmResponse.ContainsKey("STATUS"))
                {
                    if (crmResponse["CONTENT"] is null)
                    {
                        serviceResponse = new ServiceResponse<List<Estimate>> { Message = crmResponse["ERROR"] };
                    }
                    dynamic jsonObject = General.ConvertToJObject(crmResponse["CONTENT"].ToString());
                    if (!jsonObject["value"].HasValues)
                    {
                        serviceResponse = new ServiceResponse<List<Estimate>> { Information = resourceManager.GetString("CRM_STATUS_204") };
                    }
                    else
                    {
                        estimateList = new List<Estimate>();
                        foreach (var fieldValues in jsonObject["value"])
                        {
                            estimateList.Add(new Estimate
                            {
                                MoveId = fieldValues["jkmoving_moveid"].ToString(),
                                MoveNumber = fieldValues["jkmoving_movenumber"].ToString(),

                                EstimatedLineHaul = fieldValues["jkmoving_estimatedlinehaul"].ToString(),
                                Deposit = fieldValues["jkmoving_deposit"].ToString(),

                                IsActive = fieldValues["statecode"].ToString(),
                                StatusReason = fieldValues["statuscode"].ToString(),

                                Origin_Street1 = fieldValues["jkmoving_origin_line1"].ToString(),
                                Origin_Street2 = fieldValues["jkmoving_origin_line2"].ToString(),
                                Origin_City = fieldValues["jkmoving_origin_city"].ToString(),
                                Origin_State = fieldValues["jkmoving_origin_stateorprovince"].ToString(),
                                Origin_PostalCode = fieldValues["jkmoving_origin_postalcode"].ToString(),
                                Origin_Country = fieldValues["jkmoving_origin_country"].ToString(),
                                Origin_ContactName = fieldValues["jkmoving_origin_contactname"].ToString(),
                                Origin_Telephone1 = fieldValues["jkmoving_origin_telephone1"].ToString(),
                                Origin_Telephone3 = fieldValues["jkmoving_origin_telephone3"].ToString(),
                                Origin_Telephone2 = fieldValues["jkmoving_origin_telephone2"].ToString(),

                                Destination_Street1 = fieldValues["jkmoving_destination_line1"].ToString(),
                                Destination_Street2 = fieldValues["jkmoving_destination_line2"].ToString(),
                                Destination_City = fieldValues["jkmoving_destination_city"].ToString(),
                                Destination_State = fieldValues["jkmoving_destination_stateorprovince"].ToString(),
                                Destination_PostalCode = fieldValues["jkmoving_destination_postalcode"].ToString(),
                                Destination_Country = fieldValues["jkmoving_destination_country"].ToString(),
                                Destination_ContactName = fieldValues["jkmoving_destination_contactname"].ToString(),
                                Destination_Telephone1 = fieldValues["jkmoving_destination_telephone1"].ToString(),
                                Destination_Telephone3 = fieldValues["jkmoving_destination_telephone3"].ToString(),
                                Destination_Telephone2 = fieldValues["jkmoving_destination_telephone2"].ToString(),

                                PackStartDate = fieldValues["jkmoving_packfrom"].ToString(),
                                LoadStartDate = fieldValues["jkmoving_loadfrom"].ToString(),
                                MoveStartDate = fieldValues["jkmoving_deliveryfrom"].ToString(),

                                WhatMattersMost = fieldValues["jkmoving_whatmattersmost"].ToString(),
                                ExcessValuation = fieldValues["jkmoving_declaredpropertyvalue"].ToString(),
                                ValuationDeductible = fieldValues["jkmoving_valuationdeductible"].ToString(),
                                ValuationCost = fieldValues["jkmoving_valuationcost"].ToString(),
                                //------- Below data is mocked as of now --------
                                ServiceCode = "stg_Packing,stg_Loading,stg_UnLoading"
                            });

                        }

                        serviceResponse = new ServiceResponse<List<Estimate>>
                        {
                            Data = estimateList
                        };
                    }
                }
                else
                {
                    serviceResponse = new ServiceResponse<List<Estimate>> { Message = crmResponse["ERROR"] };
                }
            logger.Info(resourceManager.GetString("logDTOMapped"));
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse = new ServiceResponse<List<Estimate>> { Message = ex.ToString() };
                return serviceResponse;
            }
        }

        /// <summary>
        /// Method Name     : MapEstimateIdResponseToDTO
        /// Author          : Pratik Soni
        /// Creation Date   : 11 Jan 2018
        /// Purpose         : Mapping CRM Response to Move DTO class.
        /// Revision        : 
        /// </summary>
        /// <returns>Returns Service response with list of Estimate Ids if exist OR returns respective MESSAGE / INFORMATION </returns>
        /// 
        public ServiceResponse<List<Estimate>> MapEstimateIdResponseToDTO(Dictionary<string, string> crmResponse)
        {
            ServiceResponse<List<Estimate>> serviceResponse;
            List<Estimate> estimateList;
            serviceResponse = new ServiceResponse<List<Estimate>>();
            try
            {
                if (crmResponse.ContainsKey("STATUS"))
                {
                    if (crmResponse["CONTENT"] is null)
                    {
                        serviceResponse = new ServiceResponse<List<Estimate>> { Message = crmResponse["ERROR"] };
                    }
                    dynamic jsonObject = General.ConvertToJObject(crmResponse["CONTENT"].ToString());
                    if (!jsonObject["value"].HasValues)
                    {
                        serviceResponse = new ServiceResponse<List<Estimate>> { Information = resourceManager.GetString("CRM_STATUS_204") };
                    }
                    else
                    {
                        estimateList = new List<Estimate>();
                        foreach (var fieldValues in jsonObject["value"])
                        {

                            estimateList.Add(new Estimate
                            {
                                MoveId = fieldValues["jkmoving_moveid"].ToString(),
                                MoveNumber = fieldValues["jkmoving_movenumber"].ToString()
                            });

                        }
                        serviceResponse = new ServiceResponse<List<Estimate>>
                        {
                            Data = estimateList
                        };
                    }
                }
                else
                {
                    serviceResponse = new ServiceResponse<List<Estimate>> { Message = crmResponse["ERROR"] };
                }
            logger.Info(resourceManager.GetString("logDTOMapped"));
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse = new ServiceResponse<List<Estimate>> { Message = ex.ToString() };
                return serviceResponse;
            }
        }

        /// <summary>
        /// Method Name     : MapEstimatePdfResponseToDTO
        /// Author          : Ranjana Singh
        /// Creation Date   : 16 Jan 2018
        /// Purpose         : Mapping CRM Response to Move DTO class.
        /// Revision        : 
        /// </summary>
        /// <returns>Returns Service response with list of Estimate Ids if exist OR returns respective MESSAGE / INFORMATION </returns>
        /// 
        public ServiceResponse<List<Estimate>> MapEstimatePdfResponseToDTO(Dictionary<string, string> crmResponse)
        {
            ServiceResponse<List<Estimate>> serviceResponse;
            List<Estimate> estimateList;
            serviceResponse = new ServiceResponse<List<Estimate>>();
            try
            {
                if (crmResponse.ContainsKey("STATUS"))
                {
                    if (crmResponse["CONTENT"] is null)
                    {
                        serviceResponse = new ServiceResponse<List<Estimate>> { Message = crmResponse["ERROR"] };
                    }
                    dynamic jsonObject = General.ConvertToJObject(crmResponse["CONTENT"].ToString());
                    if (!jsonObject["value"].HasValues)
                    {
                        serviceResponse = new ServiceResponse<List<Estimate>> { Information = resourceManager.GetString("CRM_STATUS_204") };
                    }
                    else
                    {
                        estimateList = new List<Estimate>();
                        foreach (var fieldValues in jsonObject["value"])
                        {

                            estimateList.Add(new Estimate
                            {
                                MoveId = fieldValues["jkmoving_moveid"].ToString(),
                            });

                        }
                        serviceResponse = new ServiceResponse<List<Estimate>>
                        {
                            Data = estimateList
                        };
                    }
                }
                else
                {
                    serviceResponse = new ServiceResponse<List<Estimate>> { Message = crmResponse["ERROR"] };
                }
            logger.Info(resourceManager.GetString("logDTOMapped"));
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse = new ServiceResponse<List<Estimate>> { Message = ex.ToString() };
                return serviceResponse;
            }
        }

        /// <summary>
        /// Method Name     : MapMoveCustomerResponseToDTO
        /// Author          : Ranjana Singh
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Mapping CRM Response to Move DTO class.
        /// Revision        : 
        /// </summary>
        public ServiceResponse<Move> MapMoveCustomerResponseToDTO(Dictionary<string, string> crmResponse)
        {
            List<ServiceResponse<Move>> serviceResponse;
            serviceResponse = new List<ServiceResponse<Move>>();
            try
            {
                if (crmResponse.ContainsKey("STATUS"))
                {
                    if (crmResponse["CONTENT"] is null)
                    {
                        serviceResponse.Add(new ServiceResponse<Move> { Message = crmResponse["ERROR"] });
                        return serviceResponse.FirstOrDefault();
                    }
                    dynamic jsonObject = General.ConvertToJObject(crmResponse["CONTENT"].ToString());
                    if (!jsonObject["value"].HasValues)
                    {
                        serviceResponse.Add(new ServiceResponse<Move> { Information = resourceManager.GetString("CRM_STATUS_204") });
                    }
                    else
                    {
                        JObject fieldValues = jsonObject["value"][0];
                        serviceResponse.Add(new ServiceResponse<Move>
                        {
                            Data = new Move
                            {
                                CustomerEmailAddress = fieldValues["emailaddress1"].ToString(),
                                CustomerContactNumber = fieldValues["telephone1"].ToString(),
                            }
                        });
                    }
                }
                else
                {
                    serviceResponse.Add(new ServiceResponse<Move> { Message = crmResponse["ERROR"] });
                }
            logger.Info(resourceManager.GetString("logDTOMapped"));
                return serviceResponse.FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.Error(ex.InnerException);
                serviceResponse.Add(new ServiceResponse<Move> { Message = ex.ToString() });
                return serviceResponse.FirstOrDefault();
            }
        }

        /// <summary>
        /// Method Name     : MapCustomerResponseToDTO
        /// Author          : Pratik Soni
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Mapping CRM Response to Customer DTO class.
        /// Revision        : 
        /// </summary>
        public ServiceResponse<Customer> MapCustomerResponseToDTO(Dictionary<string, string> crmResponse)
        {
            ServiceResponse<Customer> serviceResponse;
            serviceResponse = new ServiceResponse<Customer>();
            StringBuilder fullName = new StringBuilder();

            dynamic jsonObject = General.ConvertToJObject(crmResponse["CONTENT"].ToString());
            Customer dtoCustomer = new Customer();

            JObject fieldValues = jsonObject["value"][0];

            foreach (JProperty property in fieldValues.Properties())
            {
                // Each field of request body for Customer DTO should be added as a "case".
                switch (property.Name)
                {
                    case "jkmoving_customernumber":
                        dtoCustomer.CustomerId = property.Value.ToString();
                        break;
                    case "firstname":
                    case "lastname":
                        fullName.Append(property.Value.ToString() + " ");
                        break;
                    case "emailaddress1":
                        dtoCustomer.EmailId = property.Value.ToString();
                        break;
                    case "onerivet_passwordhash":
                        dtoCustomer.PasswordHash = property.Value.ToString();
                        break;
                    case "onerivet_passwordsalt":
                        dtoCustomer.PasswordSalt = property.Value.ToString();
                        break;
                    case "telephone1":
                        dtoCustomer.Phone = property.Value.ToString();
                        break;
                    case "onerivet_verificationcode":
                        dtoCustomer.VerificationCode = Validations.IsValid(property.Value.ToString()) ? int.Parse(property.Value.ToString()) : 0;
                        break;
                    case "onerivet_codevalidtill":
                        if (Validations.IsValid(property.Value.ToString()))
                        {
                            string formattedDateTime = property.Value.ToObject<DateTime>().ToString("MM-dd-yyyy HH:mm:ss");
                            IFormatProvider culture = new CultureInfo("en-US", true);
                            dtoCustomer.CodeValidTill = DateTime.ParseExact(formattedDateTime, "MM-dd-yyyy HH:mm:ss", culture);
                        }
                        break;
                    case "onerivet_termsagreed":
                        dtoCustomer.TermsAgreed = Validations.IsValid(property.Value.ToString()) && bool.Parse(property.Value.ToString());
                        break;
                    case "preferredcontactmethodcode":
                        dtoCustomer.PreferredContact = property.Value.ToString();
                        break;
                    case "onerivet_receivenotification":
                        dtoCustomer.ReceiveNotifications = Validations.IsValid(property.Value.ToString()) && bool.Parse(property.Value.ToString());
                        break;
                    case "onerivet_iscustomerregistered":
                        dtoCustomer.IsCustomerRegistered = Validations.IsValid(property.Value.ToString()) && bool.Parse(property.Value.ToString());
                        break;
                }
            }
            dtoCustomer.CustomerFullName = fullName.ToString().Trim();

            serviceResponse.Data = dtoCustomer;
            logger.Info(resourceManager.GetString("logDTOMapped"));
            return serviceResponse;
        }

        /// <summary>
        /// Method Name     : MapPaymentResponseToDTO
        /// Author          : Ranjana Singh
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : Mapping CRM Response to Payment DTO class.
        /// Revision        : 
        /// </summary>
        public ServiceResponse<Payment> MapPaymentResponseToDTO(Dictionary<string, string> crmResponse)
        {
            ServiceResponse<Payment> serviceResponse;
            serviceResponse = new ServiceResponse<Payment>();

            dynamic jsonObject = General.ConvertToJObject(crmResponse["CONTENT"].ToString());
            Payment dtoPayment = new Payment();

            JObject fieldValues = jsonObject["value"][0];

            if (Convert.ToString(fieldValues["jkmoving_actuallinehaul"]) == string.Empty)
            {
                dtoPayment.TotalCost = Convert.ToString(fieldValues["jkmoving_estimatedlinehaul"]);
            }
            else
            {
                dtoPayment.TotalCost = fieldValues["jkmoving_actuallinehaul"].ToString();
            }

            foreach (JProperty property in fieldValues.Properties())
            {
                // Each field of request body for Customer DTO should be added as a "case".
                switch (property.Name)
                {
                    case "nodus_securitycode":
#warning below line is mocked  // property.Value.ToString();
                        dtoPayment.DeviceID = "a3eef35e-02d9-48f8-977c-cc0dc8df1cb6|1rivet123";
                        break;
                    case "jkmoving_totalpaid":
                        dtoPayment.TotalPaid = property.Value.ToString();
                        break;
                    case "jkmoving_deposit":
                        dtoPayment.Deposit = property.Value.ToString();
                        break;
                    case "jkmoving_totalremaining":
                        dtoPayment.TotalDue = property.Value.ToString();
                        break;
                }
            }

            serviceResponse.Data = dtoPayment;
            logger.Info(resourceManager.GetString("logDTOMapped"));
            return serviceResponse;
        }


        /// <summary>
        /// Method Name     : MapMoveDetailsResponseToDTO
        /// Author          : Pratik Soni
        /// Creation Date   : 08 Feb 2018
        /// Purpose         : Mapping CRM Response to Move DTO class.
        /// Revision        : 
        /// </summary>
        public ServiceResponse<Move> MapMoveDetailsResponseToDTO(Dictionary<string, string> crmResponse)
        {
            ServiceResponse<Move> serviceResponse;
            serviceResponse = new ServiceResponse<Move>();

            dynamic jsonObject = General.ConvertToJObject(crmResponse["CONTENT"].ToString());
            Move dtoMove = new Move();

            JObject fieldValues = jsonObject["value"][0];
            string loadStartDate = string.Empty;

            //Get conditionally Start date for Move
            if (fieldValues["jkmoving_loadfrom"] != null || fieldValues["jkmoving_packfrom"] != null)
            {
                GetConditionalStartDateForMove(fieldValues, ref loadStartDate);
                dtoMove.MoveStartDate = loadStartDate;
            }

            foreach (JProperty property in fieldValues.Properties())
            {
                // Each field of request body for Customer DTO should be added as a "case".
                switch (property.Name)
                {
                    case "jkmoving_moveid":
                        dtoMove.MoveId = property.Value.ToString();
                        break;
                    case "jkmoving_movenumber":
                        dtoMove.MoveNumber = property.Value.ToString();
                        break;
                    case "statecode":
                        dtoMove.IsActive = property.Value.ToString();
                        break;
                    case "statuscode":
                        dtoMove.StatusReason = property.Value.ToString();
                        break;
                    case "jkmoving_origin_line1":
                        dtoMove.Origin_Street1 = property.Value.ToString();
                        break;
                    case "jkmoving_origin_line2":
                        dtoMove.Origin_Street2 = property.Value.ToString();
                        break;
                    case "jkmoving_origin_city":
                        dtoMove.Origin_City = property.Value.ToString();
                        break;
                    case "jkmoving_origin_stateorprovince":
                        dtoMove.Origin_State = property.Value.ToString();
                        break;
                    case "jkmoving_origin_postalcode":
                        dtoMove.Origin_PostalCode = property.Value.ToString();
                        break;
                    case "jkmoving_origin_country":
                        dtoMove.Origin_Country = property.Value.ToString();
                        break;
                    case "jkmoving_origin_contactname":
                        dtoMove.Origin_ContactName = property.Value.ToString();
                        break;
                    case "jkmoving_origin_telephone1":
                        dtoMove.Origin_Telephone1 = property.Value.ToString();
                        break;
                    case "jkmoving_origin_telephone2":
                        dtoMove.Origin_Telephone2 = property.Value.ToString();
                        break;
                    case "jkmoving_origin_telephone3":
                        dtoMove.Origin_Telephone3 = property.Value.ToString();
                        break;
                    case "jkmoving_destination_line1":
                        dtoMove.Destination_Street1 = property.Value.ToString();
                        break;
                    case "jkmoving_destination_line2":
                        dtoMove.Destination_Street2 = property.Value.ToString();
                        break;
                    case "jkmoving_destination_city":
                        dtoMove.Destination_City = property.Value.ToString();
                        break;
                    case "jkmoving_destination_stateorprovince":
                        dtoMove.Destination_State = property.Value.ToString();
                        break;
                    case "jkmoving_destination_postalcode":
                        dtoMove.Destination_PostalCode = property.Value.ToString();
                        break;
                    case "jkmoving_destination_country":
                        dtoMove.Destination_Country = property.Value.ToString();
                        break;
                    case "jkmoving_destination_contactname":
                        dtoMove.Destination_ContactName = property.Value.ToString();
                        break;
                    case "jkmoving_destination_telephone1":
                        dtoMove.Destination_Telephone1 = property.Value.ToString();
                        break;
                    case "jkmoving_destination_telephone2":
                        dtoMove.Destination_Telephone2 = property.Value.ToString();
                        break;
                    case "jkmoving_destination_telephone3":
                        dtoMove.Destination_Telephone3 = property.Value.ToString();
                        break;
                    case "jkmoving_loadfrom":
                        dtoMove.MoveDetails_LoadStartDate = GetDateInStringFormat(property.Value.ToString());
                        break;
                    case "jkmoving_packfrom":
                        dtoMove.MoveDetails_PackStartDate = GetDateInStringFormat(property.Value.ToString());
                        break;
                    case "jkmoving_deliveryfrom":
                        string deliveryFromDate = GetDateInStringFormat(property.Value.ToString());
                        dtoMove.MoveEndDate = deliveryFromDate;
                        dtoMove.MoveDetails_DeliveryStartDate = deliveryFromDate;
                        break;
                    case "jkmoving_deliveryto":
                        string deliveryToDate = GetDateInStringFormat(property.Value.ToString());
                        dtoMove.MoveDetails_DeliveryEndDate = deliveryToDate;
                        break;
                    case "jkmoving_whatmattersmost":
                        dtoMove.WhatMattersMost = property.Value.ToString();
                        break;
                    case "jkmoving_declaredpropertyvalue":
                        dtoMove.ExcessValuation = property.Value.ToString();
                        break;
                    case "jkmoving_valuationdeductible":
                        dtoMove.ValuationDeductible = property.Value.ToString();
                        break;
                    case "jkmoving_valuationcost":
                        dtoMove.ValuationCost = property.Value.ToString();
                        break;
                    case "internalemailaddress":
                        dtoMove.MoveCoordinator_EmailAddress = property.Value.ToString();
                        break;
                    case "address1_telephone":
                        dtoMove.MoveCoordinator_ContactNumber = property.Value.ToString();
                        break;
                }
            }
            //------- Below data is mocked as of now --------
            dtoMove.ServiceCode = "stg_Packing,stg_Loading,stg_UnLoading";

            serviceResponse.Data = dtoMove;
            logger.Info(resourceManager.GetString("logDTOMapped"));
            return serviceResponse;
        }

        /// <summary>
        /// Method Name     : MapAlertResponseToDTO
        /// Author          : Pratik Soni
        /// Creation Date   : 01 Feb 2018
        /// Purpose         : Mapping CRM Response to Alert DTO class.
        /// Revision        : 
        /// </summary>
        public ServiceResponse<List<Alert>> MapAlertResponseToDTO(Dictionary<string, string> crmResponse)
        {

            ServiceResponse<List<Alert>> serviceResponse = new ServiceResponse<List<Alert>>();
            IFormatProvider culture = new CultureInfo("en-US", true);
            JObject alert;
            try
            {

                dynamic jsonObject = General.ConvertToJObject(crmResponse["CONTENT"].ToString());
                List<Alert> dtoAlertList = new List<Alert>();
                Alert dtoAlert = new Alert();

                JArray alertList = JArray.Parse(jsonObject["value"].ToString());
                for (int count = 0; count < alertList.Count; count++)
                {
                    alert = JObject.Parse(alertList[count].ToString());
                    foreach (JProperty property in alert.Properties())
                    {
                        // Each field of request body for Customer DTO should be added as a "case".
                        switch (property.Name)
                        {
                            case "activityid":
                                dtoAlert.AlertID = property.Value.ToString();
                                break;
                            case "regardingobjectid":
                                dtoAlert.CustomerID = property.Value.ToString();
                                break;
                            case "subject":
                                dtoAlert.AlertTitle = property.Value.ToString();
                                break;
                            case "description":
                                dtoAlert.AlertDescription = property.Value.ToString();
                                break;
                            case "actualstart":
                                string formattedStartDateTime = property.Value.ToObject<DateTime>().ToString("MM-dd-yyyy HH:mm:ss");
                                dtoAlert.StartDate = DateTime.ParseExact(formattedStartDateTime, "MM-dd-yyyy HH:mm:ss", culture);
                                break;
                            case "actualend":
                                if (Validations.IsValid(property.Value))
                                {
                                    string formattedEndDateTime = property.Value.ToObject<DateTime>().ToString("MM-dd-yyyy HH:mm:ss");
                                    dtoAlert.EndDate = DateTime.ParseExact(formattedEndDateTime, "MM-dd-yyyy HH:mm:ss", culture);
                                }
                                break;
                            case "onerivet_notificationtype":
                                dtoAlert.NotificationType = property.Value.ToString();
                                break;
                            case "statecode":
                                dtoAlert.IsActive = property.Value.ToString();
                                break;
                        }
                    }
                    dtoAlertList.Add(dtoAlert);
                }
                serviceResponse.Data = dtoAlertList;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString(), ex);
                return new ServiceResponse<List<Alert>> { };
            }
        }

        /// <summary>
        /// Method Name     : ValidateResponse
        /// Author          : Pratik Soni
        /// Creation Date   : 24 Jan 2018
        /// Purpose         : Validates CRM's dictionary's response.
        /// Revision        : 
        /// </summary>
        public ServiceResponse<T> ValidateResponse<T>(Dictionary<string, string> crmResponse)
        {
            ServiceResponse<T> serviceResponse;
            serviceResponse = new ServiceResponse<T>();

            if (crmResponse.ContainsKey("STATUS"))
            {
                if (crmResponse["CONTENT"] is null)
                {
                    serviceResponse = new ServiceResponse<T> { Message = crmResponse["ERROR"] };
                    return serviceResponse;
                }
                dynamic jsonObject = General.ConvertToJObject(crmResponse["CONTENT"].ToString());
                if (!jsonObject["value"].HasValues)
                {
                    serviceResponse = new ServiceResponse<T> { Information = resourceManager.GetString("CRM_STATUS_204") };
                }
            }
            else
            {
                serviceResponse = new ServiceResponse<T>
                {
                    Message = crmResponse["ERROR"]
                };
            }
            return serviceResponse;
        }

        /// <summary>
        /// Method Name     : MapDocumentListResponseToDTO
        /// Author          : Ranjana Singh
        /// Creation Date   : 09 Feb 2018
        /// Purpose         : Mapping CRM Response to Document DTO class.
        /// Revision        : 
        /// </summary>
        /// <returns>Returns Service response with list of Document Ids if exist OR returns respective MESSAGE / INFORMATION </returns>
        /// 
        public ServiceResponse<List<Document>> MapDocumentListResponseToDTO(Dictionary<string, string> crmResponse)
        {
            ServiceResponse<List<Document>> serviceResponse;

            dynamic jsonObject = General.ConvertToJObject(crmResponse["CONTENT"].ToString());
            List<Document> documentList = new List<Document>();

            foreach (var fieldValues in jsonObject["value"])
            {
                documentList.Add(new Document
                {
                    DocumentTitle = fieldValues["name"].ToString(),
                    DocumentType = null,
                    RelativeUrl = fieldValues["relativeurl"].ToString()
                });
            }
            serviceResponse = new ServiceResponse<List<Document>>
            {
                Data = documentList
            };

            serviceResponse.Data = documentList;
            logger.Info(resourceManager.GetString("logDTOMapped"));
            return serviceResponse;
        }

        /// <summary>
        /// Method Name     : MapMoveListResponseToDTO
        /// Author          : Pratik Soni
        /// Creation Date   : 15 Feb 2018
        /// Purpose         : Mapping CRM Response to Document DTO class.
        /// Revision        : 
        /// </summary>
        /// <returns>Returns Service response with list of Document Ids if exist OR returns respective MESSAGE / INFORMATION </returns>
        /// 
        public ServiceResponse<List<Move>> MapMoveListResponseToDTO(Dictionary<string, string> crmResponse)
        {
            ServiceResponse<List<Move>> serviceResponse;

            dynamic jsonObject = General.ConvertToJObject(crmResponse["CONTENT"].ToString());
            List<Move> moveList = new List<Move>();

            foreach (var fieldValues in jsonObject["value"])
            {
                string deliveryFromDate = GetDateInStringFormat(fieldValues["jkmoving_deliveryfrom"].ToString());
                string deliveryToDate = GetDateInStringFormat(fieldValues["jkmoving_deliveryto"].ToString());

                moveList.Add(new Move
                {
                    MoveId = fieldValues["jkmoving_moveid"].ToString(),
                    MoveNumber = fieldValues["jkmoving_movenumber"].ToString(),
                    IsActive = fieldValues["statecode"].ToString(),
                    ContactOfMoveId = fieldValues["_jkmoving_contactofmoveid_value"].ToString(),
                    MoveDetails_LoadStartDate = fieldValues["jkmoving_loadfrom"].ToString(),
                    MoveDetails_PackStartDate = fieldValues["jkmoving_packfrom"].ToString(),
                    MoveEndDate = deliveryFromDate,
                    MoveDetails_DeliveryStartDate = deliveryFromDate,
                    MoveDetails_DeliveryEndDate = deliveryToDate
                });
            }
            serviceResponse = new ServiceResponse<List<Move>>
            {
                Data = moveList
            };

            serviceResponse.Data = moveList;
            return serviceResponse;
        }
    }
}