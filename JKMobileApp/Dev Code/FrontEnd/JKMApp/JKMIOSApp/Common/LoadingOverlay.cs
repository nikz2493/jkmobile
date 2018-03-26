using System;
using UIKit;
using CoreGraphics;
namespace JKMIOSApp
{
    /// <summary>
    /// Class Name      : LoadingOverlay.
    /// Author          : Hiren Patel
    /// Creation Date   : 22 JAN 2018
    /// Purpose         : To display loading screen before service call and hide after service response came
    /// Revision        : 
    /// </summary>
    public class LoadingOverlay : UIView
    {
        // control declarations
        readonly private UIActivityIndicatorView activitySpinner;
        readonly private UILabel loadingLabel;

        /// <summary>
        /// Constructor Name        : LoadingOverlay.
        /// Author                  : Hiren Patel
        /// Creation Date           : 22 JAN 2018
        /// Purpose                 : Initializes a new instance of the LoadingOverlay
        /// Revision                : 
        /// </summary>
        /// <param name="frame">Frame.</param>
        public LoadingOverlay(CGRect frame) : base(frame)
        {
            // configurable bits
            BackgroundColor = UIColor.Black;
            Alpha = 0.75f;
            AutoresizingMask = UIViewAutoresizing.All;

            nfloat labelHeight = 22;
            nfloat labelWidth = Frame.Width - 20;

            // derive the center x and y
            nfloat centerX = Frame.Width / 2;
            nfloat centerY = Frame.Height / 2;

            // create the activity spinner, center it horizontall and put it 5 points above center x
            activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
            activitySpinner.Frame = new CGRect(
                centerX - (activitySpinner.Frame.Width / 2),
                centerY - activitySpinner.Frame.Height - 20,
                activitySpinner.Frame.Width,
                activitySpinner.Frame.Height);
            activitySpinner.AutoresizingMask = UIViewAutoresizing.All;
            AddSubview(activitySpinner);
            activitySpinner.StartAnimating();

            // create and configure the "Loading Data" label
            loadingLabel = new UILabel(new CGRect(
                centerX - (labelWidth / 2),
                centerY + 20,
                labelWidth,
                labelHeight
                ));
            loadingLabel.BackgroundColor = UIColor.Clear;
            loadingLabel.TextColor = UIColor.White;
            loadingLabel.Text = "Loading";
            loadingLabel.TextAlignment = UITextAlignment.Center;
            loadingLabel.AutoresizingMask = UIViewAutoresizing.All;
            AddSubview(loadingLabel);
        }

        /// <summary>
        /// Methode Name            : Hide.
        /// Author                  : Hiren Patel
        /// Creation Date           : 22 JAN 2018
        /// Purpose                 : Fades out the control and then removes it from the super view
        /// Revision                : 
        /// </summary>
        public void Hide()
        {
            UIView.Animate(
                0.5, // duration
                () => { Alpha = 0; },
                () => { RemoveFromSuperview(); }
            );
        }
    }
}
