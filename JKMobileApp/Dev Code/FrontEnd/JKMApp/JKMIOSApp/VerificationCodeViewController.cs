using JKMPCL.Model;
using System;
using UIKit;

namespace JKMIOSApp
{
    /// <summary>
    /// Controller Name : VerificationCodeViewController
    /// Author          : Hiren Patel
    /// Creation Date   : 16 JAN 2018
    /// Purpose         : To display code verification screen as part of forgot password processs
    /// Revision        : 
    /// </summary>
    public partial class VerificationCodeViewController : BaseViewController
    {
        public VerificationCodeViewController(IntPtr handle) : base(handle)
        {
        }
        public override void ViewDidLoad()
        {
            SetDigitCodeLableProperty();
            SetVerificationUITextFieldProperty();
            SetHighLightDigitCodeProperty();
            SetBackButtonProperty();
            AddTapGestureRecognizerToLabel();
            UIHelper.SetLabelFont(lblEnterYour6DigitCode);
            UIHelper.SetButtonFont(btnBack);
            UIHelper.SetButtonFont(btnContinue);
            UIHelper.DismissKeyboardOnBackgroundTap(this);
            btnContinue.TouchUpInside += BtnContinue_TouchUpInside;
        }

        /// <summary>
        /// Method Name     : SetDigitCodeLableProperty
        /// Author          : Hiren Patel
        /// Creation Date   : 22 Dec 2017
        /// Purpose         : Set font of UI label control
        /// Revision        : 
        /// </summary>
        /// <summary>
        /// Sets the digit code lable property.
        /// </summary>
        private void SetDigitCodeLableProperty()
        {
            ResetVerifcationCodeLabel();

            UIHelper.SetLabelFont(lblDigit1, 30);
            UIHelper.SetLabelFont(lblDigit2, 30);
            UIHelper.SetLabelFont(lblDigit3, 30);
            UIHelper.SetLabelFont(lblDigit4, 30);
            UIHelper.SetLabelFont(lblDigit5, 30);
            UIHelper.SetLabelFont(lblDigit6, 30);
        }

        /// <summary>
        /// Method Name     : SetVerificationUITextFieldProperty
        /// Author          : Hiren Patel
        /// Creation Date   : 22 Dec 2017
        /// Purpose         : Sets the verification UIT ext field property.
        /// Revision        : 
        /// </summary>
        private void SetVerificationUITextFieldProperty()
        {
            txtVerificationCode.Hidden = true;
            txtVerificationCode.EditingChanged += TxtVerificationCode_EditingChanged;
            txtVerificationCode.TintColor = UIColor.Clear;
            txtVerificationCode.KeyboardType = UIKeyboardType.NumberPad;
            UIHelper.SetUiTextFieldAsNumberOnly(txtVerificationCode, 6);
        }

        /// <summary>
        /// Method Name     : SetBackButtonProperty
        /// Author          : Hiren Patel
        /// Creation Date   : 22 Dec 2017
        /// Purpose         : Sets the back button property.
        /// Revision        : 
        /// </summary>
        private void SetBackButtonProperty()
        {
            btnBack.SetImage(UIImage.FromFile(AppConstant.VERIFICATION_BACK_ARROW_URL), UIControlState.Normal);
            btnBack.TintColor = UIColor.White;
            btnBack.ImageEdgeInsets = new UIEdgeInsets(0, -50, 0, 0);
            btnBack.TitleEdgeInsets = new UIEdgeInsets(0, 0, 0, -5);
            btnBack.TouchUpInside += BtnBack_TouchUpInside;
        }

        /// <summary>
        /// Method Name     : SetHighLightDigitCodeProperty
        /// Author          : Hiren Patel
        /// Creation Date   : 22 Dec 2017
        /// Purpose         : Sets the high light digit code property.
        /// Revision        : 
        /// </summary>
        private void SetHighLightDigitCodeProperty()
        {
            GetTextBoxFocusedColor(lblDigit1, viewHighLightDigit1);
            GetTextBoxFocusedColor(lblDigit2, viewHighLightDigit2);
            GetTextBoxFocusedColor(lblDigit3, viewHighLightDigit3);
            GetTextBoxFocusedColor(lblDigit4, viewHighLightDigit4);
            GetTextBoxFocusedColor(lblDigit5, viewHighLightDigit5);
            GetTextBoxFocusedColor(lblDigit6, viewHighLightDigit6);
        }

        /// <summary>
        /// Method Name     : TxtVerificationCode_EditingChanged
        /// Author          : Hiren Patel
        /// Creation Date   : 22 Dec 2017
        /// Purpose         : Texts the verification code editing changed.
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void TxtVerificationCode_EditingChanged(object sender, EventArgs e)
        {
            switch(txtVerificationCode.Text.Length)
            {
                case 6:
                    lblDigit6.Text = txtVerificationCode.Text.Substring(5, 1);
                    break;
                case 5:
                    lblDigit6.Text = string.Empty;
                    lblDigit5.Text = txtVerificationCode.Text.Substring(4, 1);
                    break;
                case 4:
                    lblDigit6.Text = string.Empty;
                    lblDigit5.Text = string.Empty;
                    lblDigit4.Text = txtVerificationCode.Text.Substring(3, 1);
                    break;
                case 3:
                    lblDigit6.Text = string.Empty;
                    lblDigit5.Text = string.Empty;
                    lblDigit4.Text = string.Empty;
                    lblDigit3.Text = txtVerificationCode.Text.Substring(2, 1);
                    break;
                case 2:
                    lblDigit6.Text = string.Empty;
                    lblDigit5.Text = string.Empty;
                    lblDigit4.Text = string.Empty;
                    lblDigit3.Text = string.Empty;
                    lblDigit2.Text = txtVerificationCode.Text.Substring(1, 1);
                    break;
                case 1:
                    ResetVerifcationCodeLabel();
                    lblDigit1.Text = txtVerificationCode.Text;
                    break;
                default :
                    ResetVerifcationCodeLabel();
                    break;
            }
            SetHighLightDigitCodeProperty();
        }

        /// <summary>
        /// Method Name     : ResetVerifcationCodeLabel
        /// Author          : Hiren Patel
        /// Creation Date   : 22 Dec 2017
        /// Purpose         : Resets the verifcation code label.
        /// Revision        : 
        /// Resets the verifcation code label.
        /// </summary>
        private void ResetVerifcationCodeLabel()
        {
            lblDigit1.Text = string.Empty;
            lblDigit2.Text = string.Empty;
            lblDigit3.Text = string.Empty;
            lblDigit4.Text = string.Empty;
            lblDigit5.Text = string.Empty;
            lblDigit6.Text = string.Empty;
        }


        /// <summary>
        /// Method Name     : GetTextBoxFocusedColor
        /// Author          : Hiren Patel
        /// Creation Date   : 22 Dec 2017
        /// Purpose         : Gets the color of the text box focused.
        /// Revision        : 
        /// </summary>
        /// <param name="UILabel">UIL abel.</param>
        /// <param name="UiView">User interface view.</param>
        private void GetTextBoxFocusedColor(UILabel UILabel, UIView UiView)
        {
            UiView.Layer.BackgroundColor = string.IsNullOrEmpty(UILabel.Text) ? UIColor.Gray.CGColor : UIColor.Red.CGColor;
        }

        /// <summary>
        /// Method Name     : AddTapGestureRecognizerToLabel
        /// Author          : Hiren Patel
        /// Creation Date   : 22 Dec 2017
        /// Purpose         : Adds the tap gesture recognizer to label.
        /// Revision        : 
        /// </summary>
        private void AddTapGestureRecognizerToLabel()
        {
            UIHelper.DismissKeyboardOnUITextField(txtVerificationCode);
            AddTapGestureRecognizerToDigitLabel(lblDigit1);
            AddTapGestureRecognizerToDigitLabel(lblDigit2);
            AddTapGestureRecognizerToDigitLabel(lblDigit3);
            AddTapGestureRecognizerToDigitLabel(lblDigit4);
            AddTapGestureRecognizerToDigitLabel(lblDigit5);
        }

        public void AddTapGestureRecognizerToDigitLabel(UILabel uiLabel)
        {
            uiLabel.UserInteractionEnabled = true;
            UITapGestureRecognizer lblDigitTap = new UITapGestureRecognizer(() =>
            {
                txtVerificationCode.BecomeFirstResponder();

            });

            uiLabel.AddGestureRecognizer(lblDigitTap);
        }

        /// <summary>
        /// Event Name      : BtnBack_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 22 Dec 2017
        /// Purpose         : To get back screen
        /// Revision        : 
        /// <summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void BtnBack_TouchUpInside(object sender, EventArgs e)
        {

            UIHelper.RedirectToViewController(sorunceViewController: this, destinationViewControllerName: JKMEnum.UIViewControllerID.EmailView);

        }

        /// <summary>
        /// Event Name      : BtnContinue_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 22 Dec 2017
        /// Purpose         : To check verification code is valid or not
        /// Revision        : 
        /// <summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void BtnContinue_TouchUpInside(object sender, EventArgs e)
        {
            string errorMessage = string.Empty;
            try
            {
                int? code = null;
                string VerificationCode = string.Format("{0}{1}{2}{3}{4}{5}", lblDigit1.Text, lblDigit2.Text, lblDigit3.Text, lblDigit4.Text, lblDigit5.Text, lblDigit6.Text);
                if(!string.IsNullOrEmpty(VerificationCode) && !string.IsNullOrWhiteSpace(VerificationCode))
                {
                    code = Convert.ToInt32(VerificationCode);
                }

                ServiceResponse serviceResponse = loginServices.GetVerifyCode(code);
                if (serviceResponse != null)
                {
                    if (string.IsNullOrEmpty(serviceResponse.Message))
                    {
                        if (serviceResponse.Status)
                        {
                            UIHelper.RedirectToViewController(sorunceViewController: this, destinationViewControllerName: JKMEnum.UIViewControllerID.CreatePasswordView);
                        }
                    }
                    else
                    {
                        errorMessage = serviceResponse.Message;
                    }
                }
            }
            catch (System.Exception error)
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

    }
}