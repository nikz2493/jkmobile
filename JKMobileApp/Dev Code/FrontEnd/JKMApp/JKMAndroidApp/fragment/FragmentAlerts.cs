using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Support.V7.Widget;
using Android.Views;
using JKMAndroidApp.adapter;
using JKMAndroidApp.Common;
using JKMPCL;
using JKMPCL.Model;
using JKMPCL.Services;
using System;
using System.Collections.Generic;
using static Android.Provider.CalendarContract;
using static JKMAndroidApp.adapter.AdapterAlerts;


namespace JKMAndroidApp.fragment
{
    /// <summary>
    /// Method Name     : FragmentAlerts
    /// Author          : Sanket Prajapati
    /// Creation Date   : 23 Jan 2018
    /// Purpose         : Fragement for Alert layout
    /// Revision        : 
    /// </summary>
    public class FragmentAlerts : Android.Support.V4.App.Fragment, IAdapterTextViewClickListener
    {
        ProgressDialog progressDialog;
        private RecyclerView recyclerViewAlerts;
        List<AlertModel> alertList;
       
        CustomerModel customerModel;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.LayoutFragmentAlerts, container, false);
            recyclerViewAlerts = view.FindViewById<RecyclerView>(Resource.Id.recyclerViewAlerts);
            SetAdapterAsync();
            return view;
        }

        /// <summary>
        /// Method Name     : SetAdapterAsync
        /// Author          : Sanket Prajapati
        /// Creation Date   : 9 jan 2018
        /// Purpose         :  Bind Data alert data   
        /// Revision        : 
        /// </summary>
        private async void SetAdapterAsync()
        {
            alertList = new List<AlertModel>();
            Alert alert;
            alert = new Alert();
            string errorMessage = string.Empty;
            try
            {
                progressDialog = UIHelper.SetProgressDailoge(Activity);
                customerModel = new CustomerModel();
                customerModel.CustomerId = UtilityPCL.LoginCustomerData.CustomerId;
                customerModel.LastLoginDate = UtilityPCL.LoginCustomerData.LastLoginDate;
                APIResponse<List<AlertModel>> serviceResponse;
                serviceResponse = await alert.GetAlertList(customerModel);
                if (serviceResponse.STATUS)
                {
                    SetAlertDataBinding(serviceResponse);
                }
                else
                {
                    errorMessage = serviceResponse.Message;
                }

            }
            catch (Exception error)
            {
                errorMessage = error.Message;
            }
            finally
            {
                progressDialog.Dismiss();
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    AlertMessage(errorMessage);
                }
            }
        }

        /// <summary>
        /// Method Name     : SetAlertDataBinding
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Use alert data bind in adapter 
        /// Revision        : 
        /// </summary>
        private void SetAlertDataBinding(APIResponse<List<AlertModel>> serviceResponse)
        {
            if (serviceResponse.DATA != null)
            {
                alertList = serviceResponse.DATA;
                if (alertList.Count > 0)
                {
                    AdapterAlerts adapterAlerts = new AdapterAlerts(alertList, this);
                    recyclerViewAlerts.SetLayoutManager(new LinearLayoutManager(Activity));
                    recyclerViewAlerts.SetItemAnimator(new DefaultItemAnimator());
                    recyclerViewAlerts.SetAdapter(adapterAlerts);
                }
            }
        }

        /// <summary>
        /// Method Name      : AlertMessage
        /// Author          : Sanket Prajapati
        /// Creation Date   : 24 jan 2018
        /// Purpose         : Show alert message
        /// Revision        : 
        /// </summary>
        public void AlertMessage(String StrErrorMessage)
        {
            AlertDialog.Builder dialog;
            AlertDialog alertdialog;
            dialog = new AlertDialog.Builder(new ContextThemeWrapper(Activity, Resource.Style.AlertDialogCustom));
            alertdialog = dialog.Create();
            alertdialog.SetMessage(StrErrorMessage);
            alertdialog.SetButton(StringResource.msgOK, (c, ev) =>
            {
                alertdialog.Dispose();
            });
            alertdialog.Show();
        }

        /// <summary>
        /// Event Name      : OnTextviewActionClick
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Set event In Calender
        /// Revision        : 
        /// </summary>
        public void OnTextviewActionClick(int position)
        {
            if (alertList.Count > 0)
            {
                AlertModel alertModel = alertList[position];
                Intent intent = new Intent(Intent.ActionInsert)
               .SetData(Events.ContentUri)
               .PutExtra(CalendarContract.ExtraEventBeginTime, alertModel.StartDate.ToString())
               .PutExtra(CalendarContract.ExtraEventEndTime, alertModel.EndDate.ToString())
               .PutExtra(CalendarContract.Events.InterfaceConsts.Title, alertModel.AlertTitle);
                StartActivityForResult(intent, 101);
            }
        }
    }
}