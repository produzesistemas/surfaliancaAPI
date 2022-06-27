

using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("OrderEvaluation")]
    public class OrderEvaluation : BaseEntity
    {
        public System.DateTime DateEvaluation { get; set; }
        public int OrderId { get; set; }
        public int Star { get; set; }
        public string Comment { get; set; }


    }
}
