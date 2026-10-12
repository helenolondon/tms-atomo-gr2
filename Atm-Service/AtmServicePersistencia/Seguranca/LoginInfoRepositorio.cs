using AtmServiceInfra.EF;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmServiceInfra.Seguranca
{
    public class LoginInfoRepositorio: Repositorio<LoginInfoODP>, ILoginInfoRepositorio
    {
        public LoginInfoRepositorio(AtmDbContext context) : base(context)
        {
        }

        public LoginInfoODP ObterLoginInfoPorLogin(string login)
        {
            return this.dbSet.Where(lgn => lgn.Login == login && (lgn.Ativo == 1))
                .OrderByDescending(lgn => lgn.DataCad)
                .SingleOrDefault();
        }
    }
}
