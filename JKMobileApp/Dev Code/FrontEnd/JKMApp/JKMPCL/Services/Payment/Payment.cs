using JKMPCL.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JKMPCL.Services.Payment
{
    /// <summary>
    /// Class Name      : Payment
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 25 Jan 2018
    /// Purpose         : class to call payment service for post transaction
    /// Revision        :   
    /// </summary>
    public class Payment
    {
        private readonly PaymentAPIService paymentAPIService;
        private readonly PaymentGateway paymentGateway;

        //Constructor
        public Payment()
        {
            paymentAPIService = new PaymentAPIService();
            paymentGateway = new PaymentGateway();
        }

        /// <summary>
        /// Method Name     : PostPaymentTransaction
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 25 Jan 2018
        /// Purpose         : call payment api service to post payment transaction details
        /// Revision        :  
        /// </summary>
        /// <param name="paymentModel"></param>
        /// <returns></returns>
        public async Task<APIResponse<PaymentModel>> PostPaymentTransaction(List<PaymentModel> paymentModel)
        {
            return await paymentAPIService.PostTransactionHistoryAsync(paymentModel);
        }

        /// <summary>
        /// Method Name     : GetDepositAmount
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 21 Feb 2018
        /// Purpose         : get deposit & paid amount 
        /// Revision        :  
        /// </summary>
        /// <param name="paymentModel"></param>
        /// <returns></returns>
        public async Task<APIResponse<PaymentModel>> GetPaymentAmount(PaymentModel paymentModel)
        {
            return await paymentAPIService.GetPaymentAmount(paymentModel);
        }

        /// <summary>
        /// Method Name     : PutPaymentAmount
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 21 Feb 2018
        /// Purpose         : update transaction amount in crm
        /// Revision        :  
        /// </summary>
        /// <param name="paymentModel"></param>
        /// <returns></returns>
        public async Task<APIResponse<PaymentModel>> PutPaymentAmount(PaymentModel paymentModel)
        {
            return await paymentAPIService.PutPaymentAmount(paymentModel);
        }

        /// <summary>
        /// Method Name     : ProcessPaymentTransaction
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 30 Jan 2018
        /// Purpose         : for payment through authorize.net using credit cards
        /// Revision        : 
        /// </summary>
        /// <param name="paymentGatewayModel"></param>
        /// <returns></returns>
        public async Task<APIResponse<PaymentTransactonModel>> ProcessPaymentTransaction(PaymentGatewayModel paymentGatewayModel)
        {
            APIResponse<PaymentTransactonModel> apiResponse = new APIResponse<PaymentTransactonModel> { STATUS = false };
            apiResponse = await paymentGateway.ProcessPaymentGateway(paymentGatewayModel);
            if (apiResponse.STATUS)
            {
                if (apiResponse.DATA.Status == Resource.msgPaymentApproved)
                {
                    apiResponse.STATUS = true;
                    apiResponse.Message = Resource.msgNodusPaymentSuccessfull;
                }
                else
                {
                    apiResponse.Message = apiResponse.DATA.Message;
                    apiResponse.STATUS = false;
                }
            }

            return apiResponse;
        }
    }
}
