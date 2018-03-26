using System;
using Foundation;
using JKMPCL.Model;
using UIKit;
namespace JKMIOSApp
{
    /// <summary>
    /// Class Name      : myNotificationTableCell
    /// Author          : Hiren Patel
    /// Creation Date   : 16 JAN 2018
    /// Purpose         : To display alert item
    /// Revision        : 
    /// </summary>
    public partial class myNotificationTableCell : UITableViewCell
    {
        public AlertModel alertModel { get; set; }

        public myNotificationTableCell(IntPtr handle) : base(handle)
        {
        }
        public override void AwakeFromNib()
        {
            scrollViewCorner.Layer.CornerRadius = 10;
            viewNotificationTypeContainer.Layer.CornerRadius = 10;
            btnAddToCalender.TouchUpInside += BtnAddToCalender_TouchUpInside;
        }

        /// <summary>
        /// Method Name     : SetNotifcationTypeImage
        /// Author          : Hiren Patel
        /// Creation Date   : 30 Dec 2017
        /// Purpose         : Sets the notifcation type image and text color and background color
        /// Revision        : 
        /// </summary>
        /// <param name="alertType">Alert type.</param>
        public void SetNotifcationTypeImage(string alertType)
        {
            string imageURL = AppConstant.ALERT_BOOK_YOUR_MOVE_IMAGE_URL;
            UIColor uiColorCode = UIColor.FromRGB(226, 194, 132);
            switch (alertType)
            {
                case "0":
                    uiColorCode = UIColor.FromRGB(224, 139, 161);
                    imageURL = AppConstant.ALERT_COMPLETE_WIZARD_REMINDER_IMAGE_URL;
                    break;
                case "1":

                    uiColorCode = UIColor.FromRGB(226, 194, 132);
                    imageURL = AppConstant.ALERT_BOOK_YOUR_MOVE_IMAGE_URL;
                    break;
                case "2":
                    uiColorCode = UIColor.FromRGB(134, 219, 180);
                    imageURL = AppConstant.ALERT_PRE_MOVE_CONFIRMATION_IMAGE_URL;
                    break;
                case "3":
                    uiColorCode = UIColor.FromRGB(134, 172, 219);
                    imageURL = AppConstant.ALERT_DAY_OF_SERVICE_CHECKING_IMAGE_URL;
                    break;
                case "4":
                    uiColorCode = UIColor.FromRGB(131, 205, 226);
                    imageURL = AppConstant.ALERT_END_OF_SERVICE_CHECKING_IMAGE_URL;
                    break;
                case "5":
                    uiColorCode = UIColor.FromRGB(244, 191, 139);
                    imageURL = AppConstant.ALERT_FINAL_PAYMENT_MODE_IMAGE_URL;
                    break;
                case "6":
                    uiColorCode = UIColor.FromRGB(134, 172, 219);
                    imageURL = AppConstant.ALERT_DATE_OF_SERVICE_CHANGE_IMAGE_URL;
                    break;

            }
            lblNotificationTime.TextColor = uiColorCode;
            viewNotificationTypeContainer.Layer.BackgroundColor = uiColorCode.CGColor;
            imgNotificationTypeIcon.Image = UIImage.FromFile(imageURL);

        }

        /// <summary>
        /// Method Name     : SetNotifcationTitle
        /// Author          : Hiren Patel
        /// Creation Date   : 30 Dec 2017
        /// Purpose         : Sets the notifcation title.
        /// Revision        : 
        /// </summary>
        /// <param name="title">Title.</param>
        public void SetNotifcationTitle(string title)
        {
            lblNotificationTitle.Text = title;
        }

        /// <summary>
        /// Method Name     : SetNotificationTime
        /// Author          : Hiren Patel
        /// Creation Date   : 30 Dec 2017
        /// Purpose         :  Sets the notification time.
        /// Revision        : 
        /// </summary>
        /// <param name="time">Time.</param>
        public void SetNotificationTime(DateTime? time)
        {
            if (time.HasValue)
            {
                lblNotificationTime.Text = GetTimeAndDay(time);
            }
            else
            {
                lblNotificationTime.Text = string.Empty;
            }
        }

        /// <summary>
        /// Method Name     : GetTimeAndDay
        /// Author          : Hiren Patel
        /// Creation Date   : 30 Dec 2017
        /// Purpose         : Gets the time and day.
        /// Revision        : 
        /// </summary>
        /// <returns>The time and day.</returns>
        /// <param name="dtInsertTime">Dt insert time.</param>
        public string GetTimeAndDay(object dtInsertTime)
        {
            DateTime dtTime = (DateTime)dtInsertTime;
            string strCreateTime = "";
            TimeSpan ts = DateTime.Now - dtTime;
            if (ts.Days == 1)
            {
                strCreateTime = ts.Days + " day ago";
            }
            else if (ts.Days >= 1)
            {
                strCreateTime = ts.Days + " days ago";
            }
            else if (ts.Hours == 1)
            {
                strCreateTime = ts.Hours + " hour ago";
            }
            else if (ts.Hours >= 1)
            {
                strCreateTime = ts.Hours + " hours ago";
            }
            else if (ts.Minutes == 1)
            {
                strCreateTime = ts.Minutes + " minute ago";
            }
            else if (ts.Minutes >= 1)
            {
                strCreateTime = ts.Minutes + " minutes ago";
            }
            else if (ts.Minutes >= 0)
            {
                strCreateTime = "Just Now";
            }
            return strCreateTime;
        }

        /// <summary>
        /// Method Name     : BtnAddToCalenderPressed
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : Add alert event in to calender 
        /// Revision        : 
        /// </summary>
        /// <param name="e">Event Argument</param>
        private void BtnAddToCalender_TouchUpInside(object sender, EventArgs e)
        {
            //https://developer.xamarin.com/guides/ios/platform_features/introduction_to_event_kit/
            // Perform any additional setup after loading the view, typically from a nib.
            using (var encoded = new NSString($"calshow://").CreateStringByAddingPercentEscapes(NSStringEncoding.UTF8))
            using (var url = NSUrl.FromString(encoded))
            {
                UIApplication.SharedApplication.OpenUrl(url);
            }

        }


    }


}
