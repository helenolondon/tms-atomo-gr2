using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AtmServiceInfra.ODP
{
    [Table("TD_CLIENTES")]
    public class ClienteODP: ObjetoDePersistencia
    {
        [Key]
        public int CodCliente { get; set; }
        public string NomeCliente { get; set; }
        public string RazaoSocial { get; set; }
        public int TipoPessoa { get; set; }
        public string CPFCNPJ { get; set; }
        public string Identidade { get; set; }
        public string OrgaoExpedidor { get; set; }
        public DateTime? DataEmissaoIdentidade { get; set; }
        public DateTime? DataNascimento { get; set; }
        public int? EstadoCivil { get; set; }
        public string Profissao { get; set; }
        public string Nacionalidade { get; set; }
        public int SituacaoIE { get; set; }
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
    }
}
