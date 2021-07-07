using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("OrderEmail")]
    public class OrderEmail : BaseEntity
    {
        public bool Send { get; set; }
        public int TypeEmailId { get; set; }
        public int OrderId { get; set; }
    }
}
