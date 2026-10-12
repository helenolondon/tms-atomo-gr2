using AtmServiceInfra.EF;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmServiceInfra.Contatos
{
    public class ContatosRepositorio : Repositorio<ContatoODP>, IContatosRepositorio
    {
        public ContatosRepositorio(AtmDbContext context) : base(context)
        {

        }

        public List<ContatoODP> ObterContatosPorNome(string nome)
        {
            IQueryable<ContatoODP> temp = null;

            if (string.IsNullOrEmpty(nome))
            {
                temp = this.dbSet;
            }
            else
            {
                temp = this.dbSet.Where(cnt => cnt.Nome.ToUpper().Contains(nome.ToUpper()));
            }

            if (temp != null)
            {
                return temp.OrderBy(cnt => cnt.Nome).ToList();
            }

            return null;
        }
    }
}
