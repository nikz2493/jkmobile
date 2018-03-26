using JKMWindowsService.AlertJSONGenerator;
using JKMWindowsService.AlertManager;
using JKMWindowsService.AlertManager.Common;
using Moq;
using Xunit;

namespace JKMWindowsService.UnitTest
{
    /// <summary>
    /// Class Name      : PreMoveConfirmationNotificationsTest
    /// Author          : Pratik Soni
    /// Creation Date   : 27 Feb 2018
    /// Purpose         : Unit Test for Alert Details
    /// Revision        : 
    /// </summary>
    public class PreMoveConfirmationNotificationsTest
    {
        readonly IPreMoveConfirmationNotifications preMoveConfirmationNotifications;
        readonly Mock<IGenericMethods> mockGenericMethods;
        readonly Mock<Utility.IAPIHelper> mockApiHelper;
        readonly Mock<IPreMoveConfirmationNotificationGenerator> mockPreMoveNotificationGenerator;

        readonly MockData.MoveData mockMoveData = new MockData.MoveData();
        readonly MockData.PreMoveConfirmationData mockPreMoveConfirmationData = new MockData.PreMoveConfirmationData();

        public PreMoveConfirmationNotificationsTest()
        {
            //Arrange
            Mock<IResourceManagerFactory> mockResourceFactory = new Mock<IResourceManagerFactory>();
            mockApiHelper = new Mock<Utility.IAPIHelper>();
            mockGenericMethods = new Mock<IGenericMethods>();
            mockPreMoveNotificationGenerator = new Mock<IPreMoveConfirmationNotificationGenerator>();
            Mock<Utility.Log.ILogger> mockLogger = new Mock<Utility.Log.ILogger>();

            preMoveConfirmationNotifications = new PreMoveConfirmationNotifications(mockApiHelper.Object,
                                                                                    mockGenericMethods.Object,
                                                                                    mockPreMoveNotificationGenerator.Object,
                                                                                    mockResourceFactory.Object,
                                                                                    mockLogger.Object);
        }

        [Fact]
        public void GetMoveDetailsTest()
        {
            mockApiHelper.Setup(x => x.InvokeGetAPI(It.IsAny<string>())).Returns(mockMoveData.InvokeGetRequest());
            mockGenericMethods.Setup(x => x.GetMoveModelList(It.IsAny<string>())).Returns(mockPreMoveConfirmationData.GetMoveModelList());
            mockPreMoveNotificationGenerator.Setup(x => x.GenerateJSON(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(mockPreMoveConfirmationData.GetAlertModelJSON());

            preMoveConfirmationNotifications.SendAlerts();

            mockApiHelper.VerifyAll();
        }
    }
}