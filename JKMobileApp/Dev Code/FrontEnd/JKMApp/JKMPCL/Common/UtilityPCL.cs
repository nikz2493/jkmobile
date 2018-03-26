using JKMPCL.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JKMPCL.Services
{
    /// <summary>
    /// Class Name      : UtilityPCL
    /// Author          : Hiren Patel
    /// Creation Date   : 10 Dec 2017
    /// Purpose         : For common validation and other stuff 
    /// Revision        : 
    /// </summary>
    public static class UtilityPCL
    {
        public static CustomerModel LoginCustomerData { get; set; }
        public static MoveDataModel CustomerMoveData { get; set; }
        public static string selectedMoveNumber { get; set; }
        public static DocumentModel selectedDocumentModel { get; set; }

        public enum CardType
        {
            Unknown,
            MasterCard,
            VISA,
            Amex,
            Discover,
            DinersClub,
            JCB,
            enRoute
        }

        /// <summary>
        /// Method Name     : SetLoginCustomerProfileData
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : To store cutomer information as login customer data 
        /// Revision        : 
        /// </summary>
        public static void SetLoginCustomerProfileData(CustomerModel customerModel)
        {
            LoginCustomerData.CodeValidTill = customerModel.CodeValidTill;
            LoginCustomerData.CustomerId = customerModel.CustomerId;
            LoginCustomerData.EmailId = customerModel.EmailId;
            LoginCustomerData.IsCustomerRegistered = customerModel.IsCustomerRegistered;
            LoginCustomerData.LastLoginDate = customerModel.LastLoginDate;
            LoginCustomerData.PasswordHash = customerModel.PasswordHash;
            LoginCustomerData.PasswordSalt = customerModel.PasswordSalt;
            LoginCustomerData.Phone = customerModel.Phone;
            LoginCustomerData.PreferredContact = customerModel.PreferredContact;
            LoginCustomerData.ReceiveNotifications = customerModel.ReceiveNotifications;
            LoginCustomerData.TermsAgreed = customerModel.TermsAgreed;
            LoginCustomerData.VerificationCode = customerModel.VerificationCode;
            LoginCustomerData.CustomerFullName = customerModel.CustomerFullName;
        }

        /// <summary>
        /// Method Name     : SetCustomerMoveData
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : To store cutomer move information 
        /// Revision        : 
        /// </summary>
        public static void SetCustomerMoveData(GetMoveDataResponse moveDataModel)
        {
            if (moveDataModel is null)
            {
                return;
            }
            else
            {
                SetMoveData(moveDataModel);
                SetMyServiceModel(moveDataModel);
                SetDaysLeft(moveDataModel);
            }
        }

        /// <summary>
        /// Method Name     : SetMyServiceModel
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 22 Dec 2017
        /// Purpose         : Bind service list in customer move model
        /// Revision        : Modified by Vivek Bhavsar on  05 Feb 2018 : Add try catch block
        /// </summary>
        /// <param name="moveDataModel"></param>
        private static void SetMyServiceModel(GetMoveDataResponse moveDataModel)
        {
            try
            {
                CustomerMoveData.MyServices = new List<MyServicesModel>();
                if (moveDataModel.MyServices != null && moveDataModel.MyServices.Count > 0)
                {
                    MyServicesModel myServiceModel;
                    foreach (MyServices myService in moveDataModel.MyServices)
                    {
                        myServiceModel = new MyServicesModel { ServicesCode = myService.ServicesCode };
                        CustomerMoveData.MyServices.Add(myServiceModel);
                    }
                }
            }
            catch
            {
                //To be implemented
            }
        }

        /// <summary>
        /// Method Name     : SetDaysLeft
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 22 Dec 2017
        /// Purpose         : Bind days diff between load start & end date to customer move model days left
        /// Revision        : Modified by Vivek Bhavsar on  05 Feb 2018 : Add try catch block
        /// </summary>
        /// <param name="moveDataModel"></param>
        private static void SetDaysLeft(GetMoveDataResponse moveDataModel)
        {
            try
            {
                if (string.IsNullOrEmpty(moveDataModel.MoveStartDate) && string.IsNullOrEmpty(moveDataModel.MoveEndDate))
                {
                    DateTime firstLoadDate = Convert.ToDateTime(moveDataModel.MoveStartDate);
                    DateTime lastLoadDate = Convert.ToDateTime(moveDataModel.MoveEndDate);
                    CustomerMoveData.daysLeft = Convert.ToString((firstLoadDate - lastLoadDate).TotalDays);
                }
            }
            catch
            {
                CustomerMoveData.daysLeft = "";
            }
        }

        /// <summary>
        /// Method Name     : SetMoveData
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 22 Dec 2017
        /// Purpose         : Bind Move data to customer move model
        /// Revision        : Modified by Vivek Bhavsar on  05 Feb 2018 : Add try catch block
        /// </summary>
        /// <param name="moveDataModel"></param>
        private static void SetMoveData(GetMoveDataResponse moveDataModel)
        {
            try
            {
                if (CustomerMoveData is null)
                    CustomerMoveData = new MoveDataModel();

                CustomerMoveData.MoveId = moveDataModel.MoveId;
                CustomerMoveData.MoveNumber = moveDataModel.MoveNumber;
                CustomerMoveData.IsActive = moveDataModel.IsActive;
                CustomerMoveData.StatusReason = moveDataModel.StatusReason;
                CustomerMoveData.Destination_City = moveDataModel.Destination_City;
                CustomerMoveData.CustomDestinationAddress = moveDataModel.CustomDestinationAddress;
                CustomerMoveData.Origin_City = moveDataModel.Origin_City;
                CustomerMoveData.CustomOriginAddress = moveDataModel.CustomOriginAddress;
                CustomerMoveData.MoveStartDate = moveDataModel.MoveStartDate;
                CustomerMoveData.MoveEndDate = moveDataModel.MoveEndDate;
                CustomerMoveData.WhatMattersMost = moveDataModel.WhatMattersMost;
                CustomerMoveData.ExcessValuation = moveDataModel.ExcessValuation;
                CustomerMoveData.ValuationDeductible = moveDataModel.ValuationDeductible;
                CustomerMoveData.ValuationCost = moveDataModel.ValuationCost;
                CustomerMoveData.ServiceCode = moveDataModel.ServiceCode;
            }
            catch
            {
                //To be implemented
            }
        }

        /// <summary>
        /// Method Name     : RefreshCustomerProfileData
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : for Refresh Customer profile Data
        /// Revision        : 
        /// </summary>
        public async static Task<string> RefreshCustomerProfileData()
        {
            LoginAPIServies loginAPIServies = new LoginAPIServies();
            APIResponse<CustomerModel> response = await loginAPIServies.GetCustomerProfileData(LoginCustomerData);

            if (!response.STATUS)
            {
                return response.Message;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Method Name     : ValidateEmail
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Check Valid Email Address 
        /// Revision        : 
        /// </summary>
        public static bool ValidateEmail(string strEmail)
        {
            Regex regex;
            Match match;

            regex = new Regex(Resource.EmailRegex);
            match = regex.Match(strEmail);

            return match.Success;
        }

        /// <summary>
        /// Method Name     : GetMoveDataDispalyValue
        /// Author          : Sanket Prajapati
        /// Creation Date   : 27 Dec 2017
        /// Purpose         : for Display Move Data Value agains Move Data Code 
        /// Revision        : By Vivek Bhavsar on  05 Feb 2018 : Add try catch block
        /// </summary>
        public static string GetMoveDataDisplayValue(string keyName, string keytype)
        {
            string resourceValue = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(keyName))
                {
                    return keyName;
                }
                else
                {
                    resourceValue = MoveDataDisplayResource.ResourceManager.GetString(keytype + keyName);
                    return resourceValue;
                }
            }
            catch
            {
                return resourceValue;
            }

        }

        /// <summary>
        /// Method Name     : CurrencyFormat
        /// Author          : Hiren Patel
        /// Creation Date   : 27 Dec 2017
        /// Purpose         : display currency in en-us particular format
        /// Revision        : By Vivek Bhavsar on  05 Feb 2018 : Add try catch block
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string CurrencyFormat(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    return 0.ToString("C", new CultureInfo("en-US"));
                }
                else
                {
                    text = RemoveCurrencyFormat(text);
                    return Convert.ToDecimal(text).ToString("C", new CultureInfo("en-US"));
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Method Name     : CurrencyFormat
        /// Author          : Hiren Patel
        /// Creation Date   : 27 Dec 2017
        /// Purpose         : display currency in en-us particular format
        /// Revision        : By Vivek Bhavsar on  05 Feb 2018 : Add try catch block
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveCurrencyFormat(string text)
        {
            try
            {
                if (!string.IsNullOrEmpty(text))
                {
                    text = text.Replace("$", string.Empty);
                    text = text.Replace(",", string.Empty);
                    return text;
                }
                else
                {
                    return "0";
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Method Name     : CalulateMoveDays
        /// Author          : Hiren Patel
        /// Creation Date   : 27 Dec 2017
        /// Purpose         : Calculate days for Move
        /// Revision        : By Vivek Bhavsar on  05 Feb 2018 : Add try catch block
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public static string CalulateMoveDays(DateTime startDate)
        {
            string leftDays = string.Empty;
            DateTime currentDate;
            try
            {
                currentDate = ConvertDateTimeInUSFormat(DateTime.Today);
                if (startDate <= currentDate)
                {
                    leftDays = string.Empty;
                }
                else
                {
                    int days = (startDate - currentDate).Days;

                    if (days < 10 && days > 0)
                    {
                        leftDays = string.Format("0{0}", days);
                    }
                    else
                    {
                        leftDays = string.Format("{0}", days);
                    }
                }

                return leftDays;
            }
            catch
            {
                return leftDays;
            }
        }

        /// <summary>
        /// Method Name     : ConvertDateTimeInUSFormat
        /// Author          : Hiren Patel
        /// Creation Date   : 27 Dec 2017
        /// Purpose         : Convert string date into US format datetime
        /// Revision        : By Vivek Bhavsar on  05 Feb 2018 : Add try catch block
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ConvertDateTimeInUSFormat(string dateTime)
        {
            try
            {
                if (string.IsNullOrEmpty(dateTime))
                {
                    return DateTime.MinValue;
                }
                else
                {
                    return Convert.ToDateTime(dateTime, new CultureInfo("en-US"));
                }
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Method Name     : ConvertDateTimeInUSFormat
        /// Author          : Hiren Patel
        /// Creation Date   : 27 Dec 2017
        /// Purpose         : Convert date into US format datetime
        /// Revision        : By Vivek Bhavsar on  05 Feb 2018 : Add try catch block
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ConvertDateTimeInUSFormat(DateTime dateTime)
        {
            try
            {
                return Convert.ToDateTime(dateTime, new CultureInfo("en-US"));
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Method Name     : DisplayDateFormatForEstimate
        /// Author          : Hiren Patel
        /// Creation Date   : 15 Jan 2018
        /// Purpose         : Display date in to specific format (Aknowledgement screen in estimates)
        /// Revision        : 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="dateFormat"></param>
        /// <returns></returns>
        public static string DisplayDateFormatForEstimate(DateTime? dateTime, string dateFormat)
        {
            if (dateTime.HasValue)
            {
                return (dateTime.Value.Month.ToString("00") + "/" + dateTime.Value.Day.ToString("00") + "/" + dateTime.Value.Year).ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Method Name     : ValidateCreditCard
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 31 Jan 2018
        /// Purpose         : check for credit card no by validating it with regular expreession
        /// Revision        : By Vivek Bhavsar on  05 Feb 2018 : Add try catch block
        /// Revision        : By Jitendra Garg on 20 Feb 2018 : Update the regex to include 12-19 characters, instead of validating for just Visa and Mastercard
        /// </summary>
        /// <param name="ValidateCreditCard"></param>
        /// <returns></returns>
        public static bool ValidateCard(string creditCardNumber)
        {
            try
            {
                Regex regexCard = new Regex(Resource.CreditCardValidationRegex);
                Match match = regexCard.Match(creditCardNumber);

                return match.Success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Method Name     : GetCardType
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 31 Jan 2018
        /// Purpose         : get type of card VISA,MASTER etc.
        /// Revision        : By Vivek Bhavsar on  05 Feb 2018 : Add try catch block
        /// https://stackoverflow.com/questions/72768/how-do-you-detect-credit-card-type-based-on-number
        /// </summary>
        /// <param name="creditCardNumber"></param>
        /// <returns></returns>
        public static CardType GetCardType(string creditCardNumber)
        {
            try
            {
                creditCardNumber = creditCardNumber.Replace("-", "").Replace(" ", "").Trim();
                foreach (CardTypeInfoModel cardType in GetCardTypes())
                {
                    if (Regex.IsMatch(creditCardNumber, cardType.Regex))
                    {
                        return cardType.CardType;
                    }
                }

                return CardType.Unknown;
            }
            catch
            {
                return CardType.Unknown;
            }
        }

        /// <summary>
        /// Method Name     : GetCardTypes
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 31 Jan 2018
        /// Purpose         : list of card types
        /// Revision        : 
        /// </summary>
        /// <returns></returns>
        public static List<CardTypeInfoModel> GetCardTypes()
        {
            List<CardTypeInfoModel> cardTypeInfo = new List<CardTypeInfoModel>();
            cardTypeInfo.Add(new CardTypeInfoModel { Regex = Resource.MasterCardTypeRegex, CardNumberLength = 16, CardType = CardType.MasterCard });
            cardTypeInfo.Add(new CardTypeInfoModel { Regex = Resource.VisaCardTypeRegex, CardNumberLength = 16, CardType = CardType.VISA });
            cardTypeInfo.Add(new CardTypeInfoModel { Regex = Resource.VisaCardTypeRegex, CardNumberLength = 13, CardType = CardType.VISA });

            return cardTypeInfo;
        }

        /// <summary>
        /// Method Name     : DisplayPhoneFormat
        /// Author          : Hiren Patel
        /// Creation Date   : 15 Jan 2018
        /// Purpose         : Display phone number in US format
        /// Revision        : 
        /// </summary>
        /// <param name="phoneNumer"></param>
        /// <returns></returns>
        public static string DisplayPhoneFormat(string phoneNumer)
        {
            string formatedNumber = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(phoneNumer))
                {
                    return formatedNumber;
                }
                else
                {
                    var numbers = Regex.Replace(phoneNumer, @"\D", "");

                    if (numbers.Length <= 3)
                    {
                        formatedNumber = numbers;
                    }
                    else if (numbers.Length <= 7)
                    {
                        formatedNumber = string.Format("({0}) {1}", numbers.Substring(0, 3), numbers.Substring(3));
                    }
                    else
                    {
                        formatedNumber = string.Format("({0}) {1}-{2}", numbers.Substring(0, 3), numbers.Substring(3, 3), numbers.Substring(6, 4));
                    }
                }

                return formatedNumber;
            }
            catch (Exception)
            {
                return formatedNumber;
            }
        }

        public static string DisplayCraditCardFormat(string CardNumer)
        {
            string formatedNumber = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(CardNumer))
                {
                    return formatedNumber;
                }
                else
                {
                    var numbers = Regex.Replace(CardNumer, @"\D", "");

                    if (numbers.Length == 4)
                    {
                        formatedNumber = string.Format("{0}-", numbers.Substring(0, 4));
                    }
                    else if (numbers.Length == 9)
                    {
                        formatedNumber = string.Format("{0}-{1}", numbers.Substring(0, 4), numbers.Substring(4));
                    }
                    else if (numbers.Length == 13)
                    {
                        formatedNumber = string.Format("{0}-{1}-{2}", numbers.Substring(0, 4), numbers.Substring(9, 4), numbers.Substring(13, 4));
                    }
                }

                return formatedNumber;
            }
            catch (Exception)
            {
                return formatedNumber;
            }
        }

        public static bool IsMoveActive(string moveStatusCode)
        {
            if (MoveDataDisplayResource.MoveActiveCodeStatus == moveStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method Name     : IsNullOrEmptyOrWhiteSpace
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Jan 2018
        /// Purpose         : Is the null or empty or white space.
        /// Revision        : 
        /// </summary>
        /// <returns><c>true</c>, if null or empty or white space, <c>false</c> otherwise.</returns>
        /// <param name="value">Value.</param>
        public static bool IsNullOrEmptyOrWhiteSpace(string value)
        {
            return (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value));
        }

        public static List<ValuationDeductibleModel> ValuationDeductibleList()
        {
            List<ValuationDeductibleModel> valueList;
            valueList = new List<ValuationDeductibleModel>();
            valueList.Add(new ValuationDeductibleModel() { Index = 0, DeductibleCode = MoveDataDisplayResource.Deductible10000000, DeductibleName = MoveDataDisplayResource.ValuationDeductible10000000 });
            valueList.Add(new ValuationDeductibleModel() { Index = 1, DeductibleCode = MoveDataDisplayResource.Deductible100000001, DeductibleName = MoveDataDisplayResource.ValuationDeductible100000001 });
            valueList.Add(new ValuationDeductibleModel() { Index = 2, DeductibleCode = MoveDataDisplayResource.Deductible100000002, DeductibleName = MoveDataDisplayResource.ValuationDeductible100000002 });
            valueList.Add(new ValuationDeductibleModel() { Index = 3, DeductibleCode = MoveDataDisplayResource.Deductible100000003, DeductibleName = MoveDataDisplayResource.ValuationDeductible100000003 });
            valueList.Add(new ValuationDeductibleModel() { Index = 4, DeductibleCode = MoveDataDisplayResource.Deductible100000004, DeductibleName = MoveDataDisplayResource.ValuationDeductible100000004 });

            return valueList;
        }

        /// <summary>
        /// Method Name     : ValuationDeductibleBindingList
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Jan 2018
        /// Purpose         : return list of valuation deductible codes from MoveDataDisplayResource
        /// Revision        : 
        /// </summary>
        public static List<String> ValuationDeductibleBindingList()
        {
            List<String> valutionList = new List<String>();
            valutionList.Add(MoveDataDisplayResource.ValuationDeductible10000000.Trim());
            valutionList.Add(MoveDataDisplayResource.ValuationDeductible100000001.Trim());
            valutionList.Add(MoveDataDisplayResource.ValuationDeductible100000002.Trim());
            valutionList.Add(MoveDataDisplayResource.ValuationDeductible100000003.Trim());
            valutionList.Add(MoveDataDisplayResource.ValuationDeductible100000004.Trim());

            return valutionList;
        }

        public static List<MonthYearModel> BindMonthList()
        {
            MonthYearModel monthYearModel;
            List<MonthYearModel> monthList;
            monthList = new List<MonthYearModel>();
            for (int i = 1; i <= 12; i++)
            {
                monthYearModel = new MonthYearModel();
                monthYearModel.Month = i.ToString("00");
                monthYearModel.Monthindex = i;
                monthList.Add(monthYearModel);
            }
            return monthList;
        }

        public static List<String> MonthList()
        {
            List<String> monthList = new List<String>();

            for (int i = 1; i <=12; i++)
            {
                string value;
                value = i.ToString("00");
                monthList.Add(value);
            }
            return monthList;
        }

        public static List<String> YearList()
        {
            List<String> yearList = new List<String>();
            int year = DateTime.Now.Year;
            for (int i = 0; i <= 40; i++)
            {
                int add = year + i;
                yearList.Add(add.ToString());
            }
            return yearList;
        }

        public static List<MonthYearModel> BindYearList()
        {
            MonthYearModel monthYearModel;
            List<MonthYearModel> yearList;
            yearList = new List<MonthYearModel>();
            int year = DateTime.Now.Year;
            for (int i = 0; i <= 40; i++)
            {
                int addYear = year + i;
                monthYearModel = new MonthYearModel();
                monthYearModel.Year = addYear.ToString();
                monthYearModel.Yearindex = addYear;
                yearList.Add(monthYearModel);
            }
            return yearList;
        }
    }
}
