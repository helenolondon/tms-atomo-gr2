using AtmService.ODT;
using System.Collections.Generic;

namespace AtmService.UnidadesDeMedida
{
    public interface IUnidadesDeMedidaService
    {
        List<UnidadeDeMedidaODT> Listar();
        UnidadeDeMedidaODT ObterPorId(int codUnidadeDeMedida);
        UnidadeDeMedidaODT Salvar(UnidadeDeMedidaODT item);
        void Excluir(int codUnidadeDeMedida);
    }
}
