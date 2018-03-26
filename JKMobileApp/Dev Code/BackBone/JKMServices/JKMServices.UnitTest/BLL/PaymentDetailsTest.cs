using Moq;
using UnitTests.Mock;
using Xunit;

namespace UnitTests.BLL
{
    public class PaymentDetailsTest
    {
        private readonly Mock<JKMServices.DAL.CRM.IPaymentDetails> mockCrmPaymentDetails;

        private readonly PaymentData paymentData = new PaymentData();
        private readonly JKMServices.BLL.PaymentDetails paymentDetails;
        private readonly Mock<JKMServices.BLL.Interface.IPaymentDetails> mockBllPaymentDetails;

        public PaymentDetailsTest()
        {
            mockCrmPaymentDetails = new Mock<JKMServices.DAL.CRM.IPaymentDetails>();
            Mock<JKMServices.BLL.Interface.IResourceManagerFactory> mockBllJKMResource = new Mock<JKMServices.BLL.Interface.IResourceManagerFactory>();
            Mock<Utility.Logger.ILogger> mockLogger = new Mock<Utility.Logger.ILogger>();
            mockBllPaymentDetails = new Mock<JKMServices.BLL.Interface.IPaymentDetails>();

            paymentDetails = new JKMServices.BLL.PaymentDetails(
                                                                mockCrmPaymentDetails.Object, 
                                                                mockBllJKMResource.Object, 
                                                                mockLogger.Object
                                                                );
        }

        [Fact]
        public void PostTransactionData_Positive()
        {
            //for Moq tests

            //Arrange
            mockBllPaymentDetails.Setup(x => x.PostTransactionHistory(It.IsAny<string>())).Returns(paymentData.MockedRequestBody_PostTransaction());
            mockCrmPaymentDetails.Setup(x => x.PostTransactionHistory(It.IsAny<JKMServices.DTO.Payment>())).Returns(paymentData.PostTransactionData);

            //Act
            paymentDetails.PostTransactionHistory(paymentData.MockedRequestBody_PostTransaction());

            //Assert
            mockCrmPaymentDetails.VerifyAll();
        }

        [Fact]
        public void GetDeviceID_Positive()
        {
            //for Moq tests

            //Arrange
            mockCrmPaymentDetails.Setup(x => x.GetDeviceID()).Returns(paymentData.MockedResponseBodyForCRM_GetDeviceID());

            //Act
            paymentDetails.GetDeviceID();

            //Assert
            mockCrmPaymentDetails.VerifyAll();
        }

        [Theory]
        [InlineData("RC0064215")] //Valid Customer ID
        [InlineData("   ")] // Blank(InValid) customer ID
        [InlineData("RC0064215_InvalidID")] // Invalid customer ID
        public void GetAmount_Positive(string customerID)
        {
            //Arrange
            mockCrmPaymentDetails.Setup(x => x.GetAmount(It.IsAny<string>())).Returns(paymentData.MockedReponseBodyForCRM_GetAmount());

            //Act
            paymentDetails.GetAmount(customerID);

            //Assert
            mockCrmPaymentDetails.VerifyAll();
        }

        [Theory]
        [InlineData("RC0064215")] //Valid Customer ID
        public void GetAmount_Negative(string customerID)
        {
            //Arrange
            mockCrmPaymentDetails.Setup(x => x.GetAmount(It.IsAny<string>())).Returns(paymentData.MockedReponseBodyForCRM_GetAmount_Negative());

            //Act
            paymentDetails.GetAmount(customerID);

            //Assert
            mockCrmPaymentDetails.VerifyAll();
        }

        [Theory]
        [InlineData("{\"MoveID\":\"RM0035656\",\"TotalPaid\":3500.500}")]
        public void PutAmount_Positive(string requestContent)
        {
            //Arrange
            mockCrmPaymentDetails.Setup(x => x.PutAmount(It.IsAny<string>())).Returns(paymentData.MockedReponseBodyForCRM_GetAmount());

            //Act
            paymentDetails.PutAmount(requestContent);

            //Assert
            mockCrmPaymentDetails.VerifyAll();
        }

        [Theory]        
        [InlineData("{\"TotalPaid\":3500.500}")]
        public void PutAmount_Negative(string requestContent)
        {
            //Arrange
            mockCrmPaymentDetails.Setup(x => x.PutAmount(It.IsAny<string>())).Returns(paymentData.MockedReponseBodyForCRM_GetAmount_Negative());

            //Act
            paymentDetails.PutAmount(requestContent);

            //Assert
            mockCrmPaymentDetails.VerifyAll();
        }
    }
}
