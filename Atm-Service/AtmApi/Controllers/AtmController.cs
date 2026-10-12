using System;
using System.Linq;
using AtmService;
using AtmService.AtmApi;
using AtmService.Erros;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AtmApi.Controllers
{
    /// <summary>
    /// Classe base dos controllers da aplicação
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AtmController : ControllerBase
    {
        /// <summary>
        /// Repositório de serviços do sistema
        /// </summary>
        public IAtmServices services;

        /// <summary>
        /// Construtor padrão da class
        /// </summary>
        public AtmController() : base()
        {
            this.services = new AtmServices(Startup.Configuration);
        }

        /// <summary>
        /// Executa o código dos controllers tratando erros.
        /// </summary>
        /// <param name="action"></param>
        /// <returns>IActionResult</returns>
        protected IActionResult Executar(Func<IActionResult> action)            
        {
            try
            {
                return action();
            }
            catch (AtmServiceNaoEncontradoSE ex)
            {
                return NotFound(ex.Message);
            }
            catch (AtmServiceValidacaoExcecao e)
            {
                return BadRequest(e.Message + (e.Erros.Count() > 0 ? ", " + string.Join(", ", e.Erros) : null));
            }            
            catch (Exception e)
            {
                return StatusCode(501, this.RetInnerException(e).Message);
            }
        }

        /// <summary>
        /// Construtor padrão do sistema
        /// </summary>
        /// <param name="services"></param>
        public AtmController(IAtmServices services): base()
        {
            this.services = services;
        }

        /// <summary>
        /// Retorna última exception da propriedade InnerException de ex
        /// </summary>
        /// <param name="ex"></param>
        /// <returns>Exception</returns>
        protected Exception RetInnerException(Exception ex)
        {
            Exception e = ex;

            while (e != null)
            {
                ex = e;
                e = e.InnerException;
            }

            return ex;
        }

        /// <summary>
        /// Retorna o valor de Authorization no cabeçalho da requisição
        /// </summary>
        /// <returns>string contendo o token</returns>
        protected string ObterTokenDoRequest()
        {
            string token = null;

            if (Request.Headers.ContainsKey("Authorization"))
            {
                token = Request.Headers["Authorization"].FirstOrDefault();
            }

            return token;
        }
    }
}