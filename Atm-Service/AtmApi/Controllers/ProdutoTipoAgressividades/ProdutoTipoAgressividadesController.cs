using AtmApi.Controllers.Autenticacao.ActionFilters;
using AtmService.ODT;
using Microsoft.AspNetCore.Mvc;

namespace AtmApi.Controllers.ProdutoTipoAgressividades
{
    [Route("api/produtotipoagressividade")]
    [ApiController]
    [AutorizarUsuarioLogado]
    public class ProdutoTipoAgressividadesController : AtmController
    {
        [HttpGet]
        public IActionResult Listar()
        {
            return this.Executar(() => Ok(this.services.ProdutoTipoAgressividadesService.Listar()));
        }

        [HttpGet("{codProdutoTipoAgressividade:int}")]
        public IActionResult ObterPorId(int codProdutoTipoAgressividade)
        {
            return this.Executar(() =>
            {
                var item = this.services.ProdutoTipoAgressividadesService.ObterPorId(codProdutoTipoAgressividade);
                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);
            });
        }

        [HttpPost]
        public IActionResult Salvar(ProdutoTipoAgressividadeODT request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            return this.Executar(() =>
            {
                var item = this.services.ProdutoTipoAgressividadesService.Salvar(request);
                this.services.Commit();
                return Ok(item);
            });
        }

        [HttpDelete("{codProdutoTipoAgressividade:int}")]
        public IActionResult Excluir(int codProdutoTipoAgressividade)
        {
            return this.Executar(() =>
            {
                this.services.ProdutoTipoAgressividadesService.Excluir(codProdutoTipoAgressividade);
                this.services.Commit();
                return NoContent();
            });
        }
    }
}
