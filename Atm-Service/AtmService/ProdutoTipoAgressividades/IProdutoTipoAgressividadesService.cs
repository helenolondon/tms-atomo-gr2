using AtmService.ODT;
using System.Collections.Generic;

namespace AtmService.ProdutoTipoAgressividades
{
    public interface IProdutoTipoAgressividadesService
    {
        List<ProdutoTipoAgressividadeODT> Listar();
        ProdutoTipoAgressividadeODT ObterPorId(int codProdutoTipoAgressividade);
        ProdutoTipoAgressividadeODT Salvar(ProdutoTipoAgressividadeODT item);
        void Excluir(int codProdutoTipoAgressividade);
    }
}
