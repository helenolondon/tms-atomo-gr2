using AtmServiceDominio.ODN;
using System.Collections.Generic;

namespace AtmServiceDominio.ObjetosDeDominio.ProdutoTipos
{
    public interface IProdutoTiposOD : IAtmObjetoDominio
    {
        List<ProdutoTipoODN> Listar();
        ProdutoTipoODN ObterPorId(int codProdutoTipo);
    }
}
