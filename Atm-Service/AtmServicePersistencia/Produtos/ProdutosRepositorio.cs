using AtmServiceInfra.EF;
using AtmServiceInfra.ODP;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceInfra.Produtos
{
    public class ProdutosRepositorio : Repositorio<ProdutoODP>, IProdutosRepositorio
    {
        public ProdutosRepositorio(AtmDbContext context) : base(context)
        {
        }

        public int ContarProdutosPorMarca(int codMarca)
        {
            return this.dbSet.Count(p => p.CodMarca == codMarca);
        }

        public int ContarProdutosPorTipo(int codProdutoTipo)
        {
            return this.dbSet.Count(p => p.CodProdutoTipo == codProdutoTipo);
        }

        public int ContarProdutosPorUnidadeDeMedida(int codUnidadeDeMedida)
        {
            return this.dbSet.Count(p => p.CodUnidadeDeMedida == codUnidadeDeMedida);
        }

        public int ContarProdutosPorGrupo(int codProdutoGrupo)
        {
            return this.dbSet.Count(p => p.CodProdutoGrupo == codProdutoGrupo);
        }

        public List<ProdutoODP> Listar()
        {
            return this.dbSet.OrderBy(item => item.Descricao).ToList();
        }
    }
}
