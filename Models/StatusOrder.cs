using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("StatusOrder")]
    public class StatusOrder : BaseEntity
    {
        [MaxLength(255)]
        public string Description { get; set; }

        [NotMapped]
        public List<OrderTracking> OrderTrackings { get; set; }
    }
}
