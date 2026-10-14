using AtmServiceDominio.ODN;
using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra;
using AtmServiceInfra.ODP;
using AtmServiceInfra.Produtos;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceDominio.ObjetosDeDominio.Produtos
{
    public class ProdutosOD : AtmObjetoDominio<ProdutoODP>, IProdutosOD
    {
        public ProdutosOD(IProdutosRepositorio repositorio) : base(repositorio)
        {
        }

        public List<ProdutoODN> Listar()
        {
            return (this.repositorio as IProdutosRepositorio)
                .Listar()
                .Select(item => new ProdutoODN
                {
                    CodProduto = item.CodProduto,
                    Descricao = item.Descricao,
                    Aka = item.Aka,
                    PrecoDeCompra = item.PrecoDeCompra,
                    PrecoMedio = item.PrecoMedio,
                    PrecoDeVenda = item.PrecoDeVenda,
                    CodUnidadeDeMedida = item.CodUnidadeDeMedida,
                    CodProdutoTipo = item.CodProdutoTipo,
                    CodMarca = item.CodMarca,
                    CodProdutoGrupo = item.CodProdutoGrupo
                })
                .ToList();
        }

        public ProdutoODN ObterPorId(int codProduto)
        {
            var odp = this.repositorio.Obter(codProduto);
            return odp == null ? null : new ProdutoODN
            {
                CodProduto = odp.CodProduto,
                Descricao = odp.Descricao,
                Aka = odp.Aka,
                PrecoDeCompra = odp.PrecoDeCompra,
                PrecoMedio = odp.PrecoMedio,
                PrecoDeVenda = odp.PrecoDeVenda,
                CodUnidadeDeMedida = odp.CodUnidadeDeMedida,
                CodProdutoTipo = odp.CodProdutoTipo,
                CodMarca = odp.CodMarca,
                CodProdutoGrupo = odp.CodProdutoGrupo
            };
        }

        protected override object[] ObterID(ObjetoDeNegocio obj)
        {
            var item = obj as ProdutoODN;
            return item != null && item.CodProduto > 0 ? new object[] { item.CodProduto } : null;
        }

        protected internal override void AntesDeSalvar(ProdutoODP obj, bool inserindo)
        {
            base.AntesDeSalvar(obj, inserindo);

            if (inserindo)
            {
                obj.CodProduto = this.repositorio.ObterProximoValorDeSequencia("TD_PRODUTO_SEQ");
            }
        }
    }
}
