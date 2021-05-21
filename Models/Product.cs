using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("Product")]
    public class Product : BaseEntity
    {
        public string Description { get; set; }
        public string ImageName { get; set; }
        public decimal Value { get; set; }
        public string UpdateApplicationUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string ApplicationUserId { get; set; }

        public int TypeSaleId { get; set; }
        public int ProductTypeId { get; set; }
        public int ProductStatusId { get; set; }

        [NotMapped]
        public string CriadoPor { get; set; }

        [NotMapped]
        public string AlteradoPor { get; set; }
        public int? BoardModelId { get; set; }
        public int? BottomId { get; set; }
        public int? SizeId { get; set; }
        public int? WidthId { get; set; }
        public int? ConstructionId { get; set; }
        public int? LitigationId { get; set; }
        public int? LaminationId { get; set; }
        public int? ShaperId { get; set; }
        public int? BoardTypeId { get; set; }
        public int? FinSystemId { get; set; }
        public int? TailId { get; set; }

        [NotMapped]
        public BoardModel BoardModel { get; set; }

    }
}
