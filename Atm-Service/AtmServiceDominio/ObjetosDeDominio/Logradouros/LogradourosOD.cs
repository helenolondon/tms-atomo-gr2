using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceDominio.ODN;
using AtmServiceInfra;
using AtmServiceInfra.Logradouros;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ObjetosDeDominio.Logradouros
{
    public class LogradourosOD: AtmObjetoDominio<LogradouroODP>, ILogradourosOD
    {
        public LogradourosOD(IRepositorio<LogradouroODP> repositorio): base(repositorio)
        {
            
        }

        public LogradouroODN ObterPorCep(string cep)
        {
            if (string.IsNullOrEmpty(cep))
            {
                return null;
            }

            cep = cep.Replace(".", null);
            cep = cep.Replace("-", null);

            var odp = (this.repositorio as ILogradourosRepositorio).ObterPorCep(cep);

            if (odp == null)
            {
                return null;
            }

            var odn = new LogradouroODN();
            odn.FromODP(odp);

            return odn;
        }

        protected override object[] ObterID(ObjetoDeNegocio obj)
        {
            var codLogradouro = (obj as LogradouroODN).CodLogradouro;

            if (codLogradouro == 0)
            {
                return null;
            }

            return new object[] { codLogradouro };
        }
    }
}
