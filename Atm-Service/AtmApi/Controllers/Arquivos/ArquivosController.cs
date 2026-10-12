using AtmApi.Controllers.Autenticacao.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AtmApi.Controllers.Anexos
{
    [Route("api/arquivos")]
    [AutorizarUsuarioLogado]
    [ApiController]
    public class ArquivosController : AtmController
    {
        [HttpGet("{codArquivo:int}")]
        public IActionResult DownloadArquivo(int codArquivo)
        {
            var arquivoODT = this.services.ArquivosService.ObterArquivo(codArquivo, true);

            if(arquivoODT == null)
            {
                return NotFound();
            }

            var fileName = arquivoODT.NomeArquivo;
            
            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = fileName,
                Inline = false
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");

            return File(arquivoODT.FileAsBytes, arquivoODT.TipoArquivo);
        }

        [HttpDelete("{codArquivo:int}")]
        public IActionResult Delete(int codArquivo)
        {
            return this.Executar(() => 
            {
                this.services.ArquivosService.Excluir(codArquivo);
                this.services.Commit();

                return NoContent();
            });
        }

        [HttpPost]
        public IActionResult Post(IList<IFormFile> files, [FromHeader]string tag, [FromHeader] string reference)
        {
            return this.Executar(() =>
            {
                this.services.ArquivosService.SalvarArquivos(files, tag, reference);
                this.services.Commit();

                return Ok();
            });
        }
    }
}
