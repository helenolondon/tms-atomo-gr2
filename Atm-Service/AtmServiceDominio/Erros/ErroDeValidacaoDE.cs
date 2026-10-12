using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.Erros
{
    public class ErroDeValidacaoDE: DominioExcecao
    {
        internal ErroDeValidacaoDE(string mesangem) : base(mesangem)
        {

        }
    }
}
