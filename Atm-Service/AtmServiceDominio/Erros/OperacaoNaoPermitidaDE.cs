namespace AtmServiceDominio.Erros
{
    public class OperacaoNaoPermitidaDE: DominioExcecao
    {
        public OperacaoNaoPermitidaDE(string message) : base(message)
        {
        }
    }
}
