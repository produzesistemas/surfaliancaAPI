using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Blog")]
    public class Blog : BaseEntity
    {
        public string Description { get; set; }
        public string ImageName { get; set; }
        public int TypeBlogId { get; set; }
        public string Details { get; set; }
        public string UpdateApplicationUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string ApplicationUserId { get; set; }

        [NotMapped]
        public string CriadoPor { get; set; }
        [NotMapped]
        public string AlteradoPor { get; set; }

        [NotMapped]
        public TypeBlog TypeBlog { get; set; }
    }
}
