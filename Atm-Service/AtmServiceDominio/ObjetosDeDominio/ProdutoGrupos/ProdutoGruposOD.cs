using AtmServiceDominio.ODN;
using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra;
using AtmServiceInfra.ODP;
using AtmServiceInfra.ProdutoGrupos;
using AtmServiceInfra.Produtos;
using AtmServiceDominio.Erros;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceDominio.ObjetosDeDominio.ProdutoGrupos
{
    public class ProdutoGruposOD : AtmObjetoDominio<ProdutoGrupoODP>, IProdutoGruposOD
    {
        IProdutosRepositorio produtosRepositorio;
        IProdutoGruposRepositorio repositorio;

        public ProdutoGruposOD(IProdutoGruposRepositorio repositorio, IProdutosRepositorio produtosRepositorio) : base(repositorio)
        {
            this.repositorio = repositorio;
            this.produtosRepositorio = produtosRepositorio;
        }

        public List<ProdutoGrupoODN> Listar()
        {
            return (this.repositorio as IProdutoGruposRepositorio)
                .Listar()
                .Select(item => new ProdutoGrupoODN
                {
                    CodProdutoGrupo = item.CodProdutoGrupo,
                    Nome = item.Nome,
                    Descricao = item.Descricao
                })
                .ToList();
        }

        public ProdutoGrupoODN ObterPorId(int codProdutoGrupo)
        {
            var odp = this.repositorio.Obter(codProdutoGrupo);
            return odp == null ? null : new ProdutoGrupoODN
            {
                CodProdutoGrupo = odp.CodProdutoGrupo,
                Nome = odp.Nome,
                Descricao = odp.Descricao
            };
        }

        protected override object[] ObterID(ObjetoDeNegocio obj)
        {
            var item = obj as ProdutoGrupoODN;
            return item != null && item.CodProdutoGrupo > 0 ? new object[] { item.CodProdutoGrupo } : null;
        }

        protected internal override void AntesDeSalvar(ProdutoGrupoODP obj, bool inserindo)
        {
            base.AntesDeSalvar(obj, inserindo);

            if (inserindo)
            {
                obj.CodProdutoGrupo = this.repositorio.ObterProximoValorDeSequencia("TD_PRODUTOGRUPO_SEQ");
            }
        }

        protected internal override void AntesDeExcluir(ProdutoGrupoODP obj)
        {
            base.AntesDeExcluir(obj);

            if (this.produtosRepositorio.ContarProdutosPorGrupo(obj.CodProdutoGrupo) > 0)
            {
                throw new OperacaoNaoPermitidaDE("Não é possível excluir o grupo de produto, pois existem produtos associados a ele.");
            }
        }
    }
}
