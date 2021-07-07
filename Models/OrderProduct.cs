

using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("OrderProduct")]
    public class OrderProduct : BaseEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal ProductValue { get; set; }
        public decimal Qtd { get; set; }
        public string Obs { get; set; }

        [NotMapped]
        public virtual Product Product { get; set; }
        [NotMapped]
        public virtual Order Order { get; set; }
    }
}
