using AtmServiceInfra.ODP;

namespace AtmServiceInfra.Seguranca
{
    public interface ISegurancaRepositorio: IRepositorio<LoginODP>
    {
        string ObterSenhaPorLogin(string login);
        LoginODP ObterLoginPorLogin(string login);
    }
}
