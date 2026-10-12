using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.Clientes.Requests
{
    public class ListarClientesRequest
    {
        [BindProperty(Name = "codCliente")]
        public int? CodCliente { get; set; }

        [BindProperty(Name = "nome")]
        public string? Nome { get; set; }
        
        [BindProperty(Name = "razaoSocial")]
        public string? RazaoSocial { get; set; }

        [BindProperty(Name = "tipoDePessoa")]
        public int? TipoDePessoa { get; set; }

        [BindProperty(Name = "cnae")]
        public int? Cnae { get; set; }

        [BindProperty(Name = "inscricaoEstadual")]
        public string? InscricaoEstadual { get; set; }

        [BindProperty(Name = "microEmpresa")]
        public bool? MicroEmpresa { get; set; }

        [BindProperty(Name = "telefone")]
        public string? Telefone { get; set; }

        [BindProperty(Name = "contato")]
        public string? Contato { get; set; }

        [BindProperty(Name = "identidade")]
        public string? Identidade { get; set; }

        [BindProperty(Name = "cpfCnpj")]
        public string? Cpf { get; set; }

        [BindProperty(Name = "situacaoInscricaoEstadual")]
        public int[]? SituacaoInscricaoEstadual { get; set; }

        [BindProperty(Name = "dataNascimento")]
        public DateTime? DataNascimento { get; set; }

        [BindProperty(Name = "estadosCivil")]
        public int[]? EstadosCivis { get; set; }

        [BindProperty(Name = "codUfs")]
        public int[]? CodUfs { get; set; }

        [BindProperty(Name = "codCidades")]
        public int[]? CodCidades { get; set; }
    }
}
