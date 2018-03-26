using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKMPCL.Model
{
    /// <summary>
    /// Class Name      : PaymentGatewayModel
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 25 Jan 2018
    /// Purpose         : DTO class for transfering payment type details to payment gateway(authorized .net)
    /// Revision        : 
    /// </summary>
    public class PaymentGatewayModel
    {
        public string CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public int CVVNo { get; set; }
        public string CreditCardNumber { get; set; }
        public string CardExpiryDate { get; set; }//monthyear e.g. 0118 (Jan 2018)
        public double TransactionAmout { get; set; }   
    }
}
