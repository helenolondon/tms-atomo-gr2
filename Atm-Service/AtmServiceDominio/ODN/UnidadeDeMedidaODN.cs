using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra.ODP;

namespace AtmServiceDominio.ODN
{
    public class UnidadeDeMedidaODN : ObjetoDeNegocio
    {
        public int CodUnidadeDeMedida { get; set; }
        public string Nome { get; set; }
        public string Unidade { get; set; }

        internal override ObjetoDePersistencia ToODP(ObjetoDePersistencia destino = null)
        {
            var odp = destino as UnidadeDeMedidaODP ?? new UnidadeDeMedidaODP();
            odp.CodUnidadeDeMedida = this.CodUnidadeDeMedida;
            odp.Nome = this.Nome;
            odp.Unidade = this.Unidade;
            return base.ToODP(odp);
        }

        internal override void FromODP(ObjetoDePersistencia obj)
        {
            if (obj == null)
            {
                return;
            }

            var odp = obj as UnidadeDeMedidaODP;
            this.CodUnidadeDeMedida = odp.CodUnidadeDeMedida;
            this.Nome = odp.Nome;
            this.Unidade = odp.Unidade;
            base.FromODP(odp);
        }
    }
}
