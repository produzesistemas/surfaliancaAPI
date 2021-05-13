using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Width")]
    public class Width : BaseEntity
    {
        public string Description { get; set; }
    }
}
