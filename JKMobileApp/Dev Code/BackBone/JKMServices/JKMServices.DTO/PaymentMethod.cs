namespace JKMServices.DTO
{
    using System.ComponentModel.DataAnnotations;

    public partial class PaymentMethod
    {
        [Key]
        [StringLength(20)]
        public string payment_methods_paymentID { get; set; }

        [Required]
        [StringLength(20)]
        public string payment_methods_CustomerID { get; set; }

        [Required]
        [StringLength(10)]
        public string payment_methods_PaymentType { get; set; }

        [Required]
        [StringLength(16)]
        public string payment_methods_CardNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string payment_methods_CardName { get; set; }

        [Required]
        [StringLength(4)]
        public string payment_methods_expirydate { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
