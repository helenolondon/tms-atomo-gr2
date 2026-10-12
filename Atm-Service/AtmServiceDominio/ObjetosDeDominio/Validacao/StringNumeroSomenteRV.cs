using AtmServiceDominio.ObjetosDeNegocio;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ObjetosDeDominio.Validacao
{
    internal class StringNumeroSomenteRV : RegraDeValidacao
    {
        internal StringNumeroSomenteRV(ObjetoDeNegocio obj, string campo, string mensagemDeErro) :
           base(obj, campo, mensagemDeErro)
        {
        }

        public override bool Validar()
        {
            var valorProp = this[campo];
            var valido = true;

            if (valorProp is string)
            {
                var strValor = valorProp.ToString();

                foreach(var c in strValor)
                {
                    if("0123456789".IndexOf(c) == -1)
                    {
                        valido = false;
                        break;
                    }
                }
            }

            return valido;
        }
    }
}
