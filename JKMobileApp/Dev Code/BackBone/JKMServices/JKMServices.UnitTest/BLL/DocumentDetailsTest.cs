using Moq;
using UnitTests.Mock;
using Utility;
using Xunit;

namespace UnitTests
{
    public class DocumentDetailsTest
    {
        private readonly Mock<JKMServices.DAL.CRM.IDocumentDetails> mockCRMDocumentDetails;
        private readonly DocumentData documentData;
        private readonly JKMServices.BLL.DocumentDetails documentDetails;
        private readonly Mock<JKMServices.DAL.SQL.JKMDBContext> mockJKMDBContext;

        public DocumentDetailsTest()
        {
            mockCRMDocumentDetails = new Mock<JKMServices.DAL.CRM.IDocumentDetails>();
            Mock<JKMServices.DAL.IResourceManagerFactory> mockJKMResource = new Mock<JKMServices.DAL.IResourceManagerFactory>();
            mockJKMDBContext = new Mock<JKMServices.DAL.SQL.JKMDBContext>(mockJKMResource.Object);
            Mock<JKMServices.BLL.Interface.IResourceManagerFactory> mockBllJKMResource = new Mock<JKMServices.BLL.Interface.IResourceManagerFactory>();
            Mock<Utility.Logger.ILogger> mockLogger = new Mock<Utility.Logger.ILogger>();
            Mock<ISharepointConsumer> mockSharepointConsumer = new Mock<ISharepointConsumer>();

            documentData = new DocumentData();
            documentDetails = new JKMServices.BLL.DocumentDetails(mockCRMDocumentDetails.Object,
                                                                  mockSharepointConsumer.Object,
                                                                  mockBllJKMResource.Object,
                                                                  mockLogger.Object);
        }
        [Theory]
        [InlineData("MOV12")]
        public void GetCustomerDocumentListTest(string moveId)
        {
            //for Moq test
            //Arrange
            mockCRMDocumentDetails.Setup(x => x.GetDocumentList(It.IsAny<string>())).Returns(documentData.GetDocumentList());
            //Act
            documentDetails.GetDocumentList(moveId);

            //Assert
            mockCRMDocumentDetails.VerifyAll();
        }
        
        [Theory]
        [InlineData("RelativeURL|DocumentTitle")]
        [InlineData("RelativeURL")]
        public void GetDocumentPDFTest(string relativeFilePath)
        {
            mockJKMDBContext.Setup(x => x.GetData<JKMServices.DTO.Document>(It.IsAny<string>(), It.IsAny<object[]>())).Returns(documentData.GetDocument());

            //Act
            documentDetails.GetDocumentPDF(relativeFilePath);

            //Assert
            mockCRMDocumentDetails.VerifyAll();
        }
    }
}
