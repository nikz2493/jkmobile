using JKMPCL.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JKMPCL.Services.Payment
{
    /// <summary>
    /// Class Name      : PaymentGateway
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 25 Jan 2018
    /// Purpose         : class to handle payment gateway related methods
    /// Revision        :  
    /// </summary>
    public class PaymentGateway
    {
       private readonly APIHelper apiHelper;

        //Contructor
        public PaymentGateway()
        {
            apiHelper = new APIHelper();
        }

        /// <summary>
        /// Class Name      : ProcessPaymentGateway
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 25 Jan 2018
        /// Purpose         : redirect to payment gateway to process payment using credit card details
        /// Revision        : 
        /// </summary>
        /// <param name="paymentGatewayModel"></param>
        /// <returns></returns>
        public async Task<APIResponse<PaymentTransactonModel>> ProcessPaymentGateway(PaymentGatewayModel paymentGatewayModel)
        {
            APIResponse<PaymentTransactonModel> apiResponse = new APIResponse<PaymentTransactonModel> { STATUS = false };
            apiResponse = await ProcessTransaction(GetCardDetails(paymentGatewayModel));

            return apiResponse;
        }

        /// <summary>
        /// Method Name     : GetCardDetails
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 30 Jan 2018
        /// Purpose         : generate json string containing card details for payment
        /// Revision        : 
        /// </summary>
        /// <param name="paymentGateway"></param>
        /// <returns></returns>
        private string GetCardDetails(PaymentGatewayModel paymentGateway)
        {
            try
            {
                StringBuilder cardDetails = new StringBuilder();
                cardDetails.Append("{");
                cardDetails.Append("\"Customer\":\"" + paymentGateway.CustomerID + "\",");
                cardDetails.Append("\"Currency\":\"" + Resource.PaymentCurrency + "\",");
                cardDetails.Append("\"Amount\":\"" + paymentGateway.TransactionAmout + "\",");
                cardDetails.Append("\"Type\":\"" + Resource.PaymentTransactionType + "\",");
                cardDetails.Append("\"SetupId\":\"" + Resource.PaymentGatewaySetupID + "\",");

                //Card Details
                cardDetails.Append("\"Card\":{");
                cardDetails.Append("\"Account\":\"" + paymentGateway.CreditCardNumber + "\",");
                cardDetails.Append("\"Cvc\":\"" + paymentGateway.CVVNo + "\",");
                cardDetails.Append("\"ExpDate\":\"" + paymentGateway.CardExpiryDate + "\",");

                // Card Holder
                cardDetails.Append("\"CardHolder\":{");
                cardDetails.Append("\"FirstName\":\"" + paymentGateway.FirstName + "\",");
                cardDetails.Append("\"LastName\":\"" + paymentGateway.LastName + "\",");
                cardDetails.Append("\"Email\":\"" + paymentGateway.EmailID + "\"");
                cardDetails.Append("}}");

                return cardDetails.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
           
        }

        /// <summary>
        /// Method Name     : ProcessTransaction
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 30 Jan 2018
        /// Purpose         : call process payment transaction using credit card info
        /// Revision        : 
        /// </summary>
        /// <param name="cardDetails"></param>
        /// <returns></returns>
        private async Task<APIResponse<PaymentTransactonModel>> ProcessTransaction(string cardDetails)
        {
            APIResponse<PaymentTransactonModel> apiResponse = new APIResponse<PaymentTransactonModel> { STATUS = false };

            try
            {
                string token = await GetTokenForTransaction();
                using (HttpWebResponse httpWebResponse = await apiHelper.ProcessPaymentTransaction(token, cardDetails))
                {
                    SetResponseStatus(httpWebResponse, apiResponse);
                    if (apiResponse.STATUS)
                    {
                        SetTransactionResponse(httpWebResponse, apiResponse);
                    }
                }
                return apiResponse;
            }
            catch (Exception ex)
            {
                apiResponse.Message = ex.Message;
                return apiResponse;
            }

        }

        /// <summary>
        /// Method Name     : GetTokenForTransaction
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 30 Jan 2018
        /// Purpose         : get token for transaction
        /// Revision        :  
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetTokenForTransaction()
        {
            string token = string.Empty;
            APIResponse<TokenResponseModel> apiTokenResponse = new APIResponse<TokenResponseModel> { STATUS = false };
            apiTokenResponse = await new TokenGenerator().CreateToken();

            if (apiTokenResponse.STATUS)
            {
                token = apiTokenResponse.DATA.Token;
            }

            return token;
        }

        /// <summary>
        /// Method Name     : SetTransactionResponse
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 30 Jan 2018
        /// Purpose         : read & set response base on input http web response 
        /// Revision        :   
        /// </summary>
        /// <param name="httpWebResponse"></param>
        /// <param name="apiResponse"></param>
        private void SetTransactionResponse(HttpWebResponse httpWebResponse, APIResponse<PaymentTransactonModel> apiResponse)
        {
            string streamResult = string.Empty;
            using (Stream responseStream = httpWebResponse.GetResponseStream())
            {
                streamResult = (new StreamReader(responseStream)).ReadToEnd();
                if (!string.IsNullOrEmpty(streamResult))
                {
                    apiResponse.DATA = JsonConvert.DeserializeObject<PaymentTransactonModel>(streamResult);
                }
            }
        }

        /// <summary>
        /// Method Name     : SetResponseStatus
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 30 Jan 2018
        /// Purpose         : set response status & message based on input httpwebresponse
        /// Revision        : 
        /// </summary>
        /// <param name="httpWebResponse"></param>
        /// <param name="apiResponse"></param>
        private void SetResponseStatus(HttpWebResponse httpWebResponse, APIResponse<PaymentTransactonModel> apiResponse)
        {
            if (httpWebResponse.StatusCode == HttpStatusCode.OK)
            {
                apiResponse.STATUS = true;
            }
            else
            {
                if (httpWebResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    apiResponse.Message = Resource.msgBadRequest;
                }
                else
                {
                    apiResponse.Message = Resource.msgPaymentRequestFail;
                }
            }
        }
    }
}
