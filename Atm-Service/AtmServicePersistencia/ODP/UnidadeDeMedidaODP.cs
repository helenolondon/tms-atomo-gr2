using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmServiceInfra.ODP
{
    [Table("TD_UNIDADESDEMEDIDA")]
    public class UnidadeDeMedidaODP : ObjetoDePersistencia
    {
        [Key]
        [Column("CODUNIDADEDEMEDIDA")]
        public int CodUnidadeDeMedida { get; set; }

        [Required]
        [MaxLength(250)]
        [Column("NOME")]
        public string Nome { get; set; }

        [Required]
        [MaxLength(3)]
        [Column("UNIDADE")]
        public string Unidade { get; set; }
    }
}
