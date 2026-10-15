using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmServiceInfra.ODP
{
    [Table("TD_SERVICOCATEGORIA")]
    public class ServicoCategoriaODP : ObjetoDePersistencia
    {
        [Key]
        [Column("CODSERVICOCATEGORIA")]
        public int CodServicoCategoria { get; set; }

        [Required]
        [MaxLength(250)]
        [Column("NOME")]
        public string Nome { get; set; }

        [Required]
        [MaxLength(250)]
        [Column("DESCRICAO")]
        public string Descricao { get; set; }
    }
}
