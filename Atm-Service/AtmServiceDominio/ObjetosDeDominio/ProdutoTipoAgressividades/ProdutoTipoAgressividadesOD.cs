using AtmServiceDominio.ODN;
using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra;
using AtmServiceInfra.ODP;
using AtmServiceInfra.ProdutoTipoAgressividades;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceDominio.ObjetosDeDominio.ProdutoTipoAgressividades
{
    public class ProdutoTipoAgressividadesOD : AtmObjetoDominio<ProdutoTipoAgressividadeODP>, IProdutoTipoAgressividadeOD
    {
        public ProdutoTipoAgressividadesOD(IProdutoTipoAgressividadeRepositorio repositorio) : base(repositorio)
        {
        }

        public List<ProdutoTipoAgressividadeODN> Listar()
        {
            return (this.repositorio as IProdutoTipoAgressividadeRepositorio)
                .Listar()
                .Select(item => new ProdutoTipoAgressividadeODN
                {
                    CodProdutoTipoAgressividade = item.CodProdutoTipoAgressividade,
                    Nome = item.Nome,
                    Descricao = item.Descricao
                })
                .ToList();
        }

        public ProdutoTipoAgressividadeODN ObterPorId(int codProdutoTipoAgressividade)
        {
            var odp = this.repositorio.Obter(codProdutoTipoAgressividade);
            return odp == null ? null : new ProdutoTipoAgressividadeODN
            {
                CodProdutoTipoAgressividade = odp.CodProdutoTipoAgressividade,
                Nome = odp.Nome,
                Descricao = odp.Descricao
            };
        }

        protected override object[] ObterID(ObjetoDeNegocio obj)
        {
            var item = obj as ProdutoTipoAgressividadeODN;
            return item != null && item.CodProdutoTipoAgressividade > 0 ? new object[] { item.CodProdutoTipoAgressividade } : null;
        }

        protected internal override void AntesDeSalvar(ProdutoTipoAgressividadeODP obj, bool inserindo)
        {
            base.AntesDeSalvar(obj, inserindo);

            if (inserindo)
            {
                obj.CodProdutoTipoAgressividade = this.repositorio.ObterProximoValorDeSequencia("TD_PRODUTOTIPOAGRESSIVIDADE_SEQ");
            }
        }
    }
}
