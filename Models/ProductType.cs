using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("ProductType")]
    public class ProductType : BaseEntity
    {
        public string Name { get; set; }

        [NotMapped]
        public List<Product> Products { get; set; }
    }
}
