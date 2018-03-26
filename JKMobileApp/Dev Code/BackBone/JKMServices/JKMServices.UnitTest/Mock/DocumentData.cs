using System.Collections.Generic;

namespace UnitTests.Mock
{
    public class DocumentData
    {
        public List<JKMServices.DTO.Document> GetDocument()
        {
            var documentDetails = new List<JKMServices.DTO.Document>();
            documentDetails.Add(new JKMServices.DTO.Document
            {
                DocumentTitle = "privacy agreement document"
            });

            return documentDetails;
        }

        public JKMServices.DTO.ServiceResponse<List<JKMServices.DTO.Document>> GetDocumentList()
        {
            var documentDetails = new List<JKMServices.DTO.Document>();

            documentDetails.Add(new JKMServices.DTO.Document
            {
                DocumentTitle = "Sharepoint Mocked Doc 1"
            });
            documentDetails.Add(new JKMServices.DTO.Document
            {
                DocumentTitle = "Sharepoint Mocked Doc 2"
            });
            documentDetails.Add(new JKMServices.DTO.Document
            {
                DocumentTitle = "Sharepoint Mocked Doc 3"
            });
            return new JKMServices.DTO.ServiceResponse<List<JKMServices.DTO.Document>> { Data = documentDetails };
        }

        public JKMServices.DTO.ServiceResponse<string> GetDocumentPDF()
        {
            string base64String = "JVBERi0xLjMNJeLjz9MNCjcgMCBvYmoNPDwvTGluZWFyaXplZCAxL0wgNzk0NS9PIDkvR";
            JKMServices.DTO.ServiceResponse<string> response = new JKMServices.DTO.ServiceResponse<string> { Data = base64String };
            return response;
        }
    }
}
