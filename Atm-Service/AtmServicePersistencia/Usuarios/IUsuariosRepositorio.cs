using AtmServiceInfra;
using AtmServiceInfra.ODP;
using System.Collections.Generic;

namespace AtmServiceInfra
{
    public interface IUsuariosRepositorio: IRepositorio<UsuarioODP>
    {
        List<UsuarioODP> ObterUsuarios(string nome, string email, string login, string telefone);
        UsuarioODP ObterUsuarioPorLogin(string login);
    }
}
