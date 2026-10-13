using AtmServiceInfra.ODP;
using System.Collections.Generic;

namespace AtmServiceInfra.Marcas
{
    public interface IMarcasRepositorio : IRepositorio<MarcaODP>
    {
        List<MarcaODP> Listar();
    }
}
