using AtmServiceDominio.ODN;
using System.Collections.Generic;

namespace AtmServiceDominio.ObjetosDeDominio.Produtos
{
    public interface IProdutosOD : IAtmObjetoDominio
    {
        List<ProdutoODN> Listar();
        ProdutoODN ObterPorId(int codProduto);
    }
}
