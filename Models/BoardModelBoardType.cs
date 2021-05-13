using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("BoardModelBoardType")]
    public class BoardModelBoardType : BaseEntity
    {
        public int BoardModelId { get; set; }
        public int BoardTypeId { get; set; }

        [NotMapped]
        public BoardType BoardType { get; set; }

        [NotMapped]
        public BoardModel BoardModel { get; set; }

        //[NotMapped]
        //public virtual List<BoardType> BoardTypes { get; set; }
        //[NotMapped]
        //public virtual List<BoardModel> BoardModels { get; set; }
    }
}
