
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("OrderProductOrdered")]
    public class OrderProductOrdered : BaseEntity
    {
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public string Obs { get; set; }
        public int OrderId { get; set; }
        public int BoardModelId { get; set; }
        public int? LevelId { get; set; }
        public int? BoardModelDimensionId { get; set; }
        public string OtherDimensions { get; set; }
        public int? ConstructionId { get; set; }
        public int? LaminationId { get; set; }
        public int? TailId { get; set; }
        public int? PaintId { get; set; }

        [NotMapped]
        public Finishing Finishing { get; set; }
        [NotMapped]
        public Paint Paint { get; set; }

        [NotMapped]
        public Tail Tail { get; set; }

        [NotMapped]
        public Lamination Lamination { get; set; }
        [NotMapped]
        public Construction Construction { get; set; }
        [NotMapped]
        public Bottom Bottom { get; set; }
        [NotMapped]
        public BoardType BoardType { get; set; }
        [NotMapped]
        public BoardModel BoardModel { get; set; }

        [NotMapped]
        public List<Order> Orders { get; set; }


    }
}
