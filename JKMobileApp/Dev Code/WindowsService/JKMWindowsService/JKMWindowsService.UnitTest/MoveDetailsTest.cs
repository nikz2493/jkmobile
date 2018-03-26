using JKMWindowsService.MoveManager;
using Moq;
using Xunit;

namespace JKMWindowsService.UnitTest
{
    public class MoveDetailsTest
    {
        readonly MoveDetails moveDetails;
        readonly Mock<Utility.IAPIHelper> mockApiHelper;
        readonly MockData.MoveData mockData = new MockData.MoveData();
        public MoveDetailsTest()
        {
            //Arrange
            Mock<IResourceManagerFactory> mockResourceFactory = new Mock<IResourceManagerFactory>();
            mockApiHelper = new Mock<Utility.IAPIHelper>();
            Mock<Utility.Log.ILogger> mockLogger = new Mock<Utility.Log.ILogger>();

            moveDetails = new MoveDetails(mockApiHelper.Object,
                                              mockResourceFactory.Object,
                                              mockLogger.Object);
        }

        [Fact]
        public void GetMoveDetailsTest()
        {
            mockApiHelper.Setup(x => x.InvokeGetAPI(It.IsAny<string>())).Returns(mockData.InvokeGetRequest());
            moveDetails.GetMoveList("booked");
            mockApiHelper.VerifyAll();
        }
    }
}
