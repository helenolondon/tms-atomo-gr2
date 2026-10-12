using System.Linq;
using AtmApi.Controllers.Autenticacao.Requests;
using AtmApi.Controllers.Usuarios.Requests;
using AtmService.Erros;
using AtmService.ODT;
using AtmServiceDominio.ObjetosDeDominio.Seguranca.Erros;
using AtmServicei.Seguranca.Erros;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AtmApi.Controllers.Autenticacao
{
    /// <summary>
    /// Serviços de autenticação
    /// </summary>
    [Route("api/auth/[controller]")]
    [ApiController]
    public class AutenticacaoController : AtmController
    {
        /// <summary>
        /// Efetua Login no sistema.
        /// Configurado para avato template
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]LoginRequest Request)
        {
            return this.Executar(() => 
            {
                string token = null;

                var usuario = this.services.UsuariosService.ObterUsuarioPorLogin(Request.Login);

                try
                {
                    if (usuario == null)
                    {
                        throw new LoginInvalidoSE("Credenciais inválidas");
                    }
                    
                    this.services.SegurancaService.Login(Request.Login, Request.Senha, out token);
                    this.services.Commit();

                    return Ok(new LoginODT() { Usuario = usuario, Email = usuario.Email, Login = usuario.Login, Token = token });
                }
                catch (LoginInvalidoDE e)
                {
                    return Unauthorized(e.Message);
                }
                catch (LoginInvalidoSE e) 
                {
                    return Unauthorized(e.Message);
                }
            } );
        }

        /// <summary>
        /// Verifica validade do token de aceso
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Ok ou UnAuthoreized</returns>
        [HttpGet]
        [Route("verify")]
        public IActionResult Verify([FromHeader] string token)
        {
            return this.Executar(() => 
            {
                if (!this.services.SegurancaService.VerificarToken(token))
                {
                    return Unauthorized();
                }

                return Ok();
            });
        }

        /// <summary>
        /// Altera a senha de um Login
        /// </summary>
        /// <param name="Request">Tipo: SalvarUsuarioMetronicRequest</param>        
        [Route("alterar-senha")]
        [HttpPost]
        public IActionResult AlterarSenha([FromBody]AlterarSenhaRequest request)
        {
            return this.Executar(() =>
            {
                var temp = this.services.UsuariosService.ObterUsuarioPorLogin(request.Login);

                if(temp == null)
                {
                    throw new AtmServiceNaoEncontradoSE("Usuário não encontrado");
                }

                this.services.SegurancaService.AlterarSenha(temp.Login, request.NovaSenha, request.Senha, request.ConfirmaSenha);

                this.services.Commit();

                return Ok("Senha alterada com sucesso");
            });
        }
    }
}