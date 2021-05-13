using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("BoardModelSize")]
    public class BoardModelSize : BaseEntity
    {
        public int BoardModelId { get; set; }
        public int SizeId { get; set; }

        [NotMapped]
        public Size Size { get; set; }

        [NotMapped]
        public BoardModel BoardModel { get; set; }
    }
}
