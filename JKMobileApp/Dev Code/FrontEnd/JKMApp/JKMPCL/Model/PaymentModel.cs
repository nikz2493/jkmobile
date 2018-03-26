namespace JKMPCL.Model
{
    /// <summary>
    /// Class Name      : Payment
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 25 Jan 2018
    /// Purpose         : DTO class for transfering payment transaction details to service & vice versa.
    /// Revision        : 
    /// </summary>
    public class PaymentModel
    {
        public string CustomerID { get; set; }                      // onerivet_contactid
        public string MoveID { get; set; }                          // onerivet_contactid
        public string TransactionNumber { get; set; }               // onerivet_transactionid
        public string TransactionAmount { get; set; }               // onerivet_transaction
        public string TransactionDate { get; set; }                 // onerivet_transactiondate
        public PaymentDeviceModel Data { get; set; }                // deviceid&pwd for token generation
        
        //for multiple payment page
        public string Deposit { get; set; }                         // jkmoving_deposit
        public string TotalCost { get; set; }                       // jkmoving_actuallinehaul OR jkmoving_estimatedlinehaul
        public string TotalDue { get; set; }                        // jkmoving_totalremaining
        public string TotalPaid { get; set; }                       // jkmoving_totalpaid
        public string MoveCoordinator_ContactNumber { get; set; }   // _jkmoving_contactofmoveid_value
    }

    public class PaymentDeviceModel
    {
        public string DeviceID { get; set; }                        // deviceid&pwd for token generation
    }
}
