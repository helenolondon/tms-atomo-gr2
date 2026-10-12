using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ODN.Attributes
{
    internal class TamanhoMinimoAttribute : System.Attribute
    {
        public int Tamanho { get; set; }
        public string MensagemDeErro { get; set; }

        public TamanhoMinimoAttribute(string mensagemDeErro, int Tamanho)
        {
            MensagemDeErro = mensagemDeErro;    
            this.Tamanho = Tamanho;
        }
    }
}
