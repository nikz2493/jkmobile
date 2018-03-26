namespace JKMServices.DTO
{
    using System.ComponentModel.DataAnnotations;

    public partial class Transaction
    {
        [StringLength(20)]
        public string TransactionID { get; set; }

        [StringLength(20)]
        public string CustomerID { get; set; }

        [StringLength(50)]
        public string TransactionAmount { get; set; }
    }
}
