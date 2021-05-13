using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("BoardModelShaper")]
    public class BoardModelShaper : BaseEntity
    {
        public int BoardModelId { get; set; }
        public int ShaperId { get; set; }

        [NotMapped]
        public Shaper Shaper { get; set; }

        [NotMapped]
        public BoardModel BoardModel { get; set; }
    }
}
