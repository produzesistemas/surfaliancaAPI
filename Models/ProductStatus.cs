using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("ProductStatus")]
    public class ProductStatus : BaseEntity
    {
        public string Name { get; set; }
    }
}
