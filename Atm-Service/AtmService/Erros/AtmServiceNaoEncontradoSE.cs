using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.Erros
{
    public class AtmServiceNaoEncontradoSE: AtmServiceExcecao
    {
        public AtmServiceNaoEncontradoSE(string mensagemDeErro) : base(mensagemDeErro, null)
        {
        }
    }
}
