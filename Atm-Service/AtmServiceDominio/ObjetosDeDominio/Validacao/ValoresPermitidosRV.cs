using AtmServiceDominio.ObjetosDeNegocio;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;

namespace AtmServiceDominio.ObjetosDeDominio.Validacao
{
    public class ValoresPermitidosRV : RegraDeValidacao
    {
        private readonly object[] valores;

        public ValoresPermitidosRV(ObjetoDeNegocio obj, string campo, object[] valores, string mensagemDeErro) : 
            base(obj, campo, mensagemDeErro)
        {
            this.MensagemDeErro = mensagemDeErro;
            this.valores = valores;
        }

        public override bool Validar()
        {
            var valorProp = this[campo];

            return this.valores.IndexOf(valorProp) != -1;
        }
    }
}
