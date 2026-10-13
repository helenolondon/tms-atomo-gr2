using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra.ODP;

namespace AtmServiceDominio.ODN
{
    public class TipoVeiculoODN : ObjetoDeNegocio
    {
        public int CodTipoVeiculo { get; set; }
        public string Descricao { get; set; }

        internal override ObjetoDePersistencia ToODP(ObjetoDePersistencia destino = null)
        {
            var odp = destino as TipoVeiculoODP ?? new TipoVeiculoODP();
            odp.CodTipoVeiculo = this.CodTipoVeiculo;
            odp.Descricao = this.Descricao;
            return base.ToODP(odp);
        }

        internal override void FromODP(ObjetoDePersistencia obj)
        {
            if (obj == null)
            {
                return;
            }

            var odp = obj as TipoVeiculoODP;
            this.CodTipoVeiculo = odp.CodTipoVeiculo;
            this.Descricao = odp.Descricao;
            base.FromODP(odp);
        }
    }
}
