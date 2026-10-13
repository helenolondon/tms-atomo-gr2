using AtmServiceDominio.ODN;
using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra;
using AtmServiceInfra.ODP;
using AtmServiceInfra.TipoVeiculos;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceDominio.ObjetosDeDominio.TipoVeiculos
{
    public class TipoVeiculosOD : AtmObjetoDominio<TipoVeiculoODP>, ITipoVeiculosOD
    {
        public TipoVeiculosOD(ITipoVeiculosRepositorio repositorio) : base(repositorio)
        {
        }

        public List<TipoVeiculoODN> Listar()
        {
            return (this.repositorio as ITipoVeiculosRepositorio)
                .Listar()
                .Select(item => new TipoVeiculoODN
                {
                    CodTipoVeiculo = item.CodTipoVeiculo,
                    Descricao = item.Descricao
                })
                .ToList();
        }

        public TipoVeiculoODN ObterPorId(int codTipoVeiculo)
        {
            var odp = this.repositorio.Obter(codTipoVeiculo);
            return odp == null ? null : new TipoVeiculoODN
            {
                CodTipoVeiculo = odp.CodTipoVeiculo,
                Descricao = odp.Descricao
            };
        }

        protected override object[] ObterID(ObjetoDeNegocio obj)
        {
            var item = obj as TipoVeiculoODN;
            return item != null && item.CodTipoVeiculo > 0 ? new object[] { item.CodTipoVeiculo } : null;
        }

        protected internal override void AntesDeSalvar(TipoVeiculoODP obj, bool inserindo)
        {
            base.AntesDeSalvar(obj, inserindo);

            if (inserindo)
            {
                obj.CodTipoVeiculo = this.repositorio.ObterProximoValorDeSequencia("TD_TIPOVEICULO_SEQ");
            }
        }
    }
}
