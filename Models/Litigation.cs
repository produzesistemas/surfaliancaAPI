
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("Litigation")]
    public class Litigation : BaseEntity
    {
        public string Description { get; set; }

        [NotMapped]
        public List<BoardModelLitigation> BoardModelLitigations { get; set; }
    }
}
