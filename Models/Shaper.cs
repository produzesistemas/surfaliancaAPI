using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Shaper")]
    public class Shaper : BaseEntity
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public int StoreId { get; set; }
        public string ImageName { get; set; }
    }
}
