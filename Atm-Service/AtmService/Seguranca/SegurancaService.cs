using AtmService.Erros;
using AtmService.ODT;
using AtmServiceDominio.ObjetosDeDominio.Seguranca;
using AtmServiceDominio.ODN;
using AtmServicei.Seguranca.Erros;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.Seguranca
{
    public class SegurancaService: AtmService, ISegurancaService
    {
        private readonly ISegurancaOD segurancaOD;

        public SegurancaService(ISegurancaOD segurancaOD)
        {
            this.segurancaOD = segurancaOD;
        }

        public string GerarSenhaSegura()
        {
            return this.segurancaOD.GerarSenhaSegura();
        }

        public void Login(string login, string senha, out string token)
        {
            LoginODN loginOdn = null;
            token = null;
            var tokenOut = "";

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(senha))
            {
                throw new LoginInvalidoSE("Nome de usuário ou senha não podem estar vazios");
            }

            this.ExecutarAcaoDeDominio(() => 
            {
                loginOdn = this.segurancaOD.Login(login, senha, out tokenOut);
            });

            if (loginOdn == null)
            {
                throw new LoginInvalidoSE("Login inválido");
            }

            token = tokenOut;
        }

        /// <summary>
        /// Verifica a validade de um token
        /// </summary>
        public bool VerificarToken(string token)
        {
            return this.segurancaOD.TokenValido(token);
        }

        /// <summary>
        /// Altera senha de um Login
        /// </summary>
        /// <param name="login"></param>
        /// <param name="novaSenha"></param>
        public void AlterarSenha(string login, string novaSenha, string senhaAntiga, string confirmaSenha)
        {
            var loginODN = this.segurancaOD.ObterLoginPorLogin(login);

            if (loginODN == null)
            {
                loginODN = new LoginODN()
                {
                    Login = login,
                    Senha = novaSenha
                };
                
                this.ExecutarAcaoDeDominio(() => 
                {
                    this.segurancaOD.SalvarLogin(loginODN);
                });                
            }
            else
            {
                if (novaSenha != confirmaSenha)
                {
                    throw new AtmServiceValidacaoExcecao("A nova senha não é igual à senha de confirmação");
                }

                this.ExecutarAcaoDeDominio(() => 
                {
                    if(loginODN.Senha != segurancaOD.Criptografar(senhaAntiga))
                    {
                        throw new AtmServiceValidacaoExcecao("Senha atual incorreta");
                    };
                    
                    this.segurancaOD.AlteraSenhaUsuario(novaSenha, loginODN);
                });                
            }            
        }
    }
}
