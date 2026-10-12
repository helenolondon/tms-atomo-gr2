using AtmService.Utils;
using AtmServiceDominio.ODN;
using AtmServiceDominio.ODN.Tipos.Fiscal;
using AtmServiceDominio.ODN.Tipos.Pessoas;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.ODT
{
    public class ClienteODT
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public int Tipo { get; set; }
        public string CPFCNPJ { get; set; }
        public string Identidade { get; set; }
        public string OrgaoExpedidor { get; set; }
        public DateTime? IEDataEmissao { get; set; }
        public DateTime? DataNascimento { get; set; }
        public DateTime? DataCadastro { get; set; }
        public int? EstadoCivil { get; set; }
        public string Profissao { get; set; }
        public string Nacionalidade { get; set; }
        public string InscricaoEstadual { get; set; }
        public int? IESituacao { get; set; }
        public int? CnaePrincipal { get; set; }
        public string RazaoSocial { get; set; }
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
        public string RazaoSocialCpfCnpj
        {
            get
            {
                return (this.Tipo == 1 ? this.CPFCNPJ.FormataCpf() : this.CPFCNPJ.FormataCnpj()) + " # " + this.RazaoSocial;
            }
        }

        public ClienteODT()
        {

        }

        public ClienteODT(ClienteODN odn)
        {
            this.FromODN(odn);
        }

        internal ClienteODN ToODN()
        {
            var odn = new ClienteODN();
            odn.CodCliente = this.Codigo;
            odn.DataCadastro = this.DataCadastro;
            odn.Cep = this.CEP.Replace("-", null).Replace(".", null);
            odn.CnaePrincipal = this.CnaePrincipal;
            odn.CodBairro = this.CodigoBairro;
            odn.CodCidade = this.CodigoCidade;
            odn.CodEstado = this.CodigoEstado;
            odn.Complemento = this.Complemento;
            odn.Contato = this.Contato;
            odn.CPFCNPJ = this.CPFCNPJ.Replace("-", null).Replace(".", null).Replace("/", null);
            odn.DataEmissaoIdentidade = this.IEDataEmissao.Value;
            odn.DataNascimento = this.DataNascimento.Value;
            odn.Email = this.Email;
            odn.EstadoCivil = this.EstadoCivil != null ? (EstadoCivil)this.EstadoCivil : new Nullable<EstadoCivil>();
            odn.Identidade = this.Identidade;
            odn.InscricaoEstadual = this.InscricaoEstadual;
            odn.InscricaoMunicipal = this.InscricaoMunicipal;
            odn.InscricaoSuframa = this.InscricaoSuframa;
            odn.Logradouro = this.Logradouro;
            odn.MicroEmpresa = this.MicroEmpresa;
            odn.Nacionalidade = this.Nacionalidade;
            odn.NomeCliente = this.Nome;
            odn.Numero = this.Numero;
            odn.Observacao = this.Observacoes;
            odn.OrgaoExpedidor = this.OrgaoExpedidor;
            odn.Profissao = this.Profissao;
            odn.RazaoSocial = this.RazaoSocial;
            odn.SituacaoIE = (SituacaoInscricaoEstadual)this.IESituacao;
            odn.Telefone = this.Telefone;
            odn.TipoPessoa = (TipoDePessoa)this.Tipo;

            return odn;
        }

        internal void FromODN(ClienteODN odn)
        {
            if(odn == null)
            {
                return;
            }

            this.Codigo = odn.CodCliente;
            this.CEP = odn.Cep;
            this.DataCadastro = odn.DataCadastro;
            this.CnaePrincipal = odn.CnaePrincipal;
            this.CodigoBairro = odn.CodBairro;
            this.CodigoCidade = odn.CodCidade;
            this.CodigoEstado = odn.CodEstado;
            this.Complemento = odn.Complemento;
            this.Contato = odn.Contato;
            this.CPFCNPJ = odn.CPFCNPJ;
            this.IEDataEmissao = odn.DataEmissaoIdentidade;
            this.DataNascimento = odn.DataNascimento;
            this.Email = odn.Email;
            this.EstadoCivil = odn.EstadoCivil != null? (int)odn.EstadoCivil: new Nullable<int>();
            this.Identidade = odn.Identidade;
            this.InscricaoEstadual = odn.InscricaoEstadual;
            this.InscricaoMunicipal = odn.InscricaoMunicipal;
            this.InscricaoSuframa = odn.InscricaoSuframa;
            this.Logradouro = odn.Logradouro;
            this.MicroEmpresa = odn.MicroEmpresa;
            this.Nacionalidade = odn.Nacionalidade;
            this.Nome = odn.NomeCliente;
            this.Numero = odn.Numero;
            this.Observacoes = odn.Observacao;
            this.OrgaoExpedidor = odn.OrgaoExpedidor;
            this.Profissao = odn.Profissao;
            this.RazaoSocial = odn.RazaoSocial;
            this.IESituacao = (int)odn.SituacaoIE;
            this.Telefone = odn.Telefone;
            this.Tipo = (int)odn.TipoPessoa;
        }
    }
}
