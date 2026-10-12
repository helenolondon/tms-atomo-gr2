using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmServiceInfra.ODP
{
    [Table("TD_EMPRESAS")]
    public class EmpresaODP: ObjetoDePersistencia
    {
        [Key]
        public int CodEmpresa { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string InscricaoEstadual { get; set; }
        public int CnaePrincipal { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string InscricaoSuframa { get; set; }
        public int MicroEmpresa { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public int CodEstado { get; set; }
        public int CodCidade { get; set; }
        public int CodBairro { get; set; }
        public string Telefone { get; set; }
        public string Responsavel { get; set; }
        public string Contato { get; set; }
        public string Email { get; set; }
        public string Observacao { get; set; }
    }
}
