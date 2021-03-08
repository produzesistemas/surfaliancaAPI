using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Store")]
    public class Store : BaseEntity
    {
        public string Name { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
        public string Cep { get; set; }
        public string CNPJ { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string ExchangePolicy { get; set; }
        public string DeliveryPolicy { get; set; }
        public decimal ValueMinimum { get; set; }
        public bool Active { get; set; }
        public string AspNetUsersId { get; set; }
        public int CityId { get; set; }


        [NotMapped]
        public virtual City City { get; set; }
        [NotMapped]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [NotMapped]
        public string imagemBase64 { get; set; }
        [NotMapped]
        public string NameCity { get; set; }

        [NotMapped]
        public decimal TaxDelivery { get; set; }

        [NotMapped]
        public virtual List<FinSystem> FinsSystem { get; set; }
        [NotMapped]
        public virtual List<ProductModel> ProductModels { get; set; }

        public Store()
        {
            FinsSystem = new List<FinSystem>();
            ProductModels = new List<ProductModel>();
        }
    }
}