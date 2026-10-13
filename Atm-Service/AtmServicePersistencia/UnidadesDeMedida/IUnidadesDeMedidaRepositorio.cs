using AtmServiceInfra.ODP;
using System.Collections.Generic;

namespace AtmServiceInfra.UnidadesDeMedida
{
    public interface IUnidadesDeMedidaRepositorio : IRepositorio<UnidadeDeMedidaODP>
    {
        List<UnidadeDeMedidaODP> Listar();
    }
}
