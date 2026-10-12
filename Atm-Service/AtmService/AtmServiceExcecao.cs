using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService
{
    public class AtmServiceExcecao: Exception
    {
        private readonly List<string> _erros = new List<string>();

        public List<string> Erros { get { return _erros; } }

        public AtmServiceExcecao(string mensagemDeErro, List<string> erros): base(mensagemDeErro)
        {
            this._erros = erros ?? new List<string>();
        }
    }
}
