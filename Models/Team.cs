

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Team")]
    public class Team : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UpdateApplicationUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string ApplicationUserId { get; set; }

        [NotMapped]
        public string CriadoPor { get; set; }
        [NotMapped]
        public string AlteradoPor { get; set; }
        public List<TeamImage> teamImages { get; set; }
        public Team()
        {
            teamImages = new List<TeamImage>();
        }
    }
}
