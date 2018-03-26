using JKMServices.BLL;
using Moq;
using UnitTests.Mock;
using Xunit;

namespace UnitTests
{
    /// <summary>
    /// Class Name      : MoveDetailsTest
    /// Author          : Ranjana Singh
    /// Creation Date   : 27 Dec 2017
    /// Purpose         : Test cases for BLL Move Details methods.
    /// Revision        :
    /// </summary>
    public class MoveDetailsTest
    {
        readonly MoveData moveData = new MoveData();
        readonly Mock<JKMServices.DAL.CRM.IMoveDetails> mockCRMMoveDetails;
        readonly MoveDetails moveDetails;
        readonly Mock<JKMServices.DAL.CRM.ICustomerDetails> mockCRMCustomerDetails;

        public MoveDetailsTest()
        {
            //Arrange
            Mock<JKMServices.BLL.Interface.IResourceManagerFactory> mockBllJKMResource = new Mock<JKMServices.BLL.Interface.IResourceManagerFactory>();
            mockCRMCustomerDetails = new Mock<JKMServices.DAL.CRM.ICustomerDetails>();
            mockCRMMoveDetails = new Mock<JKMServices.DAL.CRM.IMoveDetails>();
            Mock<Utility.Logger.ILogger> mockLogger = new Mock<Utility.Logger.ILogger>();

            moveDetails = new MoveDetails(
                                            mockCRMCustomerDetails.Object, 
                                            mockCRMMoveDetails.Object, 
                                            mockBllJKMResource.Object, 
                                            null, 
                                            mockLogger.Object);
        }       

        [Theory]
        [InlineData("JG1020")] //Testing with existing customerID
        public void GetMoveIdTest(string customerID)
        {
            //Arrange
            mockCRMMoveDetails.Setup(x => x.GetMoveId(It.IsAny<string>())).Returns(moveData.GetMoveIdDataPositive());
            mockCRMCustomerDetails.Setup(x => x.CheckCustomerRegistered(It.IsAny<string>())).Returns(true);

            //Act
            moveDetails.GetMoveID(customerID);

            //Assert
            mockCRMMoveDetails.VerifyAll();
        }
      
        /// <summary>
        /// Method Name     : PutMoveData_Positive
        /// Author          : Pratik Soni
        /// Creation Date   : 16 Jan 2018
        /// Purpose         : Test case for BLL -> MoveDetails
        /// Revision        :
        /// </summary>
        [Theory]
        [InlineData("RM0035661")]
        public void PutMoveData_Positive(string moveId)
        {
            //Moq DAL success response
            mockCRMMoveDetails.Setup(x => x.PutMoveData(It.IsAny<string>(), It.IsAny<string>())).Returns(moveData.GetMockMoveData_NoContentTest);

            //Act
            moveDetails.PutMoveData(moveId, moveData.MockedRequestBody_positive());

            //Assert
            mockCRMMoveDetails.VerifyAll();
        }

        /// <summary>
        /// Method Name     : PutMoveData_Negative
        /// Author          : Pratik Soni
        /// Creation Date   : 16 Jan 2018
        /// Purpose         : Test case for BLL -> MoveDetails
        /// Revision        :
        /// </summary>
        [Theory]
        [InlineData("RM0035661")]
        public void PutMoveData_Negative(string moveId)
        {
            //Moq DAL success response "1"
            mockCRMMoveDetails.Setup(x => x.PutMoveData(It.IsAny<string>(), It.IsAny<string>())).Returns(moveData.GetMockMoveData_NoContentTest);

            //Act
            moveDetails.PutMoveData(moveId, "");

            //Assert
            mockCRMMoveDetails.VerifyAll();
        }
    }
}