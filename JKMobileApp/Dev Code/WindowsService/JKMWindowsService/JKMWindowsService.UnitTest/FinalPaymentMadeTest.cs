using JKMWindowsService.AlertJSONGenerator;
using JKMWindowsService.AlertManager;
using JKMWindowsService.AlertManager.Common;
using Moq;
using Xunit;

namespace JKMWindowsService.UnitTest
{
    public class FinalPaymentMadeTest
    {
        readonly IFinalPaymentMade finalPaymentMade;
        readonly Mock<IGenericMethods> mockGenericMethods;
        readonly Mock<Utility.IAPIHelper> mockApiHelper;
        readonly Mock<IFinalPaymentMadeGenerator> mockFinalPaymentMadeGenerator;

        readonly MockData.MoveData mockMoveData = new MockData.MoveData();
        readonly MockData.FinalPaymentMadeData mockFinalPaymentMadeData = new MockData.FinalPaymentMadeData();

        public FinalPaymentMadeTest()
        {
            //Arrange
            Mock<IResourceManagerFactory> mockResourceFactory = new Mock<IResourceManagerFactory>();
            mockApiHelper = new Mock<Utility.IAPIHelper>();
            mockGenericMethods = new Mock<IGenericMethods>();
            mockFinalPaymentMadeGenerator = new Mock<IFinalPaymentMadeGenerator>();
            Mock<Utility.Log.ILogger> mockLogger = new Mock<Utility.Log.ILogger>();

            finalPaymentMade = new FinalPaymentMade(mockApiHelper.Object,
                                            mockGenericMethods.Object,
                                            mockFinalPaymentMadeGenerator.Object,
                                            mockResourceFactory.Object,
                                            mockLogger.Object);
        }

        [Fact]
        public void GetMoveDetailsTest()
        {
            mockApiHelper.Setup(x => x.InvokeGetAPI(It.IsAny<string>())).Returns(mockMoveData.InvokeGetRequest());
            mockGenericMethods.Setup(x => x.GetMoveModelList(It.IsAny<string>())).Returns(mockFinalPaymentMadeData.GetMoveModelList());
            mockFinalPaymentMadeGenerator.Setup(x => x.GenerateJSON(It.IsAny<string>(), It.IsAny<string>())).Returns(mockFinalPaymentMadeData.GetAlertModelJSON());

            finalPaymentMade.SendAlerts();

            mockApiHelper.VerifyAll();
        }
    }
}
