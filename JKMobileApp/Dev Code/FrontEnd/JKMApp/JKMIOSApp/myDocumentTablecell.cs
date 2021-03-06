// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using JKMPCL.Model;
using JKMPCL.Services;
using UIKit;

namespace JKMIOSApp
{
    /// <summary>
    /// Controller Name : myDocumentTablecell
    /// Author          : Hiren Patel
    /// Creation Date   : 16 JAN 2018
    /// Purpose         : To display document list
    /// Revision        : 
    /// </summary>
	public partial class myDocumentTablecell : UITableViewCell
	{
        public DocumentModel documentModel { get; set; }
        public UITableView tableView { get; set; }
        private MyDocumentViewController myDocumentViewController { get; set; }

		public myDocumentTablecell (IntPtr handle) : base (handle)
		{
		}

        public override void AwakeFromNib()
        {
            scrollViewListItem.Layer.CornerRadius = 10;
            UIHelper.SetDefaultWizardScrollViewBorderProperty(scrollViewListItemContainer);
            lblDocumentId.Hidden = true;
        }

        /// <summary>
        /// Method Name     : SetDocumentData
        /// Author          : Hiren Patel
        /// Creation Date   : 30 Dec 2017
        /// Purpose         : Set document data
        /// Revision        : 
        /// </summary>
        public void SetDocumentData(DocumentModel documentModel, UITableView tableView, MyDocumentViewController myDocumentViewController)
        {
            this.myDocumentViewController = myDocumentViewController;
            this.documentModel = documentModel;
            SetDocumentTypeImage(documentModel.DocumentType);
            lblDocumentId.Text = documentModel.MoveId;
            lblDocumentTitle.Text = documentModel.DocumentTitle;
            SetImgViewDocumentTap();
            this.tableView = tableView;
        }

        /// <summary>
        /// Method Name     : SetDocumentTypeImage
        /// Author          : Hiren Patel
        /// Creation Date   : 30 Dec 2017
        /// Purpose         : Sets the document type image and text color and background color
        /// Revision        : 
        /// </summary>
        /// <param name="documentType">Alert type.</param>
        public void SetDocumentTypeImage(string documentType)
        {
            string imageURL = AppConstant.MYDOCUMENT_VALUATION_IMAGE_URL;
            UIColor uiColorCode = UIColor.FromRGB(234, 65, 87);
            switch (documentType)
            {
                case "0": // valuation
                    uiColorCode = UIColor.FromRGB(226, 194, 131);
                    imageURL = AppConstant.MYDOCUMENT_VALUATION_IMAGE_URL;

                    break;
                case "1": // Rights & responsibilities
                    imageURL = AppConstant.MYDOCUMENT_RIGHT_RESPONSIBILITIES_IMAGE_URL;
                    uiColorCode = UIColor.FromRGB(131, 222, 226);

                    break;
                case "2":// Estimate
                    uiColorCode = UIColor.FromRGB(134, 172, 219); 
                    imageURL = AppConstant.MYDOCUMENT_VALUATION_IMAGE_URL;
                    break;
            }
            viewDocumentImageTypeContainer.Layer.BackgroundColor = uiColorCode.CGColor;
            imgDocumentType.Image = UIImage.FromFile(imageURL);

        }

        /// <summary>
        /// Method Name     : SetIimgViewDocumentTap
        /// Author          : Hiren Patel
        /// Creation Date   : 31 Jan 2017
        /// Purpose         : Sets the tap event to load document.
        /// Revision        : 
        /// </summary>
        private void SetImgViewDocumentTap()
        {
            imgVIewDocument.UserInteractionEnabled = true;
            UITapGestureRecognizer imgVIewDocumentTap = new UITapGestureRecognizer(() =>
            {
                if (myDocumentViewController != null)
                {
                    UtilityPCL.selectedDocumentModel = documentModel;
                    myDocumentViewController.PerformSegue("myDocumentToViewDocument",this);
                }
            });

            imgVIewDocument.AddGestureRecognizer(imgVIewDocumentTap);

        }
	}
}
