using AtmServiceInfra.EF;
using AtmServiceInfra.ODP;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceInfra.ProdutoGrupos
{
    public class ProdutoGruposRepositorio : Repositorio<ProdutoGrupoODP>, IProdutoGruposRepositorio
    {
        public ProdutoGruposRepositorio(AtmDbContext context) : base(context)
        {
        }

        public List<ProdutoGrupoODP> Listar()
        {
            return this.dbSet.OrderBy(item => item.Nome).ToList();
        }
    }
}
