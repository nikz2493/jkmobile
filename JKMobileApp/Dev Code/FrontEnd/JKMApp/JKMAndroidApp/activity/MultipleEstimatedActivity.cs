using Android.App;
using Android.OS;
using Android.Content;
using Android.Widget;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using JKMAndroidApp.adapter;
using Android.Views;
using System.Linq;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using JKMAndroidApp.Common;
using JKMPCL.Model;
using static JKMAndroidApp.adapter.AdapteMultipleEstimates;

namespace JKMAndroidApp.activity
{
    /// </summary>
    /// Method Name     : MultipleEstimatedActivity
    /// Author          : Sanket Prajapati
    /// Creation Date   : 2 Dec 2017
    /// Purpose         : Activity use for multiple Estimate page  
    /// Revision        : 
    /// </summary>
    [Activity(Label = "MultipleEstimatedActivity", Theme = "@style/MyTheme")]
    public class MultipleEstimatedActivity : Activity, IAdapterImageviewClickListener, IAdapterRadiobuttonClickListener
    {
        private List<EstimateModel> estimateList;
        private RecyclerView recylerViewEstimate;
        CardView relativelayCardViewBlock;
        LinearLayout linearLayoutBottom;
        TextView tvBookSelectedestimated;
        private TextView tvTitle;
        private Toolbar toolbar;
        private ImageButton imgAlert;
        private ImageButton imgContactus;
        List<ModelloadData> modelloadDatalist;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            estimateList = DTOConsumer.dtoEstimateData;
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LayoutMultipleEstimate);
            UIReferences();
            UIClickEvent();
            SetAdapter();
            ApplyFont();
        }

        /// </summary>
        /// Method Name     : UIReferences
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Finds Control  
        /// Revision        : 
        /// </summary>
        private void UIReferences()
        {
            toolbar = FindViewById<Toolbar>(Resource.Id.tool_bar);
            tvTitle = (TextView)toolbar.FindViewById(Resource.Id.toolbar_title);
            recylerViewEstimate = FindViewById<RecyclerView>(Resource.Id.recylerViewEstimate);
            relativelayCardViewBlock = FindViewById<CardView>(Resource.Id.relativelayCardViewBlock);
            linearLayoutBottom = FindViewById<LinearLayout>(Resource.Id.linearLayoutBottom);
            tvBookSelectedestimated = FindViewById<TextView>(Resource.Id.tvBookSelectedestimated);
            imgAlert = (ImageButton)toolbar.FindViewById(Resource.Id.item2);
            imgContactus = (ImageButton)toolbar.FindViewById(Resource.Id.item1);
            imgAlert.Visibility = ViewStates.Gone;
            tvTitle.Text = StringResource.titleEstimate;
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
            imgContactus.Click += delegate { StartActivity(new Intent(this, typeof(ContactusActivity))); };
            tvBookSelectedestimated.Click += TvBookSelectedestimated_Click;
        }

        private void TvBookSelectedestimated_Click(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(UIHelper.SelectedMoveNumber))
            {
                StartActivity(new Intent(this, typeof(ActivityEstimateViewPager)));
            }
            else
            {
                AlertMessage(StringResource.msgPleaseSelectAtleastOneEstimate);
            }
        }

        /// Method Name     : SetAdapter
        /// Author          : Sanket Prajapati
        /// Creation Date   : 12 Dec 2017
        /// Purpose         : bind services Data in Card view  
        /// Revision        : 
        /// </summary>
        private void SetAdapter()
        {
            if (modelloadDatalist==null)
            {
                modelloadDatalist = new List<ModelloadData>();
                foreach(EstimateModel  estimatemodal in estimateList)
                {
                    modelloadDatalist.Add(new ModelloadData() { EstimteNo = estimatemodal.MoveNumber,
                                                                EstimateAmount = estimatemodal.EstimatedLineHaul});
                }
            }

            AdapteMultipleEstimates adapterEstimateService = new AdapteMultipleEstimates(this, modelloadDatalist, this, this);
            recylerViewEstimate.SetLayoutManager(new LinearLayoutManager(this));
            recylerViewEstimate.SetItemAnimator(new DefaultItemAnimator());
            recylerViewEstimate.NestedScrollingEnabled = false;
            recylerViewEstimate.HasFixedSize = false;
            recylerViewEstimate.SetAdapter(adapterEstimateService);
        }

        /// class Name      : ModelloadData
        /// Author          : Sanket Prajapati
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Use for Card view bind
        /// Revision        : 
        /// </summary>
        public class ModelloadData
        {
            public string EstimteNo { get; set; }
            public string EstimateAmount { get; set; }
            public int id { get; set; }
            public string value { get; set; }
            public bool IsCheck { get; set; }
            public List<ModelloadData> listModelloadData { get; set; }
        }

        /// <summary>
        /// Method Name     : ApplyFont
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Apply font all control   
        /// Revision        : 
        /// </summary>
        public void ApplyFont()
        {
            UIHelper.SetTextViewFont(tvTitle, (int)UIHelper.LinotteFont.LinotteSemiBold, Assets);
            UIHelper.SetTextViewFont(tvBookSelectedestimated, (int)UIHelper.LinotteFont.LinotteSemiBold, Assets);
        }

        /// <summary>
        /// Event Name      : OnImageviewActionClick
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for open pdf
        /// Revision        : 
        /// </summary>
        public void OnImageviewActionClick(int position)
        {
            StartActivity(new Intent(this, typeof(PdfActivity)));
        }

        /// <summary>
        /// Event Name      : OnIRadiobuttonActionClick
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for set radio button check or uncheck and set single estimate id
        /// Revision        : 
        /// </summary>
        public void OnIRadiobuttonActionClick(int position)
        {
            if (modelloadDatalist.Count > 0)
            {
                ModelloadData modaldata = modelloadDatalist[position];
                UIHelper.SelectedMoveNumber = modaldata.EstimteNo;
                SetAdapter();
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
    }
}