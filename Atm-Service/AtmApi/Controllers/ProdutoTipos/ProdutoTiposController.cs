using AtmApi.Controllers.Autenticacao.ActionFilters;
using AtmService.ODT;
using Microsoft.AspNetCore.Mvc;

namespace AtmApi.Controllers.ProdutoTipos
{
    [Route("api/produtotipos")]
    [ApiController]
    //[AutorizarUsuarioLogado]
    public class ProdutoTiposController : AtmController
    {
        [HttpGet]
        public IActionResult ListarProdutoTipos()
        {
            return this.Executar(() => Ok(this.services.ProdutoTiposService.Listar()));
        }

        [HttpGet("{codProdutoTipo:int}")]
        public IActionResult ObterProdutoTipo(int codProdutoTipo)
        {
            return this.Executar(() =>
            {
                var item = this.services.ProdutoTiposService.ObterPorId(codProdutoTipo);
                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);
            });
        }

        [HttpPost]
        public IActionResult SalvarProdutoTipo(ProdutoTipoODT request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            return this.Executar(() =>
            {
                var item = this.services.ProdutoTiposService.Salvar(request);
                this.services.Commit();
                return Ok(item);
            });
        }

        [HttpDelete("{codProdutoTipo:int}")]
        public IActionResult ExcluirProdutoTipo(int codProdutoTipo)
        {
            return this.Executar(() =>
            {
                this.services.ProdutoTiposService.Excluir(codProdutoTipo);
                this.services.Commit();
                return NoContent();
            });
        }
    }
}
