using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("BoardModelLitigation")]
    public class BoardModelLitigation : BaseEntity
    {
        public int BoardModelId { get; set; }
        public int LitigationId { get; set; }

        [NotMapped]
        public Litigation Litigation { get; set; }

        [NotMapped]
        public BoardModel BoardModel { get; set; }
    }
}
