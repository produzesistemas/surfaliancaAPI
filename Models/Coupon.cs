using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("Coupon")]
    public class Coupon : BaseEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool General { get; set; }
        public bool Type { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public decimal ValueMinimum { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }

        public string UpdateApplicationUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string ApplicationUserId { get; set; }
        public string ClientId { get; set; }

        [NotMapped]
        public string CriadoPor { get; set; }
        [NotMapped]
        public string AlteradoPor { get; set; }
        [NotMapped]
        public virtual List<Order> Orders { get; set; }
        public Coupon()
        {
            Orders = new List<Order>();
        }
    }
}
