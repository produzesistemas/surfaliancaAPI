using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("TeamImage")]

    public class TeamImage : BaseEntity
    {
        public string ImageName { get; set; }
        public int TeamId { get; set; }

        [NotMapped]
        public Team Team { get; set; }


    }
}
