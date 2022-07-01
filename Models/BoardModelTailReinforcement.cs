using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("BoardModelTailReinforcement")]
    public class BoardModelTailReinforcement : BaseEntity
    {
        public int BoardModelId { get; set; }
        public int TailReinforcementId { get; set; }

        [NotMapped]
        public TailReinforcement TailReinforcement { get; set; }

        [NotMapped]
        public BoardModel BoardModel { get; set; }
    }
}
