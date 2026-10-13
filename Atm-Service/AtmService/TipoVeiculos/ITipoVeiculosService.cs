using AtmService.ODT;
using System.Collections.Generic;

namespace AtmService.TipoVeiculos
{
    public interface ITipoVeiculosService
    {
        List<TipoVeiculoODT> Listar();
        TipoVeiculoODT ObterPorId(int codTipoVeiculo);
        TipoVeiculoODT Salvar(TipoVeiculoODT item);
        void Excluir(int codTipoVeiculo);
    }
}
