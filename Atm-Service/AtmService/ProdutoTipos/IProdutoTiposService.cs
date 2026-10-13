using AtmService.ODT;
using System.Collections.Generic;

namespace AtmService.ProdutoTipos
{
    public interface IProdutoTiposService
    {
        List<ProdutoTipoODT> Listar();
        ProdutoTipoODT ObterPorId(int codProdutoTipo);
        ProdutoTipoODT Salvar(ProdutoTipoODT item);
        void Excluir(int codProdutoTipo);
    }
}
