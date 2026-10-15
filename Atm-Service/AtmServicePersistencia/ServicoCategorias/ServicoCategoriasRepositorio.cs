using AtmServiceInfra.EF;
using AtmServiceInfra.ODP;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceInfra.ServicoCategorias
{
    public class ServicoCategoriasRepositorio : Repositorio<ServicoCategoriaODP>, IServicoCategoriasRepositorio
    {
        public ServicoCategoriasRepositorio(AtmDbContext context) : base(context)
        {
        }

        public List<ServicoCategoriaODP> Listar()
        {
            return this.dbSet.OrderBy(item => item.Nome).ToList();
        }
    }
}
