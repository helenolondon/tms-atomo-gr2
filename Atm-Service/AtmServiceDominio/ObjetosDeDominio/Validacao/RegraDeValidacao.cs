using AtmServiceDominio.ObjetosDeNegocio;

namespace AtmServiceDominio.ObjetosDeDominio.Validacao
{
    public class RegraDeValidacao : IRegraDeValidacao
    {
        private readonly ObjetoDeNegocio obj;
        protected readonly string campo;

        public string MensagemDeErro { get; set; }

        public RegraDeValidacao(ObjetoDeNegocio obj, string campo, string mensagemDeErro)
        {
            this.obj = obj;
            this.campo = campo;
            this.MensagemDeErro = mensagemDeErro;
        }

        protected object this[string campo]
        {
            get
            {
                var propInfo = this.obj.GetType().GetProperty(campo);

                if (propInfo == null)
                {
                    return null;
                }

                return propInfo.GetValue(this.obj, null);
            }
        }

        public virtual bool Validar()
        {
            return true;
        }
    }
}
