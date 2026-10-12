using AtmServiceDominio.ODN;
using AtmServiceInfra;
using AtmServiceInfra.Cidades;
using AtmServiceInfra.Estados;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;

namespace AtmServiceDominio.ObjetosDeDominio.Cidades
{
    public class CidadesOD : AtmObjetoDominio<CidadeODP>, ICidadesOD
    {
        public CidadesOD(IRepositorio<CidadeODP> repositorio) : base(repositorio)
        {

        }

        public CidadeODN ObterCidadePorId(int codCidade)
        {
            var odp = this.repositorio.Obter(codCidade);

            if (odp == null)
            {
                return null;
            }

            var odn = new CidadeODN();

            odn.FromODP(odp);

            return odn;
        }
        
        public List<CidadeODN> ObterCidadesPorEstado(int codEstado)
        {
            var odp = (this.repositorio as CidadesRepositorio).ObterCidadesPorEstado(codEstado);
            var odns = new List<CidadeODN>();

            if (odp == null || odp.Count == 0)
            {
                return null;
            }

            foreach(var item in odp)
            {
                var odn = new CidadeODN();
                odn.FromODP(item);

                odns.Add(odn);
            }

            return odns;
        }

        public List<CidadeODN> ObterCidades(string nomeCidade, int? codigoEstado)
        {
            var odp = (this.repositorio as CidadesRepositorio).ObterCidades(nomeCidade, codigoEstado);
            var odns = new List<CidadeODN>();

            if (odp == null || odp.Count == 0)
            {
                return null;
            }

            foreach (var item in odp)
            {
                var odn = new CidadeODN();
                odn.FromODP(item);

                odns.Add(odn);
            }

            return odns;
        }
    }
}
