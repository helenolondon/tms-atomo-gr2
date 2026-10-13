using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmServiceInfra.ODP
{
    [Table("TD_PRODUTOTIPOAGRESSIVIDADE")]
    public class ProdutoTipoAgressividadeODP : ObjetoDePersistencia
    {
        [Key]
        [Column("CODPRODUTOTIPOAGRESSIVIDADE")]
        public int CodProdutoTipoAgressividade { get; set; }

        [Required]
        [MaxLength(250)]
        [Column("NOME")]
        public string Nome { get; set; }

        [MaxLength(250)]
        [Column("DESCRICAO")]
        public string Descricao { get; set; }
    }
}
