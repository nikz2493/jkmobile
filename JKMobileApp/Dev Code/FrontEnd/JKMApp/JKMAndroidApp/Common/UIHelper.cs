using Android.Widget;
using Android.Graphics;
using Android.Content.Res;
using Android.App;
using Android.Content;
using JKMAndroidApp.activity;


namespace JKMAndroidApp.Common
{
    public static class UIHelper
    {
        public enum LinotteFont
        {
            // The flag for SunRoof is 0001.
            LinotteBold,
            LinotteHeavy,
            LinotteLight,
            LinotteRegular,
            LinotteSemiBold
        }

        /// <summary>
        /// Enum Name       : MenuPages
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 15 Jan 2018
        /// Purpose         : Enum to check current active page selection ,used while pull to refresh on main activity
        /// Revision        : 
        /// </summary>
        public enum MenuPages
        {
            DashBoard,
            MyDocument,
            MyAccount,
            Payment,
            More,
            MyMoveDetails,
            Terms,
            MyFinalPayment
        }
        /// <summary>
        /// Method Name     : SetTextViewFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Set font of TextView control
        /// Revision        : 
        /// </summary>
        public static void SetTextViewFont(TextView uiTextview, int fontValue, AssetManager Assets)
        {
            if (uiTextview != null)
            {
                uiTextview.SetTypeface(Typeface.CreateFromAsset(mgr: Assets, path: GetFontPathFromType(fontValue)), TypefaceStyle.Normal);
            }
        }

        /// <summary>
        /// Method Name     : SetButtonFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 21 Jan 2018
        /// Purpose         : Get font path based on font type requested.
        /// Revision        : 
        /// </summary>
        private static string GetFontPathFromType(int fontValue)
        {
            string fontPath=string.Empty;

            switch (fontValue)
            {
                case (int)LinotteFont.LinotteSemiBold:
                    fontPath = "fonts/LinotteSB.otf";
                    break;
                case (int)LinotteFont.LinotteHeavy:
                    fontPath = "fonts/LinotteHeavy.otf";
                    break;
                case (int)LinotteFont.LinotteRegular:
                    fontPath = "fonts/LinotteRegular.otf";
                    break;
                case (int)LinotteFont.LinotteBold:
                    fontPath = "fonts/LinotteBold.otf";
                    break;
            }
            return fontPath;
        }

        /// <summary>
        /// Method Name     : SetButtonFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Set font of Button control
        /// Revision        : 
        /// </summary>
        public static void SetButtonFont(Button uiButton, int fontValue, AssetManager Assets)
        {
            if (uiButton != null)
            {
                uiButton.SetTypeface(Typeface.CreateFromAsset(mgr: Assets, path: GetFontPathFromType(fontValue)), TypefaceStyle.Normal);
            }
        }

        /// <summary>
        /// Method Name     : SetEditTextFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Set font of EditText control
        /// Revision        : 
        /// </summary>
        public static void SetEditTextFont(EditText uiEditText, int fontValue, AssetManager Assets)
        {
            if (uiEditText != null)
            {
                uiEditText.SetTypeface(Typeface.CreateFromAsset(mgr: Assets, path: GetFontPathFromType(fontValue)), TypefaceStyle.Normal);
            }
        }


        /// <summary>
        /// Method Name     : SetProgressDailoge
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : set progress dailoge
        /// Revision        : 
        /// </summary>
        public static ProgressDialog SetProgressDailoge(Activity activity)
        {
            ProgressDialog progressDialog;
            progressDialog = new ProgressDialog(activity);
            progressDialog.SetMessage("loading");
            progressDialog.SetCanceledOnTouchOutside(false);
            progressDialog.Show();

            return progressDialog;
        }

        // <summary>
        /// Method Name     : SelectedMoveNumber
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Jan 2018
        /// Purpose         : To redirect calling screen from contact us page
        /// Revision        : 
        /// </summary>
        /// <returns>The size.</returns>
        public static string SelectedMoveNumber
        { get; set; }

        // <summary>
        /// Method Name     : SelectedMoveNumber
        /// Author          : Hiren Patel
        /// Creation Date   : 20 Jan 2018
        /// Purpose         : To redirect calling screen from contact us page
        /// Revision        : 
        /// </summary>
        /// <returns>The size.</returns>
        public static bool Introscreen
        { get; set; }

    }
}