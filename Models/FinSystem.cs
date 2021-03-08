using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("FinSystem")]
    public class FinSystem : BaseEntity
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public int StoreId { get; set; }
    }
}
