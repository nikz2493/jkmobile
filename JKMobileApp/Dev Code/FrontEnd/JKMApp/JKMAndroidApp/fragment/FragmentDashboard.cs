using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using OxyPlot.Xamarin.Android;
using OxyPlot;
using OxyPlot.Series;
using Android.Support.V7.Widget;
using JKMAndroidApp.adapter;
using Android.Support.V7.Widget.Helper;
using JKMPCL.Services;
using JKMPCL.Model;
using JKMAndroidApp.Common;

namespace JKMAndroidApp.fragment
{
    public class FragmentDashboard : Android.Support.V4.App.Fragment
    {
        public interface IItemTouchHelperAdapter
        {
            bool OnItemMove(int oldPosition, int newPosition);
            void OnItemDismiss(int position);
        }

        private View view;
        private TextView tvLeftDays;
        private TextView tvFromCity;
        private TextView tvFromAddress;
        private TextView tvToCity;
        private TextView tvToAddress;
        private TextView tvEndDate;
        private TextView tvStartDate;
        private TextView tvStatus;
        private ImageView imgViewInnerChartStatus;
        private ImageView imgViewStatus;
        private LinearLayout lnLeftDays;
        private RelativeLayout relativeLayoutParentDashbord;
        private RecyclerView recylerViewDashboard;
        private RelativeLayout relativeLayoutPieChart;
        ProgressDialog progressDialog;
        private MoveDataModel dtoMoveData;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.LayoutFragmentDashboard, container, false);
            dtoMoveData = DTOConsumer.dtoMoveData;
            UIReference();
            BindMoveDataAsync();
            ApplyFont();
           
            
            return view;
        }

        /// Method Name     : UIReference
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Find all control   
        /// Revision        : 
        /// </summary>
        private void UIReference()
        {
            recylerViewDashboard = view.FindViewById<RecyclerView>(Resource.Id.recylerViewDashboard);
            tvLeftDays = view.FindViewById<TextView>(Resource.Id.tvLeftDays);
            lnLeftDays = view.FindViewById<LinearLayout>(Resource.Id.lnLeftDays);
            tvFromCity = view.FindViewById<TextView>(Resource.Id.tvFromCity);
            tvFromAddress = view.FindViewById<TextView>(Resource.Id.tvFromAddress);
            tvToCity = view.FindViewById<TextView>(Resource.Id.tvToCity);
            tvToAddress = view.FindViewById<TextView>(Resource.Id.tvToAddress);
            tvStartDate = view.FindViewById<TextView>(Resource.Id.tvStartDate);
            tvEndDate = view.FindViewById<TextView>(Resource.Id.tvEndDate);
            imgViewInnerChartStatus = view.FindViewById<ImageView>(Resource.Id.ImgViewInnerChartStatus);
            tvStatus = view.FindViewById<TextView>(Resource.Id.tvStatus);
            imgViewStatus = view.FindViewById<ImageView>(Resource.Id.ImgViewStatus);
            relativeLayoutPieChart = view.FindViewById<RelativeLayout>(Resource.Id.relativeLayoutPieChart);
            relativeLayoutParentDashbord = view.FindViewById<RelativeLayout>(Resource.Id.relativeLayoutParentDashbord);
            relativeLayoutParentDashbord.Visibility = ViewStates.Invisible;
        }

        /// <summary>
        /// Method Name     : BindMoveData
        /// Author          : Hiren Patel
        /// Creation Date   : 30 Dec 2017
        /// Purpose         : To bind Move data with UI element
        /// Revision        : 
        /// </summary>
        private void BindMoveDataAsync()
        {

            Move move;
            move = new Move();
            string errorMessage = string.Empty;
            try
            {
                progressDialog = UIHelper.SetProgressDailoge(Activity);
                if (dtoMoveData is null)
                {
                    relativeLayoutParentDashbord.Visibility = ViewStates.Invisible;
                    AlertMessage(StringResource.msgDashboardNotLoad);
                }
                else
                {
                    if (dtoMoveData.response_status)
                    {
                        SetMoveData(dtoMoveData);
                    }
                    else
                    {
                        errorMessage = dtoMoveData.message;
                        relativeLayoutParentDashbord.Visibility = ViewStates.Invisible;
                        progressDialog.Dismiss();
                    }
                }
            }
            catch (Exception error)
            {
                progressDialog.Dismiss();
                errorMessage = error.Message;
            }
            finally
            {
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    AlertMessage(errorMessage);
                }
            }
        }

        /// Method Name     : SetMoveData
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Use for move binding data  and check active and inactive move 
        /// Revision        : 
        /// </summary>
        private void SetMoveData(MoveDataModel dtoMoveData)
        {
            if (dtoMoveData is null)
            {
                relativeLayoutParentDashbord.Visibility = ViewStates.Invisible;
                progressDialog.Dismiss();
                AlertMessage(StringResource.msgDashboardNotLoad);
            }
            else
            {
                progressDialog.Dismiss();
                if (dtoMoveData.IsActive == "1")
                {
                    relativeLayoutParentDashbord.Visibility = ViewStates.Invisible;
                    AlertMessage(dtoMoveData.message);
                }
                else
                {
                    if (!string.IsNullOrEmpty(dtoMoveData.StatusReason))
                    {
                        if (dtoMoveData.StatusReason == "Invoiced" || dtoMoveData.StatusReason == "Booked" )
                        {
                            PopulateChartBookAndInvoicedOrDelivered(dtoMoveData.StatusReason);
                        }
                        else
                        {
                            PopulateCharttPackedAndLoadedOrInTransit(dtoMoveData.StatusReason);
                        }
                    }
                    else
                    {
                        relativeLayoutPieChart.Visibility = ViewStates.Invisible;
                    }
                    SetAdapter(dtoMoveData);
                    SetDataToControl(dtoMoveData);
                }
            }
        }

        /// Method Name     : SetDataToControl
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         :  Use for move data set to control 
        /// Revision        : 
        /// </summary>
        private void SetDataToControl(MoveDataModel dtoMoveData)
        {
            relativeLayoutParentDashbord.Visibility = ViewStates.Visible;
            tvFromCity.Text = dtoMoveData.Origin_City;
            tvToCity.Text = dtoMoveData.Destination_City;
            tvFromAddress.Text = dtoMoveData.CustomOriginAddress;
            tvToAddress.Text = dtoMoveData.CustomDestinationAddress;
            tvLeftDays.Text = dtoMoveData.daysLeft;
            tvStartDate.Text = dtoMoveData.MoveStartDate;
            tvEndDate.Text = dtoMoveData.MoveEndDate;
            if (string.IsNullOrEmpty(dtoMoveData.daysLeft))
            {
                lnLeftDays.Visibility = ViewStates.Gone;
            }
            else
            {
                lnLeftDays.Visibility = ViewStates.Visible;
                tvLeftDays.Text = dtoMoveData.daysLeft;
            }
            if (!string.IsNullOrEmpty(dtoMoveData.CustomOriginAddress) && dtoMoveData.CustomOriginAddress.Length > 25)
            {
                tvFromAddress.Text = dtoMoveData.CustomOriginAddress.Substring(0, 25);
            }
            else
            {
                tvFromAddress.Text = dtoMoveData.CustomOriginAddress;
            }

            if (!string.IsNullOrEmpty(dtoMoveData.CustomDestinationAddress) && dtoMoveData.CustomDestinationAddress.Length > 25)
            {
                tvToAddress.Text = dtoMoveData.CustomDestinationAddress.Substring(0, 25);
            }
            else
            {
                tvToAddress.Text = dtoMoveData.CustomDestinationAddress;
            }
        }

        /// <summary>
        /// Method Name     : AlertMessage
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Show alert message
        /// Revision        : 
        /// </summary>
        public void AlertMessage(String StrErrorMessage)
        {
            AlertDialog.Builder dialogue;
            AlertDialog alert;
            dialogue = new Android.App.AlertDialog.Builder(new ContextThemeWrapper(Activity, Resource.Style.AlertDialogCustom));
            alert = dialogue.Create();
            alert.SetMessage(StrErrorMessage);
            alert.SetButton(StringResource.msgOK, (c, ev) =>
            {
                alert.Dispose();
            });
            alert.Show();
        }

        /// Method Name     : SetAdapter
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Use for Binding recylerView  
        /// Revision        : 
        /// </summary>
        private void SetAdapter(MoveDataModel moveData)
        {
            List<ModelloadData> modelloadDataList = SetDataToCardview(moveData);
            if (modelloadDataList.Count > 0)
            {
                AdapterDashboard adapterDashboard = new AdapterDashboard(Activity, modelloadDataList);
                recylerViewDashboard.SetLayoutManager(new LinearLayoutManager(Activity));
                recylerViewDashboard.SetItemAnimator(new DefaultItemAnimator());
                recylerViewDashboard.NestedScrollingEnabled = false;
                recylerViewDashboard.HasFixedSize = false;
                recylerViewDashboard.SetAdapter(adapterDashboard);

                ItemTouchHelper.Callback callback = new ItemTouchHelperCallback(adapterDashboard);
                ItemTouchHelper itemTouchHelper = new ItemTouchHelper(callback);
                itemTouchHelper.AttachToRecyclerView(recylerViewDashboard);
            }
        }

        /// Method Name     : SetDataToCardview
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Use for Binding data to card view  (Myservices,valution, Whatmatermost)
        /// Revision        : 
        /// </summary>
        private List<ModelloadData> SetDataToCardview(MoveDataModel moveData)
        {
            List<ModelloadData> mModelloadDataList = new List<ModelloadData>();
            //Set myservice data
            List<ModelloadData> mModelDummyDataServiceList = new List<ModelloadData>();
            foreach (MyServicesModel myServicesmodal in moveData.MyServices)
            {
                mModelDummyDataServiceList.Add(new ModelloadData(myServicesmodal.ServicesCode, ""));
            }
            //Set valution data
            List<ModelloadData> mModelloadDataValuationList = new List<ModelloadData>();
            mModelloadDataValuationList.Add(new ModelloadData(0, StringResource.msgDeclaredValue, moveData.ExcessValuation));
            mModelloadDataValuationList.Add(new ModelloadData(1, StringResource.msgCoverage, moveData.ValuationDeductible));
            mModelloadDataValuationList.Add(new ModelloadData(2, StringResource.msgCost, moveData.ValuationCost));

            //Set fill cardview 
            mModelloadDataList.Add(new ModelloadData(0, StringResource.msgMyServices, mModelDummyDataServiceList));
            mModelloadDataList.Add(new ModelloadData(1, StringResource.msgWhatMattersMost, new ModelloadData(moveData.WhatMattersMost, "")));
            mModelloadDataList.Add(new ModelloadData(2, StringResource.msgValuation, mModelloadDataValuationList));

            return mModelloadDataList;
        }

        /// Method Name     : PopulateChart
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Use for populate chart data
        /// Revision        : 
        /// </summary>
        private void PopulateCharttPackedAndLoadedOrInTransit(string strStatusCode)
        {
            PieSeries seriesP1;
            PieSeries seriesP2;

            PlotView plotView = view.FindViewById<PlotView>(Resource.Id.plotView1);
            var modelP1 = new PlotModel { Title = "" };
            seriesP1 = new PieSeries { AngleSpan = 320, StartAngle = -250, InnerDiameter = 0.93, OutsideLabelFormat = "", LegendFormat = null };
            seriesP2 = new PieSeries { AngleSpan = 320, StartAngle = -250, InnerDiameter = 0.6, Diameter = 0.93, OutsideLabelFormat = "", AreInsideLabelsAngled = false };
            SetStatusWaysChartPackedAndLoadedOrInTransit(strStatusCode, seriesP1, seriesP2);
            RemovChartBorder(seriesP1, seriesP2);
            modelP1.Series.Add(seriesP1);
            modelP1.Series.Add(seriesP2);

            plotView.Model = modelP1;
        }

        /// Method Name     : SetStatusWaysChart
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Use for set chart status ways
        /// Revision        : 
        /// </summary>
        private void SetStatusWaysChartPackedAndLoadedOrInTransit(string strStatusCode, PieSeries seriesP1, PieSeries seriesP2)
        {
            switch (strStatusCode)
            {
                //Booked  Packed Loaded InTransit Delivered Invoiced

                case "Packed":
                    imgViewStatus.SetImageResource(Resource.Drawable.icon_box);
                    imgViewInnerChartStatus.SetImageResource(Resource.Drawable.icon_get_prepared);
                    tvStatus.Text = "Packed";
                    ChartPacked(seriesP1, seriesP2);
                    break;

                case "Loaded":
                    imgViewStatus.SetImageResource(Resource.Drawable.icon_box);
                    imgViewInnerChartStatus.SetImageResource(Resource.Drawable.icon_get_prepared);
                    tvStatus.Text = "Loaded";
                    ChartLoaded(seriesP1, seriesP2);
                    break;

                case "InTransit":
                    imgViewStatus.SetImageResource(Resource.Drawable.TruckTest120);
                    imgViewInnerChartStatus.SetImageResource(Resource.Drawable.Onrout);
                    tvStatus.Text = "InTransit";
                    tvStatus.TranslationX = -10;
                    ChartTransit(seriesP1, seriesP2);
                    break;

                case "Delivered":
                    imgViewStatus.SetImageResource(Resource.Drawable.icon_Move_Confirm_chart);
                    imgViewInnerChartStatus.SetImageResource(Resource.Drawable.Onrout);
                    tvStatus.Text = "Delivered";
                    tvStatus.TranslationX = -10;
                    ChartDeliveredAndInvoice(seriesP1, seriesP2);
                    break;
            }
        }

        /// Method Name     : PopulateChart
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Use for populate chart data
        /// Revision        : 
        /// </summary>
        private void PopulateChartBookAndInvoicedOrDelivered(string StatusReason)
        {
            PieSeries seriesP1;
            PieSeries seriesP2;

            PlotView plotView = view.FindViewById<PlotView>(Resource.Id.plotView1);
            var modelP1 = new PlotModel { Title = "" };
            seriesP1 = new PieSeries { AngleSpan = 340, StartAngle = -260, InnerDiameter = 0.93, OutsideLabelFormat = "", LegendFormat = null };
            seriesP2 = new PieSeries { AngleSpan = 340, StartAngle = -260, InnerDiameter = 0.6, Diameter = 0.93, OutsideLabelFormat = "", AreInsideLabelsAngled = false };
           
           
            tvStatus.Text = StatusReason;
            SetStatusBookAndInvoiceChart(StatusReason, seriesP1, seriesP2);
            RemovChartBorder(seriesP1, seriesP2);
            modelP1.Series.Add(seriesP1);
            modelP1.Series.Add(seriesP2);

            plotView.Model = modelP1;
        }



        /// Method Name     : SetStatusWaysChart
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Use for set chart status ways
        /// Revision        : 
        /// </summary>
        private void SetStatusBookAndInvoiceChart(string strStatusCode, PieSeries seriesP1, PieSeries seriesP2)
        {
            switch (strStatusCode)
            {
                //Booked  Packed Loaded InTransit Delivered Invoiced
                case "Booked":
                    imgViewStatus.SetImageResource(Resource.Drawable.icon_invoice_chart);
                    imgViewInnerChartStatus.SetImageResource(Resource.Drawable.icon_move_confirmed);
                    tvStatus.Text = "Booked";
                    imgViewInnerChartStatus.TranslationX = 15;
                    imgViewStatus.TranslationX = 2;
                    ChartBooked(seriesP1, seriesP2);
                    break;

               case "Invoiced":
                    imgViewStatus.SetImageResource(Resource.Drawable.icon_invoice_chart);
                    imgViewInnerChartStatus.SetImageResource(Resource.Drawable.icon_move_confirmed);
                    imgViewInnerChartStatus.TranslationX = 15;
                    tvStatus.Text = "Invoiced";
                    tvStatus.TranslationX = -5;
                    ChartDeliveredAndInvoice(seriesP1, seriesP2);
                    break;
               
            }
        }

        /// Method Name     : RemovChartBorder
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Use for remove chart border
        /// Revision        : 
        /// </summary>
        private void RemovChartBorder(PieSeries seriesP1, PieSeries seriesP2)
        {
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
        }

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

        /// Method Name     : GetloadDataList
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Use for Get loadDataList
        /// Revision        : 
        /// </summary>
        public static List<ModelloadData> GetloadDataList()
        {
            var list = new List<ModelloadData>();
            list.Add(new ModelloadData(StringResource.msgMyServices, "subtitle"));
            list.Add(new ModelloadData(StringResource.msgWhatMattersMost, "subtitle"));
            list.Add(new ModelloadData(StringResource.msgValuation, "subtitle"));

            return list;
        }

        /// class Name     : ModelloadData
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Use for Card view bind
        /// Revision        : 
        /// </summary>
        public class ModelloadData
        {
            public ModelloadData(string title, string subtitle)
            {
                this.title = title;
                this.subTitle = subtitle;
            }

            public ModelloadData(int id, string title, string value)
            {
                this.id = id;
                this.title = title;
                this.value = value;
            }

            public ModelloadData(int id, string title, Object mObject)
            {
                this.id = id;
                this.title = title;
                this.mObject = mObject;
            }

            public string title { get; set; }
            public string subTitle { get; set; }
            public int id { get; set; }
            public string value { get; set; }
            public Object mObject { get; set; }
        }

        /// class Name      : ItemTouchHelperCallback
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Use for Card view swaping 
        /// Revision        : 
        /// </summary>
        public class ItemTouchHelperCallback : ItemTouchHelper.Callback
        {
            private readonly AdapterDashboard mAdapterDashboard;

            public ItemTouchHelperCallback(AdapterDashboard adapterDashboard)
            {
                mAdapterDashboard = adapterDashboard;
            }
            public override bool IsItemViewSwipeEnabled => false;

            public override bool IsLongPressDragEnabled => true;

            public override int GetMovementFlags(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder)
            {
                int dragFlags = ItemTouchHelper.Up | ItemTouchHelper.Down;
                int swipeFlags = ItemTouchHelper.Left | ItemTouchHelper.Right;

                return MakeMovementFlags(dragFlags, swipeFlags);
            }

            public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target)
            {
                mAdapterDashboard.OnItemMove(viewHolder.AdapterPosition, target.AdapterPosition);
                return true;
            }
            public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction)
            {
                //Does not require implementation. Used to override / remove existing functionality of android.
            }
        }

        /// <summary>
        /// Method Name     : ApplyFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 4 jan 2017
        /// Purpose         : Apply font all control   
        /// Revision        : 
        /// </summary>
        public void ApplyFont()
        {
            UIHelper.SetTextViewFont(tvFromCity, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvToCity, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
            UIHelper.SetTextViewFont(tvFromAddress, (int)UIHelper.LinotteFont.LinotteRegular, Activity.Assets);
            UIHelper.SetTextViewFont(tvToAddress, (int)UIHelper.LinotteFont.LinotteRegular, Activity.Assets);
            UIHelper.SetTextViewFont(tvStartDate, (int)UIHelper.LinotteFont.LinotteRegular, Activity.Assets);
            UIHelper.SetTextViewFont(tvEndDate, (int)UIHelper.LinotteFont.LinotteRegular, Activity.Assets);
            UIHelper.SetTextViewFont(tvLeftDays, (int)UIHelper.LinotteFont.LinotteSemiBold, Activity.Assets);
        }
    }
}