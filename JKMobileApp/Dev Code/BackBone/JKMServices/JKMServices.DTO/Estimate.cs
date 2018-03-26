using System;
using System.Text;

namespace JKMServices.DTO
{
    /// <summary>
    /// Class Name      : Estimate
    /// Author          : Pratik Soni
    /// Creation Date   : 10 Jan 2018
    /// Purpose         : Class contains property for all estimate data
    /// Revision        : 
    /// </summary>
    public class Estimate
    {
        public string MoveId { get; set; }              //jkmoving_moveid
        public string MoveNumber { get; set; }          //jkmoving_movenumber

        public string EstimatedLineHaul { get; set; }   //jkmoving_estimatedlinehaul
        public string Deposit { get; set; }             //jkmoving_deposit
        public bool IsDepositPaid
        {
            get
            {
                if (string.IsNullOrEmpty(Deposit))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public bool IsEstimateBooked
        {
            get
            {
                if (string.IsNullOrEmpty(Deposit) || StatusReason != "676860000")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public string IsActive { get; set; }                //statecode
        public string StatusReason { get; set; }            //statuscode - will return the reason of status e.g. 100000000 (Pending), 100000001 (Booked)

        // ================= Destination address ===================
        public string Destination_Street1 { get; set; }     //jkmoving_destination_line1
        public string Destination_Street2 { get; set; }     //jkmoving_destination_line2
        public string Destination_City { get; set; }        //jkmoving_destination_city
        public string Destination_State { get; set; }       //jkmoving_destination_stateorprovince
        public string Destination_PostalCode { get; set; }  //jkmoving_destination_postalcode
        public string Destination_Country { get; set; }     //jkmoving_destination_country 
        public string Destination_ContactName { get; set; } //jkmoving_destination_contactname 
        public string Destination_Telephone1 { get; set; }  //jkmoving_destination_telephone1
        public string Destination_Telephone3 { get; set; }  //jkmoving_destination_telephone2
        public string Destination_Telephone2 { get; set; }  //jkmoving_destination_telephone3

        public string CustomDestinationAddress
        {
            get
            {
                StringBuilder customAddress = new StringBuilder();
                customAddress.Append(AppendString(Destination_Street1));
                customAddress.Append(AppendString(Destination_Street2));
                customAddress.Append(AppendString(Destination_City));
                customAddress.Append(AppendString(Destination_State));
                customAddress.Append(AppendString(Destination_PostalCode));
                customAddress.Append(AppendString(Destination_Country));

                if (customAddress.Length > 2)
                    return customAddress.ToString().Substring(0, customAddress.Length - 2);

                return customAddress.ToString();
            }
        }
        // ================= ==================== ===================

        // ==================== Origin address ======================
        public string Origin_Street1 { get; set; }      //jkmoving_origin_line1
        public string Origin_Street2 { get; set; }      //jkmoving_origin_line2
        public string Origin_City { get; set; }         //jkmoving_origin_city
        public string Origin_State { get; set; }        //jkmoving_origin_stateorprovince
        public string Origin_PostalCode { get; set; }   //jkmoving_origin_postalcode
        public string Origin_Country { get; set; }      //jkmoving_origin_country
        public string Origin_ContactName { get; set; }  //jkmoving_origin_contactname
        public string Origin_Telephone1 { get; set; }   //jkmoving_origin_telephone1
        public string Origin_Telephone3 { get; set; }   //jkmoving_origin_telephone3
        public string Origin_Telephone2 { get; set; }   //jkmoving_origin_telephone2
        public string CustomOriginAddress
        {
            get
            {
                StringBuilder customAddress = new StringBuilder();
                customAddress.Append(AppendString(Origin_Street1));
                customAddress.Append(AppendString(Origin_Street2));
                customAddress.Append(AppendString(Origin_City));
                customAddress.Append(AppendString(Origin_State));
                customAddress.Append(AppendString(Origin_PostalCode));
                customAddress.Append(AppendString(Origin_Country));

                if (customAddress.Length > 2)
                    return customAddress.ToString().Substring(0, customAddress.Length - 2);

                return customAddress.ToString();
            }
        }
        // ==================== ============== ======================

        private string _PackStartDate;
        public string PackStartDate                     //jkmoving_packfrom
        {
            get
            {
                return _PackStartDate;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _PackStartDate = string.Empty;
                }
                else
                {
                    DateTime packFirstDay = Convert.ToDateTime(value);
                    _PackStartDate = packFirstDay.ToString("MM-dd-yyyy");
                }
            }
        }

        private string _LoadStartDate;
        public string LoadStartDate                     //jkmoving_loadfrom
        {
            get
            {
                return _LoadStartDate;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _LoadStartDate = string.Empty;
                }
                else
                {
                    DateTime loadFirstDay = Convert.ToDateTime(value);
                    _LoadStartDate = loadFirstDay.ToString("MM-dd-yyyy");
                }
            }
        }

        private string _MoveStartDate;
        public string MoveStartDate                     //jkmoving_deliveryto
        {
            get
            {
                return _MoveStartDate;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _MoveStartDate = string.Empty;
                }
                else
                {
                    DateTime movefirstday = Convert.ToDateTime(value);
                    _MoveStartDate = movefirstday.ToString("MM-dd-yyyy");
                }
            }
        }

        public string WhatMattersMost { get; set; }     //jkmoving_whatmattersmost

        public string ExcessValuation { get; set; }     //jkmoving_declaredpropertyvalue
        public string ValuationDeductible { get; set; } //jkmoving_valuationdeductible
        public string ValuationCost { get; set; }       //jkmoving_valuationcost
        public string ServiceCode { get; set; }         //jkmoving_servicecode

        private string AppendString(string fieldValue)
        {
            if (!string.IsNullOrEmpty(fieldValue))
            {
                return fieldValue + ", ";
            }
            else
                return string.Empty;
        }
    }
}