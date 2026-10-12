using AtmServiceInfra.EF;
using AtmServiceInfra.Erros;
using AtmServiceInfra.ODP;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceInfra.Clientes
{
    public class UsuariosRepositorio: Repositorio<UsuarioODP>, IUsuariosRepositorio
    {
        public UsuariosRepositorio(AtmDbContext context): base(context)
        {
            
        }

        /// <summary>
        /// Retorna lista de usuários por nome
        /// </summary>
        /// <param name="nome"></param>
        public List<UsuarioODP> ObterUsuarios(string nome, string email, string login, string telefone)
        {
            IQueryable<UsuarioODP> temp = null;

            temp = this.dbSet
                .Where(usu => string.IsNullOrEmpty(nome) || usu.Nome.ToUpper().Contains(nome.ToUpper()))
                .Where(usu => string.IsNullOrEmpty(email) || usu.Email.ToUpper().Contains(email.ToUpper()))
                .Where(usu => string.IsNullOrEmpty(login) || usu.Login.ToUpper().Contains(login.ToUpper()))
                .Where(usu => string.IsNullOrEmpty(telefone) || usu.Telefone.ToUpper().Contains(telefone.ToUpper()));

            if (temp != null && temp.Count() > 0)
            {
                return temp.ToList();
            }

            return null;
        }

        /// <summary>
        /// Retorna usuário por login
        /// </summary>
        /// <param name="login"></param>
        public UsuarioODP ObterUsuarioPorLogin(string login)
        {
            var cons = this.dbSet.Where(usu => usu.Login.ToUpper() == login.ToUpper()).ToList();

            if (cons != null)
            {
                if (cons.Count() > 1)
                {
                    throw new MaisDeUmaOcorrenciaEncontradaPE();
                }
                else if (cons.Count() == 1)
                {
                    return cons[0];
                }
            }

            return null;
        }
    }
}
