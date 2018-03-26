namespace JKMServices.BLL.Interface
{
    public interface IDocumentDetails
    {
        string GetDocumentList(string moveId);
        string GetDocumentPDF(string relativeFilePath);
    }
}
