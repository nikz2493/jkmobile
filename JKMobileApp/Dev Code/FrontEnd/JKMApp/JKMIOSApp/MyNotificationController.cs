﻿// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using JKMPCL;
using JKMPCL.Model;
using JKMPCL.Services;
using UIKit;

namespace JKMIOSApp
{
    /// <summary>
    /// Controller Name : MyNotificationController
    /// Author          : Hiren Patel
    /// Creation Date   : 16 JAN 2018
    /// Purpose         : To display alert page screen as app shell screen
    /// Revision        : 
    /// </summary>
    public partial class MyNotificationController : UIViewController
    {
        readonly Alert alertServices;
        List<AlertModel> alertList;
        public MyNotificationController(IntPtr handle) : base(handle)
        {
            alertServices = new Alert();
        }

        public MyNotificationController()
        {
           
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitilizeIntarface();
            await BindAlertList();
        }

        private async Task BindAlertList()
        {
            await GetAlertsList();
            tableRelaodAndSetData();
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
            // InitilizeIntarface
            btnAlert.TouchUpInside += BtnAlert_TouchUpInside;
            btnBack.TouchUpInside += BtnBack_TouchUpInside;
        }

        /// <summary>
        /// Method Name     : tableRelaodAndSetData
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : Tables the relaod and set data.
        /// Revision        : 
        /// </summary>
        private void tableRelaodAndSetData()
        {
            tblView.Source = new MyNotificationTableCellSource(alertList);
            tblView.ReloadData();
        }

        /// <summary>
        /// Event Name      : BtnAlert_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : Buttons the notification pressed.
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private async void BtnAlert_TouchUpInside(object sender, EventArgs e)
        {
            await BindAlertList();
        }

        /// <summary>
        /// Method Name     : BtnBack_TouchUpInside
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : Buttons the back pressed.
        /// Revision        : 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event Argument</param>
        private void BtnBack_TouchUpInside(object sender, EventArgs e)
        {
            DismissViewControllerAsync(true);
        }

        /// <summary>
        /// Method Name     : GetAlertsList
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : Get alert list from service
        /// Revision        : 
        /// </summary>
        public async Task GetAlertsList()
        {
            APIResponse<List<AlertModel>> serviceResponse = new APIResponse<List<AlertModel>>();
            alertList = new List<AlertModel>();
            serviceResponse.STATUS = false;
            string errorMessage = string.Empty;
            try
            {
                CustomerModel customerModel = new CustomerModel() {CustomerId=UtilityPCL.LoginCustomerData.CustomerId,LastLoginDate=UtilityPCL.LoginCustomerData.LastLoginDate };
                LoadingOverlay objectLoadingScreen = UIHelper.ShowLoadingScreen(View);
                serviceResponse = await alertServices.GetAlertList(customerModel);
                objectLoadingScreen.Hide();
                if (serviceResponse.STATUS)
                {
                    if (serviceResponse.DATA != null)
                    {
                        alertList = serviceResponse.DATA;
                    }
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
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    UIHelper.ShowMessage(errorMessage);
                }
            }
        }
    }
}
