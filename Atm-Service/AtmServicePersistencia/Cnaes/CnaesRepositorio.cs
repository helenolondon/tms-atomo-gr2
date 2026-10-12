using AtmServiceInfra.EF;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmServiceInfra.Cnaes
{
    public class CnaesRepositorio : Repositorio<CnaeODP>, ICnaesRepositorio
    {
        public CnaesRepositorio(AtmDbContext context) : base(context)
        {

        }

        public List<CnaeODP> ObterCnaes(int codCnaeInicial, int? codCnaeFinal)
        {
            IQueryable<CnaeODP> temp = null;

            temp = this.dbSet.Where(cnn => cnn.CodigoCnae >= codCnaeInicial && (codCnaeFinal == null || cnn.CodigoCnae <= codCnaeFinal));

            if (temp != null)
            {
                return temp.OrderBy(cnt => cnt.CodigoCnae).ToList();
            }

            return null;
        }

        public List<CnaeODP> ObterCnaes(string filtro)
        {
            IQueryable<CnaeODP> temp = null;

            if (string.IsNullOrEmpty(filtro))
            {
                temp = this.dbSet;
            }
            else
            {
                temp = this.dbSet.Where(cnt => cnt.DescricaoCnae.ToUpper().Contains(filtro.ToUpper()) ||
                    cnt.CodigoCnae.ToString().Contains(filtro.ToUpper()));
            }

            if (temp != null)
            {
                return temp.OrderBy(cnt => cnt.DescricaoCnae).ToList();
            }

            return null;
        }
    }
}
