using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("BoardModelStringer")]
    public class BoardModelStringer : BaseEntity
    {
        public int BoardModelId { get; set; }
        public int StringerId { get; set; }

        [NotMapped]
        public Stringer Stringer { get; set; }

        [NotMapped]
        public BoardModel BoardModel { get; set; }
    }
}
