using AtmServiceInfra.ODP;
using System.Collections.Generic;

namespace AtmServiceInfra.Produtos
{
    public interface IProdutosRepositorio : IRepositorio<ProdutoODP>
    {
        List<ProdutoODP> Listar();
        int ContarProdutosPorMarca(int codMarca);
        int ContarProdutosPorTipo(int codProdutoTipo);
        int ContarProdutosPorUnidadeDeMedida(int codUnidadeDeMedida);
        int ContarProdutosPorGrupo(int codProdutoGrupo);
    }
}