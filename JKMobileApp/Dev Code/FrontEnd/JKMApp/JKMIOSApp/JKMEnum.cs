using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace JKMIOSApp
{
    /// <summary>
    /// Class Name      : JKMEnum
    /// Author          : Hiren M Patel
    /// Creation Date   : 12 Dec 2017
    /// Purpose         : 
    /// Revision        : 
    /// </summary>
    public static class JKMEnum
    {
        /// <summary>
        /// Enum Name       : PreferredContact
        /// Author          : Hiren M Patel
        /// Creation Date   : 12 Dec 2017
        /// Purpose         : to get Preferred Contact
        /// Revision        : 
        /// </summary>
        public enum PreferredContact
        {
            [StringValue("2")]
            Email,
            [StringValue("3")]
            SMS
        }



        /// <summary>
        /// Enum Name       : MoveCode
        /// Author          : Hiren M Patel
        /// Creation Date   : 12 Dec 2017
        /// Purpose         : Move code.
        /// Revision        : 
        /// </summary>
        public enum MoveCode
        {
            BoxDeliver,
            Export,
            Import,
            Load,
            Pack,
            Release,
            UnPack,
            Deliver,
            ApuFloar,
            Trailor
        }

        /// <summary>
        /// Enum Name       : LinotteFont
        /// Author          : Hiren M Patel
        /// Creation Date   : 12 Dec 2017
        /// Purpose         : Linotte font.
        /// Revision        : 
        /// </summary>
        public enum LinotteFont
        {
            [StringValue("Linotte-Bold")]
            LinotteBold,
            [StringValue("Linotte-Heavy")]
            LinotteHeavy,
            [StringValue("Linotte-Light")]
            LinotteLight,
            [StringValue("Linotte-Regular")]
            LinotteRegular,
            [StringValue("Linotte-SemiBold")]
            LinotteSemiBold
        }

        /// <summary>
        /// Enum Name       : LinotteDefaultFontSize
        /// Author          : Hiren M Patel
        /// Creation Date   : 12 Dec 2017
        /// Purpose         : Linotte default font size.
        /// Revision        : 
        /// </summary>
        public enum LinotteDefaultFontSize
        {
            [StringValue("14.0f")]
            LinotteBold,
            [StringValue("14.0f")]
            LinotteHeavy,
            [StringValue("14.0f")]
            LinotteLight,
            [StringValue("14.0f")]
            LinotteRegular,
            [StringValue("14.0f")]
            LinotteSemiBold
        }

        /// <summary>
        /// Enum Name       : UIViewControllerID
        /// Author          : Hiren M Patel
        /// Creation Date   : 12 Dec 2017
        /// Purpose         : UI View controller identifier.
        /// Revision        : 
        /// </summary>
        public enum UIViewControllerID
        {
            EmailView,
            ForgotPasswordView,
            EnterPasswordView,
            VerificationCodeView,
            CreatePasswordView,
            DashboardView,
            JKMRootView,
            PrivacyPolicyView,
            ContactusView,
            AppShellView,
            dashboardRootView,
            EstimateListView,
            ViewEstimateReviewView,
            EstimateReviewView,
            ServicesView,
            ServiceDatesView,
            AddressesVew,
            WhatMattersMostView,
            ValuationView,
            VitalInformationView,
            AcknowledgementView,
            MoveConfirmedView,

            UINavigationDashboard,



        }

        /// <summary>
        /// Method Name     : GetStringValue
        /// Author          : Hiren M Patel
        /// Creation Date   : 12 Dec 2017
        /// Purpose         : UI View controller identifier.
        /// Will get the string value for a given enums value, this will
        /// only work if you assign the StringValue attribute to
        /// the items in your enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStringValue(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }

        /// <summary>
        /// Method Name     : NumToEnum
        /// Author          : Hiren M Patel
        /// Creation Date   : 12 Dec 2017
        /// Purpose         : Get Enum from number value
        /// </summary>
        /// <typeparam name="T">Pass Enum Type</typeparam>
        /// <param name="number">Pass number</param>
        /// <returns></returns>
        public static T NumToEnum<T>(int number)
        {
            return (T)Enum.ToObject(typeof(T), number);
        }

        /// <summary>
        /// Method Name     : StringValueToEnum
        /// Author          : Hiren M Patel
        /// Creation Date   : 12 Dec 2017
        /// Purpose         : Get Enum from number value
        /// Get Enum From StringValue Attribute
        /// </summary>
        /// <typeparam name="T">Pass Enum Type</typeparam>
        /// <param name="strValue">Pass string Value</param>
        /// <returns></returns>
        public static T StringValueToEnum<T>(string strValue)
        {
            T returnResult = default(T);
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                if (GetStringValue((Enum)item).Equals(strValue, StringComparison.InvariantCultureIgnoreCase))
                {
                    returnResult = (T)item;
                    break;
                }
            }
            return returnResult;
        }


    }

    #region String Attribute Class
    /// <summary>
    /// <summary>
    /// Class Name      : StringValueAttribute
    /// Author          : Hiren M Patel
    /// Creation Date   : 12 Dec 2017
    /// Purpose         : This attribute is used to represent a string value for a value in an enum.
    /// Revision        : 
    /// </summary>
    public class StringValueAttribute : Attribute
    {
        #region Properties
        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string StringValue { get; protected set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }
        #endregion

    }
    #endregion

}
