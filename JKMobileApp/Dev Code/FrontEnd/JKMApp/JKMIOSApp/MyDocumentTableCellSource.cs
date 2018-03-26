using System;
using System.Collections.Generic;
using Foundation;
using JKMPCL.Model;
using UIKit;

namespace JKMIOSApp
{
    /// <summary>
    /// Class Name      : MyDocumentTableCellSource
    /// Author          : Hiren Patel
    /// Creation Date   : 16 JAN 2018
    /// Purpose         : To provide document list datasource to table view.
    /// Revision        : 
    /// </summary>
    public class MyDocumentTableCellSource: UITableViewSource
    {
        private List<DocumentModel> documentModelList { get; set; }
        private MyDocumentViewController myDocumentViewController { get; set; }

        public MyDocumentTableCellSource(List<DocumentModel> documentModelList, MyDocumentViewController myDocumentViewController)
        {
            this.documentModelList = documentModelList;
            this.myDocumentViewController = myDocumentViewController;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (myDocumentTablecell)tableView.DequeueReusableCell("mydocumentcell", indexPath);
            DocumentModel documentModel = documentModelList[indexPath.Row];
            cell.SetDocumentData(documentModel, tableView, myDocumentViewController);
            return cell;
        }

        /// <summary>
        /// Method Name     : RowsInSection
        /// Author          : Hiren Patel
        /// Creation Date   : 25 JAN 2018
        /// Purpose         : To get no of document list count
        /// </summary>
        /// <returns>The in section.</returns>
        /// <param name="tableview">Tableview.</param>
        /// <param name="section">Section.</param>
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            //need to pass actual estimates list count
            if (documentModelList is null)
            {
                return 0;
            }
            else
            {
              return documentModelList.Count;
            }
        }

    }
}
