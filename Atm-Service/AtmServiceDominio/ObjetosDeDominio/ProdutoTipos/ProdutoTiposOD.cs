using AtmServiceDominio.ODN;
using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra;
using AtmServiceInfra.ODP;
using AtmServiceInfra.ProdutoTipos;
using AtmServiceInfra.Produtos;
using System.Collections.Generic;
using System.Linq;
using AtmServiceDominio.Erros;

namespace AtmServiceDominio.ObjetosDeDominio.ProdutoTipos
{
    public class ProdutoTiposOD : AtmObjetoDominio<ProdutoTipoODP>, IProdutoTiposOD
    {
        IProdutosRepositorio produtosRepositorio;
        IProdutoTiposRepositorio repositorio;

        public ProdutoTiposOD(IProdutoTiposRepositorio repositorio, IProdutosRepositorio produtosRepositorio) : base(repositorio)
        {
            this.repositorio = repositorio;
            this.produtosRepositorio = produtosRepositorio;
        }

        public List<ProdutoTipoODN> Listar()
        {
            return (this.repositorio as IProdutoTiposRepositorio)
                .Listar()
                .Select(item => new ProdutoTipoODN
                {
                    CodProdutoTipo = item.CodProdutoTipo,
                    Nome = item.Nome,
                    Descricao = item.Descricao
                })
                .ToList();
        }

        public ProdutoTipoODN ObterPorId(int codProdutoTipo)
        {
            var odp = this.repositorio.Obter(codProdutoTipo);
            return odp == null ? null : new ProdutoTipoODN
            {
                CodProdutoTipo = odp.CodProdutoTipo,
                Nome = odp.Nome,
                Descricao = odp.Descricao
            };
        }

        protected override object[] ObterID(ObjetoDeNegocio obj)
        {
            var item = obj as ProdutoTipoODN;
            return item != null && item.CodProdutoTipo > 0 ? new object[] { item.CodProdutoTipo } : null;
        }

        protected internal override void AntesDeSalvar(ProdutoTipoODP obj, bool inserindo)
        {
            base.AntesDeSalvar(obj, inserindo);

            if (inserindo)
            {
                obj.CodProdutoTipo = this.repositorio.ObterProximoValorDeSequencia("TD_PRODUTOTIPO_SEQ");
            }
        }

        override protected internal void AntesDeExcluir(ProdutoTipoODP obj)
        {
            base.AntesDeExcluir(obj);
            if (this.produtosRepositorio.ContarProdutosPorTipo(obj.CodProdutoTipo) > 0)
            {
                throw new OperacaoNaoPermitidaDE("Não é possível excluir o tipo de produto, pois existem produtos associados a ele.");
            }
        }
    }
}
