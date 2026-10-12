using System;
using System.Collections.Generic;
using AtmApi.Controllers.Autenticacao.ActionFilters;
using AtmApi.Controllers.Usuarios.Requests;
using AtmService.Erros;
using AtmService.ODT;
using DocumentFormat.OpenXml.Office2016.Excel;
using Microsoft.AspNetCore.Mvc;

namespace AtmApi.Controllers.Usuarios
{
    /// <summary>
    /// Operações de manutenção dos usuários do sistema
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : AtmController
    {
        /// <summary>
        /// Retorna lista de usuários cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet("{codUsuario}")]
        [AutorizarUsuarioLogado]
        public IActionResult ObterUsuarioPorId(int codUsuario)
        {
            return this.Executar(() =>
            {
                var odt = this.services.UsuariosService.ObterUsuarioPorCodigo(codUsuario);

                if (odt == null)
                {
                    return NotFound($"Usuário {codUsuario} não existe");
                }

                return Ok(odt);
            });
        }

        /// <summary>
        /// Retorna lista de usuários cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AutorizarUsuarioLogado]
        public IActionResult ListarUsuarios([FromQuery] string? nome, [FromQuery] string? email, [FromQuery] string? telefone, [FromQuery] string? login) => this.Executar(() =>
        {
            return Ok(this.services.UsuariosService.Listar(nome, email, login, telefone));
        });

        /// <summary>
        /// Registra usuário
        /// </summary>
        /// <param name="Request">Tipo: SalvarUsuarioMetronicRequest</param>        
        [HttpPost("registrar")]
        public IActionResult RegistrarUsuario(RegistrarUsuarioRequest Request)
        {
            return this.Executar(() =>
            {
                var temp = this.services.UsuariosService.RegistrarUsuario(Request.ODT());
                this.services.Commit();

                return Ok(temp);
            });
        }

        /// <summary>
        /// Salva usuário configurado para Metronic Templa
        /// </summary>
        /// <param name="Request">Tipo: SalvarUsuarioMetronicRequest</param>        
        [HttpPost]
        [AutorizarUsuarioLogado]
        public IActionResult SalvarUsuario(SalvarUsuarioRequest Request)
        {
            return this.Executar(() =>
            {
                var temp = this.services.UsuariosService.SalvarUsuario(Request.ODT());

                this.services.Commit();

                return Ok(temp);
            });
        }

        /// <summary>
        /// Salva usuário configurado para Metronic Templa
        /// </summary>
        /// <param name="Request">Tipo: SalvarUsuarioMetronicRequest</param>        
        [HttpPost]
        [Route("novo-usuario")]
        [AutorizarUsuarioLogado]
        public IActionResult SalvarNovoUsuario(SalvarNovoUsuarioRequest request)
        {
            return this.Executar(() =>
            {
                if (request.Id > 0)
                {
                    throw new AtmServiceValidacaoExcecao("Não é permitido especificar um código para um novo usuário");
                }

                var temp = this.services.UsuariosService.SalvarUsuario(request.ODT());

                this.services.SegurancaService.AlterarSenha(temp.Login, request.Senha, null, null);

                this.services.Commit();

                return Ok(temp);
            });
        }

        /// <summary>
        /// Faz a exclusão de um usuário do sistema
        /// </summary>
        /// <param name="codUsuario"></param>
        [HttpDelete("{codUsuario}")]
        [AutorizarUsuarioLogado]
        public IActionResult ExcluirUsuario(int codUsuario)
        {
            return this.Executar(() =>
            {
                this.services.UsuariosService.Excluir(codUsuario);
                this.services.Commit();

                return Ok();
            });
        }

        /// <summary>
        /// Retorna usuário por login
        /// </summary>
        /// <param name="login"></param>
        [HttpGet("por-login/{login}")]
        [AutorizarUsuarioLogado]
        public IActionResult ObterUsuarioPorLogin(string login)
        {
            return this.Executar(() =>
            {
                var resp = this.services.UsuariosService.ObterUsuarioPorLogin(login);

                if (resp == null)
                {
                    return NotFound();
                }

                return Ok(resp);
            });
        }
    }
}