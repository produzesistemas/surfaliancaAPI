
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("FinSystemColor")]
    public class FinSystemColor : BaseEntity
    {
        public int FinSystemId { get; set; }
        public int FinColorId { get; set; }

        [NotMapped]
        public FinColor FinColor { get; set; }
    }
}
