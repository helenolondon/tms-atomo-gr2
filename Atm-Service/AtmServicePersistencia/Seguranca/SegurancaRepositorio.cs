using AtmServiceInfra.EF;
using AtmServiceInfra.ODP;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AtmServiceInfra.Seguranca
{
    public class SegurancaRepositorio: Repositorio<LoginODP>, ISegurancaRepositorio 
    {
        public SegurancaRepositorio(AtmDbContext context): base(context)
        {
        }

        public LoginODP ObterLoginPorLogin(string login)
        {
            return this.context.Logins.Where(lgn => lgn.Login == login).SingleOrDefault();
        }

        public string ObterSenhaPorLogin(string login)
        {
            var usu = context.Logins.Where(us => us.Login.ToUpper() == login.ToUpper()).SingleOrDefault();

            if (usu != null)
            {
                return usu.Senha;
            }

            return null;
        }
    }
}
