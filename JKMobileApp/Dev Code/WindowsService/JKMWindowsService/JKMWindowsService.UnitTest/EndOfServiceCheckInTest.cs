using JKMWindowsService.AlertJSONGenerator;
using JKMWindowsService.AlertManager;
using JKMWindowsService.AlertManager.Common;
using Moq;
using Xunit;

namespace JKMWindowsService.UnitTest
{
    public class EndOfServiceCheckInTest
    {
        readonly IEndOfServiceCheckIn endOfServiceCheckIn;
        readonly Mock<IGenericMethods> mockGenericMethods;
        readonly Mock<Utility.IAPIHelper> mockApiHelper;
        readonly Mock<IEndOfServiceCheckInGenerator> mockEndOfServiceCheckInGenerator;

        readonly MockData.MoveData mockMoveData = new MockData.MoveData();
        readonly MockData.EndOfServiceCheckInData mockEndOfServiceCheckInData = new MockData.EndOfServiceCheckInData();

        public EndOfServiceCheckInTest()
        {
            //Arrange
            Mock<IResourceManagerFactory> mockResourceFactory = new Mock<IResourceManagerFactory>();
            mockApiHelper = new Mock<Utility.IAPIHelper>();
            mockGenericMethods = new Mock<IGenericMethods>();
            mockEndOfServiceCheckInGenerator = new Mock<IEndOfServiceCheckInGenerator>();
            Mock<Utility.Log.ILogger> mockLogger = new Mock<Utility.Log.ILogger>();

            endOfServiceCheckIn = new EndOfServiceCheckIn(mockApiHelper.Object,
                                                                                mockGenericMethods.Object,
                                                                                mockEndOfServiceCheckInGenerator.Object,
                                                                                mockResourceFactory.Object,
                                                                                mockLogger.Object);
        }

        [Fact]
        public void GetMoveDetailsTest()
        {
            mockApiHelper.Setup(x => x.InvokeGetAPI(It.IsAny<string>())).Returns(mockMoveData.InvokeGetRequest());
            mockGenericMethods.Setup(x => x.GetMoveModelList(It.IsAny<string>())).Returns(mockEndOfServiceCheckInData.GetMoveModelList());
            mockEndOfServiceCheckInGenerator.Setup(x => x.GenerateJSON(It.IsAny<string>(), It.IsAny<string>())).Returns(mockEndOfServiceCheckInData.GetAlertModelJSON());

            endOfServiceCheckIn.SendAlerts();

            mockApiHelper.VerifyAll();
        }
    }
}