using AtmServiceInfra.EF;
using AtmServiceInfra.ODP;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceInfra.ProdutoTipoAgressividades
{
    public class ProdutoTipoAgressividadeRepositorio : Repositorio<ProdutoTipoAgressividadeODP>, IProdutoTipoAgressividadeRepositorio
    {
        public ProdutoTipoAgressividadeRepositorio(AtmDbContext context) : base(context)
        {
        }

        public List<ProdutoTipoAgressividadeODP> Listar()
        {
            return this.dbSet.OrderBy(item => item.Nome).ToList();
        }
    }
}
