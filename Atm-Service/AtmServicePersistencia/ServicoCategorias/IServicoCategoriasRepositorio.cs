using AtmServiceInfra.ODP;
using System.Collections.Generic;

namespace AtmServiceInfra.ServicoCategorias
{
    public interface IServicoCategoriasRepositorio : IRepositorio<ServicoCategoriaODP>
    {
        List<ServicoCategoriaODP> Listar();
    }
}
