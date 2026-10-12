using AtmService.ODT;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.Usuarios
{
    public interface IUsuariosService
    {
        UsuarioODT SalvarUsuario(UsuarioODT temp);
        void Excluir(int codUsuario);

        List<UsuarioODT> Listar(string nome, string email, string login, string telefone);
        UsuarioODT ObterUsuarioPorCodigo(int codigoUsuario);
        UsuarioODT ObterUsuarioPorLogin(string login);
        UsuarioODT RegistrarUsuario(UsuarioODT temp);
    }
}
