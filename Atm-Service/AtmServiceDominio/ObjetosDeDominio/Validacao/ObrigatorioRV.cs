using AtmServiceDominio.ObjetosDeNegocio;
using System;

namespace AtmServiceDominio.ObjetosDeDominio.Validacao
{
    public class ObrigatorioRV : RegraDeValidacao
    {
        public ObrigatorioRV(ObjetoDeNegocio obj, string campo, string mensagemDeErro) : 
            base(obj, campo, mensagemDeErro)
        {
        }

        public override bool Validar()
        {
            var valorProp = this[campo];

            if (valorProp is int)
            {
                return (valorProp as int?).Value != 0;
            }
            
            if (valorProp is DateTime)
            {
                return (valorProp as DateTime?) != null;
            }

            if (valorProp is int)
            {
                return (valorProp as int?).Value != 0;
            }

            if (valorProp is long)
            {
                return (valorProp as long?).Value != 0;
            }

            if (valorProp is decimal)
            {
                return (valorProp as decimal?).Value != 0;
            }

            if (valorProp is double)
            {
                return (valorProp as double?).Value != 0;
            }
            
            if (valorProp is string)
            {
                return !string.IsNullOrEmpty(valorProp as string);
            }

            return valorProp != null;
        }
    }
}
