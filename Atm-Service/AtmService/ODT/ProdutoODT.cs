using AtmServiceDominio.ODN;

namespace AtmService.ODT
{
    public class ProdutoODT
    {
        public int CodProduto { get; set; }
        public string Descricao { get; set; }
        public string Aka { get; set; }
        public float? PrecoDeCompra { get; set; }
        public float? PrecoMedio { get; set; }
        public float? PrecoDeVenda { get; set; }
        public int CodUnidadeDeMedida { get; set; }
        public int CodProdutoTipo { get; set; }
        public int CodMarca { get; set; }
        public int CodProdutoGrupo { get; set; }

        public ProdutoODT()
        {
        }

        public ProdutoODT(ProdutoODN item)
        {
            FromODN(item);
        }

        public void FromODN(ProdutoODN item)
        {
            if (item == null)
            {
                return;
            }

            this.CodProduto = item.CodProduto;
            this.Descricao = item.Descricao;
            this.Aka = item.Aka;
            this.PrecoDeCompra = item.PrecoDeCompra;
            this.PrecoMedio = item.PrecoMedio;
            this.PrecoDeVenda = item.PrecoDeVenda;
            this.CodUnidadeDeMedida = item.CodUnidadeDeMedida;
            this.CodProdutoTipo = item.CodProdutoTipo;
            this.CodMarca = item.CodMarca;
            this.CodProdutoGrupo = item.CodProdutoGrupo;
        }

        public ProdutoODN ToODN()
        {
            return new ProdutoODN
            {
                CodProduto = this.CodProduto,
                Descricao = this.Descricao,
                Aka = this.Aka,
                PrecoDeCompra = this.PrecoDeCompra,
                PrecoMedio = this.PrecoMedio,
                PrecoDeVenda = this.PrecoDeVenda,
                CodUnidadeDeMedida = this.CodUnidadeDeMedida,
                CodProdutoTipo = this.CodProdutoTipo,
                CodMarca = this.CodMarca,
                CodProdutoGrupo = this.CodProdutoGrupo
            };
        }
    }
}
