using AtmServiceInfra.ODP;
using System.Collections.Generic;

namespace AtmServiceInfra.TipoVeiculos
{
    public interface ITipoVeiculosRepositorio : IRepositorio<TipoVeiculoODP>
    {
        List<TipoVeiculoODP> Listar();
    }
}
