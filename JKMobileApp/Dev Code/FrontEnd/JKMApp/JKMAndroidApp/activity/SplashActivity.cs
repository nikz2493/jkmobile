using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Support.V7.App;
using JKMAndroidApp.Common;
using JKMAndroidApp.fragment;
using JKMPCL.Model;
using JKMPCL.Services;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JKMAndroidApp.activity
{
    /// <summary>
    /// Method Name     : SplashActivity
    /// Author          : Sanket Prajapati
    /// Creation Date   : 23 Jan 2018
    /// Purpose         : Activity for Splash page
    /// Revision        : 
    /// </summary>
    [Activity(Icon = "@drawable/icon", MainLauncher = true , Theme = "@style/MyTheme"  , ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class JKMovingApp : AppCompatActivity
    {
        private ISharedPreferences sharedPreference;
        string customerID;
        bool iIntroscreen;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);           
            SetContentView(Resource.Layout.Splashscreen);
            sharedPreference = PreferenceManager.GetDefaultSharedPreferences(this);

            CultureInfo cultureinfo = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = cultureinfo;

            if (UtilityPCL.LoginCustomerData is null)
            {
                UtilityPCL.LoginCustomerData = new CustomerModel();
            }

            //Timer  use for splash screen display time interval
            System.Timers.Timer intervaltimer = new System.Timers.Timer
            {
                Interval = 2000,
                AutoReset = false
            };
            intervaltimer.Elapsed += async delegate
            {
                await BindingDataAsync();
            };
            intervaltimer.Start();
            customerID = sharedPreference.GetString(StringResource.keyCustomerID, null);
            iIntroscreen= sharedPreference.GetBoolean(StringResource.msgIntroScreen, true);
        }
        
        /// <summary>
        /// Event Name      : OnBackPressed
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : backup button avoid splash screen
        /// Revision        : 
        /// </summary>
        public override void OnBackPressed()
        {
            //Not Required
        }

        ///// <summary>
        ///// Event Name      : BindingDataAsync
        ///// Author          : Sanket Prajapati
        ///// Creation Date   : 23 jan 2018
        ///// Purpose         : Open next activity after splash screen
        ///// Revision        : 
        ///// </summary>
        private async Task BindingDataAsync()
        {
            if (customerID == null)
            {
                StartActivity(new Intent(this, typeof(LoginActivity)));
            }
            else
            {
                UtilityPCL.LoginCustomerData.CustomerId = customerID;
                UtilityPCL.LoginCustomerData.LastLoginDate = DateTime.Now;
                await DTOConsumer.GetCustomerProfileData();
                await DTOConsumer.BindMoveDataAsync();
                SetMoveActivity();
            }
           // StartActivity(new Intent(this, typeof(ActivityMoveConfirmed)));
        }

        ///// <summary>
        ///// Event Name      : SetMoveActivity
        ///// Author          : Sanket Prajapati
        ///// Creation Date   : 23 jan 2018
        ///// Purpose         : Use for set condition ways flow  
        ///// Revision        : 
        ///// </summary>
        private void SetMoveActivity()
        {
            if (DTOConsumer.dtoEstimateData != null && DTOConsumer.dtoEstimateData.Count != 0)
            {
                if (DTOConsumer.dtoEstimateData.Count == 1)
                {
                    UIHelper.SelectedMoveNumber = DTOConsumer.dtoEstimateData.FirstOrDefault().MoveNumber;
                    Intent intent = new Intent(this, typeof(ActivityEstimateViewPager));
                    StartActivity(intent);
                }
                else
                {
                    UIHelper.SelectedMoveNumber = string.Empty;
                    Intent intent = new Intent(this, typeof(MultipleEstimatedActivity));
                    StartActivity(intent);
                }
            }
            else
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            }
        }
    }
}