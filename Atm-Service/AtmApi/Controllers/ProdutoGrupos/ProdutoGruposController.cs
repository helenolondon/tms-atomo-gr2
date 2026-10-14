using AtmApi.Controllers.Autenticacao.ActionFilters;
using AtmApi.Controllers.Autenticacao.ActionFilters;
using AtmService.ODT;
using Microsoft.AspNetCore.Mvc;

namespace AtmApi.Controllers.ProdutoGrupos
{
    [Route("api/produtogrupos")]
    [ApiController]
    //[AutorizarUsuarioLogado]
    public class ProdutoGruposController : AtmController
    {
        [HttpGet]
        public IActionResult ListarProdutoGrupos()
        {
            return this.Executar(() => Ok(this.services.ProdutoGruposService.Listar()));
        }

        [HttpGet("{codProdutoGrupo:int}")]
        public IActionResult ObterProdutoGrupo(int codProdutoGrupo)
        {
            return this.Executar(() =>
            {
                var item = this.services.ProdutoGruposService.ObterPorId(codProdutoGrupo);
                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);
            });
        }

        [HttpPost]
        public IActionResult SalvarProdutoGrupo(ProdutoGrupoODT request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            return this.Executar(() =>
            {
                var item = this.services.ProdutoGruposService.Salvar(request);
                this.services.Commit();
                return Ok(item);
            });
        }

        [HttpDelete("{codProdutoGrupo:int}")]
        public IActionResult ExcluirProdutoGrupo(int codProdutoGrupo)
        {
            return this.Executar(() =>
            {
                this.services.ProdutoGruposService.Excluir(codProdutoGrupo);
                this.services.Commit();
                return NoContent();
            });
        }
    }
}