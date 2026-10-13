using AtmServiceDominio.ODN;
using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra;
using AtmServiceInfra.ODP;
using AtmServiceInfra.ProdutoTipos;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceDominio.ObjetosDeDominio.ProdutoTipos
{
    public class ProdutoTiposOD : AtmObjetoDominio<ProdutoTipoODP>, IProdutoTiposOD
    {
        public ProdutoTiposOD(IProdutoTiposRepositorio repositorio) : base(repositorio)
        {
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
    }
}
