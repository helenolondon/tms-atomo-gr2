using AtmServiceDominio.ODN;
using System.Collections.Generic;

namespace AtmServiceDominio.ObjetosDeDominio.Marcas
{
    public interface IMarcasOD : IAtmObjetoDominio
    {
        List<MarcaODN> Listar();
        MarcaODN ObterPorId(int codMarca);
    }
}
