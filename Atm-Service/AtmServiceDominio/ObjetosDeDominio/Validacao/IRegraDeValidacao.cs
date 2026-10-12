namespace AtmServiceDominio.ObjetosDeDominio.Validacao
{    
    public interface IRegraDeValidacao
    {
        string MensagemDeErro { get; set; }
        bool Validar();
    }
}