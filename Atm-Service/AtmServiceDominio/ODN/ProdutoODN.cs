using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra.ODP;

namespace AtmServiceDominio.ODN
{
    public class ProdutoODN : ObjetoDeNegocio
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

        internal override ObjetoDePersistencia ToODP(ObjetoDePersistencia destino = null)
        {
            var odp = destino as ProdutoODP ?? new ProdutoODP();
            odp.CodProduto = this.CodProduto;
            odp.Descricao = this.Descricao;
            odp.Aka = this.Aka;
            odp.PrecoDeCompra = this.PrecoDeCompra;
            odp.PrecoMedio = this.PrecoMedio;
            odp.PrecoDeVenda = this.PrecoDeVenda;
            odp.CodUnidadeDeMedida = this.CodUnidadeDeMedida;
            odp.CodProdutoTipo = this.CodProdutoTipo;
            odp.CodMarca = this.CodMarca;
            odp.CodProdutoGrupo = this.CodProdutoGrupo;
            return base.ToODP(odp);
        }

        internal override void FromODP(ObjetoDePersistencia obj)
        {
            if (obj == null)
            {
                return;
            }

            var odp = obj as ProdutoODP;
            this.CodProduto = odp.CodProduto;
            this.Descricao = odp.Descricao;
            this.Aka = odp.Aka;
            this.PrecoDeCompra = odp.PrecoDeCompra;
            this.PrecoMedio = odp.PrecoMedio;
            this.PrecoDeVenda = odp.PrecoDeVenda;
            this.CodUnidadeDeMedida = odp.CodUnidadeDeMedida;
            this.CodProdutoTipo = odp.CodProdutoTipo;
            this.CodMarca = odp.CodMarca;
            this.CodProdutoGrupo = odp.CodProdutoGrupo;
            base.FromODP(odp);
        }
    }
}
