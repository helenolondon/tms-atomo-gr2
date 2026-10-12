using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.Erros
{
    public class AtmServiceOperacaoInvalidaExcecao : AtmServiceExcecao
    {
        public AtmServiceOperacaoInvalidaExcecao(string mensagemDeErro) : base(mensagemDeErro, null)
        {
        }
    }
}
