using JKMWindowsService.AlertJSONGenerator;
using JKMWindowsService.AlertManager;
using JKMWindowsService.AlertManager.Common;
using Moq;
using Xunit;

namespace JKMWindowsService.UnitTest
{
    public class BookYourMoveTest
    {
        readonly IBookYourMove bookYourMove;
        readonly Mock<IGenericMethods> mockGenericMethods;
        readonly Mock<Utility.IAPIHelper> mockApiHelper;
        readonly Mock<IBookYourMoveGenerator> mockBookYourMoveGenerator;

        readonly MockData.MoveData mockMoveData = new MockData.MoveData();
        readonly MockData.PreMoveConfirmationData mockPreMoveConfirmationData = new MockData.PreMoveConfirmationData();

        public BookYourMoveTest()
        {
            //Arrange
            Mock<IResourceManagerFactory> mockResourceFactory = new Mock<IResourceManagerFactory>();
            mockApiHelper = new Mock<Utility.IAPIHelper>();
            mockGenericMethods = new Mock<IGenericMethods>();
            mockBookYourMoveGenerator = new Mock<IBookYourMoveGenerator>();
            Mock<Utility.Log.ILogger> mockLogger = new Mock<Utility.Log.ILogger>();

            bookYourMove = new BookYourMove(mockApiHelper.Object,
                                            mockGenericMethods.Object,
                                            mockBookYourMoveGenerator.Object,
                                            mockResourceFactory.Object,
                                            mockLogger.Object);
        }

        [Fact]
        public void GetMoveDetailsTest()
        {
            mockApiHelper.Setup(x => x.InvokeGetAPI(It.IsAny<string>())).Returns(mockMoveData.InvokeGetRequest());
            mockGenericMethods.Setup(x => x.GetMoveModelList(It.IsAny<string>())).Returns(mockPreMoveConfirmationData.GetMoveModelList());
            mockBookYourMoveGenerator.Setup(x => x.GenerateJSON(It.IsAny<string>(), It.IsAny<string>())).Returns(mockPreMoveConfirmationData.GetAlertModelJSON());

            bookYourMove.SendAlerts();

            mockApiHelper.VerifyAll();
        }
    }
}
