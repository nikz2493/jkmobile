using Moq;
using System.Collections.Generic;
using UnitTests.Mock;
using Utility;
using Xunit;

namespace UnitTests
{
    /// <summary>
    /// Class Name      : AlertDetailsTest
    /// Author          : Pratik Soni
    /// Creation Date   : 02 Jan 2018
    /// Purpose         : Unit Test for Alert Details
    /// Revision        : 
    /// </summary>
    public class AlertDetailsTest
    {
        private readonly Mock<JKMServices.DAL.CRM.IAlertDetails> mockDALCRMAlertDetails;
        private readonly Mock<JKMServices.BLL.Interface.IAlertDetails> mockBLLCRMAlertDetails;
        private readonly Mock<JKMServices.DAL.CRM.ICustomerDetails> mockCRMCustomerDetails;
        private readonly JKMServices.BLL.AlertDetails bllAlertDetails;
        private readonly AlertData alertData;

        public AlertDetailsTest()
        {
            //for Moq test
            //Arrange
            mockDALCRMAlertDetails = new Mock<JKMServices.DAL.CRM.IAlertDetails>();
            mockBLLCRMAlertDetails = new Mock<JKMServices.BLL.Interface.IAlertDetails>();
            mockCRMCustomerDetails = new Mock<JKMServices.DAL.CRM.ICustomerDetails>();
            Mock<JKMServices.BLL.Interface.IResourceManagerFactory> mockBllJKMResource = new Mock<JKMServices.BLL.Interface.IResourceManagerFactory>();
            Mock<Utility.Logger.ILogger> mockLogger = new Mock<Utility.Logger.ILogger>();
            alertData = new AlertData();
            bllAlertDetails = new JKMServices.BLL.AlertDetails(mockDALCRMAlertDetails.Object, mockCRMCustomerDetails.Object, mockBllJKMResource.Object, mockLogger.Object);
        }

        /// <summary>
        /// Method Name     : GetAlertListTest
        /// Author          : Ranjana Singh
        /// Creation Date   : 26 Feb 2018
        /// Purpose         : Check for positive flow of Get Alert List.
        /// Revision        : 
        /// </summary>
        /// <param name="customerID"></param>
        [Theory]
        [InlineData("RC0064218")]
        public void GetAlertListTest(string customerID)
        {
            mockBLLCRMAlertDetails.Setup(x => x.GetAlertList(It.IsAny<string>(), It.IsAny<string>())).Returns(alertData.GetBLLAlertListData);
            mockCRMCustomerDetails.Setup(y => y.CheckCustomerRegistered(It.IsAny<string>())).Returns(alertData.CheckCustomerRegistered);
            mockDALCRMAlertDetails.Setup(z => z.GetAlertList(It.IsAny<string>(), It.IsAny<string>())).Returns(alertData.GetMockAlertDataList_PositiveTest);

            //Act
            bllAlertDetails.GetAlertList(customerID);

            //Assert
            mockDALCRMAlertDetails.VerifyAll();
        }

        /// <summary>
        /// Method Name     : PostAlertList_Positive
        /// Author          : Ranjana Singh
        /// Creation Date   : 26 Feb 2018
        /// Purpose         : Check for positive flow
        /// Revision        : 
        /// </summary>
        /// <param name="customerID"></param>
        [Theory]
        [InlineData("RC0064218")]
        public void PostAlertList_Positive(string customerID)
        {
            //Moq DAL success response "1"
            mockBLLCRMAlertDetails.Setup(x => x.PostAlertList(It.IsAny<string>(), It.IsAny<string>())).Returns(alertData.PostAlertData);

            //Act
            bllAlertDetails.PostAlertList(customerID, Utility.General.ConvertToJson<List<JKMServices.DTO.Alert>>(alertData.RequestBodyForPost()));

            //Assert
            mockDALCRMAlertDetails.VerifyAll();
        }

        /// <summary>
        /// Method Name     : PostAlertList_Negative
        /// Author          : Ranjana Singh
        /// Creation Date   : 26 Feb 2018
        /// Purpose         : Check for negative flow
        /// Revision        : 
        /// </summary>
        /// <param name="customerID"></param>
        [Theory]
        [InlineData("")]
        public void PostAlertList_Negative(string customerID)
        {
            //Act
            var mocktest = bllAlertDetails.PostAlertList(customerID, Utility.General.ConvertToJson<List<JKMServices.DTO.Alert>>(alertData.RequestBodyForPost()));

            //Assert
            Assert.Equal(General.ConvertToJson<List<JKMServices.DTO.Alert>>(alertData.GetAlertListFailData()), mocktest);
        }

        /// <summary>
        /// Method Name     : PutAlertList_Positive
        /// Author          : Ranjana Singh
        /// Creation Date   : 26 Feb 2018
        /// Purpose         : Check for positive flow
        /// Revision        : 
        /// </summary>
        /// <param name="alertID"></param>
        [Theory]
        [InlineData("3c737180-8318-e811-80ef-bef806786223")]
        public void PutAlertList_Positive(string alertID)
        {
            mockBLLCRMAlertDetails.Setup(x => x.PutAlertList(It.IsAny<string>(), It.IsAny<string>())).Returns(alertData.PostAlertData);

            //Act
            bllAlertDetails.PostAlertList(alertID,alertData.RequestBodyForPut());

            //Assert
            mockDALCRMAlertDetails.VerifyAll();
        }       
    }
}