using AtmServiceDominio.ODN;

namespace AtmServiceDominio.ObjetosDeDominio.Seguranca
{
    public interface ISegurancaOD: IAtmObjetoDominio
    {
        string Criptografar(string senha);
        void AlteraSenhaUsuario(string senha, LoginODN login);
        LoginODN SalvarLogin(LoginODN login);

        LoginODN Login(string login, string senha, out string token);
        LoginODN ObterLoginPorLogin(string login);
        bool TokenValido(string token);
        string GerarTokenParaLogin(string login);
        string GerarSenhaSegura();
    }
}
