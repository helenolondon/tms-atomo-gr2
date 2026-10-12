using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ODN
{
    public class CnaeODN: ObjetoDeNegocio
    {
        public int CodigoCnae { get; set; }
        public string DescricaoCnae { get; set; }

        internal override ObjetoDePersistencia ToODP(ObjetoDePersistencia destino = null)
        {
            CnaeODP odp = destino != null ? destino as CnaeODP : new CnaeODP();

            odp.CodigoCnae = this.CodigoCnae;
            odp.DescricaoCnae = this.DescricaoCnae;

            return base.ToODP(odp);
        }

        internal override void FromODP(ObjetoDePersistencia obj)
        {
            if (obj == null)
            {
                return;
            }

            var odp = obj as CnaeODP;

            this.CodigoCnae = odp.CodigoCnae;
            this.DescricaoCnae = odp.DescricaoCnae;

            base.FromODP(odp);
        }
    }
}
