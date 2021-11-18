
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("TypeBlog")]
    public class TypeBlog : BaseEntity
    {
        public string Name { get; set; }

    }
}

