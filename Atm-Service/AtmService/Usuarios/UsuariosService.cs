using System.Collections.Generic;
using AtmService.ODT;
using AtmServiceDominio.ObjetosDeDominio.Usuarios;
using AtmServiceDominio.ODN;
using AtmService.Utils;

using AtmServiceDominio.ObjetosDeDominio.Sistema;
using AtmServiceDominio.ObjetosDeDominio.Seguranca;

namespace AtmService.Usuarios
{
    public class UsuariosService : AtmService, IUsuariosService
    {
        private IUsuariosOD usuariosOD = null;
        private ISegurancaOD segurancaOD = null;
        private ISistemaOD sistemaOD = null;

        public UsuariosService(IUsuariosOD usuariosOD, ISegurancaOD segurancaOD, ISistemaOD sistemaOD)
        {
            this.usuariosOD = usuariosOD;
            this.segurancaOD = segurancaOD;
            this.sistemaOD = sistemaOD;
        }

        public UsuarioODT SalvarUsuario(UsuarioODT temp)
        {
            var odn = temp.ToUsuarioODN();

            this.ExecutarAcaoDeDominio(() =>
            {
                odn = this.usuariosOD.Salvar(odn) as UsuarioODN;
            });

            return UsuarioODT.NewFromODN(odn);
        }
        
        public UsuarioODT RegistrarUsuario(UsuarioODT temp)
        {
            var odn = temp.ToUsuarioODN();
            string senha = this.segurancaOD.GerarSenhaSegura();

            if(this.usuariosOD.ObterUsuarioPorLogin(temp.Login) != null)
            {
                throw new Erros.AtmServiceValidacaoExcecao("Já existem um cadastrado com este email");
            };

            this.ExecutarAcaoDeDominio(() =>
            {
                odn = this.usuariosOD.Salvar(odn) as UsuarioODN;
            });

            this.ExecutarAcaoDeDominio(() =>
            {
                var loginODN = this.segurancaOD.ObterLoginPorLogin(odn.Login);

                if (loginODN == null)
                {
                    loginODN = new LoginODN()
                    {
                        Login = odn.Login,
                        Senha = senha
                    };
                }
                else
                {
                    throw new Erros.AtmServiceValidacaoExcecao("Já existem um usuário cadastrado com este email");
                };

                this.ExecutarAcaoDeDominio(() =>
                {
                    this.segurancaOD.SalvarLogin(loginODN);
                });

                this.ExecutarAcaoDeDominio(() => 
                {
                    try
                    {
                        this.sistemaOD.SendEmail(temp.Email,
                            ObterRegistrarUsuárioHtmlTemplate().Replace("#login#", odn.Login).Replace("#senha#", senha),
                            "Confirmação de registro",
                            true);
                    }
                    catch (System.Exception)
                    {
                        throw new Erros.AtmServiceValidacaoExcecao("Não foi possível enviar o e-mail de confirmação, favor tentar novamente");
                        
                    }
                });
            });

            return UsuarioODT.NewFromODN(odn);
        }

        public UsuarioODT ObterUsuarioPorCodigo(int codigoUsuario)
        {
            UsuarioODN odn = null;

            this.ExecutarAcaoDeDominio(() => 
            {
                odn = this.usuariosOD.ObterUsuarioPorCodigo(codigoUsuario);
            });

            if (odn != null)
            {
                return UsuarioODT.NewFromODN(odn);
            }

            return null;
        }

        public List<UsuarioODT> Listar(string nome, string email, string login, string telefone)
        {
            List<UsuarioODN> usuarios = null;

            this.ExecutarAcaoDeDominio(() => 
            {
                usuarios = this.usuariosOD.ObterUsuarios(nome, email, login, telefone);
            });
            
            if (usuarios != null)
            {
                var temp = new List<UsuarioODT>();
                return temp.FromListaODN(usuarios);
            }

            return null;
        }

        public UsuarioODT ObterUsuarioPorLogin(string login)
        {
            UsuarioODN odn = null;

            this.ExecutarAcaoDeDominio(() => 
            {
                odn = this.usuariosOD.ObterUsuarioPorLogin(login);
            });

            if (odn != null)
            {
                var odt = UsuarioODT.NewFromODN(odn);
                return odt; 
            }

            return null;
        }

        public void Excluir(int codUsuario)
        {
            this.ExecutarAcaoDeDominio(() => 
            {
                this.usuariosOD.Excluir(codUsuario);
            });
        }

        private string ObterRegistrarUsuárioHtmlTemplate()
        {
            return "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n  <meta charset=utf-8>\r\n  <title>Informações de Login</title>\r\n  <style>\r\n    #content {\r\n      width: 650px;\r\n      height: 350px;\r\n      margin-left: auto;\r\n      margin-right: auto;\r\n      font-family: arial,helvetica,sans-serif;\r\n      font-size: 14px;\r\n      line-height: 20px;\r\n      padding-top: 30px;\r\n    }\r\n  </style>\r\n</head>\r\n<body>\r\n    <div id=\"content\">\r\n        <p>\r\n            <strong>Bem vindo!</strong>\r\n            <hr />\r\n            Seu registro no site foi finalizado com sucesso. Abaixo estão suas credenciais geradas automaticamente para acessar o sistema. <br />\r\n        </p>\r\n        <div id=\"loginSenha\">\r\n            <strong>Login:</strong> #login#\r\n            <br />\r\n            <strong>Senha</strong>: #senha#\r\n        </div>\r\n\r\n        <hr />\r\n        Qualquer dúvida, favor entrar em contato com nossa equipe.\r\n    </div>\r\n</body>\r\n</html>";
        }
    }
}
