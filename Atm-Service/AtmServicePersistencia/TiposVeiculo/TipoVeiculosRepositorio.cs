using AtmServiceInfra.EF;
using AtmServiceInfra.ODP;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceInfra.TipoVeiculos
{
    public class TipoVeiculosRepositorio : Repositorio<TipoVeiculoODP>, ITipoVeiculosRepositorio
    {
        public TipoVeiculosRepositorio(AtmDbContext context) : base(context)
        {
        }

        public List<TipoVeiculoODP> Listar()
        {
            return this.dbSet.OrderBy(item => item.Descricao).ToList();
        }
    }
}
