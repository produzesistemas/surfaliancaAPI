using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Size")]
    public class Size : BaseEntity
    {
        public string Description { get; set; }

        [NotMapped]
        public List<BoardModelSize> BoardModelSizes { get; set; }
        [NotMapped]
        public List<OrderProductOrdered> OrderProductOrdereds { get; set; }
    }
}
