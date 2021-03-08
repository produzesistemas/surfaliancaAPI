using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("BoardType")]
    public class BoardType : BaseEntity
    {
        public string Name { get; set; }
        public int StoreId { get; set; }
    }
}
