using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("Finishing")]
    public class Finishing : BaseEntity
    {
        public string Name { get; set; }
    }
}
