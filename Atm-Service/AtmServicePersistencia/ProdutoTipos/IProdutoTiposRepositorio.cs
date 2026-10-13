using AtmServiceInfra.ODP;
using System.Collections.Generic;

namespace AtmServiceInfra.ProdutoTipos
{
    public interface IProdutoTiposRepositorio : IRepositorio<ProdutoTipoODP>
    {
        List<ProdutoTipoODP> Listar();
    }
}
