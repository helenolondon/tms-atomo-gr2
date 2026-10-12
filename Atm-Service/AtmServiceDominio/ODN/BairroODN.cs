using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ODN
{
    public class BairroODN: ObjetoDeNegocio
    {
        public int CodigoBairro { get; set; }
        public int CodigoCidade { get; set; }
        public string NomeBairro { get; set; }

        internal override ObjetoDePersistencia ToODP(ObjetoDePersistencia destino = null)
        {
            BairroODP odp = destino != null ? destino as BairroODP: new BairroODP();

            odp.CodigoBairro = this.CodigoBairro;
            odp.CodigoCidade = this.CodigoCidade;
            odp.NomeBairro = this.NomeBairro;

            return base.ToODP(odp);
        }

        internal override void FromODP(ObjetoDePersistencia obj)
        {
            if(obj == null)
            {
                return;
            }

            var odp = obj as BairroODP;

            this.CodigoCidade = odp.CodigoCidade;
            this.CodigoBairro = odp.CodigoBairro;
            this.NomeBairro = odp.NomeBairro;
            
            base.FromODP(odp);
        }
    }
}
