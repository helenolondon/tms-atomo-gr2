using AtmServiceDominio.ODN;
using System.Collections.Generic;

namespace AtmServiceDominio.ObjetosDeDominio.ProdutoTipoAgressividades
{
    public interface IProdutoTipoAgressividadeOD : IAtmObjetoDominio
    {
        List<ProdutoTipoAgressividadeODN> Listar();
        ProdutoTipoAgressividadeODN ObterPorId(int codProdutoTipoAgressividade);
    }
}
