using AtmApi.Controllers.Autenticacao.ActionFilters;
using AtmService.ODT;
using Microsoft.AspNetCore.Mvc;

namespace AtmApi.Controllers.Marcas
{
    [Route("api/marcas")]
    [ApiController]
    //[AutorizarUsuarioLogado]
    public class MarcasController : AtmController
    {
        [HttpGet]
        public IActionResult ListarMarcas()
        {
            return this.Executar(() => Ok(this.services.MarcasService.Listar()));
        }

        [HttpGet("{codMarca:int}")]
        public IActionResult ObterMarca(int codMarca)
        {
            return this.Executar(() =>
            {
                var marca = this.services.MarcasService.ObterPorId(codMarca);
                if (marca == null)
                {
                    return NotFound();
                }

                return Ok(marca);
            });
        }

        [HttpPost]
        public IActionResult SalvarMarca(MarcaODT request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            return this.Executar(() =>
            {
                var marca = this.services.MarcasService.Salvar(request);
                this.services.Commit();
                return Ok(marca);
            });
        }

        [HttpDelete("{codMarca:int}")]
        public IActionResult ExcluirMarca(int codMarca)
        {
            return this.Executar(() =>
            {
                this.services.MarcasService.Excluir(codMarca);
                this.services.Commit();
                return NoContent();
            });
        }
    }
}
