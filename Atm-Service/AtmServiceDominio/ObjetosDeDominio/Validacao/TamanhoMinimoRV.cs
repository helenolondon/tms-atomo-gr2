using AtmServiceDominio.ObjetosDeNegocio;

namespace AtmServiceDominio.ObjetosDeDominio.Validacao
{
    public class TamanhoMinimoRV : RegraDeValidacao
    {
        private readonly int tam;

        public TamanhoMinimoRV(ObjetoDeNegocio obj, string campo, int tamanhoMinimo, string mensagemDeErro) : 
            base(obj, campo, mensagemDeErro)
        {
            this.tam = tamanhoMinimo;
        }

        public override bool Validar()
        {
            var valorProp = this[campo];

            if (valorProp is string)
            {
                return (valorProp as string).Length >= this.tam;
            }

            if (valorProp is null)
            {
                return true;
            }

            return false;
        }
    }
}
