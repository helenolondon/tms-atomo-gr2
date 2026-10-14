using AtmService.ODT;
using System.Collections.Generic;

namespace AtmService.Produtos
{
    public interface IProdutosService
    {
        List<ProdutoODT> Listar();
        ProdutoODT ObterPorId(int codProduto);
        ProdutoODT Salvar(ProdutoODT item);
        void Excluir(int codProduto);
    }
}
