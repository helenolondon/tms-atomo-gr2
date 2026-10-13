using AtmServiceInfra.ODP;
using System.Collections.Generic;

namespace AtmServiceInfra.ProdutoTipoAgressividades
{
    public interface IProdutoTipoAgressividadeRepositorio : IRepositorio<ProdutoTipoAgressividadeODP>
    {
        List<ProdutoTipoAgressividadeODP> Listar();
    }
}
