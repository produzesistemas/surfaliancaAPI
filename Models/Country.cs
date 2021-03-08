
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Country")]
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        public string Name_pt { get; set; }
        public string Initials { get; set; }
        public string Bacen { get; set; }

        [NotMapped]
        public virtual List<State> States { get; set; }
        public Country()
        {
            States = new List<State>();
        }
    }
}
