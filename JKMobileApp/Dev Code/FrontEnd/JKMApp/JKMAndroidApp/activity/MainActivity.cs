using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using JKMAndroidApp.Common;
using JKMAndroidApp.fragment;
using JKMPCL.Model;
using JKMPCL.Services;
using System;
using System.Threading.Tasks;
using System.Linq;
using FloatingActionButton = Android.Support.Design.Widget.FloatingActionButton;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace JKMAndroidApp.activity
{
    [Activity(Label = "", Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait, WindowSoftInputMode = SoftInput.AdjustNothing)]
    public class MainActivity : AppCompatActivity
    {
        private string currentPage { get; set; }

        FrameLayout frameLayout;
        FrameLayout frmFloatingMenu;
        ImageView ImgViewAccount;
        ImageView ImgViewDocuments;
        ImageView ImgViewDashboard;
        ImageView ImgViewPayment;
        ImageView ImgViewMore;

        View viewMayAccount;
        View viewMyDocument;
        View viewDeshbord;
        View viewPayment;
        View viewMore;
        LinearLayout lnmove;
        LinearLayout lnterms;
        LinearLayout lnabout;
        LinearLayout lnaClose;
        FloatingActionButton btnMoveDetails;
        FloatingActionButton btnTerms;
        FloatingActionButton btnAbout;
        FloatingActionButton btnFabClose;
        RelativeLayout RelativeInfoClose;

        private Handler mHandler;
        private Android.Support.V4.App.Fragment mPrevoiusFragment = null;
        private Android.Support.V4.App.Fragment mCurrentFragment = null;
        private Android.Support.V4.App.FragmentManager mFragmentManager = null;
        private Android.Support.V4.App.FragmentTransaction mFragmentTransaction = null;
        private Class mCurrentFragmentClass = null;
        private TextView tvCenterClose;
        private TextView tvTitle;
        private Toolbar toolbar;
        private ISharedPreferences sharedPreference;
        private  SwipeRefreshLayout swipeRefreshLayout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            mHandler = new Handler();
            sharedPreference = PreferenceManager.GetDefaultSharedPreferences(this);
            UIReference();
           
            CheckDisplayInfoLayout();
            UIClickEvent();
            SetInActiveImageAndColor();

            ImgViewDashboard.SetImageResource(Resource.Drawable.icon_dashboard_active);
            viewDeshbord.SetBackgroundColor(Color.ParseColor("#ce0a45"));
            replaceFragment(new FragmentDashboard(), StringResource.msgDashboard, StringResource.msgDashboard, 0);

            //Added by Vivek Bhavsar on 12 Jan 2018 for refreshing  dashboard on pull to refresh(Swipe)
            swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.dashboardRefresher);
            swipeRefreshLayout.Refresh += SwipeRefreshLayout_Refresh;
            currentPage = UIHelper.MenuPages.DashBoard.ToString();
            
        }

        // <summary>
        /// Method Name      : UIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : For find all control  
        /// Revision        : 
        /// </summary>
        private void UIReference()
        {
            // Find control
            toolbar = FindViewById<Toolbar>(Resource.Id.tool_bar);
            tvTitle = (TextView)toolbar.FindViewById(Resource.Id.toolbar_title);
            SetSupportActionBar(toolbar);
            frameLayout = FindViewById<FrameLayout>(Resource.Id.framlayInfo);
            frmFloatingMenu = FindViewById<FrameLayout>(Resource.Id.frmFloatingMenu);
            tvCenterClose = FindViewById<TextView>(Resource.Id.btnCenterClose);
            FindImageView();
            FindLinerLayout();
            FindFloatingActionButton();
            FindViews();
        }

        // <summary>
        /// Method Name     : FindImageView
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : For find imageview all control  
        /// Revision        : 
        /// </summary>
        private void FindImageView()
        {
            ImgViewAccount = FindViewById<ImageView>(Resource.Id.ImgViewAccount);
            ImgViewDocuments = FindViewById<ImageView>(Resource.Id.ImgViewDocuments);
            ImgViewDashboard = FindViewById<ImageView>(Resource.Id.ImgViewDashboard);
            ImgViewPayment = FindViewById<ImageView>(Resource.Id.ImgViewPayment);
            ImgViewMore = FindViewById<ImageView>(Resource.Id.ImgViewMore);
        }

        // <summary>
        /// Method Name     : FindLinerLayout
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : For find linerlayout  all control  
        /// Revision        : 
        /// </summary>
        private void FindLinerLayout()
        {
            lnterms = FindViewById<LinearLayout>(Resource.Id.lnterms);
            lnabout = FindViewById<LinearLayout>(Resource.Id.lnabout);
            lnaClose = FindViewById<LinearLayout>(Resource.Id.lnaClose);
            lnmove = FindViewById<LinearLayout>(Resource.Id.lnmove);
            RelativeInfoClose = FindViewById<RelativeLayout>(Resource.Id.relativeClose);
            RelativeInfoClose.Visibility = ViewStates.Visible;
        }

        // <summary>
        /// Method Name      : FindFloatingActionButton
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : For find floatingActionButton  all control  
        /// Revision        : 
        /// </summary>
        private void FindFloatingActionButton()
        {
            btnMoveDetails= FindViewById<FloatingActionButton>(Resource.Id.btnMoveDetails);
            btnTerms = FindViewById<FloatingActionButton>(Resource.Id.btnTerms);
            btnAbout = FindViewById<FloatingActionButton>(Resource.Id.btnAbout);
            btnFabClose = FindViewById<FloatingActionButton>(Resource.Id.btnFabClose);
        }

        // <summary>
        /// Method Name      : FindViews
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : For find view  all control  
        /// Revision        : 
        /// </summary>
        private void FindViews()
        {
            viewMayAccount = FindViewById<View>(Resource.Id.viewMayAccount);
            viewMyDocument = FindViewById<View>(Resource.Id.viewMyDocument);
            viewDeshbord = FindViewById<View>(Resource.Id.viewDeshbord);
            viewPayment = FindViewById<View>(Resource.Id.viewPayment);
            viewMore = FindViewById<View>(Resource.Id.viewMore);
        }

        //Added by Vivek Bhavsar on 12 Jan 2018 for refreshing  dashboard on pull to refresh(Swipe)
        private async void SwipeRefreshLayout_Refresh(object sender, EventArgs e)
        {
            await refreshDataForActivePage();
        }

        /// <summary>
        /// Method Name     : refreshDataForActivePage
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 15 Jan 2018
        /// Purpose         : for refreshing  data based on current active page on pull to refresh(Swipe)
        /// Revision        : 
        /// </summary>
        private async Task refreshDataForActivePage()
        {
            switch (currentPage)
            {
                case "Payment":
				case "Terms":
                    SwipeRefreshLayoutEnable();
                    break;
                case "DashBoard":
                   await SetRedirectActivityAsync();
                    break;
                case "MyDocument":
                    replaceFragment(new FragmentMyDocuments(), StringResource.msgMyDocuments, StringResource.msgMyDocuments, 0);
                    break;
                case "MyAccount":
                    replaceFragment(new FragmentMyAccount(), StringResource.msgMyAccount, StringResource.msgMyAccount, 0);
                    break;
                case "MyMoveDetails":
                    await DTOConsumer.BindMoveDataAsync();
                    replaceFragment(new FragmentMyMoveDetails(), StringResource.msgMyMoveDetails, StringResource.msgMyMoveDetails, 0);
                    break;
                default:
                    break;
            }

            swipeRefreshLayout.Refreshing = false;
        }

        public void SwipeRefreshLayoutEnable()
        {
            swipeRefreshLayout.Refreshing = false;
            swipeRefreshLayout.Enabled = false;
        }
        /// <summary>
        /// Method Name     : SetRedirectActivity
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : To Redirect Activity  
        /// Revision        : By Vivek Bhavsar on 07 Feb 2018 : Change condition check for estimate wizard
        /// </summary>
        private async Task SetRedirectActivityAsync()
        {
            await DTOConsumer.BindMoveDataAsync();
            if (DTOConsumer.dtoMoveData is null)
            {
                replaceFragment(new FragmentDashboard(), StringResource.msgDashboard, StringResource.msgDashboard, 0);
            }
            else
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
                    SetInActiveImageAndColor();
                    ImgViewDashboard.SetImageResource(Resource.Drawable.icon_dashboard_active);
                    viewDeshbord.SetBackgroundColor(Color.ParseColor("#ce0a45"));
                    replaceFragment(new FragmentDashboard(), StringResource.msgDashboard, StringResource.msgDashboard, 0);
                }
            }
        }

        /// <summary>
        /// Event Name      : BtnFabClose_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : To Close floating menu
        /// Revision        : 
        /// </summary>
        private void BtnFabClose_Click(object sender, EventArgs e)
        {
            CloseFabMenu();
            frmFloatingMenu.Visibility = ViewStates.Gone;
           
          
            currentPage = Common.UIHelper.MenuPages.DashBoard.ToString();
        }

        

        /// <summary>
        /// Event Name      : BtnAbout_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Open Aboutus fragment
        /// Revision        : 
        /// </summary>
        private void BtnAbout_Click(object sender, System.EventArgs e)
        {
            SetInActiveImageAndColor();
            frmFloatingMenu.Visibility = Android.Views.ViewStates.Invisible;
            var uri = Android.Net.Uri.Parse(StringResource.JKMAboutUsUrl);
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }


        /// <summary>
        /// Event Name      : BtnAbout_Click
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Open Aboutus fragment
        /// Revision        : 
        /// </summary>
        private void BtnTerms_Click(object sender, System.EventArgs e)
        {
            SetInActiveImageAndColor();
            frmFloatingMenu.Visibility = Android.Views.ViewStates.Invisible;
            currentPage = Common.UIHelper.MenuPages.Terms.ToString();
            replaceFragment(new FragmentTerms(), StringResource.msgTerms, StringResource.msgTerms, 0);

        }

        /// <summary>
        /// Method Name     : UIClickEvent
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Set Click delegate for all control
        /// Revision        : 
        /// </summary>
        private void UIClickEvent()
        {
            btnTerms.Click += BtnTerms_Click;
            btnAbout.Click += BtnAbout_Click;
            btnFabClose.Click += BtnFabClose_Click;
            btnMoveDetails.Click += BtnMoveDetails_Click;
            RelativeInfoClose.Click += RelativeInfoClose_Click;
            tvCenterClose.Click += RelativeInfoClose_Click;
            MyAccountDelegate();
            DashboardDelegate();
            PaymentDelegate();
            MyDocumentDelegate();
            MoreDelegate();
            FindAlertControl();
            FindContactusControl();
        }


        private void BtnMoveDetails_Click(object sender, EventArgs e)
        {
            SetInActiveImageAndColor();
            frmFloatingMenu.Visibility = Android.Views.ViewStates.Invisible;
            replaceFragment(new FragmentMyMoveDetails(), StringResource.msgMyMoveDetails, StringResource.msgMyMoveDetails, 0);
            SetInActiveImageAndColor();
            currentPage = Common.UIHelper.MenuPages.MyMoveDetails.ToString();
        }

        /// <summary>
        /// Method Name     : MyAccountDelegate
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Open myaccount fragment
        /// Revision        : 
        /// </summary>
        private void MyAccountDelegate()
        {
            FindViewById<LinearLayout>(Resource.Id.linearLayoutMyAccount).Click += delegate
            {
                StartActivity(new Intent(this, typeof(MyAccountActivity)));
                SetInActiveImageAndColor();
                ImgViewAccount.SetImageResource(Resource.Drawable.account_active);
                viewMayAccount.SetBackgroundColor(Color.ParseColor("#ce0a45"));
                currentPage = Common.UIHelper.MenuPages.MyAccount.ToString();
            };
        }

        /// <summary>
        /// Method Name     : MyAccountDelegate
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Open myaccount fragment
        /// Revision        : 
        /// </summary>
        private void MyDocumentDelegate()
        {
            FindViewById<LinearLayout>(Resource.Id.linearLayoutMyDocument).Click += delegate
            {
                replaceFragment(new FragmentMyDocuments(), StringResource.msgMyDocuments, StringResource.msgMyDocuments, 0);
                SetInActiveImageAndColor();
                ImgViewDocuments.SetImageResource(Resource.Drawable.icon_mydocuments_active);
                viewMyDocument.SetBackgroundColor(Color.ParseColor("#ce0a45"));
                currentPage = Common.UIHelper.MenuPages.MyDocument.ToString();
            };
        }

        /// <summary>
        /// Method Name     : DashboardDelegate
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Open Dashboard fragment
        /// Revision        : 
        /// </summary>
        private void DashboardDelegate()
        {
            FindViewById<LinearLayout>(Resource.Id.linearLayoutDashboard).Click += delegate
            {
                replaceFragment(new FragmentDashboard(), StringResource.msgDashboard, StringResource.msgDashboard, 0);
                SetInActiveImageAndColor();
                ImgViewDashboard.SetImageResource(Resource.Drawable.icon_dashboard_active);
                viewDeshbord.SetBackgroundColor(Color.ParseColor("#ce0a45"));
                currentPage = Common.UIHelper.MenuPages.DashBoard.ToString();
            };
        }

        /// <summary>
        /// Method Name     : PaymentDelegate
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Open payment fragment
        /// Revision        : 
        /// </summary>
        private void PaymentDelegate()
        {
            FindViewById<LinearLayout>(Resource.Id.linearLayoutPayment).Click += delegate
            {
                replaceFragment(new FragmentPayment(), StringResource.msgPayment, StringResource.msgPayment, 0);
                SetInActiveImageAndColor();
                ImgViewPayment.SetImageResource(Resource.Drawable.icon_payment_active);
                viewPayment.SetBackgroundColor(Color.ParseColor("#ce0a45"));
                currentPage = Common.UIHelper.MenuPages.Payment.ToString();
                SwipeRefreshLayoutEnable();
            };
        }

        /// <summary>
        /// Method Name     : MoreDelegate
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Open more menu fragment
        /// Revision        : 
        /// </summary>
        private void MoreDelegate()
        {
            FindViewById<LinearLayout>(Resource.Id.linearLayoutMore).Click += delegate
            {
                frmFloatingMenu.Visibility = Android.Views.ViewStates.Visible;
                frmFloatingMenu.Clickable = true;
                ShowFabMenu();
                SetInActiveImageAndColor();
                ImgViewMore.SetImageResource(Resource.Drawable.icon_more_active);
                viewMore.SetBackgroundColor(Color.ParseColor("#ce0a45"));
                currentPage = Common.UIHelper.MenuPages.More.ToString();
            };
        }

        /// <summary>
        /// Method Name     : FindAlertControl
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Set Find Alert control id
        /// Revision        : 
        /// </summary>
        private void FindAlertControl()
        {
            toolbar.FindViewById(Resource.Id.item2).Click += delegate
            {
                replaceFragment(new FragmentAlerts(), StringResource.msgAlert, StringResource.msgAlert, 0);
                SetInActiveImageAndColor();
            };
        }

        /// <summary>
        /// Method Name     : FindAlertControl
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Set Find Alert control id
        /// Revision        : 
        /// </summary>
        private void FindContactusControl()
        {
            toolbar.FindViewById(Resource.Id.item1).Click += delegate
            {
                Intent intent = new Intent(this, typeof(ContactusActivity));
                intent.PutExtra(StringResource.msgFromActvity, StringResource.msgDashBoardActivity);
                StartActivity(intent);
                SetInActiveImageAndColor();
            };
        }

        /// <summary>
        /// Method Name     : SetInActiveImageAndColor
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Set inactive icon and background color inactive menu
        /// Revision        : 
        /// </summary>
        public void SetInActiveImageAndColor()
        {
            ImgViewAccount.SetImageResource(Resource.Drawable.account_inactive);
            ImgViewDocuments.SetImageResource(Resource.Drawable.icon_mydocuments_inactive);
            ImgViewDashboard.SetImageResource(Resource.Drawable.icon_dashboard_inactive);
            ImgViewPayment.SetImageResource(Resource.Drawable.icon_payment_inactive);
            ImgViewMore.SetImageResource(Resource.Drawable.icon_more_inactive);

            viewMayAccount.SetBackgroundColor(Color.ParseColor("#ffffff"));
            viewMyDocument.SetBackgroundColor(Color.ParseColor("#ffffff"));
            viewDeshbord.SetBackgroundColor(Color.ParseColor("#ffffff"));
            viewPayment.SetBackgroundColor(Color.ParseColor("#ffffff"));
            viewMore.SetBackgroundColor(Color.ParseColor("#ffffff"));
        }

        /// <summary>
        /// Method Name     : replaceFragment
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Use for Replace Fragment
        /// Revision        : 
        /// </summary>
        private void replaceFragment(Android.Support.V4.App.Fragment mFragment, string fragmentTag, string screenTitle, int waitDuration)
        {
            mHandler.PostDelayed(() =>
            {
                mCurrentFragment = mFragment;
                mCurrentFragmentClass = mCurrentFragment.Class;
                mFragmentManager = SupportFragmentManager;
                mFragmentTransaction = mFragmentManager.BeginTransaction();
                mFragmentTransaction.Replace(Resource.Id.contents, mCurrentFragment, fragmentTag)
                .SetTransition(FragmentTransit.FragmentOpen.GetHashCode())
                .Commit();
                tvTitle.Text = screenTitle;
                mPrevoiusFragment = mCurrentFragment;
            }, waitDuration);
        }

        /// <summary>
        /// Method Name     : ShowFabMenu
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Show floating menu
        /// Revision        : 
        /// </summary>
        private void ShowFabMenu()
        {
            lnterms.Visibility = ViewStates.Visible;
            lnabout.Visibility = ViewStates.Visible;
            lnaClose.Visibility = ViewStates.Visible;
            lnmove.Visibility = ViewStates.Visible;

            lnmove.Animate().TranslationY(DpToPx(-220)).Rotation(0f);
            btnMoveDetails.Animate().Rotation(0f);
            lnterms.Animate().TranslationY(DpToPx(-160)).Rotation(0f);
            btnTerms.Animate().Rotation(0f);
            lnabout.Animate().TranslationY(DpToPx(-100)).Rotation(0f);
            btnAbout.Animate().Rotation(0f);
            lnaClose.Animate().TranslationY(DpToPx(-40)).Rotation(0f);
            btnFabClose.Animate().Rotation(0f);
        }

        /// <summary>
        /// Method Name     : CloseFabMenu
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Close floating menu
        /// Revision        : 
        /// </summary>
        private void CloseFabMenu()
        {
            lnmove.Animate().TranslationY(0f).Rotation(0f);
            btnMoveDetails.Animate().TranslationY(0f).Rotation(0f);
            lnterms.Animate().TranslationY(0f).Rotation(0f);
            btnTerms.Animate().TranslationY(0f).Rotation(0f);
            lnabout.Animate().TranslationY(0f).Rotation(0f);
            btnAbout.Animate().TranslationY(0f).Rotation(0f);
            lnaClose.Animate().TranslationY(0f).Rotation(0f);          
            btnFabClose.Animate().Rotation(0f);

            lnterms.Visibility = ViewStates.Gone;
            lnabout.Visibility = ViewStates.Gone;
            lnaClose.Visibility = ViewStates.Gone;
        }

        /// <summary>
        /// Method Name     : DpToPx
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Convert Dp to px
        /// Revision        : 
        /// </summary>
        private float DpToPx(float dp)
        {
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, Resources.DisplayMetrics);
        }

        /// <summary>
        /// Event Name     : DpToPx
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Convert Dp to px
        /// Revision        : 
        /// </summary>
        private void RelativeInfoClose_Click(object sender, System.EventArgs e)
        {
            frameLayout = FindViewById<FrameLayout>(Resource.Id.framlayInfo);
            frameLayout.Visibility = Android.Views.ViewStates.Gone;
        }


        /// <summary>
        /// Event Name      : OnBackPressed
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : backup button avoid create password & customer password screen
        /// Revision        : 
        /// </summary>
        public override void OnBackPressed()
        {
            Android.App.AlertDialog.Builder dialogue;
            Android.App.AlertDialog alert;
            dialogue = new Android.App.AlertDialog.Builder(new ContextThemeWrapper(this, Resource.Style.AlertDialogCustom));
            alert = dialogue.Create();
            alert.SetMessage(StringResource.msgApplicationExit);
            alert.SetButton(StringResource.msgYes, (c, ev) =>
            {
                alert.Dispose();
                Android.Support.V4.App.ActivityCompat.FinishAffinity(this);
            });
            alert.SetButton2(StringResource.msgNo, (c, ev) => { });
            alert.Show();
        }

        /// <summary>
        /// Method Name     : CheckDisplayInfoLayout
        /// Author          : Sanket Prajapati
        /// Creation Date   : 4 jan 2017
        /// Purpose         : Info Screen Hide Or Show 
        /// Revision        : 
        /// </summary>
        public void CheckDisplayInfoLayout()
        {
            var cusID = sharedPreference.GetString(StringResource.keyCustomerID, string.Empty);
            bool iDisplay  = sharedPreference.GetBoolean(StringResource.msgIntroScreen, true);

            if (iDisplay)
            {
                //true
                sharedPreference.Edit().PutBoolean(StringResource.msgIntroScreen, false).Apply();
                frameLayout.Visibility = Android.Views.ViewStates.Visible;
            }
            else
            {
                //false
                frameLayout.Visibility = Android.Views.ViewStates.Gone;
            }
            SetCutomerId(cusID);
        }

        /// <summary>
        /// Method Name     : SetCutomerId
        /// Author          : Sanket Prajapati
        /// Creation Date   : 4 jan 2017
        /// Purpose         : Set customerID 
        /// Revision        : 
        /// </summary>
        public void SetCutomerId(string cusID)
        {
            if (UtilityPCL.LoginCustomerData is null)
            {
                UtilityPCL.LoginCustomerData = new CustomerModel();
            }
            if (!string.IsNullOrEmpty(cusID))
            {
                UtilityPCL.LoginCustomerData.CustomerId = cusID;
            }
        }

        /// <summary>
        /// Method Name     : AlertMessage
        /// Author          : Sanket Prajapati
        /// Creation Date   : 4 jan 2017
        /// Purpose         : Show Alert msessage
        /// Revision        : 
        /// </summary>
        public void AlertMessage(string StrErrorMessage)
        {
            Android.App.AlertDialog.Builder dialogue;
            Android.App.AlertDialog alert;
            dialogue = new Android.App.AlertDialog.Builder(new ContextThemeWrapper(this, Resource.Style.AlertDialogCustom));
            alert = dialogue.Create();
            alert.SetMessage(StrErrorMessage);
            alert.SetButton(StringResource.msgOK, (c, ev) =>
            {
                alert.Dispose();
            });
            alert.Show();
        }

    }
}



