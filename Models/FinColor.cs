using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("FinColor")]
    public class FinColor : BaseEntity
    {
        public string Name { get; set; }
    }
}
