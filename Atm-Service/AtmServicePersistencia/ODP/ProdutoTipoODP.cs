using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmServiceInfra.ODP
{
    [Table("TD_PRODUTOTIPO")]
    public class ProdutoTipoODP : ObjetoDePersistencia
    {
        [Key]
        [Column("CODPRODUTOTIPO")]
        public int CodProdutoTipo { get; set; }

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
