using AtmServiceDominio.ODN;
using AtmServiceDominio.Utils;
using AtmServiceInfra;
using AtmServiceInfra.Estados;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ObjetosDeDominio.Estados
{
    public class EstadosOD: AtmObjetoDominio<EstadoODP>, IEstadosOD
    {
        public EstadosOD(IRepositorio<EstadoODP> repositorio) : base(repositorio)
        {
        }

        public EstadoODN ObterEstadoPorId(int codigoEstado)
        {
            var odp = this.repositorio.Obter(codigoEstado);

            if(odp == null)
            {
                return null;
            }

            var odn = new EstadoODN();
            odn.FromODP(odp);

            return odn;
        }

        public List<EstadoODN> ObterEstados()
        {
            var odps = (this.repositorio as EstadosRepositorio).ObterEstados();
            var ret = new List<EstadoODN>();

            if (odps == null || odps.Count == 0)
            {
                return ret;
            };

            return ret.FromListaODP(odps);
        }
    }
}
