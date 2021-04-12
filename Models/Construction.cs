using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Construction")]
    public class Construction : BaseEntity
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public string UpdateApplicationUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string ApplicationUserId { get; set; }

        [NotMapped]
        public string CriadoPor { get; set; }
        [NotMapped]
        public string AlteradoPor { get; set; }
    }
}

