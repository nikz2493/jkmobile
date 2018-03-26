namespace JKMPCL.Model
{
    /// <summary>
    /// Class Name      : PaymentTransactonModel
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 30 Jan 2018
    /// Purpose         : Model for payment transaction response while processing transaction
    /// Revision        :  
    /// </summary>
    public class PaymentTransactonModel
    {
        public string Message { get; set; }
        public string OriginationID { get; set;}
        public string TransactionID { get { return OriginationID; } }
        public string Status { get; set; }
    }
}
