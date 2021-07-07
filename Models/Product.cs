using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("Product")]
    public class Product : BaseEntity
    {
        public string Name { get; set; }
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

        [NotMapped]
        public ProductStatus ProductStatus { get; set; }

        [NotMapped]
        public ProductType ProductType { get; set; }

        public bool Active { get; set; }
        public bool IsPromotion { get; set; }
        public bool IsSpotlight { get; set; }
        public decimal? ValuePromotion { get; set; }

        [NotMapped]
        public List<OrderProduct> OrderProducts { get; set; }

    }
}
