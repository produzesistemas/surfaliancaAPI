using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Size")]
    public class Size : BaseEntity
    {
        public string Description { get; set; }
    }
}
