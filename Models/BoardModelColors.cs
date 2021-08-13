using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("BoardModelColors")]
    public class BoardModelColors : BaseEntity
    {
        public int BoardModelId { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public string Rgb { get; set; }
    }
}
