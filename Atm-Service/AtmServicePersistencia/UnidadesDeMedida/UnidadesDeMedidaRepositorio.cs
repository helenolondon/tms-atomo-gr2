using AtmServiceInfra.EF;
using AtmServiceInfra.ODP;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceInfra.UnidadesDeMedida
{
    public class UnidadesDeMedidaRepositorio : Repositorio<UnidadeDeMedidaODP>, IUnidadesDeMedidaRepositorio
    {
        public UnidadesDeMedidaRepositorio(AtmDbContext context) : base(context)
        {
        }

        public List<UnidadeDeMedidaODP> Listar()
        {
            return this.dbSet.OrderBy(item => item.Nome).ToList();
        }
    }
}
