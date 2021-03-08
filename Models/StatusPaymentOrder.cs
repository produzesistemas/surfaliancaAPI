using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{ 

    [Table("StatusPaymentOrder")]
    public class StatusPaymentOrder : BaseEntity
    {
        //[Key]
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
    }
}