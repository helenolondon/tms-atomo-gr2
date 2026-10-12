using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.Erros
{
    public class DominioExcecao : Exception
    {
        public DominioExcecao(string message) : base(message)
        {
        }
    }
}
