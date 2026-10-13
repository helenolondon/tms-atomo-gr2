using AtmApi.Controllers.Autenticacao.ActionFilters;
using AtmService.ODT;
using Microsoft.AspNetCore.Mvc;

namespace AtmApi.Controllers.TipoVeiculos
{
    [Route("api/tipoveiculo")]
    [ApiController]
    [AutorizarUsuarioLogado]
    public class TipoVeiculosController : AtmController
    {
        [HttpGet]
        public IActionResult Listar()
        {
            return this.Executar(() => Ok(this.services.TipoVeiculosService.Listar()));
        }

        [HttpGet("{codTipoVeiculo:int}")]
        public IActionResult ObterPorId(int codTipoVeiculo)
        {
            return this.Executar(() =>
            {
                var item = this.services.TipoVeiculosService.ObterPorId(codTipoVeiculo);
                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);
            });
        }

        [HttpPost]
        public IActionResult Salvar(TipoVeiculoODT request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            return this.Executar(() =>
            {
                var item = this.services.TipoVeiculosService.Salvar(request);
                this.services.Commit();
                return Ok(item);
            });
        }

        [HttpDelete("{codTipoVeiculo:int}")]
        public IActionResult Excluir(int codTipoVeiculo)
        {
            return this.Executar(() =>
            {
                this.services.TipoVeiculosService.Excluir(codTipoVeiculo);
                this.services.Commit();
                return NoContent();
            });
        }
    }
}
