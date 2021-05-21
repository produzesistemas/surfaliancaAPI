using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("TypeSale")]
    public class TypeSale : BaseEntity
    {
        public string Name { get; set; }
    }
}
