using AtmService.ODT;
using System.Collections.Generic;

namespace AtmService.ProdutoGrupos
{
    public interface IProdutoGruposService
    {
        List<ProdutoGrupoODT> Listar();
        ProdutoGrupoODT ObterPorId(int codProdutoGrupo);
        ProdutoGrupoODT Salvar(ProdutoGrupoODT item);
        void Excluir(int codProdutoGrupo);
    }
}
