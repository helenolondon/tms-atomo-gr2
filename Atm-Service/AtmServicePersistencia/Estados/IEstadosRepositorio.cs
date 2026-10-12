using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceInfra.Estados
{
    public interface IEstadosRepositorio: IRepositorio<EstadoODP>
    {
        public EstadoODP ObterEstadoPorId(int codigoEstado);
        public List<EstadoODP> ObterEstados();
    }
}
