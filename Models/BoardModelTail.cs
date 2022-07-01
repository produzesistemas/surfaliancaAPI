using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("BoardModelTail")]
    public class BoardModelTail : BaseEntity
    {
        public int BoardModelId { get; set; }
        public int TailId { get; set; }

        [NotMapped]
        public Tail Tail { get; set; }

        [NotMapped]
        public BoardModel BoardModel { get; set; }
    }
}
