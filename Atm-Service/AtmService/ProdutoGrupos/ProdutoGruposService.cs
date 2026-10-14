using AtmService.ODT;
using AtmServiceDominio.ObjetosDeDominio.ProdutoGrupos;
using AtmServiceDominio.ODN;
using System.Collections.Generic;
using System.Linq;

namespace AtmService.ProdutoGrupos
{
    public class ProdutoGruposService : AtmService, IProdutoGruposService
    {
        private readonly IProdutoGruposOD produtoGruposOD;

        public ProdutoGruposService(IProdutoGruposOD produtoGruposOD)
        {
            this.produtoGruposOD = produtoGruposOD;
        }

        public List<ProdutoGrupoODT> Listar()
        {
            return this.produtoGruposOD.Listar().Select(item => new ProdutoGrupoODT(item)).ToList();
        }

        public ProdutoGrupoODT ObterPorId(int codProdutoGrupo)
        {
            ProdutoGrupoODT resultado = null;
            this.ExecutarAcaoDeDominio(() =>
            {
                var item = this.produtoGruposOD.ObterPorId(codProdutoGrupo);
                if (item != null)
                {
                    resultado = new ProdutoGrupoODT(item);
                }
            });
            return resultado;
        }

        public ProdutoGrupoODT Salvar(ProdutoGrupoODT item)
        {
            ProdutoGrupoODT resultado = null;
            this.ExecutarAcaoDeDominio(() =>
            {
                resultado = new ProdutoGrupoODT(this.produtoGruposOD.Salvar(item.ToODN()) as ProdutoGrupoODN);
            });
            return resultado;
        }

        public void Excluir(int codProdutoGrupo)
        {
            this.ExecutarAcaoDeDominio(() => this.produtoGruposOD.Excluir(codProdutoGrupo));
        }
    }
}
