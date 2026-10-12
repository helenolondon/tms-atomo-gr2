using AtmServiceDominio.Erros;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AtmServiceDominio.ObjetosDeDominio.Seguranca.Erros
{
    public class LoginInvalidoDE : DominioExcecao
    {
        public LoginInvalidoDE(string mensagem): base(mensagem) { }
    }
}
