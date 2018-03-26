using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using JKMAndroidApp.activity;
using JKMAndroidApp.adapter;
using JKMPCL.Model;
using System;
using JKMPCL.Services;
using System.Collections.Generic;
using static JKMAndroidApp.adapter.AdapterMyDocument;
using JKMAndroidApp.Common;

namespace JKMAndroidApp.fragment
{

    /// <summary>
    /// Method Name     : FragmentMyDocument
    /// Author          : Sanket Prajapati
    /// Creation Date   : 23 Jan 2018
    /// Purpose         : Fragement for MyDocuments page
    /// Revision        : 
    /// </summary>
    public class FragmentMyDocuments : Android.Support.V4.App.Fragment, IAdapterImageviewClickListener
    {
        ProgressDialog progressDialog;
        private RecyclerView recyclerViewDocuments;
        List<DocumentModel> docList;
        MyDocument myDocument;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.LayoutFragmentMyDocuments, container, false);
            recyclerViewDocuments = view.FindViewById<RecyclerView>(Resource.Id.recyclerViewDocuments);
            SetAdapterAsync();
            return view;
        }

        /// <summary>
        /// Method Name     : SetAdapterAsync
        /// Author          : Sanket Prajapati
        /// Creation Date   : 9 jan 2018
        /// Purpose         : Bind Data alert data   
        /// Revision        : 
        /// </summary>
        private async void SetAdapterAsync()
        {
            myDocument = new MyDocument();
            string errorMessage = string.Empty;
            try
            {
                progressDialog = UIHelper.SetProgressDailoge(Activity);
                APIResponse<List<DocumentModel>> serviceResponse;
                serviceResponse = await myDocument.GetDocumentList(UtilityPCL.LoginCustomerData.CustomerId);
                if (serviceResponse.STATUS)
                {
                    SetMyDocumentDataBinding(serviceResponse);
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
        /// Purpose         : Use mydocument data bind in adapter 
        /// Revision        : 
        /// </summary>
        private void SetMyDocumentDataBinding(APIResponse<List<DocumentModel>> serviceResponse)
        {
            if (serviceResponse.DATA != null)
            {
                docList = serviceResponse.DATA;
                if (docList.Count > 0)
                {
                    AdapterMyDocument adapterAlerts = new AdapterMyDocument(docList, this);
                    recyclerViewDocuments.SetLayoutManager(new LinearLayoutManager(Activity));
                    recyclerViewDocuments.SetItemAnimator(new DefaultItemAnimator());
                    recyclerViewDocuments.SetAdapter(adapterAlerts);
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
        /// Event Name      : OnImageviewActionClick
        /// Author          : Sanket Prajapati
        /// Creation Date   : 2 Dec 2017
        /// Purpose         : Use for open pdf
        /// Revision        : 
        /// </summary>
        public void OnImageviewActionClick(int position)
        {
            StartActivity(new Intent(Activity, typeof(PdfActivity)));
        }
        
    }
}