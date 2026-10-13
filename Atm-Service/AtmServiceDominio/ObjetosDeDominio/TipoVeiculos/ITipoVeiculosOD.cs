using AtmServiceDominio.ODN;
using System.Collections.Generic;

namespace AtmServiceDominio.ObjetosDeDominio.TipoVeiculos
{
    public interface ITipoVeiculosOD : IAtmObjetoDominio
    {
        List<TipoVeiculoODN> Listar();
        TipoVeiculoODN ObterPorId(int codTipoVeiculo);
    }
}
