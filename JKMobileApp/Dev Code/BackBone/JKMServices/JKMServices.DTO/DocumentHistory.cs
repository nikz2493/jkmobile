namespace JKMServices.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DocumentHistory")]
    public partial class DocumentHistory
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string document_history_DocumentID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string document_history_HistoryTitle { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string document_history_CustomersID { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime document_history_ActionDate { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Document Document { get; set; }
    }
}
