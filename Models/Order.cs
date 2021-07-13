
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Order")]
    public class Order : BaseEntity
    {
        public string Obs { get; set; }
        
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string District { get; set; }
        public string Complement { get; set; }
        public string Reference { get; set; }
        public string State { get; set; }
        public decimal TaxValue { get; set; }
        public string ApplicationUserId { get; set; }
        public int PaymentConditionId { get; set; }

        public int Installments { get; set; }
        public int CapturedAmount { get; set; }
        public string PaymentId { get; set; }

        [NotMapped]
        public string EnviadoPor { get; set; }

        [NotMapped]
        public virtual List<OrderProduct> OrderProduct { get; set; }
        [NotMapped]
        public virtual List<OrderTracking> OrderTracking { get; set; }
        [NotMapped]
        public virtual List<OrderProductOrdered> OrderProductOrdered { get; set; }
        [NotMapped]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [NotMapped]
        public virtual PaymentCondition PaymentCondition { get; set; }

        public Order()
        {
            OrderTracking = new List<OrderTracking>();
            OrderProduct = new List<OrderProduct>();
            OrderProductOrdered = new List<OrderProductOrdered>();
        }
    }
}
