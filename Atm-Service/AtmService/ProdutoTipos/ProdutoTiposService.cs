using AtmService.ODT;
using AtmServiceDominio.ObjetosDeDominio.ProdutoTipos;
using AtmServiceDominio.ODN;
using System.Collections.Generic;
using System.Linq;

namespace AtmService.ProdutoTipos
{
    public class ProdutoTiposService : AtmService, IProdutoTiposService
    {
        private readonly IProdutoTiposOD produtoTiposOD;

        public ProdutoTiposService(IProdutoTiposOD produtoTiposOD)
        {
            this.produtoTiposOD = produtoTiposOD;
        }

        public List<ProdutoTipoODT> Listar()
        {
            return this.produtoTiposOD.Listar().Select(item => new ProdutoTipoODT(item)).ToList();
        }

        public ProdutoTipoODT ObterPorId(int codProdutoTipo)
        {
            ProdutoTipoODT resultado = null;
            this.ExecutarAcaoDeDominio(() =>
            {
                var item = this.produtoTiposOD.ObterPorId(codProdutoTipo);
                if (item != null)
                {
                    resultado = new ProdutoTipoODT(item);
                }
            });
            return resultado;
        }

        public ProdutoTipoODT Salvar(ProdutoTipoODT item)
        {
            ProdutoTipoODT resultado = null;
            this.ExecutarAcaoDeDominio(() =>
            {
                resultado = new ProdutoTipoODT(this.produtoTiposOD.Salvar(item.ToODN()) as ProdutoTipoODN);
            });
            return resultado;
        }

        public void Excluir(int codProdutoTipo)
        {
            this.ExecutarAcaoDeDominio(() => this.produtoTiposOD.Excluir(codProdutoTipo));
        }
    }
}
