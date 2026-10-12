using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ODN
{
    public class CidadeODN: ObjetoDeNegocio
    {
        public int CodigoCidade { get; set; }
        public int CodigoEstado { get; set; }
        public string NomeCidade { get; set; }

        internal override void FromODP(ObjetoDePersistencia obj)
        {
            var odp = obj as CidadeODP;

            if(odp == null)
            {
                return;
            }

            this.CodigoCidade = odp.CodigoCidade;
            this.CodigoEstado = odp.CodigoEstado;
            this.NomeCidade = odp.NomeCidade;
        }

        internal override ObjetoDePersistencia ToODP(ObjetoDePersistencia destino = null)
        {
            CidadeODP odp = destino as CidadeODP;

            if(odp == null)
            {
                odp = new CidadeODP();
            }

            odp.CodigoCidade = this.CodigoCidade;
            odp.CodigoEstado = this.CodigoEstado;
            odp.NomeCidade = this.NomeCidade;

            return odp;
        }
    }
}
