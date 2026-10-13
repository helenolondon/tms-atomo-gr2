using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmServiceInfra.ODP
{
    [Table("TD_MARCAS")]
    public class MarcaODP : ObjetoDePersistencia
    {
        [Key]
        [Column("CODMARCA")]
        public int CodMarca { get; set; }

        [Required]
        [MaxLength(250)]
        [Column("NOME")]
        public string Nome { get; set; }

        [Required]
        [MaxLength(250)]
        [Column("DESCRICAO")]
        public string Descricao { get; set; }

        [Required]
        [MaxLength(500)]
        [Column("OBSERVACAO")]
        public string Observacao { get; set; }
    }
}
