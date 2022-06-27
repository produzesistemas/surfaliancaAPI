using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Paint")]
    public class Paint : BaseEntity
    {
        public string Name { get; set; }
        public decimal? Value { get; set; }
        public string ImageName { get; set; }
        public bool Active { get; set; }
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
