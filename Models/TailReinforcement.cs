﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("TailReinforcement")]
    public class TailReinforcement : BaseEntity
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public decimal? Value { get; set; }
        public string UpdateApplicationUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string ApplicationUserId { get; set; }
        public bool Active { get; set; }

        [NotMapped]
        public string CriadoPor { get; set; }
        [NotMapped]
        public string AlteradoPor { get; set; }

        [NotMapped]
        public List<OrderProductOrdered> OrderProductOrdereds { get; set; }
    }
}