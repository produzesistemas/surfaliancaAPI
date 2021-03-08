using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("ProductModel")]
    public class ProductModel : BaseEntity
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public int StoreId { get; set; }
    }
}
