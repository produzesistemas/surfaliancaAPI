

using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("OrderProduct")]
    public class OrderProduct : BaseEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal ProductValue { get; set; }
        public int Qtd { get; set; }
        public string Obs { get; set; }

        [NotMapped]
        public Product Product { get; set; }
        [NotMapped]
        public Order Order { get; set; }
    }
}
