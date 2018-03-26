using JKMWindowsService.AlertJSONGenerator;
using JKMWindowsService.AlertManager;
using JKMWindowsService.AlertManager.Common;
using Moq;
using Xunit;

namespace JKMWindowsService.UnitTest
{
    public class BeginningOfDayOfServiceTest
    {
        readonly IBeginningOfDayOfServiceCheckIn beginningOfDayOfServiceCheckIn;
        readonly Mock<IGenericMethods> mockGenericMethods;
        readonly Mock<Utility.IAPIHelper> mockApiHelper;
        readonly Mock<IBeginningOfDayOfServiceGenerator> mockBeginningofDayOfServiceGenerator;

        readonly MockData.MoveData mockMoveData = new MockData.MoveData();
        readonly MockData.BeginingOfDayOfServiceData mockBeginingOfDayOfServiceData = new MockData.BeginingOfDayOfServiceData();

        public BeginningOfDayOfServiceTest()
        {
            //Arrange
            Mock<IResourceManagerFactory> mockResourceFactory = new Mock<IResourceManagerFactory>();
            mockApiHelper = new Mock<Utility.IAPIHelper>();
            mockGenericMethods = new Mock<IGenericMethods>();
            mockBeginningofDayOfServiceGenerator = new Mock<IBeginningOfDayOfServiceGenerator>();
            Mock<Utility.Log.ILogger> mockLogger = new Mock<Utility.Log.ILogger>();

            beginningOfDayOfServiceCheckIn = new BeginningOfDayOfServiceCheckIn(mockApiHelper.Object,
                                                                                mockGenericMethods.Object,
                                                                                mockBeginningofDayOfServiceGenerator.Object,
                                                                                mockResourceFactory.Object,
                                                                                mockLogger.Object);
        }

        [Fact]
        public void GetMoveDetailsTest()
        {
            mockApiHelper.Setup(x => x.InvokeGetAPI(It.IsAny<string>())).Returns(mockMoveData.InvokeGetRequest());
            mockGenericMethods.Setup(x => x.GetMoveModelList(It.IsAny<string>())).Returns(mockBeginingOfDayOfServiceData.GetMoveModelList());
            mockBeginningofDayOfServiceGenerator.Setup(x => x.GenerateJSON(It.IsAny<string>(), It.IsAny<string>())).Returns(mockBeginingOfDayOfServiceData.GetAlertModelJSON());

            beginningOfDayOfServiceCheckIn.SendAlerts();

            mockApiHelper.VerifyAll();
        }
    }
}