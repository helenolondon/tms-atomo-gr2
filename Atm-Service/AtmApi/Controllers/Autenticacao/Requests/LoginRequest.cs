using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtmApi.Controllers.Autenticacao.Requests
{
    /// <summary>
    /// Request padrão de login
    /// </summary>
    public class LoginRequest
    {
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
