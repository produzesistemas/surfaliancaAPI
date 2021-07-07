

using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("OrderTracking")]
    public class OrderTracking : BaseEntity
    {
        public System.DateTime DateTracking { get; set; }
        public int OrderId { get; set; }
        public int StatusOrderId { get; set; }
        public int StatusPaymentOrderId { get; set; }

        [NotMapped]
        public StatusOrder StatusOrder { get; set; }
        [NotMapped]
        public StatusPaymentOrder StatusPaymentOrder { get; set; }

    }
}
