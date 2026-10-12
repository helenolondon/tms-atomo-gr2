using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtmApi.Controllers.Autenticacao.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtmApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AutorizarUsuarioLogado]
    public class EnderecosController : AtmController
    {
        /// <summary>
        /// Retorna lista de usuários cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet("{cep}")]
        public IActionResult ObterEnderecoPorCep(string cep)
        {
            return this.Executar(() =>
            {
                return Ok(this.services.EnderecosService.ObterEnderecoPorCep(cep));
            });
        }

        /// <summary>
        /// Retorna lista de estados cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet("estados")]
        public IActionResult ObterEstados()
        {
            return this.Executar(() =>
            {
                var odt = this.services.EnderecosService.ObterEstados();

                if (odt == null)
                {
                    return Ok();
                }

                return Ok(odt);
            });
        }
        
        /// <summary>
        /// Retorna lista de cidades por estados cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet("estados/{codEstado}/cidades")]
        public IActionResult ObterCidadesPorEstados(int codEstado)
        {
            return this.Executar(() =>
            {
                var odt = this.services.EnderecosService.ObterCidadesPorEstado(codEstado);

                if (odt == null)
                {
                    return Ok();
                }

                return Ok(odt);
            });
        }

        [HttpGet("cidades")]
        public IActionResult ObterBairrosPorCidades(string nomeCidade, string uf)
        {
            return this.Executar(() =>
            {
                var odt = this.services.EnderecosService.ObterCidadesPorNome(nomeCidade, uf);

                if (odt == null)
                {
                    return Ok();
                }

                return Ok(odt);
            });
        }

        /// <summary>
        /// Retorna lista de bairros por cidades cadastradas
        /// </summary>
        /// <returns></returns>
        [HttpGet("cidades/{codCidade}/bairros")]
        public IActionResult ObterBairrosPorCidades(int codCidade)
        {
            return this.Executar(() =>
            {
                var odt = this.services.EnderecosService.ObterBairrosPorCidade(codCidade);

                if (odt == null)
                {
                    return Ok();
                }

                return Ok(odt);
            });
        }
    }
}
