using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceDominio.ODN.Tipos.Pessoas;
using AtmServiceDominio.ObjetosDeDominio.Validacao;
using System;
using System.Collections.Generic;
using System.Text;
using AtmServiceInfra.ODP;
using AtmServiceDominio.ODN.Tipos.Fiscal;

namespace AtmServiceDominio.ODN
{
    public class ClienteODN: ObjetoDeNegocio
    {
        public int CodCliente { get; set; }
        public string NomeCliente { get; set; }
        public string RazaoSocial { get; set; }
        public TipoDePessoa TipoPessoa { get; set; }
        public string CPFCNPJ { get; set; }
        public string Identidade { get; set; }
        public string OrgaoExpedidor { get; set; }
        public DateTime? DataEmissaoIdentidade { get; set; }
        public DateTime? DataNascimento { get; set; }
        public DateTime? DataCadastro { get; set; }
        public EstadoCivil? EstadoCivil { get; set; }
        public string Profissao { get; set; }
        public string Nacionalidade { get; set; }
        public SituacaoInscricaoEstadual? SituacaoIE { get; set; }
        public string InscricaoEstadual { get; set; }
        public int? CnaePrincipal { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string InscricaoSuframa { get; set; }
        public bool MicroEmpresa { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public int CodEstado { get; set; }
        public int CodBairro { get; set; }
        public int CodCidade { get; set; }
        public string Telefone { get; set; }
        public string Contato { get; set; }
        public string Email { get; set; }
        public string Observacao { get; set; }

        protected internal override void ConfiguraRegrasDeValidacao()
        {
            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "NomeCliente", 200, "Nome deve ter no máximo 200 caracteres"));
            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "RazaoSocial", 200, "Razão social deve ter no máximo 200 caracteres"));
            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "CPFCNPJ", 14, "CPF/CNPJ deve ter no máximo 14 caracteres"));
            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "CPFCNPJ", 14, "CPF/CNPJ deve ter no máximo 14 caracteres"));

            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "InscricaoEstadual", 30, "Inscrição Estadual social deve ter no máximo 30 caracteres"));
            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "InscricaoMunicipal", 30, "Inscrição Municipal deve ter no máximo 30 caracteres"));
            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "InscricaoSuframa", 30, "Inscrição Suframa deve ter no máximo 30 caracteres"));
            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "Identidade", 20, "Identidade deve ter no máximo 20 caracteres"));
            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "OrgaoExpedidor", 10, "Orgão expedidor da identidade deve ter no máximo 10 caracteres"));
            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "Profissao", 50, "Profissão deve ter no máximo 50 caracteres"));
            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "Nacionalidade", 50, "Nacionalidade deve ter no máximo 50 caracteres"));

            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "Cep", 9, "CEP deve ter no máximo 9 caracteres"));
            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "Logradouro", 300, "Logradouro social deve ter no máximo 300 caracteres"));
            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "Complemento", 100, "Complemento deve ter no máximo 100 caracteres"));
            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "Telefone", 30, "Telefone deve ter no máximo 30 caracteres"));
            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "Contato", 30, "Contato deve ter no máximo 30 caracteres"));
            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "Email", 60, "Email deve ter no máximo 200 caracteres"));
            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "Observacao", 300, "Observação deve ter no máximo 300 caracteres"));
            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "Numero", 10, "Número deve ter no máximo 10 caracteres"));
            this.regrasDeValidacao.Add(new ObrigatorioRV(this, "CPFCNPJ", "CPF/CNPJ é obrigatório"));
            this.regrasDeValidacao.Add(new ObrigatorioRV(this, "Logradouro", "Logradouro é obrigatório"));
            this.regrasDeValidacao.Add(new ObrigatorioRV(this, "Numero", "Número é obrigatório"));
            this.regrasDeValidacao.Add(new ObrigatorioRV(this, "Telefone", "Telefone é obrigatório"));
            this.regrasDeValidacao.Add(new ObrigatorioRV(this, "Email", "Email é obrigatório"));
        }

        internal override void FromODP(ObjetoDePersistencia obj)
        {
            if(obj == null)
            {
                return;
            }

            var odp = obj as ClienteODP;

            this.Cep = odp.Cep;
            this.DataCadastro = odp.DataCad;
            this.CodCliente = odp.CodCliente;
            this.CnaePrincipal = odp.CnaePrincipal;
            this.CodBairro = odp.CodBairro;
            this.CodCidade = odp.CodCidade;
            this.CodEstado = odp.CodEstado;
            this.Complemento = odp.Complemento;
            this.Contato = odp.Contato;
            this.CPFCNPJ = odp.CPFCNPJ;
            this.DataEmissaoIdentidade = odp.DataEmissaoIdentidade;
            this.DataNascimento = odp.DataNascimento;
            this.Email = odp.Email;
            this.EstadoCivil = odp.EstadoCivil != null ? (EstadoCivil)odp.EstadoCivil : new Nullable<EstadoCivil>(); 
            this.Identidade = odp.Identidade;
            this.InscricaoEstadual = odp.InscricaoEstadual;
            this.InscricaoMunicipal = odp.InscricaoMunicipal;
            this.InscricaoSuframa = odp.InscricaoSuframa;
            this.Logradouro = odp.Logradouro;
            this.MicroEmpresa = odp.MicroEmpresa;
            this.Nacionalidade = odp.Nacionalidade;
            this.NomeCliente = odp.NomeCliente;
            this.Numero = odp.Numero;
            this.Observacao = odp.Observacao;
            this.OrgaoExpedidor = odp.OrgaoExpedidor;
            this.Profissao = odp.Profissao;
            this.RazaoSocial = odp.RazaoSocial;
            this.SituacaoIE = (SituacaoInscricaoEstadual)odp.SituacaoIE;
            this.Telefone = odp.Telefone;
            this.TipoPessoa = (TipoDePessoa)odp.TipoPessoa;
        }

        internal override ObjetoDePersistencia ToODP(ObjetoDePersistencia destino = null)
        {
            if(destino == null)
            {
                destino = new ClienteODP();
            };

            var odp = destino as ClienteODP;

            odp.Cep = this.Cep;
            odp.CodCliente = this.CodCliente;
            odp.CnaePrincipal = this.CnaePrincipal;
            odp.CodBairro = this.CodBairro;
            odp.CodCidade = this.CodCidade;
            odp.CodEstado = this.CodEstado;
            odp.Complemento = this.Complemento;
            odp.Contato = this.Contato;
            odp.CPFCNPJ = this.CPFCNPJ;
            odp.DataEmissaoIdentidade = this.DataEmissaoIdentidade;
            odp.DataNascimento = this.DataNascimento;
            odp.Email = this.Email;
            odp.EstadoCivil = this.EstadoCivil != null ? (int)this.EstadoCivil : new Nullable<int>();
            odp.Identidade = this.Identidade;
            odp.InscricaoEstadual = this.InscricaoEstadual;
            odp.InscricaoMunicipal = this.InscricaoMunicipal;
            odp.InscricaoSuframa = this.InscricaoSuframa;
            odp.Logradouro = this.Logradouro;
            odp.MicroEmpresa = this.MicroEmpresa;
            odp.Nacionalidade = this.Nacionalidade;
            odp.NomeCliente = this.NomeCliente;
            odp.Numero = this.Numero;
            odp.Observacao = this.Observacao;
            odp.OrgaoExpedidor = this.OrgaoExpedidor;
            odp.Profissao = this.Profissao;
            odp.RazaoSocial = this.RazaoSocial;
            odp.SituacaoIE = (int)this.SituacaoIE;
            odp.Telefone = this.Telefone;
            odp.TipoPessoa = (int)this.TipoPessoa;

            return odp;
        }
    }
}
