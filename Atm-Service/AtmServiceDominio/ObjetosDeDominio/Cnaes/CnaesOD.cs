using AtmServiceDominio.ODN;
using AtmServiceInfra;
using AtmServiceInfra.Cidades;
using AtmServiceInfra.Cnaes;
using AtmServiceInfra.Estados;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceDominio.ObjetosDeDominio.Cnaes
{
    public class CnaesOD : AtmObjetoDominio<CnaeODP>, ICnaesOD
    {
        public CnaesOD(IRepositorio<CnaeODP> repositorio) : base(repositorio)
        {

        }

        public CnaeODN ObterCnaePorId(int codCnae)
        {
            var odp = this.repositorio.Obter(codCnae);

            if (odp == null)
            {
                return null;
            }

            var odn = new CnaeODN();

            odn.FromODP(odp);

            return odn;
        }

        public List<CnaeODN> ObterCnaesPorId(int codCnaeInicial, int? codCnaeFinal)
        {
            var odp = (this.repositorio as CnaesRepositorio).ObterCnaes(codCnaeInicial, codCnaeFinal);
            var odns = new List<CnaeODN>();

            if (odp == null || odp.Count == 0)
            {
                return null;
            }

            foreach (var item in odp)
            {
                var odn = new CnaeODN();
                odn.FromODP(item);

                odns.Add(odn);
            }

            odns.OrderBy(odn => odn.CodigoCnae);

            return odns;
        }

        public List<CnaeODN> ObterCnaes(string filtro)
        {
            var odp = (this.repositorio as CnaesRepositorio).ObterCnaes(filtro);
            var odns = new List<CnaeODN>();

            if (odp == null || odp.Count == 0)
            {
                return null;
            }

            foreach(var item in odp)
            {
                var odn = new CnaeODN();
                odn.FromODP(item);

                odns.Add(odn);
            }

            odns.OrderBy(odn => odn.CodigoCnae);

            return odns;
        }
    }
}
