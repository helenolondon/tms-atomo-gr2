using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ODN
{
    public class EstadoODN: ObjetoDeNegocio
    {
        public int CodigoEstado { get; set; }
        public string NomeEstado { get; set; }
        public string UF { get; set; }

        internal override void FromODP(ObjetoDePersistencia obj)
        {
            if(obj == null)
            {
                return;
            }

            var odp = obj as EstadoODP;

            this.CodigoEstado = odp.CodigoEstado;
            this.NomeEstado = odp.NomeEstado;
            this.UF = odp.UF;

            base.FromODP(obj);
        }

        internal override ObjetoDePersistencia ToODP(ObjetoDePersistencia destino = null)
        {
            EstadoODP odp = destino != null ? destino as EstadoODP : new EstadoODP();
            
            odp.CodigoEstado = this.CodigoEstado;
            odp.NomeEstado = this.NomeEstado;
            odp.UF = this.UF;

            return odp;
        }
    }
}
