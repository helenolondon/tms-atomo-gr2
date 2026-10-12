using AtmService.ODT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtmApi.Controllers.Clientes.Requests
{
    public class SalvarClienteRequest
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public int Tipo { get; set; }
        public string CPFCNPJ { get; set; }
        public string Identidade { get; set; }
        public string OrgaoExpedidor { get; set; }
        public DateTime? IEDataEmissao { get; set; }
        public DateTime? DataNascimento { get; set; }
        public int? EstadoCivil { get; set; }
        public string Profissao { get; set; }
        public string Nacionalidade { get; set; }
        public string InscricaoEstadual { get; set; }
        public int? IESituacao { get; set; }
        public int? CodCnae { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string InscricaoSuframa { get; set; }
        public bool MicroEmpresa { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public int CodigoEstado { get; set; }
        public int CodigoCidade { get; set; }
        public int CodigoBairro { get; set; }
        public string Telefone { get; set; }
        public string Contato { get; set; }
        public string Email { get; set; }
        public string Observacoes { get; set; }

        internal ClienteODT ToODT()
        {
            var odt = new ClienteODT();

            odt.CEP = this.CEP;
            odt.Codigo = this.Codigo;
            odt.CnaePrincipal = this.CodCnae ?? 0;
            odt.CodigoBairro = this.CodigoBairro;
            odt.CodigoCidade = this.CodigoCidade;
            odt.CodigoEstado = this.CodigoEstado;
            odt.Complemento = this.Complemento;
            odt.Contato = this.Contato;
            odt.CPFCNPJ = this.CPFCNPJ;
            odt.IEDataEmissao = this.IEDataEmissao.Value;
            odt.DataNascimento = this.DataNascimento.Value;
            odt.Email = this.Email;
            odt.EstadoCivil = (int)this.EstadoCivil;
            odt.Identidade = this.Identidade;
            odt.InscricaoEstadual = this.InscricaoEstadual;
            odt.InscricaoMunicipal = this.InscricaoMunicipal;
            odt.InscricaoSuframa = this.InscricaoSuframa;
            odt.Logradouro = this.Logradouro;
            odt.MicroEmpresa = this.MicroEmpresa;
            odt.Nacionalidade = this.Nacionalidade;
            odt.Nome = this.Nome;
            odt.Numero = this.Numero;
            odt.Observacoes = this.Observacoes;
            odt.OrgaoExpedidor = this.OrgaoExpedidor;
            odt.Profissao = this.Profissao;
            odt.RazaoSocial = this.RazaoSocial;
            odt.IESituacao = (int)this.IESituacao;
            odt.Telefone = this.Telefone;
            odt.Tipo = (int)this.Tipo;

            return odt;
        }
    }
}
