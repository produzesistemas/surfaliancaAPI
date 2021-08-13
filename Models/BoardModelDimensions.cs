using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("BoardModelDimensions")]
    public class BoardModelDimensions : BaseEntity
    {
        public int BoardModelId { get; set; }
        public string Description { get; set; }
    }
}
