using JKMServices.BLL;
using Moq;
using System.Collections.Generic;
using UnitTests.Mock;
using Xunit;

namespace UnitTests
{
    /// <summary>
    /// Class Name      : EstimateDetailsTest
    /// Author          : Ranjana Singh
    /// Creation Date   : 11 Jan 2018
    /// Purpose         : Test cases for BLL Estimate Details methods.
    /// Revision        :
    /// </summary>
    public class EstimateDetailsTest
    {
        private readonly EstimateDetails estimateDetails;
        private readonly EstimateData estimateData;
        private readonly Mock<JKMServices.DAL.CRM.IEstimateDetails> mockCRMEstimateDetails;
        private readonly Mock<JKMServices.DAL.CRM.ICustomerDetails> mockCRMCustomerDetails;

        public EstimateDetailsTest()
        {
            //Arrange
            estimateData = new EstimateData();
            Mock<JKMServices.BLL.Interface.IResourceManagerFactory> mockBllJKMResource = new Mock<JKMServices.BLL.Interface.IResourceManagerFactory>();
            mockCRMCustomerDetails = new Mock<JKMServices.DAL.CRM.ICustomerDetails>();
            mockCRMEstimateDetails = new Mock<JKMServices.DAL.CRM.IEstimateDetails>();
            Mock<JKMServices.DAL.CRM.IMoveDetails> mockCRMMoveDetails = new Mock<JKMServices.DAL.CRM.IMoveDetails>();
            Mock<JKMServices.BLL.EmailEngine.IEmailHandler> mockEmailEngine = new Mock<JKMServices.BLL.EmailEngine.IEmailHandler>();
            Mock<Utility.Logger.ILogger> mockLogger = new Mock<Utility.Logger.ILogger>();
            Mock<Utility.ISharepointConsumer> mockSharepointConsumer = new Mock<Utility.ISharepointConsumer>();

            estimateDetails = new EstimateDetails(mockCRMCustomerDetails.Object,
                                                  mockCRMEstimateDetails.Object,
                                                  mockCRMMoveDetails.Object,
                                                  mockEmailEngine.Object,
                                                  mockLogger.Object,
                                                  mockBllJKMResource.Object,
                                                  mockSharepointConsumer.Object);
        }

        /// <summary>
        /// Method Name     : GetMoveData_TestBLL
        /// Author          : Ranjana Singh
        /// Creation Date   : 28 Dec 2017
        /// Purpose         : Test case for BLL -> EstimateDetails
        /// Revision        :
        /// </summary>
        [Theory]
        [InlineData("RC0064219")] //Testing with existing estimate ID
        public void GetMoveData_TestBLL(string customerId)
        {
            mockCRMCustomerDetails.Setup(x => x.CheckCustomerRegistered(It.IsAny<string>())).Returns(true);
            mockCRMEstimateDetails.Setup(x => x.GetEstimateList(It.IsAny<string>())).Returns(estimateData.GetMockEstimateDataList_PositiveTest());
            mockCRMEstimateDetails.Setup(x => x.GetEstimateData(It.IsAny<List<string>>())).Returns(estimateData.GetMockEstimateDataList_PositiveTest());

            //Act
            estimateDetails.GetEstimateData(customerId);

            //Assert
            mockCRMEstimateDetails.VerifyAll();
        }

        [Theory]
        [InlineData(null)]
        public void GetMoveDataNullCheck(string customerID)
        {
            //Act
            estimateDetails.GetEstimateData(customerID);

            //Assert
            mockCRMEstimateDetails.VerifyAll();
        }           
    }
}