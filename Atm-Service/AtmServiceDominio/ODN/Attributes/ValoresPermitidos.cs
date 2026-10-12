using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ODN.Attributes
{
    internal class ValoresPermitidosAttribute: System.Attribute
    {
        public string MensagemDeErro { get; set; }
        public object[] Valores { get; set; }

        public ValoresPermitidosAttribute(string mensagemDeErro)
        {
            MensagemDeErro = mensagemDeErro;    
            //Valores = valores;
        }
    }
}
