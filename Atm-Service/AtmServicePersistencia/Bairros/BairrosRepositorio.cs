using AtmServiceInfra.EF;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmServiceInfra.Bairros
{
    public class BairrosRepositorio : Repositorio<BairroODP>, IBairrosRepositorio
    {
        public BairrosRepositorio(AtmDbContext context) : base(context)
        { }

        public List<BairroODP> ObterBairroPorCidade(int codCidade)
        {
            var odps = this.dbSet.Where(bairro => bairro.CodigoCidade == codCidade);

            if (odps == null || odps.Count() == 0)
            {
                return null;
            }

            return odps.OrderBy(bairro => bairro.NomeBairro).ToList();
        }
    }
}
