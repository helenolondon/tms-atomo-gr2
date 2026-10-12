namespace AtmServiceDominio.ObjetosDeDominio.Sistema
{
    public interface ISistemaOD: IAtmObjetoDominio
    {
        void SendEmail(string destinatario, string mensagem, string assunto, bool html);
    }
}
