namespace JKMPCL.Model
{
    /// <summary>
    /// Class Name      : DocumentModel
    /// Author          :Sanket prajapati
    /// Creation Date   : 07 Feb 2018
    /// Purpose         : 
    /// Revision        : 
    /// </summary>
    public class DocumentModel
    {
        public string DocumentTitle { get; set; }
        public string DocumentType { get; set; }
        public string RelativeUrl { get; set; }
        public string MoveId { get; set; }
    }

    public class GetDocumentPDF
    {
        public string DATA { get; set; }
    }
}
