using AtmApi.Controllers.Autenticacao.ActionFilters;
using AtmService.ODT;
using Microsoft.AspNetCore.Mvc;

namespace AtmApi.Controllers.UnidadesDeMedida
{
    [Route("api/unidadesdemedida")]
    [ApiController]
    //[AutorizarUsuarioLogado]
    public class UnidadesDeMedidaController : AtmController
    {
        [HttpGet]
        public IActionResult Listar()
        {
            return this.Executar(() => Ok(this.services.UnidadesDeMedidaService.Listar()));
        }

        [HttpGet("{codUnidadeDeMedida:int}")]
        public IActionResult ObterPorId(int codUnidadeDeMedida)
        {
            return this.Executar(() =>
            {
                var item = this.services.UnidadesDeMedidaService.ObterPorId(codUnidadeDeMedida);
                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);
            });
        }

        [HttpPost]
        public IActionResult Salvar(UnidadeDeMedidaODT request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            return this.Executar(() =>
            {
                var item = this.services.UnidadesDeMedidaService.Salvar(request);
                this.services.Commit();
                return Ok(item);
            });
        }

        [HttpDelete("{codUnidadeDeMedida:int}")]
        public IActionResult Excluir(int codUnidadeDeMedida)
        {
            return this.Executar(() =>
            {
                this.services.UnidadesDeMedidaService.Excluir(codUnidadeDeMedida);
                this.services.Commit();
                return NoContent();
            });
        }
    }
}
