using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("BoardModelBottom")]
    public class BoardModelBottom : BaseEntity
    {
        public int BoardModelId { get; set; }
        public int BottomId { get; set; }

        [NotMapped]
        public Bottom Bottom { get; set; }

        [NotMapped]
        public BoardModel BoardModel { get; set; }
    }
}
