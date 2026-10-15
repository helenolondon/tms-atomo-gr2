using AtmServiceDominio.ODN;
using System.Collections.Generic;

namespace AtmServiceDominio.ObjetosDeDominio.ServicoCategorias
{
    public interface IServicoCategoriasOD : IAtmObjetoDominio
    {
        List<ServicoCategoriaODN> Listar();
        ServicoCategoriaODN ObterPorId(int codServicoCategoria);
    }
}
