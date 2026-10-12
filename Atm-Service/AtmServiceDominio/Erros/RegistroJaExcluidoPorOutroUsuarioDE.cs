namespace AtmServiceDominio.Erros
{
    public class RegistroJaExcluidoPorOutroUsuarioDE : DominioExcecao
    {
        public RegistroJaExcluidoPorOutroUsuarioDE(string message) : base(message)
        {
        }
    }
}
