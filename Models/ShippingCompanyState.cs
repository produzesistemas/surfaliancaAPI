using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("ShippingCompanyState")]
    public class ShippingCompanyState : BaseEntity
    {
        public decimal TaxValue { get; set; }
        public int ShippingCompanyId { get; set; }
        public int StateId { get; set; }

        [NotMapped]
        public virtual ShippingCompany ShippingCompany { get; set; }
    }
}
