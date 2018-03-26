using System;
using CoreGraphics;
using Foundation;
using JKMPCL.Model;
using UIKit;
using JKMPCL.Services;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Xamarin.iOS;

namespace JKMIOSApp
{
    /// <summary>
    /// Controller Name : MyDashboardViewController
    /// Author          : Hiren Patel
    /// Creation Date   : 16 JAN 2018
    /// Purpose         : To display  customer dashboard screen 
    /// Revision        : 
    /// </summary>
    public partial class MyDashboardViewController : UIViewController
    {
        readonly Move moveService;
        JKTabController parent;
        LoadingOverlay loadingOverlay;
        public MyDashboardViewController(IntPtr handle) : base(handle)
        {
            moveService = new Move();
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            UIHelper.CallingScreenContactUs = JKMEnum.UIViewControllerID.UINavigationDashboard;
            if(!string.IsNullOrEmpty(UtilityPCL.LoginCustomerData.CustomerId) && (!string.IsNullOrWhiteSpace(UtilityPCL.LoginCustomerData.CustomerId)))
            {
                UIHelper.SaveCustomerIDInCache(UtilityPCL.LoginCustomerData.CustomerId);
            }
           
            InitilizeIntarface();
            ResetDashBoardData();

            if (this.ParentViewController != null)
            {
                parent = this.ParentViewController as JKTabController;
                scrollViewIntro.Frame = parent.View.Frame;
                parent.View.Add(scrollViewIntro);
            }
            scrollViewIntro.Hidden = !HasIntroPagePermission();
            await BindMoveData();
        }


        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.NavigationBarHidden = true;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        /// <summary>
        /// Method Name     : InitilizeIntarface
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To Initilizes the intarface.
        /// Revision        : 
        /// </summary>
        public void InitilizeIntarface()
        {
            SetScrollViewHeight();
            scrollviewStartEndDate.Layer.CornerRadius = scrollviewStartEndDate.Frame.Size.Height / 2;

            viewDaysLeft.Layer.CornerRadius = 10;
            viewDaysLeft.ClipsToBounds = true;
            viewDaysLeft.Layer.BorderColor = UIColor.DarkGray.CGColor;
            viewDaysLeft.Layer.BorderWidth = 1;

            scrollviewMyServices.Layer.CornerRadius = 10;
            scrollviewWhatMattersMost.Layer.CornerRadius = 10;
            scrollviewValuation.Layer.CornerRadius = 10;
            txtViewWhatMattersMost.Editable = false;

            // Intro scroll view property
            SetIntroScrollViewProperty();

            btnAlert.TouchUpInside += BtnAlert_TouchUpInside;
            btnContactUs.TouchUpInside += BtnContactUs_TouchUpInside;
        }

        /// <summary>
        /// Method Name     : SetScrollViewHeigh
        /// Author          : Hiren Patel
        /// Creation Date   : 18 Jan 2018
        /// Purpose         : To set scroll view height 
        /// Revision        : 
        /// </summary>
        public void SetIntroScrollViewProperty()
        {
            UIHelper.DismissKeyboardOnBackgroundTap(this);
            UIHelper.SetLabelFont(lblClose);
            var lblCloseUnderLineText = new NSAttributedString(lblClose.Text,
            new UIStringAttributes { UnderlineStyle = NSUnderlineStyle.Single });
            lblClose.AttributedText = lblCloseUnderLineText;

            scrollViewIntro.Layer.Opacity = AppConstant.INTROPAGE_SCREEN_OPACITY;

            viewNeedHelp.UserInteractionEnabled = true;
            viewMyNeedHelpBottom.UserInteractionEnabled = true;
            viewCloseButton.UserInteractionEnabled = true;
            scrollViewIntro.UserInteractionEnabled = true;

            UITapGestureRecognizer screenForTap = new UITapGestureRecognizer(() =>
            {
                scrollViewIntro.Hidden = true;
                SetIntroPagePermission();
            });
            viewNeedHelp.AddGestureRecognizer(screenForTap);
            viewMyNeedHelpBottom.AddGestureRecognizer(screenForTap);
            viewCloseButton.AddGestureRecognizer(screenForTap);
            scrollViewIntro.AddGestureRecognizer(screenForTap);
            scrollViewIntro.Hidden = false;
        }

        /// <summary>
        /// Method Name     : SetScrollViewHeigh
        /// Author          : Hiren Patel
        /// Creation Date   : 18 Jan 2018
        /// Purpose         : To set scroll view height 
        /// Revision        : 
        /// </summary>
        public void SetScrollViewHeight()
        {
            int scrollviewIncremental = 570;
            if (UIHelper.IsIPAD(View))
            {
                scrollviewIncremental = 150;
            }
            else if (UIHelper.ScreenSize(View) == AppConstant.IPHONE_5_WIDTH_AND_HEIGHT)
            {
                scrollviewIncremental = 570;
            }
            else if (UIHelper.ScreenSize(View) == AppConstant.IPHONE_6_WIDTH_AND_HEIGHT)
            {
                scrollviewIncremental = 550;
            }
            else if (UIHelper.ScreenSize(View) == AppConstant.IPHONE_6_PLUS_WIDTH_AND_HEIGHT)
            {
                scrollviewIncremental = 420;
            }

            scrollview.ContentSize = new CGSize(scrollview.Frame.Size.Width, scrollview.Frame.Size.Height + scrollviewIncremental);
        }

        /// <summary>
        /// Event Name      : BtnAlert_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : To redirect notification
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnAlert_TouchUpInside(object sender, EventArgs e)
        {
            PerformSegue("dashboardToItem",this);
        }

        /// <summary>
        /// Event Name      : BtnContactUs_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To redirect contactus page
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnContactUs_TouchUpInside(object sender, EventArgs e)
        {
            PerformSegue("dashboardToContactUs", this);
        }

        /// <summary>
        /// Method Name     : DrawChart
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : Draw Move chart
        /// Revision        : 
        /// </summary>
        /// <param name="statusCode">statusCode.</param>
        public void DrawChart(string statusCode)
        {
            var plotView = new PlotView
            {
                Model = PopulateChart(statusCode),
                Frame = new CoreGraphics.CGRect(new CoreGraphics.CGPoint(0, 0), new CoreGraphics.CGSize(viewChart.Frame.Size)),
                BackgroundColor = UIColorExtensions.FromHexString(null,"#efeff5")
            };

            viewChart.AddSubview(plotView);
            viewChart.AddSubview(imgMoveStatus);
            plotView.AddSubview(lblMoveStatus);
            plotView.AddSubview(imgChartTruck);

        }

        /// <summary>
        /// Method Name     : SetCenterFirstAndLastDate
        /// Author          : Hiren Patel
        /// Creation Date   : 18 Jan 2018
        /// Purpose         : Buttons the alert pressed.
        /// Revision        : 
        /// </summary>
        public void SetCenterFirstAndLastDate()
        {
            viewFirstDate.Layer.CornerRadius = viewFirstDate.Frame.Size.Height / 2;
            viewLastDate.Layer.CornerRadius = viewLastDate.Frame.Size.Height / 2;
            nfloat centerX = scrollviewStartEndDate.Frame.Width / 2;
            viewFirstDate.Frame = new CGRect(
                (centerX - viewFirstDate.Frame.Width),
                viewFirstDate.Frame.Y,
                viewFirstDate.Frame.Width,
                viewFirstDate.Frame.Height);

            viewLastDate.Frame = new CGRect(
               (centerX),
                viewLastDate.Frame.Y,
                viewLastDate.Frame.Width,
                viewLastDate.Frame.Height);
        }

        /// <summary>
        /// Method Name     : BindMoveData
        /// Author          : Hiren Patel
        /// Creation Date   : 30 Dec 2017
        /// Purpose         : To bind Move data with UI element
        /// Revision        : 
        /// </summary>
        private async Task BindMoveData()
        {
            string errorMessage = string.Empty;
            try
            {
                if (this.ParentViewController != null)
                {
                    parent = this.ParentViewController as JKTabController;
                    loadingOverlay = UIHelper.ShowLoadingScreen(parent.View);
                }
                APIResponse<MoveDataModel> response = await moveService.GetMoveData(UtilityPCL.LoginCustomerData.CustomerId);
                loadingOverlay.Hide();
                if (response.STATUS)
                {
                    if (UtilityPCL.IsMoveActive(response.DATA.IsActive))
                    {
                        MoveDataModel moveDataModel = response.DATA;
                        SetMoveAddressAndValueDataToUILabel(moveDataModel);
                        if (moveDataModel.MyServices != null)
                        {
                            UIHelper.BindMyServiceData(moveDataModel.MyServices, scrollviewMyServices, View);
                        }

                        CheckStatusReason(moveDataModel);
                        scrollview.Hidden = false;
                    }
                    else
                    {
                        scrollview.Hidden = true;
                        UIHelper.ShowMessage(response.Message); 
                    }
                }
                else
                {
                    scrollview.Hidden = true;
                    errorMessage = response.Message;
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
                    ResetDashBoardData();
                    UIHelper.ShowAlertMessage(this, errorMessage);
                }
                loadingOverlay.Hide();
            }
        }

        /// <summary>
        /// Method Name     : CheckStatusReason
        /// Author          : Hiren Patel
        /// Creation Date   : 3 Jan 2018
        /// Purpose         : To set scrollview property base on move statusReason  data
        /// Revision        : 
        /// </summary>
        /// <param name="moveDataModel">Object move data.</param>
        private void CheckStatusReason(MoveDataModel moveDataModel)
        {
            
                if (string.IsNullOrEmpty(moveDataModel.StatusReason))
                {
                    scrollviewChart.Hidden = true;
                    imgChartTruck.Hidden = true;
                    lblMoveStatus.Hidden = true;
                }
                else
                {
                
                    scrollviewChart.Hidden = false;
                    imgChartTruck.Hidden = false;
                    lblMoveStatus.Hidden = false;
                    DrawChart(moveDataModel.StatusReason);
                }
          
        }


        /// <summary>
        /// Method Name     : SetMoveAddressAndValueDataToUILabel
        /// Author          : Hiren Patel
        /// Creation Date   : 3 Jan 2018
        /// Purpose         : To set move address value and valuation data to UL label to display on screen
        /// Revision        : 
        /// </summary>
        /// <param name="moveDataModel">Object move data.</param>
        private void SetMoveAddressAndValueDataToUILabel(MoveDataModel moveDataModel)
        {
            lblOriginCity.Text = moveDataModel.Origin_City;
            lbldestinationCity.Text = moveDataModel.Destination_City;
            lblOriginAddress.Text = moveDataModel.CustomOriginAddress;
            lblDestinationAddress.Text = moveDataModel.CustomDestinationAddress;
            if (string.IsNullOrEmpty(moveDataModel.daysLeft))
            {
                viewDaysLeft.Hidden = true;
                SetCenterFirstAndLastDate();
            }
            else
            {
                viewDaysLeft.Hidden = false;
                lblDaysLeftNumber.Text = moveDataModel.daysLeft;
            }
            lblLoadFirstDay.Text = moveDataModel.MoveStartDate;
            lblLoadLastDay.Text = moveDataModel.MoveEndDate;
            txtViewWhatMattersMost.Text = moveDataModel.WhatMattersMost;
            lblDeclaredValue.Text = moveDataModel.ExcessValuation;
            lblCoverageValue.Text = moveDataModel.ValuationDeductible;
            lblCostValue.Text = moveDataModel.ValuationCost;
        }

        /// <summary>
        /// Method Name     : HasIntroPagePermission
        /// Author          : Hiren Patel
        /// Creation Date   : 7 Jan 2018
        /// Purpose         : To check has customer has intro page display permission or not.
        /// Revision        : 
        /// </summary>
        /// Has the intro page permission.
        /// <returns><c>true</c>, if intro page permission was hased, <c>false</c> otherwise.</returns>
        private bool HasIntroPagePermission()
        {
            string key = string.Format(AppConstant.CUSTOMER_INTRO_KEY_FORMAT, UtilityPCL.LoginCustomerData.CustomerId);
            NSString nsString = new NSString(key);
            var NsValue = NSUserDefaults.StandardUserDefaults.ValueForKey(nsString);
            if (NsValue is null)
            {
                return true;
            }
            else if (string.IsNullOrEmpty(NsValue.ToString()))
            {
                return true;
            }
            else
            {
                return !Convert.ToBoolean(NsValue.ToString());
            }
        }

        /// <summary>
        /// Method Name     : SetIntroPagePermission
        /// Author          : Hiren Patel
        /// Creation Date   : 7 Jan 2018
        /// Purpose         : To set intro page permission
        /// Revision        : 
        /// <summary>
        private void SetIntroPagePermission()
        {
            string key = string.Format(AppConstant.CUSTOMER_INTRO_KEY_FORMAT, UtilityPCL.LoginCustomerData.CustomerId);
            NSUserDefaults.StandardUserDefaults.SetString(true.ToString(), key);
        }

        /// <summary>
        /// Method Name     : ResetDashBoardData
        /// Author          : Hiren Patel
        /// Creation Date   : 3 Jan 2018
        /// Purpose         : To reset Dashboard UI data
        /// Revision        : 
        /// </summary>
        private void ResetDashBoardData()
        {
            lblOriginCity.Text = string.Empty;
            lbldestinationCity.Text = string.Empty;
            lblOriginAddress.Text = string.Empty;
            lblDestinationAddress.Text = string.Empty;
            lblDaysLeftNumber.Text = string.Empty;
            lblLoadFirstDay.Text = string.Empty;
            lblLoadLastDay.Text = string.Empty;
            txtViewWhatMattersMost.Text = string.Empty;
            lblDeclaredValue.Text = string.Empty;
            lblCoverageValue.Text = string.Empty;
            lblCostValue.Text = string.Empty;
            scrollview.Hidden = true;
            scrollviewChart.Hidden = true;
            imgChartTruck.Hidden = true;
            lblMoveStatus.Hidden = true;

        }

        /// <summary>
        /// Method Name     : PopulateChart
        /// Author          : Hiren Patel
        /// Creation Date   : 3 Jan 2018
        /// Purpose         : Populates the chart.
        /// Revision        : 
        /// </summary>
        /// <returns>PlotModel</returns>
        /// <param name="strStatusCode">String status code.</param>
        private PlotModel PopulateChart(string strStatusCode)
        {
            var modelP1 = new PlotModel { Title = string.Empty };
            PieSeries seriesP1 = new PieSeries { AngleSpan = 300, StartAngle = -240, InnerDiameter = 0.93, OutsideLabelFormat = "", LegendFormat = null };
            PieSeries seriesP2 = new PieSeries { AngleSpan = 300, StartAngle = -240, InnerDiameter = 0.6, Diameter = 0.93, OutsideLabelFormat = "", AreInsideLabelsAngled = false };

            seriesP1.OutsideLabelFormat = "";
            seriesP1.TickHorizontalLength = 0;
            seriesP1.TickRadialLength = 0.0;
            seriesP1.StrokeThickness = 0.0;
            seriesP2.StrokeThickness = 0.0;
            seriesP1.TickDistance = 0;
            seriesP1.Stroke = OxyColors.Transparent;
            seriesP2.Stroke = OxyColors.Transparent;
            seriesP2.OutsideLabelFormat = "";
            seriesP2.TickHorizontalLength = 0;
            seriesP2.TickRadialLength = 0;

            switch (strStatusCode)
            {
                case "Loaded":
                    SetChartMiddleStatusImageAndStatus("Loaded","icon_box.png","move_status_get_prepared.png");
                    ChartLoaded(seriesP1, seriesP2);
                    break;

                case "Packed":
                    SetChartMiddleStatusImageAndStatus("Packed", "icon_box.png", "move_status_get_prepared.png");
                    ChartPacked(seriesP1, seriesP2);
                    break;

                case "InTransit":
                    lblMoveStatus.Text = "InTransit";
                    SetChartMiddleStatusImageAndStatus("InTransit", "truck.png", "move_status_on_route.png");
                    ChartTransit(seriesP1, seriesP2);
                    break;

                case "Invoiced":
                    modelP1 = PopulateChartInvoiced(seriesP1, seriesP2);
                    SetChartMiddleStatusImageAndStatus("PENDING FINAL PAYMENT", "icon_invoice_chart.png", "move_status_final_payment.png");
                    break;
                case "Booked":
                    ChartBooked(seriesP1,seriesP2);
                    SetChartMiddleStatusImageAndStatus("Booked", "icon_invoice_chart.png", "move_status_confirmed.png");
                    break;

                case "Delivered":
                    SetChartMiddleStatusImageAndStatus("PENDING FINAL PAYMENT", "icon_Move_Confirm_chart.png", "move_status_final_payment.png");
                    ChartDeliveredAndInvoice(seriesP1, seriesP2);
                    break;
            }
            modelP1.Series.Add(seriesP1);
            modelP1.Series.Add(seriesP2);

            return modelP1;

        }

        /// <summary>
        /// Method Name     : PopulateChart
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Use for populate chart data
        /// Revision        : 
        /// </summary>
        private PlotModel PopulateChartInvoiced(PieSeries seriesP1, PieSeries seriesP2)
        {
            var modelP1 = new PlotModel { Title = "" };
            seriesP1 = new PieSeries { AngleSpan = 340, StartAngle = -260, InnerDiameter = 0.93, OutsideLabelFormat = "", LegendFormat = null };
            seriesP2 = new PieSeries { AngleSpan = 340, StartAngle = -260, InnerDiameter = 0.6, Diameter = 0.93, OutsideLabelFormat = "", AreInsideLabelsAngled = false };
            SetChartMiddleStatusImageAndStatus("Invoiced", "icon_invoice_chart.png", "move_status_confirmed.png");

            seriesP1.OutsideLabelFormat = "";
            seriesP1.TickHorizontalLength = 0;
            seriesP1.TickRadialLength = 0.0;
            seriesP1.StrokeThickness = 0.0;
            seriesP2.StrokeThickness = 0.0;
            seriesP1.TickDistance = 0;
            seriesP1.Stroke = OxyColors.Transparent;
            seriesP2.Stroke = OxyColors.Transparent;
            seriesP2.OutsideLabelFormat = "";
            seriesP2.TickHorizontalLength = 0;
            seriesP2.TickRadialLength = 0;
           
            ChartDeliveredAndInvoice(seriesP1, seriesP2);
            modelP1.Series.Add(seriesP1);
            modelP1.Series.Add(seriesP2);

            return modelP1;
        }

        /// <summary>
        /// Method Name     : SetChartMiddleStatusImageAndStatus
        /// Author          : Hiren Patel
        /// Creation Date   : 3 Jan 2018
        /// Purpose         : Sets the chart middle status image and status
        /// Revision        : 
        /// 
        /// </summary>
        /// <param name="moveStatus">Move status.</param>
        /// <param name="bottomStatusImageURL">Bottom status image URL.</param>
        /// <param name="centerStatusImageURL">Center status image URL.</param>
        private void SetChartMiddleStatusImageAndStatus(string moveStatus,string bottomStatusImageURL, string centerStatusImageURL)
        {
            lblMoveStatus.Text = moveStatus;
            imgChartTruck.Image = UIImage.FromFile(bottomStatusImageURL);
            imgMoveStatus.Image = UIImage.FromFile(centerStatusImageURL);
        }

        /// <summary>
        /// Method Name     : ChartLoad
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Use for binding chart status Load
        /// Revision        : 
        /// </summary>

        //1  Book
        public void ChartBooked(PieSeries seriesP1, PieSeries seriesP2)
        {
            //outer
            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(236, 61, 98) });

            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(50, 212, 212) });
            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(202, 206, 221) });
            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(252, 238, 233) });
            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(225, 249, 255) });

            //inner
            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = false, Fill = OxyColor.FromRgb(232, 144, 165) });

            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = false, Fill = OxyColor.FromRgb(227, 227, 233) });
            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = false, Fill = OxyColor.FromRgb(227, 227, 233) });
            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = true, Fill = OxyColor.FromRgb(227, 227, 233) });
            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = true, Fill = OxyColor.FromRgb(227, 227, 233) });
        }

        /// <summary>
        /// Method Name     : ChartPacked
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Use for binding chart status Packed
        /// Revision        : 
        /// </summary>
        //2 Packed
        public void ChartPacked(PieSeries seriesP1, PieSeries seriesP2)
        {
            //outer
            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(236, 61, 98) });
            seriesP1.Slices.Add(new PieSlice("", 60) { Fill = OxyColor.FromRgb(50, 212, 212) });
            seriesP1.Slices.Add(new PieSlice("", 40) { Fill = OxyColor.FromRgb(138, 219, 222) });
            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(202, 206, 221) });
            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(252, 238, 233) });
            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(225, 249, 255) });

            //inner
            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = false, Fill = OxyColor.FromRgb(232, 144, 165) });
            seriesP2.Slices.Add(new PieSlice("", 60) { IsExploded = false, Fill = OxyColor.FromRgb(201, 225, 233) });
            seriesP2.Slices.Add(new PieSlice("", 40) { IsExploded = false, Fill = OxyColor.FromRgb(227, 227, 233) });
            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = false, Fill = OxyColor.FromRgb(227, 227, 233) });
            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = true, Fill = OxyColor.FromRgb(227, 227, 233) });
            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = true, Fill = OxyColor.FromRgb(227, 227, 233) });

        }

        /// <summary>
        /// Method Name     : ChartTransit
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Use for binding chart status Transit
        /// Revision        : 
        /// </summary>
        //3 Loaded
        public void ChartLoaded(PieSeries seriesP1, PieSeries seriesP2)
        {
            //outer
            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(236, 61, 98) });
            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(50, 212, 212) });
            seriesP1.Slices.Add(new PieSlice("", 60) { Fill = OxyColor.FromRgb(22, 51, 99) });
            seriesP1.Slices.Add(new PieSlice("", 40) { Fill = OxyColor.FromRgb(202, 206, 221) });
            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(248, 221, 211) });
            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(225, 249, 255) });

            //inner
            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = false, Fill = OxyColor.FromRgb(232, 144, 165) });
            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = false, Fill = OxyColor.FromRgb(138, 219, 222) });
            seriesP2.Slices.Add(new PieSlice("", 60) { IsExploded = false, Fill = OxyColor.FromRgb(124, 139, 166) });
            seriesP2.Slices.Add(new PieSlice("", 40) { IsExploded = false, Fill = OxyColor.FromRgb(227, 227, 233) });
            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = false, Fill = OxyColor.FromRgb(227, 227, 233) });
            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = true, Fill = OxyColor.FromRgb(227, 227, 233) });
        }

        /// <summary>
        /// Method Name     : ChartInvoiced
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Use for binding chart status Invoiced
        /// Revision        : 
        /// </summary>
        //4 InTransit
        public void ChartTransit(PieSeries seriesP1, PieSeries seriesP2)
        {
            //outer
            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(236, 61, 98) });
            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(50, 212, 212) });
            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(22, 51, 99) });
            seriesP1.Slices.Add(new PieSlice("", 60) { Fill = OxyColor.FromRgb(242, 130, 116) });
            seriesP1.Slices.Add(new PieSlice("", 40) { Fill = OxyColor.FromRgb(248, 221, 211) });
            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(225, 249, 255) });

            //inner
            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = false, Fill = OxyColor.FromRgb(232, 144, 165) });
            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = false, Fill = OxyColor.FromRgb(138, 219, 222) });
            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = false, Fill = OxyColor.FromRgb(124, 139, 166) });
            seriesP2.Slices.Add(new PieSlice("", 60) { IsExploded = false, Fill = OxyColor.FromRgb(252, 238, 233) });
            seriesP2.Slices.Add(new PieSlice("", 40) { IsExploded = false, Fill = OxyColor.FromRgb(227, 227, 233) });
            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = true, Fill = OxyColor.FromRgb(227, 227, 233) });

        }

        /// <summary>
        /// Method Name     : ChartInvoiced
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Use for binding chart status Delivered
        /// Revision        : 
        /// </summary>
        //5
        public void ChartDeliveredAndInvoice(PieSeries seriesP1, PieSeries seriesP2)
        {
            //outer
            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(236, 61, 98) });
            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(50, 212, 212) });
            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(22, 51, 99) });
            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(242, 130, 116) });
            seriesP1.Slices.Add(new PieSlice("", 100) { Fill = OxyColor.FromRgb(9, 205, 255) });


            //inner
            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = false, Fill = OxyColor.FromRgb(232, 144, 165) });
            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = false, Fill = OxyColor.FromRgb(138, 219, 222) });
            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = false, Fill = OxyColor.FromRgb(124, 139, 166) });
            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = false, Fill = OxyColor.FromRgb(241, 190, 171) });
            seriesP2.Slices.Add(new PieSlice("", 100) { IsExploded = false, Fill = OxyColor.FromRgb(195, 219, 233) });

        }
    }
}
