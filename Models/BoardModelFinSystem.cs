using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("BoardModelFinSystem")]
    public class BoardModelFinSystem : BaseEntity
    {
        public int BoardModelId { get; set; }
        public int FinSystemId { get; set; }

        [NotMapped]
        public FinSystem FinSystem { get; set; }

        [NotMapped]
        public BoardModel BoardModel { get; set; }
    }
}
