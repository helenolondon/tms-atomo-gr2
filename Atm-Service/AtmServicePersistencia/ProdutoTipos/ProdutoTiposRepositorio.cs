using AtmServiceInfra.EF;
using AtmServiceInfra.ODP;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceInfra.ProdutoTipos
{
    public class ProdutoTiposRepositorio : Repositorio<ProdutoTipoODP>, IProdutoTiposRepositorio
    {
        public ProdutoTiposRepositorio(AtmDbContext context) : base(context)
        {
        }

        public List<ProdutoTipoODP> Listar()
        {
            return this.dbSet.OrderBy(item => item.Nome).ToList();
        }
    }
}
