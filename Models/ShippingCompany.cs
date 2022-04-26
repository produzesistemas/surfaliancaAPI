﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("ShippingCompany")]
    public class ShippingCompany : BaseEntity
    {
        public string Name { get; set; }
        public string ImageName { get; set; }
        public bool Active { get; set; }
        public string UpdateApplicationUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string ApplicationUserId { get; set; }

        [NotMapped]
        public string CriadoPor { get; set; }
        [NotMapped]
        public string AlteradoPor { get; set; }

        [NotMapped]
        public List<ShippingCompanyState> ShippingCompanyStates { get; set; }
    }
}