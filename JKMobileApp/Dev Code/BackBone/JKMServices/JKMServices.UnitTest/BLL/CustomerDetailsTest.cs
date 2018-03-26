using JKMServices.BLL;
using Moq;
using UnitTests.Mock;
using Xunit;

namespace UnitTests
{
    public class CustomerDetailsTest
    {
        private readonly CustomerData customerData;
        private readonly Mock<JKMServices.DAL.CRM.ICustomerDetails> mockCRMCustomerDetails;
        private readonly Mock<JKMServices.BLL.Interface.ICustomerDetails> mockBllCustomerDetails;
        private readonly CustomerDetails customerDetails;

        public CustomerDetailsTest()
        {
            customerData = new CustomerData();
            mockCRMCustomerDetails = new Mock<JKMServices.DAL.CRM.ICustomerDetails>();
            Mock<Utility.IGenerator> mockGenerator = new Mock<Utility.IGenerator>();
            Mock<JKMServices.BLL.Interface.IResourceManagerFactory> mockBllJKMResource = new Mock<JKMServices.BLL.Interface.IResourceManagerFactory>();
            Mock<Utility.Logger.ILogger> mockLogger = new Mock<Utility.Logger.ILogger>();
            mockBllCustomerDetails = new Mock<JKMServices.BLL.Interface.ICustomerDetails>();
            customerDetails = new CustomerDetails(mockCRMCustomerDetails.Object, mockGenerator.Object, mockBllJKMResource.Object, mockLogger.Object);
        }

        [Theory]
        [InlineData("kirrey@mailinator.com")]
        public void GetCustomerIdTest(string emailId)
        {
            //Arrange
            mockBllCustomerDetails.Setup(x => x.GetCustomerID(It.IsAny<string>())).Returns(emailId);
            mockCRMCustomerDetails.Setup(x => x.GetCustomerIDAsync(It.IsAny<string>())).Returns(customerData.GetCustomerDataPositive());
            //Act
            customerDetails.GetCustomerID(emailId);

            //Assert
            mockCRMCustomerDetails.VerifyAll();
        }

        [Theory]
        [InlineData("RC0064215")]
        public void GetCustomerVerificationTest(string customerId)
        {
            //for Moq test

            //Arrange
            mockBllCustomerDetails.Setup(x => x.GetCustomerVerificationData(It.IsAny<string>())).Returns(customerData.GetBLLVerificationData());
            mockCRMCustomerDetails.Setup(x => x.GetCustomerVerificationData(It.IsAny<string>())).Returns(customerData.GetCRMVerificationData());

            //Act
            customerDetails.GetCustomerVerificationData(customerId);

            //Assert
            mockCRMCustomerDetails.VerifyAll();
        }

        [Theory]
        [InlineData("RC0064215")]
        public void GetCustomerProfileDataTest(string customerId)
        {
            //Arrange
            mockBllCustomerDetails.Setup(x => x.GetCustomerProfileData(It.IsAny<string>())).Returns(customerData.GetBLLProfileData());
            mockCRMCustomerDetails.Setup(x => x.GetCustomerProfileData(It.IsAny<string>())).Returns(customerData.GetCRMProfileData());

            //Act
            customerDetails.GetCustomerProfileData(customerId);

            //Assert
            mockCRMCustomerDetails.VerifyAll();
        }

        [Theory]
        [InlineData("RJ1892")]
        public void PutCustomerProfileDataTest(string customerId)
        {
            //Arrange
            mockBllCustomerDetails.Setup(x => x.PutCustomerProfileData(It.IsAny<string>(), It.IsAny<string>())).Returns(customerData.PutBLLCustomerProfileData());
            mockCRMCustomerDetails.Setup(x => x.PutCustomerProfileData(It.IsAny<string>(), It.IsAny<string>())).Returns(customerData.PutCRMProfileData());

            //Act
            customerDetails.PutCustomerProfileData(customerId, customerData.MockedRequestBody_Profile());

            //Assert
            mockCRMCustomerDetails.VerifyAll();
        }

        [Theory]
        [InlineData("RC0064215")]
        public void PutCustomerMyAccountTest(string customerId)
        {
            //Arrange
            mockBllCustomerDetails.Setup(x => x.PutCustomerProfileData(It.IsAny<string>(), It.IsAny<string>())).Returns(customerData.PutBLLCustomerMyAccountData());
            mockCRMCustomerDetails.Setup(y => y.PutCustomerProfileData(It.IsAny<string>(), It.IsAny<string>())).Returns(customerData.PutCRMProfileData());

            //Act
            customerDetails.PutCustomerProfileData(customerId, customerData.MockedRequestBody_MyAccount());

            //Assert
            mockCRMCustomerDetails.VerifyAll();
        }

        [Theory]
        [InlineData("RC0064215")]
        public void PutCustomerVerificationData(string customerId)
        {
            //Arrange
            mockBllCustomerDetails.Setup(x => x.PutCustomerVerificationData(It.IsAny<string>(), It.IsAny<string>())).Returns(customerData.PutBLLVerificationData());
            mockCRMCustomerDetails.Setup(y => y.PutCustomerVerificationData(It.IsAny<string>(), It.IsAny<string>())).Returns(customerData.PutCRMProfileData());

            //Act
            customerDetails.PutCustomerVerificationData(customerId, customerData.MockedRequestBody_Verification());

            //Assert
            mockCRMCustomerDetails.VerifyAll();
        }
    }
}
