using AtmServiceDominio.ODN;
using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra;
using AtmServiceInfra.ODP;
using AtmServiceInfra.UnidadesDeMedida;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceDominio.ObjetosDeDominio.UnidadesDeMedida
{
    public class UnidadesDeMedidaOD : AtmObjetoDominio<UnidadeDeMedidaODP>, IUnidadesDeMedidaOD
    {
        public UnidadesDeMedidaOD(IUnidadesDeMedidaRepositorio repositorio) : base(repositorio)
        {
        }

        public List<UnidadeDeMedidaODN> Listar()
        {
            return (this.repositorio as IUnidadesDeMedidaRepositorio)
                .Listar()
                .Select(item => new UnidadeDeMedidaODN
                {
                    CodUnidadeDeMedida = item.CodUnidadeDeMedida,
                    Nome = item.Nome,
                    Unidade = item.Unidade
                })
                .ToList();
        }

        public UnidadeDeMedidaODN ObterPorId(int codUnidadeDeMedida)
        {
            var odp = this.repositorio.Obter(codUnidadeDeMedida);
            return odp == null ? null : new UnidadeDeMedidaODN
            {
                CodUnidadeDeMedida = odp.CodUnidadeDeMedida,
                Nome = odp.Nome,
                Unidade = odp.Unidade
            };
        }

        protected override object[] ObterID(ObjetoDeNegocio obj)
        {
            var item = obj as UnidadeDeMedidaODN;
            return item != null && item.CodUnidadeDeMedida > 0 ? new object[] { item.CodUnidadeDeMedida } : null;
        }

        protected internal override void AntesDeSalvar(UnidadeDeMedidaODP obj, bool inserindo)
        {
            base.AntesDeSalvar(obj, inserindo);

            if (inserindo)
            {
                obj.CodUnidadeDeMedida = this.repositorio.ObterProximoValorDeSequencia("TD_UNIDADESDEMEDIDA_SEQ");
            }
        }
    }
}
