using System;
using System.Collections.Generic;
using Foundation;
using JKMPCL.Model;
using UIKit;

namespace JKMIOSApp
{
    /// <summary>
    /// Class Name      : EstimateListTableCellSource
    /// Author          : Hiren Patel
    /// Creation Date   : 16 JAN 2018
    /// Purpose         : To provide estimate list datasource to table view.
    /// Revision        : 
    /// </summary>
    public class EstimateListTableCellSource:UITableViewSource
    {
        private List<EstimateModel> estimateModelList { get; set; }
        private EstimateListViewController estimateListViewController { get; set; }

        public EstimateListTableCellSource(List<EstimateModel> estimateModelList,EstimateListViewController estimateListViewController)
        {
            this.estimateModelList = estimateModelList;
            this.estimateListViewController = estimateListViewController;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (estimateListTableCell)tableView.DequeueReusableCell("estimateCell", indexPath);
            EstimateModel alertModel = estimateModelList[indexPath.Row];
            cell.SetEstimateData(alertModel,tableView,estimateListViewController);
            return cell;
        }

        /// <summary>
        /// Method Name     : RowsInSection
        /// Author          : Hiren Patel
        /// Creation Date   : 25 JAN 2018
        /// Purpose         : To get no of estimate list count
        /// </summary>
        /// <returns>The in section.</returns>
        /// <param name="tableview">Tableview.</param>
        /// <param name="section">Section.</param>
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            //need to pass actual estimates list count
            return estimateModelList is null?0:estimateModelList.Count;
        }
    }
}
