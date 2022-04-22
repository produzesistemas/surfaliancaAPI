using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("State")]
    public class State : BaseEntity
    {
        public string Description { get; set; }
        public string Sg { get; set; }
    }
}

