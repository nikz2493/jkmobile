using System;

namespace JKMServices.DTO
{
    public class Payment
    {
        public string TransactionNumber { get; set; }               // onerivet_transactionnumber
        public int TransactionAmount { get; set; }                  // onerivet_transactionamount
        public string TransactionDate { get; set; }                 // onerivet_transactiondate
        public string CustomerID { get; set; }                      // onerivet_contactid
        public string MoveID { get; set; }                          // onerivet_moveid
        public string DeviceID { get; set; }                        // nodus_securitycode

        public string Deposit { get; set; }                         // jkmoving_deposit
        public string TotalCost { get; set; }                       // jkmoving_actuallinehaul OR jkmoving_estimatedlinehaul
        public string TotalDue { get; set; }                        // jkmoving_totalremaining
        public string TotalPaid { get; set; }                       // jkmoving_totalpaid
        public string MoveCoordinator_ContactNumber { get; set; }   // _jkmoving_contactofmoveid_value
    }
}
