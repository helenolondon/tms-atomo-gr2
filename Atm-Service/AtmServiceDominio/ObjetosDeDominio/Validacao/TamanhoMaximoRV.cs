using AtmServiceDominio.ObjetosDeNegocio;

namespace AtmServiceDominio.ObjetosDeDominio.Validacao
{
    public class TamanhoMaximoRV : RegraDeValidacao
    {
        private readonly int tam;

        public TamanhoMaximoRV(ObjetoDeNegocio obj, string campo, int tamanhoMaximo, string mensagemDeErro) : 
            base(obj, campo, mensagemDeErro)
        {
            this.tam = tamanhoMaximo;
        }

        public override bool Validar()
        {
            var valorProp = this[campo];

            if (valorProp is string)
            {
                return (valorProp as string).Length <= this.tam;
            }

            if(valorProp is null)
            {
                return true;
            }

            return false;
        }
    }
}
