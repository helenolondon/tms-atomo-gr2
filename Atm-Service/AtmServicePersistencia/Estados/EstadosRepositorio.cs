using AtmServiceInfra.EF;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmServiceInfra.Estados
{
    public class EstadosRepositorio : Repositorio<EstadoODP>, IEstadosRepositorio
    {
        public EstadosRepositorio(AtmDbContext context) : base(context)
        {
        }

        public EstadoODP ObterEstadoPorId(int codigoEstado)
        {
            var odp = this.ObterPorID(codigoEstado);

            if (odp == null)
            {
                return null;
            }

            return odp;
        }
        
        public List<EstadoODP> ObterEstados()
        {
            var odp = this.dbSet.ToList()?.OrderBy(e => e.UF)?.ToList();

            if (odp == null)
            {
                return null;
            }

            return odp;
        }
    }
}
