using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtmApi.Controllers.Autenticacao.ActionFilters;
using AtmApi.Controllers.Clientes.Requests;
using AtmApi.Controllers.Clientes.Responses;
using AtmApi.Controllers.Contatos.Requests;
using AtmService.Clientes.Requests;
using AtmService.ODT;
using DocumentFormat.OpenXml.Office2016.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtmApi.Controllers.Clientes
{
    [Route("api/[controller]")]
    [ApiController]
    [AutorizarUsuarioLogado]
    public class ClientesController : AtmController
    {
        /// <summary>
        /// Retorna lista de clientes
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult ListarClientes([FromQuery]ListarClientesRequest request)
        {
            return this.Executar(() =>
            {
                var clientsODTs = this.services.ClientesService.ListarClientes(request.CodCliente ?? 0, request.Nome,
                    request.TipoDePessoa, request.Cnae ?? 0, request.InscricaoEstadual, request.MicroEmpresa,
                    request.Telefone, request.Contato, request.Identidade, request.Cpf, request.SituacaoInscricaoEstadual,
                    request.DataNascimento, request.EstadosCivis, request.CodUfs, request.CodCidades, request.RazaoSocial);

                var response = new List<ClienteResponse>();

                if (clientsODTs.Count() == 0)
                {
                    return Ok(response);
                }

                var cnaesODTs = this.services.FiscalService.ObterCnaesPorCodigo(clientsODTs.Min(cli => cli.CnaePrincipal ?? 0), clientsODTs.Max(cli => cli.CnaePrincipal ?? 0));

                foreach (var odt in clientsODTs)
                {
                    response.Add(new ClienteResponse(odt, cnaesODTs.Where(cnn => cnn.CodigoCnae == odt.CnaePrincipal).FirstOrDefault()));
                }

                return Ok(response);
            });
        }

        [HttpGet("obter-por-razaosocial-ou-cpjcnpj")]
        public IActionResult ObterClientePorNomeOuCnpj(string razaoSocialOuCnpfCnpj)
        {
            var clientsODTs = this.services.ClientesService.ListarClientesPorNomeOuCnfCnpj(razaoSocialOuCnpfCnpj);
            var response = new List<ClienteResponse>();

            if (clientsODTs.Count() == 0)
            {
                return Ok(response);
            }
            var cnaesODTs = this.services.FiscalService.ObterCnaesPorCodigo(clientsODTs.Min(cli => cli.CnaePrincipal ?? 0), clientsODTs.Max(cli => cli.CnaePrincipal ?? 0));

            foreach (var odt in clientsODTs)
            {
                response.Add(new ClienteResponse(odt, cnaesODTs.Where(cnn => cnn.CodigoCnae == odt.CnaePrincipal).FirstOrDefault()));
            }

            return Ok(response);
        }

        /// <summary>
        /// Retorna 1 cliente pelo código
        /// </summary>
        /// <returns>Cliente</returns>
        [HttpGet("{codCliente}")]
        public IActionResult ObterCliente(int codCliente)
        {
            return this.Executar(() =>
            {
                var odt = this.services.ClientesService.ObterCliente(codCliente);
                CnaeODT cnae = null;

                if (odt.CnaePrincipal != null)
                {
                    cnae = this.services.FiscalService.ObterCnaePorCodigo(odt.CnaePrincipal.Value);
                }

                return Ok(new ClienteResponse(odt, cnae));
            });
        }

        [HttpPost]
        public IActionResult SalvarCliente(ClienteODT request)
        {
            return this.Executar(() =>
            {
                var odt = this.services.ClientesService.SalvarCliente(request);
                this.services.Commit();

                CnaeODT cnae = null;

                if(odt.CnaePrincipal != null)
                {
                    cnae = this.services.FiscalService.ObterCnaePorCodigo(odt.CnaePrincipal.Value);
                }

                return Ok(new ClienteResponse(odt, cnae));
            });
        }

        [HttpDelete("{codCliente:int}")]
        public IActionResult ExcluirCliente(int codCliente)
        {
            return this.Executar(() => 
            {
                this.services.ClientesService.Excluir(codCliente);
                this.services.Commit();

                return Ok();
            });            
        }

        [HttpGet("tipos-pessoa")]
        public IActionResult ListarTiposDePessoa()
        {
            return Ok(this.services.ClientesService.ListarTiposDePessoa());
        }
        
        [HttpGet("estados-civis")]
        public IActionResult ListarEstadosCivis()
        {
            return Ok(this.services.ClientesService.ListarEstadosCivis());
        }
    }
}
