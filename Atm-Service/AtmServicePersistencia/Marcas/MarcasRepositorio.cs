using AtmServiceInfra.EF;
using AtmServiceInfra.ODP;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceInfra.Marcas
{
    public class MarcasRepositorio : Repositorio<MarcaODP>, IMarcasRepositorio
    {
        public MarcasRepositorio(AtmDbContext context) : base(context)
        {
        }

        public List<MarcaODP> Listar()
        {
            return this.dbSet.OrderBy(marca => marca.Nome).ToList();
        }
    }
}
