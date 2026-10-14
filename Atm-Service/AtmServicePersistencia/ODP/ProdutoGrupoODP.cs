using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmServiceInfra.ODP
{
    [Table("TD_PRODUTOGRUPO")]
    public class ProdutoGrupoODP : ObjetoDePersistencia
    {
        [Key]
        [Column("CODPRODUTOGRUPO")]
        public int CodProdutoGrupo { get; set; }

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
