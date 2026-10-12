using AtmApi.Controllers.Empresas.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtmApi.Controllers.Empresas
{
    [Route("api/empresas")]
    [ApiController]
    public class EmpresasController : AtmController
    {
        [HttpGet]
        public IActionResult ListarEmpresas(string? razaoSocial, string? nomeFantasia, string? cnpj, string? responsavel, string? inscricaoEstadual, string? telefone, string? email)
        {
            return this.Executar(() =>
            {
                return Ok(this.services.EmpresasService.ListarEmpresas(razaoSocial, nomeFantasia, cnpj, responsavel, inscricaoEstadual, telefone, email));
            });
        }
        
        [HttpGet("{codEmpresa:int}")]
        public IActionResult ObterEmpresa(int codEmpresa)
        {
            return this.Executar(() =>
            {
                var result = this.services.EmpresasService.ObterEmpresa(codEmpresa);

                if(result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            });
        }

        [HttpPost]
        public IActionResult SalvarEmpresa(SalvarEmpresaRequest request)
        {
            if(request== null)
            {
                return BadRequest();
            };

            return this.Executar(() => 
            {
                var odt = request.ToODT();

                odt = this.services.EmpresasService.SalvarEmpresa(odt);
                this.services.Commit();

                return Ok(odt);    
            });
        }

        [HttpDelete("{codEmpresa:int}")]
        public IActionResult ExcluirEmpresa(int codEmpresa)
        {
            return this.Executar(() => 
            {
                this.services.EmpresasService.Excluir(codEmpresa);
                this.services.Commit();

                return NoContent();
            });
        }
    }
}
