using AtmServiceDominio.ODN;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ObjetosDeDominio.Usuarios
{
    public interface IUsuariosOD : IAtmObjetoDominio
    {
        List<UsuarioODN> ObterUsuarios(string nome, string email, string login, string telefone);
        UsuarioODN ObterUsuarioPorCodigo(int codigoUsuario);
        UsuarioODN ObterUsuarioPorLogin(string login);
    }
}
