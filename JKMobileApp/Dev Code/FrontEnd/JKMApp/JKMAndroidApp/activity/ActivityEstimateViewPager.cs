using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using JKMAndroidApp.Common;
using JKMAndroidApp.fragment;
using JKMAndroidApp.Utility;
using JKMPCL.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JKMAndroidApp.activity
{
    [Activity(Label = "ActivityViewPager", Icon = "@drawable/icon", MainLauncher = false, Theme = "@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait,
        WindowSoftInputMode = SoftInput.AdjustPan)]

    public class ActivityEstimateViewPager : AppCompatActivity, Android.Support.V4.View.ViewPager.IOnPageChangeListener
    {
        //private readonly string[] titleListwithoutDeposit = new string[] { StringResource.wizEstimadeReview, StringResource.wizServices ,
        //                                             StringResource.wizServiceDates,StringResource.wizAddresses,
        //                                             StringResource.wizWhatMattersMost,StringResource.wizValuation,
        //                                             StringResource.wizVitalInformation,StringResource.wizAcknowledgement,StringResource.wizMoveConfirmed};

        //private readonly string[] titleListwithDeposit = new string[] { StringResource.wizEstimadeReview, StringResource.wizServices ,
        //                                             StringResource.wizServiceDates,StringResource.wizAddresses,
        //                                             StringResource.wizWhatMattersMost,StringResource.wizValuation,
        //                                             StringResource.wizVitalInformation,StringResource.wizAcknowledgement,
        //                                             StringResource.wizDeposit, StringResource.wizMoveConfirmed};

        private readonly string[] titleListwithoutDeposit = new string[] { StringResource.wizEstimadeReview, StringResource.wizServices ,
                                                     StringResource.wizServiceDates,StringResource.wizAddresses,
                                                     StringResource.wizWhatMattersMost,StringResource.wizValuation,
                                                     StringResource.wizVitalInformation,StringResource.wizAcknowledgement};

        private readonly string[] titleListwithDeposit = new string[] { StringResource.wizEstimadeReview, StringResource.wizServices ,
                                                     StringResource.wizServiceDates,StringResource.wizAddresses,
                                                     StringResource.wizWhatMattersMost,StringResource.wizValuation,
                                                     StringResource.wizVitalInformation,StringResource.wizAcknowledgement,
                                                     StringResource.wizDeposit};

        private int count = 1;
        private int iCount;
        private int currentPosition;
        private double progress;
        private string[] titleList;
        private ProgressBar progressBar;
        private NonSwipeableViewPager nonSwipeableViewPager;
        private FrameLayout frameTen;
        private ISharedPreferences sharedPreference;

        private TextView tvTitle, tvCount, tvBack, tvNext;
        private ImageView imageViewOne, imageViewTwo, imageViewThree, imageViewFour, imageViewFive, imageViewSix, imageViewSeven, imageViewEight, imageViewNine, imageViewTen;
        private ImageView imageViewCompletedOne, imageViewCompletedTwo, imageViewCompletedThree, imageViewCompletedFour, imageViewCompletedFive, imageViewCompletedSix,
                            imageViewCompletedSeven, imageViewCompletedEight, imageViewCompletedNine, imageViewCompletedTen;
        private View viewOne, viewTwo, viewThree, viewFour, viewFive, viewSix, viewSeven, viewEight, viewNine;
        EstimateModel estimate;
        ViewPagerAdapter viewPagerAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LayoutActivityViewPager);
            sharedPreference = PreferenceManager.GetDefaultSharedPreferences(this);
            titleList = titleListwithoutDeposit;
            nonSwipeableViewPager = FindViewById<NonSwipeableViewPager>(Resource.Id.nonSwipeableViewPager);
            nonSwipeableViewPager.SetPagingEnabled(false);
            nonSwipeableViewPager.SetOnPageChangeListener(this);
            UIReference();
            ApplyFont();
            SetUpViewPager(nonSwipeableViewPager);
            nonSwipeableViewPager.OffscreenPageLimit = 1;
            if (iCount == 8)
            {
                titleList = titleListwithoutDeposit;
            }
            else
            {
                titleList = titleListwithDeposit;
            }


            UIClickEvents();


        }

        private void UIClickEvents()
        {
            tvBack.Click += delegate
            {
                if (nonSwipeableViewPager.CurrentItem != 0)
                {
                    currentPosition = nonSwipeableViewPager.CurrentItem;
                    tvTitle.Text = titleList[currentPosition - 1];
                    count--;
                    nonSwipeableViewPager.SetCurrentItem(currentPosition - 1, false);
                    SetDisplayNumberofWizard();
                }
            };

            tvNext.Click += delegate
            {
                if (nonSwipeableViewPager.CurrentItem != iCount)
                {
                    currentPosition = nonSwipeableViewPager.CurrentItem;
                    tvTitle.Text = titleList[currentPosition + 1];
                    count++;
                    nonSwipeableViewPager.SetCurrentItem(currentPosition + 1, false);
                    nonSwipeableViewPager.Invalidate();
                    SetDisplayNumberofWizard();
                }
            };
        }

        // <summary>
        /// Method Name     : UIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         :  Use for Apply font 
        /// Revision        : 
        /// </summary>
        public void ApplyFont()
        {
            UIHelper.SetTextViewFont(tvTitle, (int)UIHelper.LinotteFont.LinotteBold, Assets);
            UIHelper.SetTextViewFont(tvCount, (int)UIHelper.LinotteFont.LinotteBold, Assets);
        }

        // <summary>
        /// Method Name     : UIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : For find all control  
        /// Revision        : 
        /// </summary>
        private void UIReference()
        {
            FindProgressbarAndTextView();
            FindImageView();
            FindImageViewCompleted();
            FindView();
            imageViewOne.Selected = true;
        }

        // <summary>
        /// Method Name     : FindImageView
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : For find all imageview Completed control  
        /// Revision        : 
        /// </summary>
        private void FindImageViewCompleted()
        {
            imageViewCompletedOne = FindViewById<ImageView>(Resource.Id.imageViewCompletedOne);
            imageViewCompletedTwo = FindViewById<ImageView>(Resource.Id.imageViewCompletedTwo);
            imageViewCompletedThree = FindViewById<ImageView>(Resource.Id.imageViewCompletedThree);
            imageViewCompletedFour = FindViewById<ImageView>(Resource.Id.imageViewCompletedFour);
            imageViewCompletedFive = FindViewById<ImageView>(Resource.Id.imageViewCompletedFive);
            imageViewCompletedSix = FindViewById<ImageView>(Resource.Id.imageViewCompletedSix);
            imageViewCompletedSeven = FindViewById<ImageView>(Resource.Id.imageViewCompletedSeven);
            imageViewCompletedEight = FindViewById<ImageView>(Resource.Id.imageViewCompletedEight);
            imageViewCompletedNine = FindViewById<ImageView>(Resource.Id.imageViewCompletedNine);
            imageViewCompletedTen = FindViewById<ImageView>(Resource.Id.imageViewCompletedTen);
        }

        // <summary>
        /// Method Name     : FindImageView
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : For find all imageview control  
        /// Revision        : 
        /// </summary>
        private void FindImageView()
        {
            imageViewOne = FindViewById<ImageView>(Resource.Id.imageViewOne);
            imageViewTwo = FindViewById<ImageView>(Resource.Id.imageViewTwo);
            imageViewThree = FindViewById<ImageView>(Resource.Id.imageViewThree);
            imageViewFour = FindViewById<ImageView>(Resource.Id.imageViewFour);
            imageViewFive = FindViewById<ImageView>(Resource.Id.imageViewFive);
            imageViewSix = FindViewById<ImageView>(Resource.Id.imageViewSix);
            imageViewSeven = FindViewById<ImageView>(Resource.Id.imageViewSeven);
            imageViewEight = FindViewById<ImageView>(Resource.Id.imageViewEight);
            imageViewNine = FindViewById<ImageView>(Resource.Id.imageViewNine);
            imageViewTen = FindViewById<ImageView>(Resource.Id.imageViewTen);
        }

        // <summary>
        /// Method Name     : FindImageView
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : For find all view control  
        /// Revision        : 
        /// </summary>
        private void FindView()
        {
            viewOne = FindViewById<View>(Resource.Id.viewOne);
            viewTwo = FindViewById<View>(Resource.Id.viewTwo);
            viewThree = FindViewById<View>(Resource.Id.viewThree);
            viewFour = FindViewById<View>(Resource.Id.viewFour);
            viewFive = FindViewById<View>(Resource.Id.viewFive);
            viewSix = FindViewById<View>(Resource.Id.viewSix);
            viewSeven = FindViewById<View>(Resource.Id.viewSeven);
            viewEight = FindViewById<View>(Resource.Id.viewEight);
            viewNine = FindViewById<View>(Resource.Id.viewNine);
            frameTen = FindViewById<FrameLayout>(Resource.Id.frameTen);
        }

        // <summary>
        /// Method Name     : FindImageView
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : For find textview and progress control  
        /// Revision        : 
        /// </summary>
        private void FindProgressbarAndTextView()
        {
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            progressBar.ProgressDrawable = Resources.GetDrawable(Resource.Drawable.progressbar);
            progress = Convert.ToDouble(100 / titleList.Length-1);
            progressBar.Progress = (int)progress;
            tvBack = FindViewById<TextView>(Resource.Id.textViewBack);
            tvNext = FindViewById<TextView>(Resource.Id.textViewNext);
            tvTitle = FindViewById<TextView>(Resource.Id.textViewTitle);
            tvCount = FindViewById<TextView>(Resource.Id.textViewCount);
            tvTitle.Text = titleList[0];
            //if (iCount <= 8)
            //{
            //    tvCount.Text = "0" + count;
            //}
            //else
            //{
            //    tvCount.Text =  count.ToString();
            //}
            tvCount.Text = count.ToString("00");
        }

        // <summary>
        /// Method Name     : SetUpViewPager
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : Set fragment for wizard
        /// Revision        : 
        /// </summary>
        private void SetUpViewPagerWithoutDeposit(NonSwipeableViewPager nonSwipeableViewPager)
        {
            viewPagerAdapter = new ViewPagerAdapter(SupportFragmentManager);

            viewPagerAdapter.addFrag(new FragmentEstimatedReview());
            viewPagerAdapter.addFrag(new FragmentServices());
            viewPagerAdapter.addFrag(new FragmentServiceDates());
            viewPagerAdapter.addFrag(new FragmentAddresses());
            viewPagerAdapter.addFrag(new FragmentWhatMattersMost());
            viewPagerAdapter.addFrag(new FragmentValuation());
            viewPagerAdapter.addFrag(new FragmentVitalInformation());
            viewPagerAdapter.addFrag(new FragmentAcknowledgement());
            //  viewPagerAdapter.addFrag(new FragmentMoveConfirmed());
            nonSwipeableViewPager.Adapter = viewPagerAdapter;
        }


        // <summary>
        /// Method Name     : SetUpViewPager
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : Set fragment for wizard
        /// Revision        : 
        /// </summary>
        private void SetUpViewPagerWithDeposit(NonSwipeableViewPager nonSwipeableViewPager)
        {
            viewPagerAdapter = new ViewPagerAdapter(SupportFragmentManager);
            viewPagerAdapter.addFrag(new FragmentEstimatedReview());
            viewPagerAdapter.addFrag(new FragmentServices());
            viewPagerAdapter.addFrag(new FragmentServiceDates());
            viewPagerAdapter.addFrag(new FragmentAddresses());
            viewPagerAdapter.addFrag(new FragmentWhatMattersMost());
            viewPagerAdapter.addFrag(new FragmentValuation());
            viewPagerAdapter.addFrag(new FragmentVitalInformation());
            viewPagerAdapter.addFrag(new FragmentAcknowledgement());
            viewPagerAdapter.addFrag(new FragmentDeposit());
            // viewPagerAdapter.addFrag(new FragmentMoveConfirmed());
            nonSwipeableViewPager.Adapter = viewPagerAdapter;
        }

        // <summary>
        /// Method Name     : SetUpViewPager
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : Set fragment for wizard
        /// Revision        : 
        /// </summary>
        private void SetUpViewPager(NonSwipeableViewPager nonSwipeableViewPager)
        {
            //estimate = DTOConsumer.dtoEstimateData.FirstOrDefault(rc => rc.MoveNumber == UIHelper.SelectedMoveNumber);
            //if (estimate != null && !estimate.IsDepositPaid)
            //{
            //    viewNine.Visibility = ViewStates.Visible;
            //    frameTen.Visibility = ViewStates.Visible;
            //    SetUpViewPagerWithDeposit(nonSwipeableViewPager);
            //    iCount = 9;
            //}
            //else
            //{
                viewNine.Visibility = ViewStates.Gone;
                frameTen.Visibility = ViewStates.Gone;
                SetUpViewPagerWithoutDeposit(nonSwipeableViewPager);
                iCount = 8;
            //}
        }

        // <summary>
        /// Method Name     : FragmentNext
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : Move next fragment for  wizard
        /// Revision        : 
        /// </summary>
        public void FragmentNext()
        {
            try
            {
                if (nonSwipeableViewPager.CurrentItem != iCount)
                {
                    currentPosition = nonSwipeableViewPager.CurrentItem;
                    tvTitle.Text = titleList[currentPosition + 1];
                    count++;
                    nonSwipeableViewPager.SetCurrentItem(currentPosition + 1, false);
                    SetDisplayNumberofWizard();
                }
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }

        }

        // <summary>
        /// Method Name     : FragmentBack
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : Move back fragment for  wizard
        /// Revision        : 
        /// </summary>
        public void FragmentBack()
        {
            if (nonSwipeableViewPager.CurrentItem != 0)
            {
                currentPosition = nonSwipeableViewPager.CurrentItem;
                tvTitle.Text = titleList[currentPosition - 1];
                count--;
                nonSwipeableViewPager.SetCurrentItem(currentPosition - 1, false);
                SetDisplayNumberofWizard();
            }
        }

        // <summary>
        /// Method Name     : SetDisplayNumberofWizard
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : Set display number of wizard
        /// Revision        : 
        /// </summary>
        private void SetDisplayNumberofWizard()
        {
            if (count <= 8)
            {
                tvCount.Text = "0" + count;
            }
            else
            {
                tvCount.Text = count.ToString();
            }
            if (count <= 8)
            {
                ImageSelector(count);
            }
            else
            {
                ImageSelectorWitDeposit(count);
            }

            progressBar.Progress = (int)(progress * (1*(count-1)));
            if (nonSwipeableViewPager.CurrentItem == iCount)
            {
                tvBack.Visibility = ViewStates.Gone;
            }
            else
            {
                tvBack.Visibility = ViewStates.Visible;
            }
        }

        // <summary>
        /// Method Name     : ImageSelector
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : Set case in Active or inactive image for wizard
        /// Revision        : 
        /// </summary>
        private void ImageSelector(int count)
        {
            switch (count)
            {
                case 1:
                    wizEstimadeReview();
                    break;
                case 2:
                    wizServices();
                    break;
                case 3:
                    wizServiceDates();
                    break;
                case 4:
                    wizAddresses();
                    break;
                case 5:
                    wizWhatMattersMost();
                    break;
                case 6:
                    wizValuation();
                    break;
                case 7:
                    wizVitalInformation();
                    break;
                case 8:
                    wizAcknowledgement();
                    break;
                //case 9:
                //    wizMoveConfirmed();
                //  break;
                default:
                    break;
            }
        }

        // <summary>
        /// Method Name     : ImageSelector
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : Set case in Active or inactive image for wizard
        /// Revision        : 
        /// </summary>
        private void ImageSelectorWitDeposit(int count)
        {
            switch (count)
            {
                case 1:
                    wizEstimadeReview();
                    break;
                case 2:
                    wizServices();
                    break;
                case 3:
                    wizServiceDates();
                    break;
                case 4:
                    wizAddresses();
                    break;
                case 5:
                    wizWhatMattersMost();
                    break;
                case 6:
                    wizValuation();
                    break;
                case 7:
                    wizVitalInformation();
                    break;
                case 8:
                    wizAcknowledgement();
                    break;
                case 9:
                    wizDeposit();
                    break;
                //case 10:
                //    wizMoveConfirmedWitDeposit();
                //  break;

                default:
                    break;
            }
        }
        // <summary>
        /// Method Name     : ResetImageSelector
        /// Author          : Sanket Prajapati
        /// Creation Date   : 15 Dec 2017
        /// Purpose         : Reset image Selector
        /// Revision        : 
        /// </summary>
        private void ResetImageSelector()
        {
            imageViewOne.Selected = false;
            imageViewTwo.Selected = false;
            imageViewThree.Selected = false;
            imageViewFour.Selected = false;
            imageViewFive.Selected = false;
            imageViewSix.Selected = false;
            imageViewSeven.Selected = false;
            imageViewEight.Selected = false;
            imageViewNine.Selected = false;

            imageViewCompletedOne.Visibility = ViewStates.Gone;
            imageViewCompletedTwo.Visibility = ViewStates.Gone;
            imageViewCompletedThree.Visibility = ViewStates.Gone;
            imageViewCompletedFour.Visibility = ViewStates.Gone;
            imageViewCompletedFive.Visibility = ViewStates.Gone;
            imageViewCompletedSix.Visibility = ViewStates.Gone;
            imageViewCompletedSeven.Visibility = ViewStates.Gone;
            imageViewCompletedEight.Visibility = ViewStates.Gone;
            imageViewCompletedNine.Visibility = ViewStates.Gone;
        }

        // <summary>
        /// Method Name     : wizEstimadeReview
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Set image Selector for EstimadeReview wizard
        /// Revision        : 
        /// </summary>
        private void wizEstimadeReview()
        {
            ResetImageSelector();
            imageViewOne.Selected = true;
            viewOne.Selected = false;
        }

        // <summary>
        /// Method Name     : wizEstimadeReview
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Set image Selector for services wizard
        /// Revision        : 
        /// </summary>
        private void wizServices()
        {
            ResetImageSelector();
            imageViewTwo.Selected = true;
            imageViewCompletedOne.Visibility = ViewStates.Visible;
            viewOne.Selected = true;
            viewTwo.Selected = false;
            viewThree.Selected = false;
            viewFour.Selected = false;
            viewFive.Selected = false;
            viewSix.Selected = false;
            viewSeven.Selected = false;
            viewEight.Selected = false;
            viewNine.Selected = false;
        }

        // <summary>
        /// Method Name     : wizServiceDates
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Set image Selector for service dates wizard
        /// Revision        : 
        /// </summary>
        private void wizServiceDates()
        {
            ResetImageSelector();
            imageViewThree.Selected = true;
            imageViewCompletedOne.Visibility = ViewStates.Visible;
            imageViewCompletedTwo.Visibility = ViewStates.Visible;
            viewOne.Selected = true;
            viewTwo.Selected = true;
            viewThree.Selected = false;
            viewFour.Selected = false;
            viewFive.Selected = false;
            viewSix.Selected = false;
            viewSeven.Selected = false;

        }

        // <summary>
        /// Method Name     : wizAddresses
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Set image Selector for service dates wizard
        /// Revision        : 
        /// </summary>
        private void wizAddresses()
        {
            ResetImageSelector();
            imageViewFour.Selected = true;
            imageViewCompletedOne.Visibility = ViewStates.Visible;
            imageViewCompletedTwo.Visibility = ViewStates.Visible;
            imageViewCompletedThree.Visibility = ViewStates.Visible;
            viewOne.Selected = true;
            viewTwo.Selected = true;
            viewThree.Selected = true;
            viewFour.Selected = false;
            viewFive.Selected = false;
            viewSix.Selected = false;
            viewSeven.Selected = false;
        }

        // <summary>
        /// Method Name     : wizWhatMattersMost
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Set image Selector for What Matters Most wizard
        /// Revision        : 
        /// </summary>
        private void wizWhatMattersMost()
        {
            ResetImageSelector();
            imageViewFive.Selected = true;
            imageViewCompletedOne.Visibility = ViewStates.Visible;
            imageViewCompletedTwo.Visibility = ViewStates.Visible;
            imageViewCompletedThree.Visibility = ViewStates.Visible;
            imageViewCompletedFour.Visibility = ViewStates.Visible;
            viewOne.Selected = true;
            viewTwo.Selected = true;
            viewThree.Selected = true;
            viewFour.Selected = true;
            viewFive.Selected = false;
            viewSix.Selected = false;
            viewSeven.Selected = false;
        }

        // <summary>
        /// Method Name     : wizValuation
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Set image Selector for Valuation wizard
        /// Revision        : 
        /// </summary>
        private void wizValuation()
        {
            ResetImageSelector();
            imageViewSix.Selected = true;
            imageViewCompletedOne.Visibility = ViewStates.Visible;
            imageViewCompletedTwo.Visibility = ViewStates.Visible;
            imageViewCompletedThree.Visibility = ViewStates.Visible;
            imageViewCompletedFour.Visibility = ViewStates.Visible;
            imageViewCompletedFive.Visibility = ViewStates.Visible;
            viewOne.Selected = true;
            viewTwo.Selected = true;
            viewThree.Selected = true;
            viewFour.Selected = true;
            viewFive.Selected = true;
            viewSix.Selected = false;
            viewSeven.Selected = false;
        }

        // <summary>
        /// Method Name     : wizVitalInformation
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Set image Selector for VitalInformation wizard
        /// Revision        : 
        /// </summary>
        private void wizVitalInformation()
        {
            ResetImageSelector();
            imageViewSeven.Selected = true;
            imageViewCompletedOne.Visibility = ViewStates.Visible;
            imageViewCompletedTwo.Visibility = ViewStates.Visible;
            imageViewCompletedThree.Visibility = ViewStates.Visible;
            imageViewCompletedFour.Visibility = ViewStates.Visible;
            imageViewCompletedFive.Visibility = ViewStates.Visible;
            imageViewCompletedSix.Visibility = ViewStates.Visible;
            viewOne.Selected = true;
            viewTwo.Selected = true;
            viewThree.Selected = true;
            viewFour.Selected = true;
            viewFive.Selected = true;
            viewSix.Selected = true;
            viewSeven.Selected = false;
            viewEight.Selected = false;
        }

        // <summary>
        /// Method Name     : wizAcknowledgement
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Set image Selector for Acknowledgement wizard
        /// Revision        : 
        /// </summary>
        private void wizAcknowledgement()
        {
            ResetImageSelector();
            imageViewEight.Selected = true;
            imageViewCompletedOne.Visibility = ViewStates.Visible;
            imageViewCompletedTwo.Visibility = ViewStates.Visible;
            imageViewCompletedThree.Visibility = ViewStates.Visible;
            imageViewCompletedFour.Visibility = ViewStates.Visible;
            imageViewCompletedFive.Visibility = ViewStates.Visible;
            imageViewCompletedSix.Visibility = ViewStates.Visible;
            imageViewCompletedSeven.Visibility = ViewStates.Visible;
            viewOne.Selected = true;
            viewTwo.Selected = true;
            viewThree.Selected = true;
            viewFour.Selected = true;
            viewFive.Selected = true;
            viewSix.Selected = true;
            viewSeven.Selected = true;
            viewEight.Selected = false;
        }

        // <summary>
        /// Method Name     : wizMoveConfirmed
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Set image Selector for MoveConfirmed wizard
        /// Revision        : 
        /// </summary>
        private void wizMoveConfirmed()
        {
            ResetImageSelector();
            imageViewNine.Selected = true;
            imageViewCompletedOne.Visibility = ViewStates.Visible;
            imageViewCompletedTwo.Visibility = ViewStates.Visible;
            imageViewCompletedThree.Visibility = ViewStates.Visible;
            imageViewCompletedFour.Visibility = ViewStates.Visible;
            imageViewCompletedFive.Visibility = ViewStates.Visible;
            imageViewCompletedSix.Visibility = ViewStates.Visible;
            imageViewCompletedSeven.Visibility = ViewStates.Visible;
            imageViewCompletedEight.Visibility = ViewStates.Visible;
            viewOne.Selected = true;
            viewTwo.Selected = true;
            viewThree.Selected = true;
            viewFour.Selected = true;
            viewFive.Selected = true;
            viewSix.Selected = true;
            viewSeven.Selected = true;
            viewEight.Selected = true;
        }

        // <summary>
        /// Method Name     : wizMoveConfirmed
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Set image Selector for MoveConfirmed wizard
        /// Revision        : 
        /// </summary>
        private void wizMoveConfirmedWitDeposit()
        {
            ResetImageSelector();
            imageViewTen.Selected = true;
            imageViewCompletedOne.Visibility = ViewStates.Visible;
            imageViewCompletedTwo.Visibility = ViewStates.Visible;
            imageViewCompletedThree.Visibility = ViewStates.Visible;
            imageViewCompletedFour.Visibility = ViewStates.Visible;
            imageViewCompletedFive.Visibility = ViewStates.Visible;
            imageViewCompletedSix.Visibility = ViewStates.Visible;
            imageViewCompletedSeven.Visibility = ViewStates.Visible;
            imageViewCompletedEight.Visibility = ViewStates.Visible;
            imageViewCompletedNine.Visibility = ViewStates.Visible;

            viewOne.Selected = true;
            viewTwo.Selected = true;
            viewThree.Selected = true;
            viewFour.Selected = true;
            viewFive.Selected = true;
            viewSix.Selected = true;
            viewSeven.Selected = true;
            viewEight.Selected = true;
            viewNine.Selected = true;

        }

        private void wizDeposit()
        {
            ResetImageSelector();
            imageViewNine.Selected = true;
            imageViewCompletedOne.Visibility = ViewStates.Visible;
            imageViewCompletedTwo.Visibility = ViewStates.Visible;
            imageViewCompletedThree.Visibility = ViewStates.Visible;
            imageViewCompletedFour.Visibility = ViewStates.Visible;
            imageViewCompletedFive.Visibility = ViewStates.Visible;
            imageViewCompletedSix.Visibility = ViewStates.Visible;
            imageViewCompletedSeven.Visibility = ViewStates.Visible;
            imageViewCompletedEight.Visibility = ViewStates.Visible;
            imageViewCompletedNine.Visibility = ViewStates.Gone;
            viewOne.Selected = true;
            viewTwo.Selected = true;
            viewThree.Selected = true;
            viewFour.Selected = true;
            viewFive.Selected = true;
            viewSix.Selected = true;
            viewSeven.Selected = true;
            viewEight.Selected = true;
            viewNine.Selected = false;
        }

        // <summary>
        /// class Name      : ViewPagerAdapter
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : Set ViewPagerAdapter for view pager
        /// Revision        : 
        /// </summary>
        public class ViewPagerAdapter : FragmentPagerAdapter
        {
            private readonly List<Android.Support.V4.App.Fragment> fragmentList = new List<Android.Support.V4.App.Fragment>();

            public ViewPagerAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
            {
            }

            protected ViewPagerAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
            {
            }

            public override int Count => fragmentList.Count;

            public override Android.Support.V4.App.Fragment GetItem(int position)
            {

                return fragmentList[position];

            }

            public void addFrag(Android.Support.V4.App.Fragment fragment)
            {
                fragmentList.Add(fragment);
            }
        }

        /// <summary>
        /// Event Name      : OnBackPressed
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : backup button avoid privacy police and customer password screen
        /// Revision        : 
        /// </summary>
        public override void OnBackPressed()
        {
            if (nonSwipeableViewPager.CurrentItem == 8)
            {
                AlertMessage(StringResource.msgBackButtonDisable);
            }
            else if (nonSwipeableViewPager.CurrentItem > 0 && nonSwipeableViewPager.CurrentItem < titleList.Length - 1)
            {
                FragmentBack();
            }
            else
            {
                SetSharedPreference();
            }
        }

        /// <summary>
        /// Method Name      : SetSharedPreference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 jan 2018
        /// Purpose         : set Customer Id in Shared perfence
        /// Revision        : 
        /// </summary>
        private void SetSharedPreference()
        {

            string customerId;
            customerId = sharedPreference.GetString(StringResource.keyCustomerID, string.Empty);
            if (!string.IsNullOrEmpty(customerId))
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
            else
            {
                StartActivity(new Intent(this, typeof(LoginActivity)));
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

        public void OnPageScrollStateChanged(int state)
        {
            //
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
            //
        }

        public void OnPageSelected(int position)
        {
            //
        }
    }
}