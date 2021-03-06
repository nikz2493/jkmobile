﻿// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using Foundation;
using JKMPCL.Model;
using UIKit;

namespace JKMIOSApp
{
    /// <summary>
    /// Class Name      : myNotificationTableCell
    /// Author          : Hiren Patel
    /// Creation Date   : 16 JAN 2018
    /// Purpose         : My notification table cell source.
    /// Revision        : 
    /// </summary>
    internal class MyNotificationTableCellSource : UITableViewSource
    {
        private List<AlertModel> alertsList { get; set; }
        public MyNotificationTableCellSource(List<AlertModel> objAlertsList)
        {
             this.alertsList = objAlertsList;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (myNotificationTableCell)tableView.DequeueReusableCell("notificationcell", indexPath);
            AlertModel alertModel= alertsList[indexPath.Row];
            cell.SetNotifcationTitle(alertModel.AlertTitle);
            cell.SetNotificationTime(alertModel.StartDate);
            cell.SetNotifcationTypeImage(alertModel.AlertType.ToString());
            cell.alertModel=alertsList[indexPath.Row];
            return cell;
        }

        /// <summary>
        /// Class Name      : RowsInSection
        /// Author          : Hiren Patel
        /// Creation Date   : 16 JAN 2018
        /// Purpose         : To get no of alert count
        /// </summary>
        /// <returns>The in section.</returns>
        /// <param name="tableview">Tableview.</param>
        /// <param name="section">Section.</param>
        public override nint RowsInSection(UITableView tableview, nint section)
        {
                if (alertsList is null)
                {
                    return 0;
                }
                else
                {
                    return alertsList.Count;
                }
        }
    }
}