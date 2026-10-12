using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AtmApi.Controllers.Autenticacao.ActionFilters;
using AtmApi.Controllers.Contatos.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace AtmApi.Controllers.Contatos
{
    [Route("api/[controller]")]
    [ApiController]
    [AutorizarUsuarioLogado]
    public class ContatosController : AtmController
    {
        /// <summary>
        /// Retorna lista de contatos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ListarContatos()
        {
            return this.Executar(() =>
            {
                var odts = this.services.ContatosService.ListarTodos();

                if(odts == null)
                {
                    return NotFound();
                }

                return Ok(odts);
            });
        }

        /// <summary>
        /// Faz a exclusão de um usuário do sistema
        /// </summary>
        /// <param name="codUsuario"></param>
        [HttpGet("excluir/{codContato}")]
        public IActionResult ExcluirUsuario(int codContato)
        {
            return this.Executar(() =>
            {
                this.services.ContatosService.Excluir(codContato);
                this.services.Commit();

                return Ok();
            });
        }

        /// <summary>
        /// Salva contato
        /// </summary>
        /// <param name="Request">Tipo: SalvarContatoRequest</param>        
        [HttpPost]
        public IActionResult SalvarContato(SalvarContatoRequest Request)
        {
            return this.Executar(() =>
            {
                var temp = this.services.ContatosService.SalvarContato(Request.ODT());
                this.services.Commit();

                return Ok(temp);
            });
        }
    }
}
