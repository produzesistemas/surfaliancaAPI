using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Width")]
    public class Width : BaseEntity
    {
        public string Description { get; set; }


        [NotMapped]
        public List<OrderProductOrdered> OrderProductOrdereds { get; set; }
    }
}
