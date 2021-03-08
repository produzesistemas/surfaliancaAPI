using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("TypeEmail")]
    public class TypeEmail : BaseEntity
    {
        //[Key]
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }

        [MaxLength(20)]
        public string Description { get; set; }

    }
}
