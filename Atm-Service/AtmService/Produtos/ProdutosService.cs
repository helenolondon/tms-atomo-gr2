using AtmService.ODT;
using AtmServiceDominio.ObjetosDeDominio.Produtos;
using AtmServiceDominio.ODN;
using System.Collections.Generic;
using System.Linq;

namespace AtmService.Produtos
{
    public class ProdutosService : AtmService, IProdutosService
    {
        private readonly IProdutosOD produtosOD;

        public ProdutosService(IProdutosOD produtosOD)
        {
            this.produtosOD = produtosOD;
        }

        public List<ProdutoODT> Listar()
        {
            return this.produtosOD.Listar().Select(item => new ProdutoODT(item)).ToList();
        }

        public ProdutoODT ObterPorId(int codProduto)
        {
            ProdutoODT resultado = null;
            this.ExecutarAcaoDeDominio(() =>
            {
                var item = this.produtosOD.ObterPorId(codProduto);
                if (item != null)
                {
                    resultado = new ProdutoODT(item);
                }
            });
            return resultado;
        }

        public ProdutoODT Salvar(ProdutoODT item)
        {
            ProdutoODT resultado = null;
            this.ExecutarAcaoDeDominio(() =>
            {
                resultado = new ProdutoODT(this.produtosOD.Salvar(item.ToODN()) as ProdutoODN);
            });
            return resultado;
        }

        public void Excluir(int codProduto)
        {
            this.ExecutarAcaoDeDominio(() => this.produtosOD.Excluir(codProduto));
        }
    }
}
