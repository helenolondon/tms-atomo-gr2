using AtmServiceInfra.ODP;
using System.Collections.Generic;

namespace AtmServiceInfra.ProdutoGrupos
{
    public interface IProdutoGruposRepositorio : IRepositorio<ProdutoGrupoODP>
    {
        List<ProdutoGrupoODP> Listar();
    }
}
