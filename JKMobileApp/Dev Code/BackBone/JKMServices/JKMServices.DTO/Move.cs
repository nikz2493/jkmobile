using System;
using System.Text;

namespace JKMServices.DTO
{
    /// <summary>
    /// Class Name      : Move
    /// Author          : Pratik Soni
    /// Creation Date   : 26 Dec 2017
    /// Purpose         : Class contains property for all move data
    /// Revision        : 
    /// </summary>
    public class Move
    {
        public string MoveId { get; set; }                  //jkmoving_moveid
        public string MoveNumber { get; set; }              //jkmoving_movenumber
        public string IsActive { get; set; }                //statecode
        public string StatusReason { get; set; }            //statuscode - will return the reason of status e.g. 100000000 (Pending), 100000001 (Booked)
        public string ContactOfMoveId { get; set; }         //_jkmoving_contactofmoveid_value

        private string _MoveCoordinator_ContactNumber;
        public string MoveCoordinator_ContactNumber         //address1_telephone1
        {
            get
            {
                return _MoveCoordinator_ContactNumber;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _MoveCoordinator_ContactNumber = "7032604282";
                }
                else
                {
                    _MoveCoordinator_ContactNumber = value;
                }
            }
        }

        private string _MoveCoordinator_EmailAddress;
        public string MoveCoordinator_EmailAddress          //internalemailaddress
        {
            get
            {
                return _MoveCoordinator_EmailAddress;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _MoveCoordinator_EmailAddress = "INFO@JKMOVING.COM";
                }
                else
                {
                    _MoveCoordinator_EmailAddress = value;
                }
            }
        }
        public string FirstName { get; set; }               //firstname
        public string LastName { get; set; }                //lastname
        public string Fullname
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string Destination_Street1 { get; set; }     //jkmoving_destination_line1
        public string Destination_Street2 { get; set; }     //jkmoving_destination_line2
        public string Destination_City { get; set; }        //jkmoving_destination_city
        public string Destination_State { get; set; }       //jkmoving_destination_stateorprovince
        public string Destination_PostalCode { get; set; }  //jkmoving_destination_postalcode
        public string Destination_Country { get; set; }     //jkmoving_destination_country 
        public string Destination_ContactName { get; set; } //jkmoving_destination_contactname 
        public string Destination_Telephone1 { get; set; }  //jkmoving_destination_telephone1
        public string Destination_Telephone2 { get; set; }  //jkmoving_destination_telephone2
        public string Destination_Telephone3 { get; set; }  //jkmoving_destination_telephone3

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

        public string Origin_Street1 { get; set; }         //jkmoving_origin_line1
        public string Origin_Street2 { get; set; }         //jkmoving_origin_line2
        public string Origin_City { get; set; }            //jkmoving_origin_city
        public string Origin_State { get; set; }           //jkmoving_origin_stateorprovince 
        public string Origin_PostalCode { get; set; }      //jkmoving_origin_postalcode
        public string Origin_Country { get; set; }         //jkmoving_origin_country
        public string Origin_ContactName { get; set; }     //jkmoving_origin_contactname
        public string Origin_Telephone1 { get; set; }      //jkmoving_origin_telephone1
        public string Origin_Telephone3 { get; set; }      //jkmoving_origin_telephone3
        public string Origin_Telephone2 { get; set; }      //jkmoving_origin_telephone2
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
       
        public string MoveStartDate { get; set; }                           //jkmoving_packfrom, If empty then jkmoving_loadfrom
        public string MoveEndDate { get; set; }                             //jkmoving_deliveryfrom
        
        public string MoveDetails_PackStartDate { get; set; }               //jkmoving_packfrom
        public string MoveDetails_LoadStartDate { get; set; }               //jkmoving_loadfrom
        public string MoveDetails_DeliveryStartDate { get; set; }           //jkmoving_deliveryfrom
        public string MoveDetails_DeliveryEndDate { get; set; }             //jkmoving_deliveryto

        public string WhatMattersMost { get; set; }                         //jkmoving_whatmattersmost

        public string ExcessValuation { get; set; }                         //jkmoving_declaredpropertyvalue
        public string ValuationDeductible { get; set; }                     //jkmoving_valuationdeductible
        public string ValuationCost { get; set; }                           //jkmoving_valuationcost
        public string ServiceCode { get; set; }                             //jkmoving_servicecode

        private string AppendString(string fieldValue)
        {
            if (!string.IsNullOrEmpty(fieldValue))
            {
                return fieldValue + ", ";
            }
            else
                return string.Empty;
        }

        public string CustomerContactNumber { get; set; }
        public string CustomerEmailAddress { get; set; }
    }
}