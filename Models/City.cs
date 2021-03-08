using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("City")]
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public int StateId { get; set; }
        public string Ibge { get; set; }

        [NotMapped]
        public virtual State State { get; set; }
        [NotMapped]
        public virtual List<Store> Stores { get; set; }

        public City()
        {
            Stores = new List<Store>();
        }
    }
}
