using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("Cupom")]
    public class Cupom : BaseEntity
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public bool Geral { get; set; }
        public bool Tipo { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorMinimo { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }

        public string UpdateApplicationUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string AspNetUsersId { get; set; }
        public string ClienteId { get; set; }

        [NotMapped]
        public string CriadoPor { get; set; }
        [NotMapped]
        public string AlteradoPor { get; set; }
        [NotMapped]
        public virtual List<Order> Orders { get; set; }
        public Cupom()
        {
            Orders = new List<Order>();
        }
    }
}
