using AtmService.ODT;
using System.Collections.Generic;

namespace AtmService.ServicoCategorias
{
    public interface IServicoCategoriasService
    {
        List<ServicoCategoriaODT> Listar();
        ServicoCategoriaODT ObterPorId(int codServicoCategoria);
        ServicoCategoriaODT Salvar(ServicoCategoriaODT item);
        void Excluir(int codServicoCategoria);
    }
}
