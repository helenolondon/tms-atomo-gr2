using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ODN.Attributes
{
    internal class ObrigatorioAttribute: System.Attribute
    {
        public string MensagemDeErro { get; set; }

        public ObrigatorioAttribute(string mensagemDeErro)
        {
            MensagemDeErro = mensagemDeErro;    
        }
    }
}
