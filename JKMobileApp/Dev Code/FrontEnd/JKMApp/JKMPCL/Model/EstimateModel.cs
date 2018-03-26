using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JKMPCL.Services;

namespace JKMPCL.Model
{
    /// <summary>
    /// Class Name      : EstimateModel.
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 10 Jan 2018
    /// Purpose         :  to get & set Estimate details for the move
    /// Revision        : 
    /// </summary>
    public class EstimateModel
    {
        public string MoveId { get; set; }
        public string MoveNumber { get; set; }
        public string EstimatedLineHaul { get; set; }
        public string Deposit { get; set; }
        public string DepositValue
        {
            get
            {
                
                Deposit= UtilityPCL.RemoveCurrencyFormat(Deposit);
                if (string.IsNullOrEmpty(Deposit) || Convert.ToDouble(UtilityPCL.RemoveCurrencyFormat(Deposit)) == 0)
                {
                    return "500.50";
                }
                else
                {
                    return Deposit;
                }
            }
        }
        public bool IsDepositPaid { get; set; }
        public bool IsEstimateBooked { get; set; }
        public string IsActive { get; set; }//statecode
        public string StatusReason { get; set; }//statuscode
        public string Destination_Street1 { get; set; }
        public string Destination_Street2 { get; set; }
        public string Destination_City { get; set; }
        public string Destination_State { get; set; }
        public string Destination_PostalCode { get; set; }
        public string Destination_Country { get; set; }
        public string Destination_ContactName { get; set; }
        public string Destination_Telephone1 { get; set; }
        public string Destination_Telephone3 { get; set; }
        public string Destination_Telephone2 { get; set; }
        public string Origin_Street1 { get; set; }
        public string Origin_Street2 { get; set; }
        public string Origin_City { get; set; }
        public string Origin_State { get; set; }
        public string Origin_PostalCode { get; set; }
        public string Origin_Country { get; set; }
        public string Origin_ContactName { get; set; }
        public string Origin_Telephone1 { get; set; }
        public string Origin_Telephone3 { get; set; }
        public string Origin_Telephone2 { get; set; }
        public string CustomOriginAddress { get; set; }
        public string CustomDestinationAddress { get; set; }
        public string PackStartDate { get; set; }
        public string LoadStartDate { get; set; }
        public string MoveStartDate { get; set; }
        public string WhatMattersMost { get; set; }
        public string ExcessValuation { get; set; }
        public string ValuationDeductible { get; set; }
        public string ValuationCost { get; set; }
        public string ServiceCode { get; set; }
        public List<MyServicesModel> MyServices { get; set; }
        public bool Response_status { get; set; }
        public string message { get; set; }

        public bool IsServiceDate { get; set; }
        public bool IsAddressEdited { get; set; }
        public bool IsValuationEdited { get; set; }
        public bool IsWhatMatterMostEdited { get; set; }
        public string TransactionId { get; set; }
        public bool PaymentStatus { get; set; }
        public bool IsDepositPaidByCheck { get; set; }

    }

    /// <summary>
    /// Class Name      : GetEstimatePDF.
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 10 Jan 2018
    /// Purpose         : to get pdf in bytes from service
    /// Revision        : 
    /// </summary>
    public class GetEstimatePDF
    {
        public string DATA { get; set; }

    }
}
