using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra.ODP;

namespace AtmServiceDominio.ODN
{
    public class MarcaODN : ObjetoDeNegocio
    {
        public int CodMarca { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }

        internal override ObjetoDePersistencia ToODP(ObjetoDePersistencia destino = null)
        {
            var odp = destino as MarcaODP ?? new MarcaODP();
            odp.CodMarca = this.CodMarca;
            odp.Nome = this.Nome;
            odp.Descricao = this.Descricao;
            odp.Observacao = this.Observacao;
            return base.ToODP(odp);
        }

        internal override void FromODP(ObjetoDePersistencia obj)
        {
            if (obj == null)
            {
                return;
            }

            var odp = obj as MarcaODP;
            this.CodMarca = odp.CodMarca;
            this.Nome = odp.Nome;
            this.Descricao = odp.Descricao;
            this.Observacao = odp.Observacao;
            base.FromODP(odp);
        }
    }
}
