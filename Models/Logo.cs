using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    [Table("Logo")]

    public class Logo : BaseEntity
    {
        public string Name { get; set; }
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
