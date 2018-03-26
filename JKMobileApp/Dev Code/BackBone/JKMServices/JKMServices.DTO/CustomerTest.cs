using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JKMServices.DTO
{
    [Table("CustomerTest",Schema ="dbo")]
    public class CustomerTest
    {
        [Key]
        public int ID { get; set; }
        public string CustomerName { get; set; }

    }
}
