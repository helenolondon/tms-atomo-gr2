using AtmServiceInfra.EF;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmServiceInfra.Logradouros
{
    public class LogradourosRepositorio: Repositorio<LogradouroODP>, ILogradourosRepositorio
    {
        public LogradourosRepositorio(AtmDbContext context): base(context)
        {

        }

        public LogradouroODP ObterPorCep(string cep)
        {
            var odp = context.Logradouros.Where(l => l.Cep == cep).FirstOrDefault();

            if(odp == null)
            {
                return null;
            }

            return odp;
        }
    }
}
