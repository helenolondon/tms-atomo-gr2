using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmServiceInfra.ODP
{
    [Table("TD_PRODUTO")]
    public class ProdutoODP : ObjetoDePersistencia
    {
        [Key]
        [Column("CODPRODUTO")]
        public int CodProduto { get; set; }

        [Required]
        [MaxLength(250)]
        [Column("DESCRICAO")]
        public string Descricao { get; set; }

        [Required]
        [MaxLength(250)]
        [Column("AKA")]
        public string Aka { get; set; }

        [Column("PRECODECOMPRA")]
        public float? PrecoDeCompra { get; set; }

        [Column("PRECOMEDIO")]
        public float? PrecoMedio { get; set; }

        [Column("PRECODEVENDA")]
        public float? PrecoDeVenda { get; set; }

        [Required]
        [Column("CODUNIDADEDEMEDIDA")]
        public int CodUnidadeDeMedida { get; set; }

        [Required]
        [Column("CODPRODUTOTIPO")]
        public int CodProdutoTipo { get; set; }

        [Required]
        [Column("CODMARCA")]
        public int CodMarca { get; set; }

        [Required]
        [Column("CODPRODUTOGRUPO")]
        public int CodProdutoGrupo { get; set; }

        public UnidadeDeMedidaODP UnidadeDeMedida { get; set; }
        public ProdutoTipoODP ProdutoTipo { get; set; }
        public MarcaODP Marca { get; set; }
        public ProdutoGrupoODP ProdutoGrupo { get; set; }
    }
}
