using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("BoardModelWidth")]
    public class BoardModelWidth : BaseEntity
    {
        public int BoardModelId { get; set; }
        public int WidthId { get; set; }

        [NotMapped]
        public Width Width { get; set; }

        [NotMapped]
        public BoardModel BoardModel { get; set; }
    }
}
