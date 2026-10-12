using AtmServiceInfra;
using AtmServiceInfra.Utils;

namespace AtmServiceDominio.ObjetosDeDominio.Sistema
{
    public class SistemaOD: AtmObjetoDominio, ISistemaOD
    {
        private readonly IInfraResources infraResources;

        public SistemaOD(IInfraResources infraResources)
        {
            this.infraResources = infraResources;
        }

        public void SendEmail(string destinatario, string mensagem, string assunto, bool html)
        {
            this.infraResources.Comunicacao.SendEmail(destinatario, mensagem, assunto, html);
        }
    }
}
