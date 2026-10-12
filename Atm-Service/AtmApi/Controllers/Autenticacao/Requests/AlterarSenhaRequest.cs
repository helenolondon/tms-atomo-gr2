using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtmApi.Controllers.Autenticacao.Requests
{
    public class AlterarSenhaRequest
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public string NovaSenha { get; set; }
        public string ConfirmaSenha { get; set; }
    }
}
