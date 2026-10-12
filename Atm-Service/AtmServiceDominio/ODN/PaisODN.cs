using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ODN
{
    public class PaisODN: ObjetoDeNegocio
    {
        public int Codigo { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }

        internal override void FromODP(ObjetoDePersistencia obj)
        {
            var odp = obj as PaisODP;

            if (obj == null)
            {
                return;
            };

            this.Codigo = odp.CodigoPais;
            this.Sigla = odp.SiglaPais;
            this.Nome = odp.NomePais;
        }
    }
}
