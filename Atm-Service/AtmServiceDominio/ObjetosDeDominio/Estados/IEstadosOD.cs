using AtmServiceDominio.ODN;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ObjetosDeDominio.Estados
{
    public interface IEstadosOD : IAtmObjetoDominio
    {
        public EstadoODN ObterEstadoPorId(int CodigoEstado);
        public List<EstadoODN> ObterEstados();
    }
}
