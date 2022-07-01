using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("BoardModelLamination")]
    public class BoardModelLamination : BaseEntity
    {
        public int BoardModelId { get; set; }
        public int LaminationId { get; set; }

        [NotMapped]
        public Lamination Lamination { get; set; }

        [NotMapped]
        public BoardModel BoardModel { get; set; }
    }
}
