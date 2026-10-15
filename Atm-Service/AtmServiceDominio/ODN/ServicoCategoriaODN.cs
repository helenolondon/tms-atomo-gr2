using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra.ODP;

namespace AtmServiceDominio.ODN
{
    public class ServicoCategoriaODN : ObjetoDeNegocio
    {
        public int CodServicoCategoria { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        internal override ObjetoDePersistencia ToODP(ObjetoDePersistencia destino = null)
        {
            var odp = destino as ServicoCategoriaODP ?? new ServicoCategoriaODP();
            odp.CodServicoCategoria = this.CodServicoCategoria;
            odp.Nome = this.Nome;
            odp.Descricao = this.Descricao;
            return base.ToODP(odp);
        }

        internal override void FromODP(ObjetoDePersistencia obj)
        {
            if (obj == null)
            {
                return;
            }

            var odp = obj as ServicoCategoriaODP;
            this.CodServicoCategoria = odp.CodServicoCategoria;
            this.Nome = odp.Nome;
            this.Descricao = odp.Descricao;
            base.FromODP(odp);
        }
    }
}
