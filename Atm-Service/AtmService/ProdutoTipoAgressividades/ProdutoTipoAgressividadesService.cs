using AtmService.ODT;
using AtmServiceDominio.ObjetosDeDominio.ProdutoTipoAgressividades;
using AtmServiceDominio.ODN;
using System.Collections.Generic;
using System.Linq;

namespace AtmService.ProdutoTipoAgressividades
{
    public class ProdutoTipoAgressividadesService : AtmService, IProdutoTipoAgressividadesService
    {
        private readonly IProdutoTipoAgressividadeOD produtoTipoAgressividadeOD;

        public ProdutoTipoAgressividadesService(IProdutoTipoAgressividadeOD produtoTipoAgressividadeOD)
        {
            this.produtoTipoAgressividadeOD = produtoTipoAgressividadeOD;
        }

        public List<ProdutoTipoAgressividadeODT> Listar()
        {
            return this.produtoTipoAgressividadeOD.Listar().Select(item => new ProdutoTipoAgressividadeODT(item)).ToList();
        }

        public ProdutoTipoAgressividadeODT ObterPorId(int codProdutoTipoAgressividade)
        {
            ProdutoTipoAgressividadeODT resultado = null;
            this.ExecutarAcaoDeDominio(() =>
            {
                var item = this.produtoTipoAgressividadeOD.ObterPorId(codProdutoTipoAgressividade);
                if (item != null)
                {
                    resultado = new ProdutoTipoAgressividadeODT(item);
                }
            });
            return resultado;
        }

        public ProdutoTipoAgressividadeODT Salvar(ProdutoTipoAgressividadeODT item)
        {
            ProdutoTipoAgressividadeODT resultado = null;
            this.ExecutarAcaoDeDominio(() =>
            {
                resultado = new ProdutoTipoAgressividadeODT(this.produtoTipoAgressividadeOD.Salvar(item.ToODN()) as ProdutoTipoAgressividadeODN);
            });
            return resultado;
        }

        public void Excluir(int codProdutoTipoAgressividade)
        {
            this.ExecutarAcaoDeDominio(() => this.produtoTipoAgressividadeOD.Excluir(codProdutoTipoAgressividade));
        }
    }
}
