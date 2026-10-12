using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.Erros
{
    public class AtmServiceValidacaoExcecao : AtmServiceExcecao
    {
        public AtmServiceValidacaoExcecao(string mensagemDeErro) : base(mensagemDeErro, null)
        {
        }

        public AtmServiceValidacaoExcecao(string mensagemDeErro, List<string> erros): base(mensagemDeErro, erros)
        {   
        }
    }
}
