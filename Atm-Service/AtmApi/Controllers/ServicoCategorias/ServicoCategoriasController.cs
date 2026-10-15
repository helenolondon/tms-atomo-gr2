using AtmApi.Controllers.Autenticacao.ActionFilters;
using AtmService.ODT;
using Microsoft.AspNetCore.Mvc;

namespace AtmApi.Controllers.ServicoCategorias
{
    [Route("api/servicocategorias")]
    [ApiController]
    //[AutorizarUsuarioLogado]
    public class ServicoCategoriasController : AtmController
    {
        [HttpGet]
        public IActionResult ListarServicoCategorias()
        {
            return this.Executar(() => Ok(this.services.ServicoCategoriasService.Listar()));
        }

        [HttpGet("{codServicoCategoria:int}")]
        public IActionResult ObterServicoCategoria(int codServicoCategoria)
        {
            return this.Executar(() =>
            {
                var item = this.services.ServicoCategoriasService.ObterPorId(codServicoCategoria);
                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);
            });
        }

        [HttpPost]
        public IActionResult SalvarServicoCategoria(ServicoCategoriaODT request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            return this.Executar(() =>
            {
                var item = this.services.ServicoCategoriasService.Salvar(request);
                this.services.Commit();
                return Ok(item);
            });
        }

        [HttpDelete("{codServicoCategoria:int}")]
        public IActionResult ExcluirServicoCategoria(int codServicoCategoria)
        {
            return this.Executar(() =>
            {
                this.services.ServicoCategoriasService.Excluir(codServicoCategoria);
                this.services.Commit();
                return NoContent();
            });
        }
    }
}
