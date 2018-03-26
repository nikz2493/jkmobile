using System;
using System.Collections;
using System.Collections.Generic;
using JKMPCL.Model;

namespace JKMPCL.Services.Estimate
{
    /// <summary>
    /// Class Name      : EstimateValidateServices.
    /// Author          : Hiren Patel
    /// Creation Date   : 5 Dec 2017
    /// Purpose         : Validate model. 
    /// Revision        : 
    /// </summary> 
    public class EstimateValidateServices
    {
        /// <summary>
        /// Method Name     : ValidateServiceDates
        /// Author          : Hiren Patel
        /// Creation Date   : 31 Jan 2018
        /// Purpose         : Validate service dates. 
        /// Revision        : 
        /// </summary>
        /// <param name="packDate">Text pack date.</param>
        /// <param name="loadDate">Text load date.</param>
        /// <param name="moveDate">Text move date.</param>
        public string ValidateServiceDates(string packDate, string loadDate, string moveDate)
        {
            string errorMessage = string.Empty;

            if (UtilityPCL.IsNullOrEmptyOrWhiteSpace(packDate))
            {
                errorMessage = Resource.msgPackDateIsRequired;
            }
            else if(Convert.ToDateTime(packDate)<=DateTime.Today)
            {
                errorMessage = Resource.msgInvalidServiceDate;
            }
            else if (UtilityPCL.IsNullOrEmptyOrWhiteSpace(loadDate))
            {
                errorMessage = Resource.msgLoadDateIsRequired;
            }
            else if (Convert.ToDateTime(loadDate) <= DateTime.Today)
            {
                errorMessage = Resource.msgInvalidServiceDate;
            }
            else if (UtilityPCL.IsNullOrEmptyOrWhiteSpace(moveDate))
            {
                errorMessage = Resource.msgMoveDateIsRequired;
            }
            else if (Convert.ToDateTime(moveDate) <= DateTime.Today)
            {
                errorMessage = Resource.msgInvalidServiceDate;
            }
            else
            {
                DateTime dtpackDate = Convert.ToDateTime(packDate);
                DateTime dtloadDate = Convert.ToDateTime(loadDate);
                DateTime dtmoveDate = Convert.ToDateTime(moveDate);

                if (dtpackDate > dtloadDate)
                {
                    errorMessage = Resource.msgPackdatemustbelessthanloaddate;
                }
                else if (dtpackDate > dtmoveDate)
                {
                    errorMessage = Resource.msgPackdatemustbelessthanmovedate;
                }
                else if (dtloadDate > dtmoveDate)
                {
                    errorMessage = Resource.msgPleaseSelectLoadDateGreaterThanorEqualMoveDate;
                }

            }

            return errorMessage;
        }

        /// <summary>
        /// Method Name     : ValidateCustomerAddress
        /// Author          : Hiren Patel
        /// Creation Date   : 31 Jan 2018
        /// Purpose         : Validates the move address.
        /// Revision        : 
        /// </summary>
        /// <param name="originAddress">Text origin address.</param>
        /// <param name="destinationAddress">Text destination address.</param>
        public string ValidateCustomerAddress(string originAddress, string destinationAddress)
        {
            string errorMessage = string.Empty;

            if (UtilityPCL.IsNullOrEmptyOrWhiteSpace(originAddress))
            {
                errorMessage = Resource.msgCustomOriginAddressIsRequired;
            }
            else if (UtilityPCL.IsNullOrEmptyOrWhiteSpace(destinationAddress))
            {
                errorMessage = Resource.msgCustomDestinationAddressIsRequired;
            }

            return errorMessage;
        }

        /// <summary>
        /// Method Name     : ValidateWhatMattersMost
        /// Author          : Hiren Patel
        /// Creation Date   : 31 Jan 2018
        /// Purpose         : Validates the what matters most.
        /// Revision        : 
        /// </summary>
        /// <param name="whatMattersMost">Text what matters most.</param>
        public string ValidateWhatMattersMost(string whatMattersMost)
        {
            string errorMessage = string.Empty;
            if (UtilityPCL.IsNullOrEmptyOrWhiteSpace(whatMattersMost.Trim()))
            {
                errorMessage = Resource.msgWhatMattersMostIsRequired;
            }
            return errorMessage;
        }


        /// <summary>
        /// Method Name     : ValidateValuationDeductible
        /// Author          : Hiren Patel
        /// Creation Date   : 31 Jan 2018
        /// Purpose         : Validates the valuation data.
        /// Revision        : 
        /// </summary>
        /// <returns>The valuation data.</returns>
        /// <param name="declaredValue">Declared value.</param>
        /// <param name="valuationDeductible">Valuation deductible.</param>
        /// <param name="cost">Cost.</param>
        public string ValidateValuationData(string declaredValue, string valuationDeductible, string cost)
        {
            string errorMessage = string.Empty;
            decimal number;
            if (UtilityPCL.IsNullOrEmptyOrWhiteSpace(declaredValue))
            {
                return Resource.msgvaluationDeclaredValueIsRequired;
            }
            else
            {
                if (!Decimal.TryParse(declaredValue, out number))
                {
                    return Resource.msgvaluationDeclaredValueIsNumeric;
                }
            }

            if (UtilityPCL.IsNullOrEmptyOrWhiteSpace(valuationDeductible))
            {
                return Resource.msgvaluationDeductibleIsRequired;
            }

            if (UtilityPCL.IsNullOrEmptyOrWhiteSpace(cost))
            {
                return Resource.msgvaluationCostValueIsRequired;
            }
            else
            {
                if (!Decimal.TryParse(cost, out number))
                {
                    return Resource.msgvaluationCostValueIsNumeric;
                }

            }

            return errorMessage;
        }

        /// <summary>
        /// Method Name     : ValidateDepositModel
        /// Author          : Hiren Patel
        /// Creation Date   : 31 Jan 2018
        /// Purpose         : Validates the valuation data.
        /// Revision        : 
        /// </summary>
        /// <returns>The valuation data.</returns>
        /// <param name="declaredValue">Declared value.</param>
        /// <param name="valuationDeductible">Valuation deductible.</param>
        /// <param name="cost">Cost.</param>
        public string ValidateDepositData(string nameOfCardholder, string cardNumber, string expiryDate, string CVV)
        {
            string errorMessage = string.Empty;
            decimal number;
            if (UtilityPCL.IsNullOrEmptyOrWhiteSpace(nameOfCardholder))
            {
                return Resource.msgCardHolderNameIsRequired;
            }
            else if (UtilityPCL.IsNullOrEmptyOrWhiteSpace(cardNumber))
            {
                return Resource.msgCardNumberIsRequired;
            }
            else if (UtilityPCL.IsNullOrEmptyOrWhiteSpace(expiryDate))
            {
                return Resource.msgCardExpiryDateIsRequired;
            }
            else if (UtilityPCL.IsNullOrEmptyOrWhiteSpace(CVV))
            {
                return Resource.msgCardCCVIsRequired;
            }
            else
            { 
                if (!Decimal.TryParse(CVV, out number))
                {
                    return Resource.msgCCVValueIsNumeric;
                }
            }
           
            return errorMessage;
        }

        public string ValidatePaymentModel(PaymentGatewayModel paymentGatewayModel)
        {
            string errorMessage = string.Empty;

            if (UtilityPCL.IsNullOrEmptyOrWhiteSpace(paymentGatewayModel.FirstName))
            {
                return Resource.msgCardFirstNameIsRequired;
            }
            else if (UtilityPCL.IsNullOrEmptyOrWhiteSpace(paymentGatewayModel.LastName))
            {
                return Resource.msgCardLastNameIsRequired;
            }
            else if (UtilityPCL.IsNullOrEmptyOrWhiteSpace(paymentGatewayModel.CreditCardNumber))
            {
                return Resource.msgCardNumberIsRequired;
            }
            else if (!ValidateCardNumber(paymentGatewayModel.CreditCardNumber.Replace(" ", "").Replace("-", "")))
            {
                return Resource.msgInvalidCardNumber;
            }
            else if (UtilityPCL.IsNullOrEmptyOrWhiteSpace(paymentGatewayModel.CardExpiryDate))
            {
                return Resource.msgCardExpiryDateIsRequired;
            }
            else if (paymentGatewayModel.CVVNo==-1)
            {
                return Resource.msgCardCCVIsRequired;
            }

            return errorMessage;
        }

        /// <summary>
        /// Method Name     : InitilizeIntarface
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : Validates the card number.
        /// Revision        : 
        /// </summary>
        /// <returns><c>true</c>, if card number was validated, <c>false</c> otherwise.</returns>
        /// <param name="cardNumber">Card number.</param>
        public  bool ValidateCardNumber(string cardNumber)
        {
            if(cardNumber.Length<16)
            {
                return false;
            }
            try
            {
                // Array to contain individual numbers
                List<int> CheckNumbers = new List<int>();
                // So, get length of card
                int CardLength = cardNumber.Length;

                // Double the value of alternate digits, starting with the second digit
                // from the right, i.e. back to front.
                // Loop through starting at the end
                for (int i = CardLength - 2; i >= 0; i = i - 2)
                {
                    // Now read the contents at each index, this
                    // can then be stored as an array of integers

                    // Double the number returned
                    CheckNumbers.Add( Int32.Parse(cardNumber[i].ToString()) * 2);
                }

                int CheckSum = 0;    // Will hold the total sum of all checksum digits

                // Second stage, add separate digits of all products
                for (int iCount = 0; iCount <= CheckNumbers.Count - 1; iCount++)
                {
                    int _count = 0;    // will hold the sum of the digits

                    // determine if current number has more than one digit
                    if (CheckNumbers[iCount] > 9)
                    {
                        int _numLength = (CheckNumbers[iCount]).ToString().Length;
                        // add count to each digit
                        for (int x = 0; x < _numLength; x++)
                        {
                            _count = _count + Int32.Parse(
                                  (CheckNumbers[iCount]).ToString()[x].ToString());
                        }
                    }
                    else
                    {
                        // single digit, just add it by itself
                        _count = CheckNumbers[iCount];
                    }
                    CheckSum = CheckSum + _count;    // add sum to the total sum
                }
                // Stage 3, add the unaffected digits
                // Add all the digits that we didn't double still starting from the
                // right but this time we'll start from the rightmost number with 
                // alternating digits
                int OriginalSum = 0;
                for (int y = CardLength - 1; y >= 0; y = y - 2)
                {
                    OriginalSum = OriginalSum + Int32.Parse(cardNumber[y].ToString());
                }

                // Perform the final calculation, if the sum Mod 10 results in 0 then
                // it's valid, otherwise its false.
                return (((OriginalSum + CheckSum) % 10) == 0);
            }
            catch
            {
                return false;
            }
        }
    }
}
