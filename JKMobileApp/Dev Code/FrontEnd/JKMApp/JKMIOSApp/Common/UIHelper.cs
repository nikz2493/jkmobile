using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UIKit;
using Plugin.Connectivity;
using JKMPCL.Services;
using System.Threading.Tasks;
using JKMPCL.Model;
using JKMPCL.Common;
using CoreGraphics;
using static JKMPCL.Services.UtilityPCL;

namespace JKMIOSApp
{
    /// <summary>
    /// Class Name      : UIHelper.
    /// Author          : Hiren Patel
    /// Creation Date   : 2 Dec 2017
    /// Purpose         : To decalre and impelement all UI related common method
    /// Revision        : 
    /// </summary>
    public static class UIHelper
    {
        /// <summary>
        /// Method Name     : ShowLoadingScreen.
        /// Author          : Hiren Patel
        /// Creation Date   : 22 JAN 2018
        /// Purpose         : Shows the loading screen.
        /// Revision        : 
        /// </summary>
        /// <returns>The loading screen.</returns>
        /// <param name="view">View.</param>
        public static LoadingOverlay ShowLoadingScreen(UIView view)
        {
            LoadingOverlay loadingOverlay = new LoadingOverlay(UIScreen.MainScreen.Bounds);
            view.Add(loadingOverlay);
            return loadingOverlay;
        }

        /// <summary>
        /// Method Name     : ShowMessageWithOKConfirm.
        /// Author          : Hiren Patel
        /// Creation Date   : 22 JAN 2018
        /// Purpose         : The message with OK Confirm.
        /// Revision        : 
        /// </summary>
        /// <returns>The message with OK Confirm.</returns>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        /// <param name="buttons">Buttons.</param>
        public static Task<int> ShowMessageWithOKConfirm(string title, string message, params string[] buttons)
        {
            var tcs = new TaskCompletionSource<int>();
            var alert = new UIAlertView
            {
                Title = title,
                Message = message
            };
            foreach (var button in buttons)
                alert.AddButton(button);
            alert.Clicked += (s, e) => tcs.TrySetResult((int)e.ButtonIndex);
            alert.Show();
            return tcs.Task;
        }

        /// <summary>
        /// Method Name     : ShowMessage.
        /// Author          : Hiren Patel
        /// Creation Date   : 22 JAN 2018
        /// Purpose         : Shows the message.
        /// Revision        : 
        /// </summary>
        /// <param name="message">Message.</param>
        public static void ShowMessage(string message)
        {
            UIAlertView alert = new UIAlertView()
            {
                Title = "",
                Message = message
            };
            alert.AddButton(AppConstant.ALERT_OK_BUTTON_TEXT);
            alert.Show();
        }

        /// <summary>
        /// Method Name     : GetUIFont
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : GetUI Font
        /// Revision        : 
        /// </summary>
        /// <param name="fontName">Name of the font</param>
        /// <param name="size">Size of the font</param>
        /// <returns>return UIFont</returns>
        private static UIFont GetUIFont(string fontName, float? size)
        {
            string defaultFontName = JKMEnum.LinotteFont.LinotteRegular.GetStringValue();
            if (!string.IsNullOrEmpty(fontName))
            {
                defaultFontName = fontName;
            }

            var fontname = UIFont.FromName(fontName, (size.HasValue ? (float)size : (float)AppConstant.LINOTTE_DEFAULT_FONTSIZE));
            if (fontname is null)
                return null;
            else
                return fontname;
        }

        /// <summary>
        /// Method Name     : SetLabelFont
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Set font of UI label control
        /// Revision        : 
        /// </summary>
        /// <param name="uiLabel">UILabel Control</param>
        /// <param name="fontName">Font Name</param>
        /// <param name="size">Size of the font</param>
        public static void SetLabelFont(UILabel uiLabel, string fontName, float? size)
        {
            if (uiLabel != null)
            {
                UIFont fontname = GetUIFont(fontName, size);
                if (fontname != null)
                    uiLabel.Font = fontname;
            }

        }

        /// <summary>
        /// Method Name     : SetLabelFont
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Set font of UI label control
        /// Revision        : 
        /// </summary>
        /// <param name="uiLabel">UILabel Control</param>
        /// <param name="fontName">Font Name</param>
        public static void SetLabelFont(UILabel uiLabel, string fontName)
        {
            if (uiLabel != null)
            {
                UIFont fontname = GetUIFont(fontName, (float)AppConstant.LINOTTE_DEFAULT_FONTSIZE);
                if (!(fontname is null))
                    uiLabel.Font = fontname;
            }

        }

        /// <summary>
        /// Method Name     : SetLabelFont
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Set font of UI label control
        /// Revision        : 
        /// </summary>
        /// <param name="uiLabel">UILabel Control</param>
        /// <param name="size">Size of the font</param>
        public static void SetLabelFont(UILabel uiLabel, float? size)
        {
            if (uiLabel != null)
            {
                UIFont fontname = GetUIFont(JKMEnum.LinotteFont.LinotteRegular.GetStringValue(), size);
                if (fontname != null)
                    uiLabel.Font = fontname;
            }

        }

        /// <summary>
        /// Method Name     : SetLabelFont
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Set font of UI label control
        /// Revision        : 
        /// </summary>
        /// <param name="uiLabel">UILabel Control</param>
        public static void SetLabelFont(UILabel uiLabel)
        {
            if (uiLabel != null)
            {
                UIFont fontname = GetUIFont(JKMEnum.LinotteFont.LinotteRegular.GetStringValue(), (float)AppConstant.LINOTTE_DEFAULT_FONTSIZE);
                if (fontname != null)
                    uiLabel.Font = fontname;
            }

        }

        /// <summary>
        /// Method Name     : SetTextFieldFont
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Set font of UI text field control
        /// Revision        : 
        /// </summary>
        /// <param name="uiTextField">UITextField Control</param>
        /// <param name="fontName">Font Name</param>
        /// <param name="size">Size of the font</param>
        public static void SetTextFieldFont(UITextField uiTextField, string fontName, float? size)
        {
            if (uiTextField != null)
            {
                UIFont fontname = GetUIFont(fontName, size);
                if (fontname != null)
                    uiTextField.Font = fontname;
            }

        }

        /// <summary>
        /// Method Name     : SetTextFieldFont
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Set font of UI text field control
        /// Revision        : 
        /// </summary>
        /// <param name="uiTextField">UITextField Control</param>
        /// <param name="fontName">Font Name</param>
        public static void SetTextFieldFont(UITextField uiTextField, string fontName)
        {
            if (uiTextField != null)
            {
                UIFont fontname = GetUIFont(fontName, (float)AppConstant.LINOTTE_DEFAULT_FONTSIZE);
                if (fontname != null)
                    uiTextField.Font = fontname;
            }

        }

        /// <summary>
        /// Method Name     : SetTextFieldFont
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Set font of UI text field control
        /// Revision        : 
        /// </summary>
        /// <param name="uiTextField">uiTextField Control</param>
        /// <param name="size">Size of the font</param>
        public static void SetTextFieldFont(UITextField uiTextField, float? size)
        {
            if (uiTextField != null)
            {
                UIFont fontname = GetUIFont(JKMEnum.LinotteFont.LinotteRegular.GetStringValue(), size);
                if (fontname != null)
                    uiTextField.Font = fontname;
            }

        }

        /// <summary>
        /// Method Name     : SetTextFieldFont
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Set font of UI text field control
        /// Revision        : 
        /// </summary>
        /// <param name="uiTextField">UITextField Control</param>
        public static void SetTextFieldFont(UITextField uiTextField)
        {
            if (uiTextField != null)
            {
                UIFont fontname = GetUIFont(JKMEnum.LinotteFont.LinotteRegular.GetStringValue(), (float)AppConstant.LINOTTE_DEFAULT_FONTSIZE);
                if (fontname != null)
                    uiTextField.Font = fontname;
            }

        }

        /// <summary>
        /// Method Name     : SetButtonFont
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Set font of UI button control
        /// Revision        : 
        /// </summary>
        /// <param name="uiButton">UIButton Control</param>
        /// <param name="fontName">Font Name</param>
        /// <param name="size">Size of the font</param>
        public static void SetButtonFont(UIButton uiButton, string fontName = "Linotte-SemiBold", float? size = 16.0f)
        {
            if (uiButton != null)
            {
                UIFont fontname = GetUIFont(fontName, size);
                if (fontname != null)
                    uiButton.Font = fontname;
            }

        }

        /// <summary>
        /// Method Name     : SetMaximumUiTextFieldLength
        /// Author          : Hiren Patel
        /// Creation Date   : 3 Dec 2017
        /// Purpose         : Set lenghth of UITextField control
        /// Revision        : 
        /// </summary>
        /// <param name="uiTextField">UITextField control</param>
        /// <param name="size">Maximum lenght of the UITextField</param>
        public static void SetMaximumUiTextFieldLength(UITextField uiTextField, int? size = 50)
        {
            if (!size.HasValue)
                return;
            uiTextField.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;
                return newLength <= size;
            };
        }

        /// <summary>
        /// Method Name     : SetUiTextFieldLength
        /// Author          : Hiren Patel
        /// Creation Date   : 3 Dec 2017
        /// Purpose         : Set length of UITextField control
        /// Revision        : 
        /// </summary>
        /// <param name="uiTextField">UITextField control</param>
        /// <param name="size">Maximum length of the UITextField</param>
        public static void SetUiTextFieldAsNumberOnly(UITextField uiTextField, int? size = 50)
        {
            
            if (!size.HasValue)
                return;
            string ACCEPTABLE_CHARECTERS = "1234567890";
            uiTextField.KeyboardType = UIKeyboardType.NumberPad;
            uiTextField.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;
                return ACCEPTABLE_CHARECTERS.Contains(replacementString) && newLength <= size;
            };
        }

        /// <summary>
        /// Method Name     : SetUiTextFieldLength
        /// Author          : Hiren Patel
        /// Creation Date   : 3 Dec 2017
        /// Purpose         : Set length of UITextField control
        /// Revision        : 
        /// </summary>
        /// <param name="uiTextField">UITextField control</param>
        /// <param name="size">Maximum length of the UITextField</param>
        public static void SetUiTextFieldAsAlphabetOnly(UITextField uiTextField, int? size = 50)
        {
            if (!size.HasValue)
                return;
            string ACCEPTABLE_CHARECTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ";
            uiTextField.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;  
               
                return ACCEPTABLE_CHARECTERS.Contains(replacementString) && newLength <= size;

            };
        }

        /// <summary>
        /// Method Name     : SetUiTextFieldLength
        /// Author          : Hiren Patel
        /// Creation Date   : 3 Dec 2017
        /// Purpose         : Set lenghth of UITextField control
        /// Revision        : 
        /// </summary>
        /// <param name="uiTextField">UITextField control</param>
        /// <param name="size">Maximum lenght of the UITextField</param>
        public static void SetUiTextFieldAsPassword(UITextField uiTextField, int? size = 50)
        {
            if (size.HasValue)
			{
				uiTextField.ShouldChangeCharacters = (textField, range, replacementString) =>
				{
					var newLength = textField.Text.Length + replacementString.Length - range.Length;

					return (replacementString!=" " && newLength <= size);

				};
			}
        }

        /// <summary>
        /// Method Name     : SetUiTextFieldAsCreditCardNumber
        /// Author          : Hiren Patel
        /// Creation Date   : 3 Dec 2017
        /// Purpose         : Set lenghth of UITextField control
        /// Revision        : 
        /// </summary>
        /// <param name="uiTextField">UITextField control</param>
        /// <param name="size">Maximum lenght of the UITextField</param>
        public static void SetUiTextFieldAsCreditCardNumber(UITextField uiTextField, int? size = 50)
        {
            if (!size.HasValue)
                return;
            string ACCEPTABLE_CHARECTERS = "1234567890-";
            uiTextField.KeyboardType = UIKeyboardType.NumberPad;
            uiTextField.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;
                return ACCEPTABLE_CHARECTERS.Contains(replacementString) && newLength <= size;
            };
        }



        /// <summary>
        /// Method Name     : SetUiTextFieldAsAmount
        /// Author          : Hiren Patel
        /// Creation Date   : 3 Dec 2017
        /// Purpose         : Set lenghth of UITextField control
        /// Revision        : 
        /// </summary>
        /// <param name="uiTextField">UITextField control</param>
        /// <param name="size">Maximum lenght of the UITextField</param>
        public static void SetUiTextFieldAsAmount(UITextField uiTextField, int? size = 50)
        {
            if (!size.HasValue)
                return;
            string ACCEPTABLE_CHARECTERS = "1234567890.";
            uiTextField.KeyboardType = UIKeyboardType.NumberPad;
            uiTextField.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;
                return ACCEPTABLE_CHARECTERS.Contains(replacementString) && newLength <= size;
            };
        }

        /// <summary>
        /// Gets or sets the calling screen.
        /// </summary>
        /// <value>The calling screen.</value>
        public static JKMEnum.UIViewControllerID CallingScreenContactUs { get; set; }

        /// <summary>
        /// Gets or sets the calling screen.
        /// </summary>
        /// <value>The calling screen.</value>
        public static JKMEnum.UIViewControllerID CallingScreen { get; set; }


        /// <summary>
        /// Method Name     : RedirectToViewController
        /// Author          : Hiren Patel
        /// Creation Date   : 4 Dec 2017
        /// Purpose         : Redirect UIViewController from other UIViewController
        /// Revision        : 
        /// </summary>
        /// <param name="sorunceViewController">Current UIViewController</param>
        /// <param name="destinationViewControllerName">Name of the destination UIViewController</param>
        /// <param name="storyboardName">Storayboard name</param>
        public static void RedirectToViewController(UIViewController sorunceViewController, JKMEnum.UIViewControllerID destinationViewControllerName, string storyboardName = "Main")
        {
            UIViewController destinationViewController;
            switch (destinationViewControllerName)
            {
                case JKMEnum.UIViewControllerID.EmailView:
                    destinationViewController = sorunceViewController.Storyboard.InstantiateViewController(JKMEnum.UIViewControllerID.EmailView.ToString()) as EmailViewController;
                    break;
                case JKMEnum.UIViewControllerID.EnterPasswordView:
                    destinationViewController = sorunceViewController.Storyboard.InstantiateViewController(destinationViewControllerName.ToString()) as EnterPasswordViewController;
                    break;
                case JKMEnum.UIViewControllerID.VerificationCodeView:
                    destinationViewController = sorunceViewController.Storyboard.InstantiateViewController(destinationViewControllerName.ToString()) as VerificationCodeViewController;
                    break;
                case JKMEnum.UIViewControllerID.CreatePasswordView:
                    destinationViewController = sorunceViewController.Storyboard.InstantiateViewController(destinationViewControllerName.ToString()) as CreatePasswordViewController;
                    break;
                case JKMEnum.UIViewControllerID.ContactusView:
                    destinationViewController = sorunceViewController.Storyboard.InstantiateViewController(destinationViewControllerName.ToString()) as ContactusViewController;
                    break;
                case JKMEnum.UIViewControllerID.PrivacyPolicyView:
                    destinationViewController = sorunceViewController.Storyboard.InstantiateViewController(destinationViewControllerName.ToString()) as PrivacyPolicyViewController;
                    break;
                case JKMEnum.UIViewControllerID.EstimateListView:
                    destinationViewController = sorunceViewController.Storyboard.InstantiateViewController(destinationViewControllerName.ToString()) as EstimateListViewController;
                    break;
                case JKMEnum.UIViewControllerID.EstimateReviewView:
                    destinationViewController = sorunceViewController.Storyboard.InstantiateViewController(destinationViewControllerName.ToString()) as EstimateReviewController;
                    break;
                case JKMEnum.UIViewControllerID.ViewEstimateReviewView:
                    destinationViewController = sorunceViewController.Storyboard.InstantiateViewController(destinationViewControllerName.ToString()) as ViewEstimateController;
                    break;
                case JKMEnum.UIViewControllerID.ServicesView:
                    destinationViewController = sorunceViewController.Storyboard.InstantiateViewController(destinationViewControllerName.ToString()) as ServicesViewController;
                    break;
                case JKMEnum.UIViewControllerID.ServiceDatesView:
                    destinationViewController = sorunceViewController.Storyboard.InstantiateViewController(destinationViewControllerName.ToString()) as ServiceDatesViewController;
                    break;
                case JKMEnum.UIViewControllerID.AddressesVew:
                    destinationViewController = sorunceViewController.Storyboard.InstantiateViewController(destinationViewControllerName.ToString()) as AddressesVewController;
                    break;
                case JKMEnum.UIViewControllerID.WhatMattersMostView:
                    destinationViewController = sorunceViewController.Storyboard.InstantiateViewController(destinationViewControllerName.ToString()) as WhatMattersMostViewController;
                    break;
                case JKMEnum.UIViewControllerID.ValuationView:
                    destinationViewController = sorunceViewController.Storyboard.InstantiateViewController(destinationViewControllerName.ToString()) as ValuationViewController;
                    break;
                case JKMEnum.UIViewControllerID.VitalInformationView:
                    destinationViewController = sorunceViewController.Storyboard.InstantiateViewController(destinationViewControllerName.ToString()) as VitalInformationViewController;
                    break;
                case JKMEnum.UIViewControllerID.AcknowledgementView:
                    destinationViewController = sorunceViewController.Storyboard.InstantiateViewController(destinationViewControllerName.ToString()) as AcknowledgementViewController;
                    break;
                case JKMEnum.UIViewControllerID.MoveConfirmedView:
                    destinationViewController = sorunceViewController.Storyboard.InstantiateViewController(destinationViewControllerName.ToString()) as MoveConfirmedViewController;
                    break;
                case JKMEnum.UIViewControllerID.UINavigationDashboard:
                    destinationViewController = sorunceViewController.Storyboard.InstantiateViewController(destinationViewControllerName.ToString()) as UINavigationController;
                    break;
                default:
                    destinationViewController = sorunceViewController.Storyboard.InstantiateViewController(destinationViewControllerName.ToString()) as EmailViewController;
                    break;

            }

            List<UIView> listUIView = new List<UIView>();
            foreach (UIView view in sorunceViewController.View.Subviews)
            {
                listUIView.Add(view);
            }
            sorunceViewController.View.AddSubview(destinationViewController.View);
            foreach (UIView view in listUIView)
            {
                view.RemoveFromSuperview();
            }
        }

        /// <summary>
        /// Method Name     : ShowAlertMessage
        /// Author          : Hiren Patel
        /// Creation Date   : 4 Dec 2017
        /// Purpose         : To display alert message 
        /// Revision        : 
        /// </summary>
        /// <param name="currentUIViewController"></param>
        /// <param name="message">message to display</param>
        public static void ShowAlertMessage(UIViewController currentUIViewController, string message = "")
        {
            string actionName = AppConstant.ALERT_OK_BUTTON_TEXT;
            //Create Alert
            var okAlertController = UIAlertController.Create("", message, UIAlertControllerStyle.Alert);

            //Add Action
            okAlertController.AddAction(UIAlertAction.Create(actionName, UIAlertActionStyle.Default, null));

            // Present Alert
            currentUIViewController.PresentViewController(okAlertController, true, null);
        }

        /// <summary>
        /// Method Name     : DismissKeyboardOnBackgroundTap
        /// Author          : Hiren Patel
        /// Creation Date   : 4 Dec 2017
        /// Purpose         :  Dismisses the keyboard on background tap.
        /// Revision        : 
        /// </summary>
        /// <param name="uiViewController">User interface view controller.</param>
        public static void DismissKeyboardOnBackgroundTap(UIViewController uiViewController)
        {          // Add gesture recognizer to hide keyboard            
            var tap = new UITapGestureRecognizer { CancelsTouchesInView = false };
            tap.AddTarget(() => uiViewController.View.EndEditing(true));
            uiViewController.View.AddGestureRecognizer(tap);
        }

        /// <summary>
        /// Method Name     : DismissKeyboardOnUITextField
        /// Author          : Hiren Patel
        /// Creation Date   : 4 Dec 2017
        /// Purpose         : Dismisses the keyboard on UITextField.
        /// Revision        : 
        /// </summary>
        /// <param name="uiTextField">User interface text field.</param>
        public static void DismissKeyboardOnUITextField(UITextField uiTextField)
        {
            uiTextField.ShouldReturn += (textField) =>
            {
                uiTextField.ResignFirstResponder();
                return true;
            };
        }

        /// <summary>
        /// Method Name     : DismissKeyboardOnUITextField
        /// Author          : Hiren Patel
        /// Creation Date   : 4 Dec 2017
        /// Purpose         : Dismisses the keyboard on UITextView.
        /// Revision        : 
        /// </summary>
        /// <param name="uiTextView">User interface text view.</param>
        public static void DismissKeyboardOnUITextField(UITextView uiTextView)
        {
            uiTextView.Ended += (sender, e) =>
            {
                uiTextView.ResignFirstResponder();
            };
        }

        /// <summary>
        /// Method Name     : IsInternetIsAvailabel
        /// Author          : Hiren Patel
        /// Creation Date   : 4 Dec 2017
        /// Purpose         : To check internet connection is availabel or not
        /// Revision        : 
        /// </summary>
        /// <returns><c>true</c>, if internet is availabel was ised, <c>false</c> otherwise.</returns>
        /// <param name="uiViewController">User interface view controller.</param>
        public static bool IsInternetIsAvailabel(UIViewController uiViewController)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                return true;
            }
            else
            {
                UIHelper.ShowAlertMessage(uiViewController, AppConstant.NO_INTERNET_MESSAGE);
                return false;
            }
        }

        /// <summary>
        /// Method Name     : SaveCustomerIDInCache
        /// Author          : Hiren Patel
        /// Creation Date   : 7 Jan 2018
        /// Purpose         : Saves the customer ID In cache.
        /// Revision        : 
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        public static void SaveCustomerIDInCache(string customerId)
        {
            string key = AppConstant.CUSTOMER_ID_CACHE_KEY;
            NSUserDefaults.StandardUserDefaults.SetString(customerId, key);
        }

        /// <summary>
        /// Method Name     : GetCustomerIDFromCach
        /// Author          : Hiren Patel
        /// Creation Date   : 7 Jan 2018
        /// Purpose         : Get Customer ID
        /// Revision        : 
        /// </summary>
        public static string GetCustomerIDFromCache()
        {
            NSString nsString = new NSString(AppConstant.CUSTOMER_ID_CACHE_KEY);
            var NsValue = NSUserDefaults.StandardUserDefaults.ValueForKey(nsString);
            if (NsValue is null)
            {
                return string.Empty;
            }
            else if (string.IsNullOrEmpty(NsValue.ToString()))
            {
                return string.Empty;
            }
            else
            {
                return NsValue.ToString();
            }
        }

        /// <summary>
        /// Method Name     : ClearIntroPagePermission
        /// Author          : Hiren Patel
        /// Creation Date   : 7 Jan 2018
        /// Purpose         : To clear intro page permission
        /// Revision        : 
        /// </summary>
        public static void ClearIntroPagePermission()
        {
            string key = string.Format(AppConstant.CUSTOMER_INTRO_KEY_FORMAT, UtilityPCL.LoginCustomerData.CustomerId);
            NSUserDefaults.StandardUserDefaults.SetString(string.Empty, key);
        }

        /// <summary>
        /// Method Name     : GetMyServiceRowAndColumnCount
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Jan 2018
        /// Purpose         : Gets my service row and column count.
        /// Revision        : 
        /// <summary>
        /// <param name="rowsCount">Rows count.</param>
        /// <param name="columnsCount">Columns count.</param>
        /// <param name="totalDisplayServiceCount">Total display service count.</param>
        public static void GetMyServiceRowAndColumnCount(ref int rowsCount, ref int columnsCount, int totalDisplayServiceCount, UIView uiMainView)
        {
            string deviceSize = UIHelper.ScreenSize(uiMainView);
            rowsCount = 1;
            columnsCount = totalDisplayServiceCount;
            int totalServiceCount = totalDisplayServiceCount;
            if (deviceSize == AppConstant.IPAD_WIDTH_AND_HEIGHT)
            {
                rowsCount = 1;
            }
            else if (totalServiceCount >= 7)
            {
                rowsCount = 3;
                columnsCount = 3;
            }
            else if (totalServiceCount >= 6)
            {
                rowsCount = 2;
                columnsCount = 3;
            }
            else if (totalServiceCount >= 3)
            {
                rowsCount = 1;
                columnsCount = 3;
            }

        }

        /// <summary>
        /// Method Name     : BindMyServiceData
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Jan 2018
        /// Purpose         : Binds my service data.
        /// Revision        : 
        /// </summary>
        /// <param name="myServicesModelList">myServicesModelList</param>
        public static void BindMyServiceData(List<MyServicesModel> myServicesModelList, UIScrollView scrollviewMyServices, UIView uiMainView, bool IsWizard = false)
        {
            int rowsCount = 1;
            int columnsCount = 3;
            MyServiceDisplayModel[] objMyServiceDisplayModelList = GetMyServiceDisplayModelList(myServicesModelList);
            int totalServiceCount = objMyServiceDisplayModelList.Length;
            UIHelper.GetMyServiceRowAndColumnCount(ref rowsCount, ref columnsCount, totalServiceCount, uiMainView);

            int firstLineY = 70;
            int count = 0;
            int separatorValue = IsWizard ? 90 : 100;
            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    if (count < totalServiceCount)
                    {
                        UIView objView = new UIView();
                        objView.ViewForBaselineLayout.Frame = new CoreGraphics.CGRect(10 + (j * separatorValue), (firstLineY), 125, 25);
                        UIImageView uiImageView = new UIImageView();
                        uiImageView.ViewForBaselineLayout.Frame = new CoreGraphics.CGRect(0, 0, 25, 25);
                        uiImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
                        uiImageView.Image = UIImage.FromFile(objMyServiceDisplayModelList[count].imageURL);
                        objView.Add(uiImageView);

                        UILabel uiLabel = new UILabel();
                        uiLabel.ViewForBaselineLayout.Frame = new CoreGraphics.CGRect(35, 0, 100, 25);
                        uiLabel.Text = string.Format(objMyServiceDisplayModelList[count].serviceName);
                        uiLabel.TextColor = UIColor.DarkGray;
                        UIHelper.SetLabelFont(uiLabel, 14.0f);
                        objView.Add(uiLabel);
                        scrollviewMyServices.Add(objView);
                    }
                    count++;
                }
                firstLineY += 35;
            }
        }

        /// <summary>
        /// Method Name     : GetMyServiceDisplayModelList
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Jan 2018
        /// Purpose         : To get my service display model list
        /// Revision        : 
        /// </summary>
        /// <param name="myServicesModelList">moveDataModel</param>
        public static MyServiceDisplayModel[] GetMyServiceDisplayModelList(List<MyServicesModel> myServicesModelList) //(MoveDataModel moveDataModel)
        {
            string imageURL = string.Empty;
            string serviceName = string.Empty;
            int count = 0;
            MyServiceDisplayModel[] objMyServiceDisplayModelList = new MyServiceDisplayModel[myServicesModelList.Count];
            foreach (MyServicesModel myServicesmodal in myServicesModelList)
            {
                MyServiceDisplayModel objMyServiceDisplayModel = new MyServiceDisplayModel();
                switch (myServicesmodal.ServicesCode)
                {

                    case "stg_Packing":
                        imageURL = AppConstant.MYSERVICE_BOOKED_IMAGE_URL;
                        serviceName = JKMEnum.MoveCode.Pack.ToString();
                        break;
                    case "stg_Loading":
                        imageURL = AppConstant.MYSERVICE_LOADED_IMAGE_URL;
                        serviceName = JKMEnum.MoveCode.Load.ToString();
                        break;
                    case "stg_UnLoading":
                        imageURL = AppConstant.MYSERVICE_PENDING_IMAGE_URL;
                        serviceName = JKMEnum.MoveCode.Export.ToString();
                        break;
                    case "stg_UnPacking":
                        imageURL = AppConstant.MYSERVICE_INVOICED_IMAGE_URL;
                        serviceName = JKMEnum.MoveCode.UnPack.ToString();
                        break;
                    case "stg_Storge":
                        imageURL = AppConstant.MYSERVICE_NEEDS_OVERRIDE_IMAGE_URL;
                        serviceName = JKMEnum.MoveCode.UnPack.ToString();
                        break;
                }
                objMyServiceDisplayModel.imageURL = imageURL;
                objMyServiceDisplayModel.serviceName = serviceName;
                objMyServiceDisplayModelList[count] = objMyServiceDisplayModel;
                count++;
            }
            return objMyServiceDisplayModelList;
        }

        /// <summary>
        /// Method Name     : IsIPAD
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Jan 2018
        /// Purpose         : To check Is Ipad device
        /// Revision        : 
        /// </summary>
        /// <returns><c>true</c>, if ipad <c>false</c> otherwise.</returns>
        /// <param name="mainView">Main view.</param>
        public static bool IsIPAD(UIView mainView)
        {
            return ScreenSize(mainView) == AppConstant.IPAD_WIDTH_AND_HEIGHT;
        }

        /// <summary>
        /// Method Name     : IsIPAD
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Jan 2018
        /// Purpose         : Screen size
        /// Revision        : 
        /// </summary>
        /// <returns>The size.</returns>
        /// <param name="mainView">Main view.</param>
        public static string ScreenSize(UIView mainView)
        {
            return string.Format("{0}X{1}", mainView.Frame.Width, mainView.Frame.Height);
        }



        ///// <summary>
        ///// Event Name      : BindgDataAsync
        ///// Author          : Hiren Patel
        ///// Creation Date   : 23 jan 2018
        ///// Purpose         : Bind data 
        ///// Revision        : 
        ///// </summary>
        public static async Task BindingDataAsync()
        {
            if (!string.IsNullOrEmpty(UIHelper.GetCustomerIDFromCache()))
            {
                if (UtilityPCL.LoginCustomerData is null)
                {
                    UtilityPCL.LoginCustomerData = new JKMPCL.Model.CustomerModel();
                }

                UtilityPCL.LoginCustomerData.CustomerId = UIHelper.GetCustomerIDFromCache();
                await DTOConsumer.GetCustomerProfileData();
                await DTOConsumer.BindMoveDataAsync();

            }
        }

        ///// <summary>
        ///// Event Name      : SetDefaultWizardScrollViewBorderProperty
        ///// Author          : Sanket Prajapati
        ///// Creation Date   : 29 jan 2018
        ///// Purpose         : Sets the default wizard scroll view border property.
        ///// Revision        : 
        ///// </summary>
        /// <param name="uiScrollView">User interface scroll view.</param>
        public static void SetDefaultWizardScrollViewBorderProperty(UIScrollView uiScrollView)
        {
            // http://spazzarama.com/2011/01/08/monotouch-uiview-with-curved-border-and-shadow/
            uiScrollView.Layer.BorderColor = UIColor.White.CGColor;
            uiScrollView.Layer.BackgroundColor = UIColor.White.CGColor;

            uiScrollView.Layer.MasksToBounds = false;
            uiScrollView.Layer.CornerRadius = 10;
            uiScrollView.Layer.ShadowColor = UIColor.LightGray.CGColor;
            uiScrollView.Layer.ShadowOpacity = 1.0f;
            uiScrollView.Layer.ShadowRadius = 3.0f;
            uiScrollView.Layer.ShadowOffset = new System.Drawing.SizeF(0f, 3f);
        }

        ///// <summary>
        ///// Event Name      : BindgDataAsync
        ///// Author          : Sanket Prajapati
        ///// Creation Date   : 29 jan 2018
        ///// Purpose         : Sets the default wizard scroll view border property.
        ///// Revision        : 
        ///// </summary>
        /// <param name="uiScrollView">User interface scroll view.</param>
        public static void SetDefaultScrollViewBorderProperty(UIScrollView uiScrollView)
        {
            // http://spazzarama.com/2011/01/08/monotouch-uiview-with-curved-border-and-shadow/
            uiScrollView.Layer.BorderColor = UIColor.LightGray.CGColor;
            uiScrollView.Layer.BorderWidth = 1;
            uiScrollView.Layer.BackgroundColor = UIColor.White.CGColor;
            uiScrollView.Layer.CornerRadius = 10;
            uiScrollView.Layer.ShadowColor = UIColor.LightGray.CGColor;
            uiScrollView.Layer.ShadowOpacity = 1.0f;
            uiScrollView.Layer.ShadowRadius = 3.0f;
            uiScrollView.Layer.ShadowOffset = new System.Drawing.SizeF(0f, 3f);

            uiScrollView.Layer.MasksToBounds = true;
            uiScrollView.ClipsToBounds = true;
        }

        ///// <summary>
        ///// Event Name      : BindgDataAsync
        ///// Author          : Sanket Prajapati
        ///// Creation Date   : 29 jan 2018
        ///// Purpose         : Sets the default estimate button border property.
        ///// Revision        : 
        ///// </summary>
        /// <param name="uiButton">User interface scroll view.</param>
        public static void SetDefaultEstimateButtonProperty(UIButton uiButton)
        {
            // https://stackoverflow.com/questions/4754392/uiview-with-rounded-corners-and-drop-shadow
            uiButton.Layer.CornerRadius = uiButton.Frame.Height / 2;

            uiButton.Layer.BorderColor = UIColor.White.CGColor;
            uiButton.Layer.BackgroundColor = UIColor.White.CGColor;
            // shadow
            uiButton.Layer.ShadowColor = UIColor.LightGray.CGColor;
            uiButton.Layer.ShadowOffset = new System.Drawing.SizeF(1.0f, 1.0f);
            uiButton.Layer.ShadowOpacity = 1.0f;
            uiButton.Layer.ShadowRadius = 2.0f;
        }

        ///// <summary>
        ///// Event Name      : BindgDataAsync
        ///// Author          : Sanket Prajapati
        ///// Creation Date   : 29 jan 2018
        ///// Purpose         : Sets the logged in customer IDI n cache.
        ///// Revision        : 
        ///// </summary>
        public static void SetLoggedInCustomerIDInCache()
        {
            if (!string.IsNullOrEmpty(UIHelper.GetCustomerIDFromCache()))
            {
                UtilityPCL.LoginCustomerData.CustomerId = UIHelper.GetCustomerIDFromCache();
            }
            else
            {
                if (UtilityPCL.LoginCustomerData is null)
                {
                    // No Customer Data found
                }
                else
                {
                    UIHelper.SaveCustomerIDInCache(UtilityPCL.LoginCustomerData.CustomerId);
                    UtilityPCL.LoginCustomerData.CustomerId = UIHelper.GetCustomerIDFromCache();
                }
            }
        }


        ///// <summary>
        ///// Event Name      : BindgDataAsync
        ///// Author          : Sanket Prajapati
        ///// Creation Date   : 29 jan 2018
        ///// Purpose         : Sets the submit button property.
        ///// Revision        : 
        ///// </summary>
        /// <param name="uiButton">User interface button.</param>
        public static void SetSubmitButtonProperty(UIButton uiButton)
        {
            uiButton.Layer.BackgroundColor = UIColor.FromRGB(229, 253, 254).CGColor;
            uiButton.SetTitleColor(UIColor.FromRGB(51, 212, 213), UIControlState.Normal);
        }

        ///// <summary>
        ///// Event Name      : SetUpdateNeedButtonProperty
        ///// Author          : Sanket Prajapati
        ///// Creation Date   : 29 jan 2018
        ///// Purpose         : Sets the update need button property.
        ///// Revision        : 
        ///// </summary>
        /// <param name="uiButton">User interface button.</param>
        public static void SetUpdateNeedButtonProperty(UIButton uiButton)
        {
            uiButton.Layer.BackgroundColor = UIColor.FromRGB(247, 248, 253).CGColor;
            uiButton.SetTitleColor(UIColor.FromRGB(159, 170, 196), UIControlState.Normal);
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
        public static string DisplayDateFormatForEstimate(DateTime? dateTime, string dateFormat = "MM/dd/yyyy")
        {
            try
            {
                if (dateTime.HasValue)
                {
                    return dateTime.Value.ToString(dateFormat);
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception)
            {
                return dateTime.ToString();
            }

        }

        /// <summary>
        /// Method Name     : DisplayDateFormatForCardExpiryDate
        /// Author          : Hiren Patel
        /// Creation Date   : 15 Jan 2018
        /// Purpose         : Display date in to specific format (Aknowledgement screen in estimates)
        /// Revision        : 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="dateFormat"></param>
        /// <returns></returns>
        public static string DisplayDateFormatForCardExpiryDate(DateTime? dateTime, string dateFormat = "MM/yy")
        {
            try
            {
                if (dateTime.HasValue)
                {
                    return dateTime.Value.ToString(dateFormat);
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception)
            {
                return dateTime.ToString();
            }

        }

        /// <summary>
        /// Method Name     : CreateWizardHeader
        /// Author          : Hiren Patel
        /// Creation Date   : 15 Jan 2018
        /// Purpose         : Creates the wizard header.
        /// Revision        : 
        /// </summary>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="headerView">Header view.</param>
        public static void CreateWizardHeader(int pageIndex, UIView headerView, EstimateModel estimateModel)
        {
            int PageLength = 10;
            bool isDepositePaid = false;
            if (estimateModel != null)
            {
                PageLength = estimateModel.IsDepositPaid ? 9 : 10;
                isDepositePaid = estimateModel.IsDepositPaid;
            }

            ClearHeaderView(headerView);
            AddProgressBarToHeaderView(headerView, pageIndex, PageLength);

            UIView uiViewsteps = new UIView();
            uiViewsteps.ViewForBaselineLayout.Frame = new CoreGraphics.CGRect(headerView.Frame.Width - 318, 20, 318, 40);

            uiViewsteps.AutoresizingMask = UIViewAutoresizing.FlexibleBottomMargin | UIViewAutoresizing.FlexibleLeftMargin;
            headerView.AddSubview(uiViewsteps);

            int rowsCount = isDepositePaid ? 17 : 19;
            int imageCounter = 1;
            int startXposition = 7;
            if (rowsCount == 17)
            {
                startXposition = 35;
            }

            for (int i = 0; i < rowsCount; i++)
            {
                if (i % 2 == 0)
                {
                    UIImageView uiImageView = new UIImageView();
                    uiImageView.ViewForBaselineLayout.Frame = new CoreGraphics.CGRect(startXposition + (i * 16), 25, 15, 15);
                    uiImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
                    uiImageView.Image = UIImage.FromFile(GetWizardStepImageURl(imageCounter, pageIndex));
                    uiViewsteps.AddSubview(uiImageView);
                    imageCounter++;
                }
                else
                {
                    UIView uiView = new UIView();
                    uiView.ViewForBaselineLayout.Frame = new CoreGraphics.CGRect(startXposition + (16 * i), 32, 15, 2);
                    uiView.Layer.BackgroundColor = GetSeparatorviewBackGroundColor(imageCounter, pageIndex);
                    uiViewsteps.AddSubview(uiView);
                }

            }
        }

        /// <summary>
        /// Method Name     : ClearHeaderView
        /// Author          : Hiren Patel
        /// Creation Date   : 15 Jan 2018
        /// Purpose         : Clears the header view.
        /// Revision        : 
        /// </summary>
        /// <param name="headerView">Header view.</param>
        private static void ClearHeaderView(UIView headerView)
        {
            foreach (UIView view in headerView.Subviews)
            {
                view.RemoveFromSuperview();
            }
        }

        /// <summary>
        /// Method Name     : AddProgressBarToHeaderView
        /// Author          : Hiren Patel
        /// Creation Date   : 15 Jan 2018
        /// Purpose         : Adds the progress bar to header view.
        /// Revision        : 
        /// </summary>
        /// <param name="headerView">Header view.</param>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageLength">Page length.</param>
        private static void AddProgressBarToHeaderView(UIView headerView, int pageIndex, int pageLength)
        {
            UIProgressView progressview = new UIProgressView();
            progressview.SetProgress((float)pageIndex / pageLength , true);
            progressview.ProgressTintColor = UIColor.FromRGB(127, 219, 216);
            progressview.ViewForBaselineLayout.Frame = new CoreGraphics.CGRect(0, 25, 414, 15);
            headerView.AddSubview(progressview);
        }

        /// <summary>
        /// Method Name     : GetWizardStepImageURl
        /// Author          : Hiren Patel
        /// Creation Date   : 15 Jan 2018
        /// Purpose         : Gets the wizard step image UR.
        /// Revision        : 
        /// </summary>
        /// <returns>The wizard step image UR.</returns>
        /// <param name="imageCounter">Image counter.</param>
        /// <param name="pageIndex">Page index.</param>
        private static string GetWizardStepImageURl(int imageCounter, int pageIndex)
        {
            string imageUrl = "completed.png";

            if ((imageCounter) < pageIndex)
            {
                imageUrl = "completed.png";
            }
            else if (imageCounter == pageIndex)
            {
                imageUrl = string.Format("{0}_active.png", imageCounter);
            }
            else
            {
                imageUrl = string.Format("{0}_disable.png", imageCounter);
            }
            return imageUrl;
        }

        /// <summary>
        /// Method Name     : GetSeparatorviewBackGroundColor
        /// Author          : Hiren Patel
        /// Creation Date   : 15 Jan 2018
        /// Purpose         : Gets the color of the separatorview back ground.
        /// Revision        : 
        /// </summary>
        /// <returns>The separatorview back ground color.</returns>
        /// <param name="imageCounter">Image counter.</param>
        /// <param name="pageIndex">Page index.</param>
        private static CGColor GetSeparatorviewBackGroundColor(int imageCounter, int pageIndex)
        {
            CGColor viewBackGroundColor = UIColor.FromRGB(236, 237, 243).CGColor;
            if ((imageCounter) <= pageIndex)
            {
                viewBackGroundColor = UIColor.FromRGB(127, 219, 216).CGColor;
            }
            else
            {
                viewBackGroundColor = UIColor.FromRGB(236, 237, 243).CGColor;
            }
            return viewBackGroundColor;
        }

        public static string GetPaymentCardImage(string cardNumner)
        {
            CardType cardType = UtilityPCL.GetCardType(cardNumner);
            if (CardType.MasterCard == cardType)
            {
                return AppConstant.PAYMENT_MASTER_CARD_IMAGE_URL;
            }
            else if (CardType.VISA == cardType)
            {
                return AppConstant.PAYMENT_VISA_CARD_IMAGE_URL;
            }
            else
            {
                return AppConstant.PAYMENT_DEFAULT_CARD_IMAGE_URL;
            }
        }

        /// <summary>
        /// Method Name     : BindCardHolderNameToPaymentModel
        /// Author          : Hiren Patel
        /// Creation Date   : 15 Feb 2018
        /// Purpose         : Binds the card holder name to payment model.
        /// Revision        : 
        /// </summary>
        /// <param name="paymentGatewayModel">Payment gateway model.</param>
        /// <param name="nameOfCardholder">Name of cardholder.</param>
        public static void BindCardHolderNameToPaymentModel(PaymentGatewayModel paymentGatewayModel, string nameOfCardholder)
        {
            if (!string.IsNullOrEmpty(nameOfCardholder))
            {
                if (nameOfCardholder.Split(' ').Length > 1)
                {
                    paymentGatewayModel.FirstName = nameOfCardholder.Split(' ')[0];
                    paymentGatewayModel.LastName = nameOfCardholder.Split(' ')[1];
                }
                else
                {
                    paymentGatewayModel.FirstName = nameOfCardholder.Split(' ')[0];
                }
            }
        }

        /// <summary>
        /// Method Name     : BindCardNumberToPaymentModel
        /// Author          : Hiren Patel
        /// Creation Date   : 15 Feb 2018
        /// Purpose         : Binds the card number to payment model.
        /// Revision        : 
        /// </summary>
        /// <param name="paymentGatewayModel">Payment gateway model.</param>
        /// <param name="cardNumber">Card Number.</param>
        public static void BindCardNumberToPaymentModel(PaymentGatewayModel paymentGatewayModel,string cardNumber)
        {
            if (!string.IsNullOrEmpty(cardNumber))
            {
                paymentGatewayModel.CreditCardNumber = cardNumber.Replace(" ", "").Replace("-", "");
            }
        }

        /// <summary>
        /// Method Name     : BindCVVToPaymentModel
        /// Author          : Hiren Patel
        /// Creation Date   : 15 Feb 2018
        /// Purpose         : Binds the CVVT o payment model.
        /// Revision        : 
        /// </summary>
        /// <param name="paymentGatewayModel">Payment gateway model.</param>
        /// <param name="CVV">CVV.</param>
        public static void BindCVVToPaymentModel(PaymentGatewayModel paymentGatewayModel, string CVV)
        {
            if (!string.IsNullOrEmpty(CVV))
            {
                decimal number;
                if (!Decimal.TryParse(CVV, out number))
                {
                    paymentGatewayModel.CVVNo = 0;
                }
                else
                {
                    paymentGatewayModel.CVVNo = Convert.ToInt32(CVV);
                }
            }
            else 
            {
                paymentGatewayModel.CVVNo = -1;
            }
        }

        /// <summary>
        /// Method Name     : BindExpiryDateToPaymentModel
        /// Author          : Hiren Patel
        /// Creation Date   : 15 Feb 2018
        /// Purpose         : Binds the expiry date to payment model.
        /// Revision        : 
        /// </summary>
        /// <param name="paymentGatewayModel">Payment gateway model.</param>
        /// <param name="expiredDate">Expiry Date.</param>
        public static void BindExpiryDateToPaymentModel(PaymentGatewayModel paymentGatewayModel, string expiredDate)
        {
            if (!string.IsNullOrEmpty(expiredDate))
            {
                paymentGatewayModel.CardExpiryDate = expiredDate.Replace("/", "");
            }
        }

        /// <summary>
        /// Method Name     : BindDepositToPaymentModel
        /// Author          : Hiren Patel
        /// Creation Date   : 15 Feb 2018
        /// Purpose         : Binds the deposit to payment model.
        /// Revision        : 
        /// </summary>
        /// <param name="paymentGatewayModel">Payment gateway model.</param>
        /// <param name="transactionAmout">Transaction Amout.</param>
        public static void BindDepositToPaymentModel(PaymentGatewayModel paymentGatewayModel,string transactionAmout)
        {
            if (!IsNullOrEmptyOrWhiteSpace(transactionAmout))
            {
                paymentGatewayModel.TransactionAmout = Convert.ToDouble(RemoveCurrencyFormat(transactionAmout));
            }
        }

    }


}
