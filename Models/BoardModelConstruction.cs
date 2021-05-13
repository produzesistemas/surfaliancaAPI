using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("BoardModelConstruction")]
    public class BoardModelConstruction : BaseEntity
    {
        public int BoardModelId { get; set; }
        public int ConstructionId { get; set; }

        [NotMapped]
        public Construction Construction { get; set; }

        [NotMapped]
        public BoardModel BoardModel { get; set; }
    }
}
