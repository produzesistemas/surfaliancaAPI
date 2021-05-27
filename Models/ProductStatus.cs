using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("ProductStatus")]
    public class ProductStatus : BaseEntity
    {
        public string Name { get; set; }

        [NotMapped]
        public List<Product> Products { get; set; }
    }
}
