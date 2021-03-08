using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("State")]
    public class State : BaseEntity
    {
        public string Name { get; set; }
        public string Ddd { get; set; }
        public string Uf { get; set; }
        public int CountryId { get; set; }
        public int Ibge { get; set; }

        [NotMapped]
        public virtual List<City> Cities { get; set; }
        public State()
        {
            Cities = new List<City>();
        }
    }
}
