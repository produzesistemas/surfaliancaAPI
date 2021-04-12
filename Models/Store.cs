using System;
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
        public string ApplicationUserId { get; set; }
        public int CityId { get; set; }

        public string UpdateApplicationUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        [NotMapped]
        public string CriadoPor { get; set; }
        [NotMapped]
        public string AlteradoPor { get; set; }


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

    }
}