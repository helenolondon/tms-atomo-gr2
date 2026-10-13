using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmServiceInfra.ODP
{
    [Table("TD_TIPOVEICULO")]
    public class TipoVeiculoODP : ObjetoDePersistencia
    {
        [Key]
        [Column("CODTIPOVEICULO")]
        public int CodTipoVeiculo { get; set; }

        [Required]
        [MaxLength(250)]
        [Column("DESCRICAO")]
        public string Descricao { get; set; }
    }
}
