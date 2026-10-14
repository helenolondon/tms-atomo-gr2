using AtmApi.Controllers.Autenticacao.ActionFilters;
using AtmService.ODT;
using Microsoft.AspNetCore.Mvc;

namespace AtmApi.Controllers.Produtos
{
    [Route("api/produtos")]
    [ApiController]
    [AutorizarUsuarioLogado]
    public class ProdutosController : AtmController
    {
        [HttpGet]
        public IActionResult Listar()
        {
            return this.Executar(() => Ok(this.services.ProdutosService.Listar()));
        }

        [HttpGet("{codProduto:int}")]
        public IActionResult ObterPorId(int codProduto)
        {
            return this.Executar(() =>
            {
                var item = this.services.ProdutosService.ObterPorId(codProduto);
                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);
            });
        }

        [HttpPost]
        public IActionResult Salvar(ProdutoODT request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            return this.Executar(() =>
            {
                var item = this.services.ProdutosService.Salvar(request);
                this.services.Commit();
                return Ok(item);
            });
        }

        [HttpDelete("{codProduto:int}")]
        public IActionResult Excluir(int codProduto)
        {
            return this.Executar(() =>
            {
                this.services.ProdutosService.Excluir(codProduto);
                this.services.Commit();
                return NoContent();
            });
        }
    }
}
