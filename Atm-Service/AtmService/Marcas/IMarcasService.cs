using AtmService.ODT;
using System.Collections.Generic;

namespace AtmService.Marcas
{
    public interface IMarcasService
    {
        List<MarcaODT> Listar();
        MarcaODT ObterPorId(int codMarca);
        MarcaODT Salvar(MarcaODT marca);
        void Excluir(int codMarca);
    }
}
