using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("TypeEmail")]
    public class TypeEmail : BaseEntity
    {

        [MaxLength(20)]
        public string Description { get; set; }

    }
}
