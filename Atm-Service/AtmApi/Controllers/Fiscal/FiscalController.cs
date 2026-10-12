using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtmApi.Controllers.Autenticacao.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtmApi.Controllers
{
    [Route("api/[controller]")]
    [AutorizarUsuarioLogado]
    [ApiController]    
    public class FiscalController : AtmController
    {
        /// <summary>
        /// Retorna lista de cnaes
        /// </summary>
        /// <returns></returns>
        [HttpGet("cnaes")]
        public IActionResult ObterCnaes([FromQuery]string filtro)
        {
            return this.Executar(() =>
            {
                var odt = this.services.FiscalService.ListarCnaes(filtro);

                if (odt == null)
                {
                    return Ok();
                }

                return Ok(odt);
            });
        }

        [HttpGet("inscricao-estadual/situacoes")]
        public IActionResult ObterListaSituacaoDeIE()
        {
            return Ok(this.services.FiscalService.ObterTiposDeSituacaoDeInscricaoEstadual());
        }
    }
}
