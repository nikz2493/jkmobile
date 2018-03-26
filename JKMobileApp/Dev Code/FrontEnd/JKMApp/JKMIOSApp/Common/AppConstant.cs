using JKMPCL;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using CoreGraphics;

namespace JKMIOSApp
{
    /// <summary>
    /// Class Name      : App constant.
    /// Author          : Hiren Patel
    /// Creation Date   : 2 Dec 2017
    /// Purpose         : To declare all constant for IOS application
    /// Revision        : 
    /// </summary>
    public static class AppConstant
    {
        public const int MAXIMUM_EMAIL_LENGTH = 80;
        public const int MAXIMUM_PASSWORD_LENGTH = 20;
        //Font size constant
        public const float LINOTTE_DEFAULT_FONTSIZE = 14.0f;
        public const float LINOTTE_DEFAULT_LABEL_FONTSIZE = 14.0f;
        public const float LINOTTE_DEFAULT_TEXTFIELD_FONTSIZE = 14.0f;
        public const float LINOTTE_DEFAULT_BUTTON_FONTSIZE = 14.0f;
        //Image URl
        public const string CONTACTUS_SEND_BUTTON_IMAGE_URL = "Images/contactus_send.png";
        public const string VERIFICATION_BACK_ARROW_URL = "Images/arrow-back.png";
        //Image for estimate list
        public const string ESTIMATE_SELECETD_IMAGE_URL = "radio-active.png";
        public const string ESTIMATE_UNSELECETD_IMAGE_URL = "radio.png";

        // Alert type images
        public const string ALERT_COMPLETE_WIZARD_REMINDER_IMAGE_URL = "alert-complete-wizard-reminder.png";
        public const string ALERT_BOOK_YOUR_MOVE_IMAGE_URL = "alert-book-your-move.png";
        public const string ALERT_PRE_MOVE_CONFIRMATION_IMAGE_URL = "alert-pre-move-confrimation.png";
        public const string ALERT_DAY_OF_SERVICE_CHECKING_IMAGE_URL = "alert-day-of-service-checking.png";
        public const string ALERT_END_OF_SERVICE_CHECKING_IMAGE_URL = "alert-end-of-service-checking.png";
        public const string ALERT_FINAL_PAYMENT_MODE_IMAGE_URL = "alert-final-payment-mode.png";
        public const string ALERT_DATE_OF_SERVICE_CHANGE_IMAGE_URL = "alert-date-of-service-change.png";
        // Dashboard myservice Icon
        public const string MYSERVICE_BOOKED_IMAGE_URL = "myservice-icon-booked.png";
        public const string MYSERVICE_DELIVERED_IMAGE_URL = "myservice-icon-delivered.png";
        public const string MYSERVICE_TRANSMIT_IMAGE_URL = "myservice-icon-in-transmit.png";
        public const string MYSERVICE_INVOICED_IMAGE_URL = "myservice-icon-invoiced.png";
        public const string MYSERVICE_LOADED_IMAGE_URL = "myservice-icon-loaded.png";
        public const string MYSERVICE_NEEDS_OVERRIDE_IMAGE_URL = "myservice-icon-needs-override.png";
        public const string MYSERVICE_PENDING_IMAGE_URL = "myservice-icon-pending.png";

        // My Document Type
        public const string MYDOCUMENT_RIGHT_RESPONSIBILITIES_IMAGE_URL = "rights&responsibilities.png";
        public const string MYDOCUMENT_VALUATION_IMAGE_URL = "valuation.png";

        // Intro page
        public const float INTROPAGE_SCREEN_OPACITY = 0.70f;
        public const float BUTTON_ADD_ANOTHER_CARD_OPACITY = 0.40f;

        public const string CREATED_PASSWORD_MESSAGE = "Your password has been created sucessfully.";
        public const string ESTIMATE_DATA_UPODATED_SUCESS_MESSAGE = "Estimates data updated successfully.";
        public const string CONTACTUS_MAIL_SUBJECT = "Customer have some query";
        public const string CONTACTUS_MAIL_SUBJECT_WITH_NAME_FORMAT = "{0} has some query";
        public const string CONTACTUS_MAIL_VALIDATIN_MESSAGE = "Please enter your question.";
        public const string CONTACTUS_MAIL_SUCESS_MESSAGE = "Your question has been sent successfully.";

        public const string NO_INTERNET_MESSAGE = "Please check your internet connectivity.";
        public const string PRIVACY_POLICY_TERMS_DOCUMENT_URL = "Content/privacypolicy.html";
        public const string VITAL_INFORMATION_DOCUMENT_URL = "Content/VitalInformation.pdf";
        public const string VIEW_ESTIMATE_URL = "Content/test.pdf";
        public const string VIEW_RIGHTS_AND_RESPONSIBILITIES_URL = "Content/test.pdf";

        public const string CUSTOMER_INTRO_KEY_FORMAT = "{0}_INTRO";
        public const string CUSTOMER_ID_CACHE_KEY = "CustomerId";

        public const string UPDATE_ADDRESS_BUTTON_LABEL = "UPDATES ADDRESSES";
        public const string UPDATE_WHAT_MATTERS_MOST_BUTTON_LABEL = "UPDATES NEEDED";
       
        public const string CHANGE_MY_SERVICE_DATE_BUTTON_LABEL = "CHANGE MY SERVICE DATES";


        public const string UPDATE_NEED_BUTTON_LABEL = "UPDATES NEEDED";
        public const string SUBMIT_CHANGES_BUTTON_LABEL = "SUBMIT CHANGES";

        public const string ABOUT_US_URL = "https://www.jkmoving.com/about";

        // Alert Buttons Tetx
        public const string ALERT_OK_BUTTON_TEXT = "OK";
        public const string ALERT_TRY_AGAIN_BUTTON_TEXT = "Try again";

        // Iphone 4 and 5 width
        public const string IPHONE_4_WIDTH_AND_HEIGHT = "320X480";
        public const string IPHONE_5_WIDTH_AND_HEIGHT = "320X568";
        public const string IPHONE_6_WIDTH_AND_HEIGHT = "375X667";
        public const string IPHONE_6_PLUS_WIDTH_AND_HEIGHT = "414X736";
        public const string IPAD_WIDTH_AND_HEIGHT = "768X1024";

        // Acknowledgement icons
        public const string WIZARD_CHECKED_YELLOW_IMAGE_URL = "checked_yellow.png";
        public const string WIZARD_CHECKED_IMAGE_URL = "checked.png";
        public const string ACKNOWLEDGEMENT_SAVE_DATA_CONFIRM_MESSAGE = "Do you want to save data?";

        public const string WIZARD_EDIT_SELECTED_IMAGE_URL = "edit_selected.png";
        public const string WIZARD_EDIT_IMAGE_URL = "edit.png"; 

        public const string WIZARD_IAGREE_IMAGE_URL = "checkedAgree.png";
        public const string WIZARD_DISAGREE_IMAGE_URL = "unchecked.png";

        public const string WIZARD_DISAGREE_MESSAGE = "You should agree the terms to continue further.";
        public const string WIZARD_INVALIDCARD_MESSAGE = "Invalid card number.";

        // MyAccount
        public const string MYACCOUNT_OK_BUTTON_TEXT = "Okay";
        public const string MYACCOUNT_SAVE_BUTTON_TEXT = "Save";
        public const string MYACCOUNT_LOGOUT_CONFIRM_TEXT = "Are you sure you want to Logout?";

        public const string CONIRM_YES_BUTTON_TEXT = "Yes";
        public const string CONIRM_NO_BUTTON_TEXT = "No";

        // My Payment
        public const string PAYMENT_STATUS_SUCCESS_TITLE = "Payment Submitted";
        public const string PAYMENT_STATUS_SUCCESS_IMAGE_URL = "succesfull.png";
        public const string PAYMENT_STATUS_SUCCESS_MSG_LINE1 = "Your payment has been successfully";
        public const string PAYMENT_STATUS_SUCCESS_MSG_LINE2 = "processed.";

        public const string PAYMENT_STATUS_FAILED_TITLE = "Payment Failed";
        public const string PAYMENT_STATUS_FAILED_MSG_LINE1 = "Your payment was unsuccessful.";
        public const string PAYMENT_STATUS_FAILED_MSG_LINE2 = "Please try again";
        public const string PAYMENT_STATUS_FAILED_IMAGE_URL = "failed.png";

        public const string PAYMENT_MASTER_CARD_IMAGE_URL = "master.png";
        public const string PAYMENT_VISA_CARD_IMAGE_URL = "visa.png";
        public const string PAYMENT_DEFAULT_CARD_IMAGE_URL = "payment-selected.png";
        public const string PAYMENT_TOTAL_DUE_AMOUNT_LESS_THAN_TOTAL_COST = "Trnasaction amount should be less than or equal to TotalDue.";
        public const string PAYMENT_TRANSACTION_AMOUNT_IS_REQUIRED = "Amount is required.";
    }
}
