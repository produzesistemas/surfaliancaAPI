using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("BoardModel")]
    public class BoardModel : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public decimal Value { get; set; }
        public int DaysProduction { get; set; }
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
        public List<BoardModelBoardType> BoardModelBoardTypes { get; set; }
        [NotMapped]
        public List<BoardModelShaper> BoardModelShapers { get; set; }
        [NotMapped]
        public List<BoardModelBottom> BoardModelBottoms { get; set; }
        [NotMapped]
        public List<BoardModelConstruction> BoardModelConstructions { get; set; }

        [NotMapped]
        public List<BoardModelLamination> BoardModelLaminations { get; set; }
        [NotMapped]
        public List<BoardModelSize> BoardModelSizes { get; set; }
        [NotMapped]
        public List<BoardModelTail> BoardModelTails { get; set; }
        [NotMapped]
        public List<BoardModelWidth> BoardModelWidths { get; set; }

        [NotMapped]
        public List<Product> Products { get; set; }

    }
}
