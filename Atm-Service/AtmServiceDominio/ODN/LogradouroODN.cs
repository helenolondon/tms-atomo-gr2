using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ODN
{
    public class LogradouroODN: ObjetoDeNegocio
    {
        public int CodLogradouro { get; set; }
        public int CodBairro { get; set; }
        public string NomeLogradouro { get; set; }
        public string Cep { get; set; }
        public string Tipo { get; set; }

        internal override void FromODP(ObjetoDePersistencia obj)
        {
            base.FromODP(obj);
            var odp = obj as LogradouroODP;

            this.CodLogradouro = odp.CodigoLogradouro;
            this.CodBairro = odp.CodigoBairro;
            this.NomeLogradouro = odp.NomeLogradouro;
            this.Cep = odp.Cep;
            this.Tipo = odp.Tipo;
        }

        internal override ObjetoDePersistencia ToODP(ObjetoDePersistencia destino = null)
        {
            base.ToODP(destino);
            
            var odp = destino as LogradouroODP;

            odp.CodigoLogradouro = this.CodLogradouro;
            odp.CodigoBairro = this.CodBairro;
            odp.NomeLogradouro = this.NomeLogradouro;
            odp.Cep = this.Cep;
            odp.Tipo = this.Tipo;

            return odp;
        }
    }
}
