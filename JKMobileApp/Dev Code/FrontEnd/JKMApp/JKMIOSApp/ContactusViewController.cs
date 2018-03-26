using Foundation;
using System;
using UIKit;
using JKMPCL.Model;
using JKMPCL.Services;
using System.Threading.Tasks;

namespace JKMIOSApp
{
    /// <summary>
    /// Controller Name : ContactusViewController
    /// Author          : Hiren Patel
    /// Creation Date   : 29 Dec 2017
    /// Purpose         : To display contactus page screen
    /// Revision        : 
    /// </summary>
    public partial class ContactusViewController : UIViewController
    {
        readonly Move moveServices;
        string fullName = string.Empty;

        public ContactusViewController(IntPtr handle) : base(handle)
        {
            moveServices = new Move();
        }
        public async override void ViewDidLoad()
        {
            UIHelper.SetButtonFont(btnSend);
            UIHelper.SetButtonFont(btnClose);

            UIHelper.DismissKeyboardOnBackgroundTap(this);
            UIHelper.DismissKeyboardOnUITextField(txtYourQuestion);

            SetSendButtonProperty();

            txtYourQuestion.Layer.MasksToBounds = true;
            txtYourQuestion.Layer.CornerRadius = 8.0f;
            txtYourQuestion.Layer.BorderWidth = 1;
            txtYourQuestion.Layer.BorderColor = new CoreGraphics.CGColor(0.5f, 0.5f);

            lblEmailId.Text = string.Empty;
            lblPhoneNumber.Text = string.Empty;

            await CallGetContactMoveService();

            SetPhoneOpenProperty();
            SetEmailOpenProperty();

            btnSend.TouchUpInside += BtnSend_TouchUpInside;
            btnClose.TouchUpInside += BtnClose_TouchUpInside;


        }

        /// <summary>
        /// Method Name     : CallGetContactMoveService
        /// Author          : Hiren Patel
        /// Creation Date   : 5 Jan 2018
        /// Purpose         : To calls get contact move services
        /// Revision        : 
        /// </summary>
        private async Task CallGetContactMoveService()
        {
            string errorMessage = string.Empty;
            try
            {
                if (UtilityPCL.LoginCustomerData != null)
                {
                    fullName = UtilityPCL.LoginCustomerData.CustomerFullName;
                }
                
                LoadingOverlay objectLoadingScreen = UIHelper.ShowLoadingScreen(View);
                APIResponse<GetContactListForMoveResponse> serviceResponse = await moveServices.GetContactListForMove();
                objectLoadingScreen.Hide();
                if (serviceResponse.STATUS)
                {
                    lblEmailId.Text = serviceResponse.DATA.internalemailaddress;
                    lblPhoneNumber.Text = serviceResponse.DATA.address1_telephone1;

                }
            }
            catch (Exception error)
            {
                errorMessage = error.Message;
            }
            finally
            {
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    UIHelper.ShowAlertMessage(this, errorMessage);
                }
            }
        }

        /// <summary>
        /// Method Name     : SetSentButtonProperty
        /// Author          : Hiren Patel
        /// Creation Date   : 3 Dec 2017
        /// Purpose         : Sets the send button property.
        /// Revision        : 
        /// </summary>
        private void SetSendButtonProperty()
        {
            btnSend.SetImage(UIImage.FromFile(AppConstant.CONTACTUS_SEND_BUTTON_IMAGE_URL), UIControlState.Normal);
            btnSend.TintColor = UIColor.White;
            btnSend.ImageEdgeInsets = new UIEdgeInsets(0, -50, 0, 0);
            btnSend.TitleEdgeInsets = new UIEdgeInsets(0, -10, 0, 0);
            btnSend.Layer.MasksToBounds = true;
            btnSend.Layer.CornerRadius = 20.0f;
            btnSend.Layer.BorderWidth = 0;
            btnSend.Layer.BorderColor = new CoreGraphics.CGColor(0.5f, 0.5f);
        }

        /// <summary>
        /// Method Name     : SetPhoneOpenProperty
        /// Author          : Hiren Patel
        /// Creation Date   : 3 Dec 2017
        /// Purpose         : Sets the phone open property.
        /// Revision        : 
        /// </summary>
        private void SetPhoneOpenProperty()
        {
            string phoneNumner = lblPhoneNumber.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();

            UITapGestureRecognizer labelPhoneTap = new UITapGestureRecognizer(() =>
           {
               using (var encoded = new NSString(string.Format("tel:{0}", phoneNumner)))
               using (var url = NSUrl.FromString(encoded))
               {
                   if (url != null)
                   {
                       UIApplication.SharedApplication.OpenUrl(url);
                   }

               }
           });

            UITapGestureRecognizer icomPhoneTap = new UITapGestureRecognizer(() =>
            {
                using (var encoded = new NSString(string.Format("tel:{0}", phoneNumner)))
                using (var url = NSUrl.FromString(encoded))
                {
                    if (url != null)
                    {
                        UIApplication.SharedApplication.OpenUrl(url);
                    }
                }

            });

            lblPhoneNumber.AddGestureRecognizer(labelPhoneTap);
            lblPhoneNumber.UserInteractionEnabled = true;

            imgPhoneIcon.AddGestureRecognizer(icomPhoneTap);
            imgPhoneIcon.UserInteractionEnabled = true;
        }

        /// <summary>
        /// Method Name     : SetEmailOpenProperty
        /// Author          : Hiren Patel
        /// Creation Date   : 3 Dec 2017
        /// Purpose         : Sets the email open property.
        /// Revision        : 
        /// </summary>
        private void SetEmailOpenProperty()
        {
            UITapGestureRecognizer labelEmailTap = new UITapGestureRecognizer(() =>
            {
                OpenMail("");
            });

            UITapGestureRecognizer imgEmailIconTap = new UITapGestureRecognizer(() =>
            {
                OpenMail("");
            });

            lblEmailId.AddGestureRecognizer(labelEmailTap);
            lblEmailId.UserInteractionEnabled = true;

            imgEmailIcon.AddGestureRecognizer(imgEmailIconTap);
            imgEmailIcon.UserInteractionEnabled = true;

        }

        /// <summary>
        /// Method Name     : OpenMail
        /// Author          : Hiren Patel
        /// Creation Date   : 3 Dec 2017
        /// Purpose         : Opens the mail to send message
        /// Revision        : 
        /// </summary>
        private void OpenMail(string message)
        {
            string email = lblEmailId.Text;
            string subject = AppConstant.CONTACTUS_MAIL_SUBJECT;
            if (string.IsNullOrEmpty(fullName))
            {
                subject = AppConstant.CONTACTUS_MAIL_SUBJECT;
            }
            else
            {
                subject = string.Format(AppConstant.CONTACTUS_MAIL_SUBJECT_WITH_NAME_FORMAT, fullName);
            }

            string body = message;
            using (var encoded = new NSString($"mailto:{email}?subject={subject}&body={body}").CreateStringByAddingPercentEscapes(NSStringEncoding.UTF8))
            using (var url = NSUrl.FromString(encoded))
            {
                if (url != null)
                {
                    UIApplication.SharedApplication.OpenUrl(url);
                }
            }

        }

        /// <summary>
        /// Method Name     : BtnSend_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 3 Dec 2017
        /// Purpose         : To close screen
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Arguments</param>
        private void BtnSend_TouchUpInside(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtYourQuestion.Text) || string.IsNullOrWhiteSpace(txtYourQuestion.Text))
            {
                UIHelper.ShowAlertMessage(this, AppConstant.CONTACTUS_MAIL_VALIDATIN_MESSAGE);
            }
            else
            {
                UIHelper.ShowAlertMessage(this, AppConstant.CONTACTUS_MAIL_SUCESS_MESSAGE);
            }
        }

        /// <summary>
        /// Method Name     : BtnClose_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 3 Dec 2017
        /// Purpose         : To call OpenMail method
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Arguments</param>
        private void BtnClose_TouchUpInside(object sender, EventArgs e)
        {
            if (NavigationController != null)
            {
                DismissViewControllerAsync(true);
            }
            else
            {
                if (UIHelper.CallingScreenContactUs == JKMEnum.UIViewControllerID.UINavigationDashboard)
                {
                    PerformSegue("contactusToDashboard", this);
                }
                else
                {
                    UIHelper.RedirectToViewController(this, UIHelper.CallingScreenContactUs);
                }
            }

        }
    }
}