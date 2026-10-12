using AtmService;

namespace AtmServicei.Seguranca.Erros
{
    public class LoginInvalidoSE : AtmServiceExcecao
    {
        public LoginInvalidoSE(string mensagemDeErro) : base(mensagemDeErro, null)
        {
        }
    }
}
