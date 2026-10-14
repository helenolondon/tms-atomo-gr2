using AtmServiceDominio.ODN;
using System.Collections.Generic;

namespace AtmServiceDominio.ObjetosDeDominio.ProdutoGrupos
{
    public interface IProdutoGruposOD : IAtmObjetoDominio
    {
        List<ProdutoGrupoODN> Listar();
        ProdutoGrupoODN ObterPorId(int codProdutoGrupo);
    }
}
