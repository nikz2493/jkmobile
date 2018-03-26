using System.Net;
using System.Threading.Tasks;
using Xunit;
namespace JKMUnitTests
{
    public class LoginTests
    {
        [Fact]
        public async Task TestpaymentAsync()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            JKMPCL.Services.Payment.TokenGenerator tokenGenerator = new JKMPCL.Services.Payment.TokenGenerator();
             await tokenGenerator.CreateToken();
        }
       [Fact]
        public async Task TestPaymentTransaction()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            JKMPCL.Services.Payment.Payment paymentGateway = new JKMPCL.Services.Payment.Payment();
            JKMPCL.Model.PaymentGatewayModel paymentModel = new JKMPCL.Model.PaymentGatewayModel();
            paymentModel.CardExpiryDate = "0619";
            paymentModel.CreditCardNumber = "4111111111111111";
            paymentModel.CustomerID = "AARONFIT0001";
            paymentModel.FirstName = "Vivek";
            paymentModel.LastName = "Bhavsar";
            paymentModel.EmailID = "vivek.bhavsar@1rivet.com";
            paymentModel.CVVNo = 745;
            paymentModel.TransactionAmout = 15;
             await paymentGateway.ProcessPaymentTransaction(paymentModel);
        }
    }
}
