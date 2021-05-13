
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("Litigation")]
    public class Litigation : BaseEntity
    {
        public string Description { get; set; }
    }
}
