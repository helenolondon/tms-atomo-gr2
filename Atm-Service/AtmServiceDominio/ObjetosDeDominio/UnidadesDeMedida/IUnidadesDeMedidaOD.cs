using AtmServiceDominio.ODN;
using System.Collections.Generic;

namespace AtmServiceDominio.ObjetosDeDominio.UnidadesDeMedida
{
    public interface IUnidadesDeMedidaOD : IAtmObjetoDominio
    {
        List<UnidadeDeMedidaODN> Listar();
        UnidadeDeMedidaODN ObterPorId(int codUnidadeDeMedida);
    }
}
