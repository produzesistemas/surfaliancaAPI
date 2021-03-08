using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("ProductType")]
    public class ProductType : BaseEntity
    {
        public string Name { get; set; }
    }
}
