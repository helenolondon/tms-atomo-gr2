using AtmService.ODT;

namespace AtmApi.Controllers.Empresas.Requests
{
    public class SalvarEmpresaRequest
    {
        public int CodEmpresa { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public int SituacaoIE { get; set; }
        public string InscricaoEstadual { get; set; }
        public int CnaePrincipal { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string InscricaoSuframa { get; set; }
        public bool MicroEmpresa { get; set; }
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

        public EmpresaODT ToODT()
        {
            var odt = new EmpresaODT();

            odt.CodEmpresa = this.CodEmpresa;
            odt.NomeFantasia = this.NomeFantasia;
            odt.RazaoSocial = this.RazaoSocial;
            odt.Cnpj = this.Cnpj;
            odt.InscricaoEstadual = this.InscricaoEstadual;
            odt.CnaePrincipal = this.CnaePrincipal;
            odt.InscricaoMunicipal = this.InscricaoMunicipal;
            odt.InscricaoSuframa = this.InscricaoSuframa;
            odt.MicroEmpresa = this.MicroEmpresa;
            odt.CEP = this.CEP;
            odt.Logradouro = this.Logradouro;
            odt.Complemento = this.Complemento;
            odt.Numero = this.Numero;
            odt.CodEstado = this.CodEstado;
            odt.CodCidade = this.CodCidade;
            odt.CodBairro = this.CodBairro;
            odt.Telefone = this.Telefone;
            odt.Responsavel = this.Responsavel;
            odt.Contato = this.Contato;
            odt.Email = this.Email;
            odt.Observacao = this.Observacao;

            return odt;
        }
    }
}
