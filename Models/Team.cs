

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Team")]
    public class Team : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int StoreId { get; set; }
        public List<TeamImage> teamImages { get; set; }
        public Team()
        {
            teamImages = new List<TeamImage>();
        }
    }
}
