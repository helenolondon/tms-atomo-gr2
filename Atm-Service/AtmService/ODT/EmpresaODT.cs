using AtmServiceDominio.ODN;
using AtmServiceInfra.ODP;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AtmService.ODT
{
    public class EmpresaODT
    {
        public int CodEmpresa { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
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

        public CnaeODT Cnae { get; set; }

        public EmpresaODT()
        {

        }

        public EmpresaODT(EmpresaODN odn)
        {
            this.FromODN(odn);
        }

        internal void FromODN(EmpresaODN odn)
        {
            if (odn == null)
            {
                return;
            }

            this.CodEmpresa = odn.CodEmpresa;
            this.NomeFantasia = odn.NomeFantasia;
            this.RazaoSocial = odn.RazaoSocial;
            this.Cnpj = odn.Cnpj;
            this.InscricaoEstadual = odn.InscricaoEstadual;
            this.CnaePrincipal = odn.CnaePrincipal;
            this.InscricaoMunicipal = odn.InscricaoMunicipal;
            this.InscricaoSuframa = odn.InscricaoSuframa;
            this.MicroEmpresa = odn.MicroEmpresa;
            this.CEP = odn.CEP;
            this.Logradouro = odn.Logradouro;
            this.Complemento = odn.Complemento;
            this.Numero = odn.Numero;
            this.CodEstado = odn.CodEstado;
            this.CodCidade = odn.CodCidade;
            this.CodBairro = odn.CodBairro;
            this.Telefone = odn.Telefone;
            this.Responsavel = odn.Responsavel;
            this.Contato = odn.Contato;
            this.Email = odn.Email;
            this.Observacao = odn.Observacao;
        }

        internal EmpresaODN ToODN()
        {
            var odn = new EmpresaODN();

            odn.CodEmpresa = this.CodEmpresa;
            odn.NomeFantasia = this.NomeFantasia;
            odn.RazaoSocial = this.RazaoSocial;
            odn.Cnpj = this.Cnpj.Replace("-", null).Replace(".", null).Replace("/", null);
            odn.InscricaoEstadual = this.InscricaoEstadual;
            odn.CnaePrincipal = this.CnaePrincipal;
            odn.InscricaoMunicipal = this.InscricaoMunicipal;
            odn.InscricaoSuframa = this.InscricaoSuframa;
            odn.MicroEmpresa = this.MicroEmpresa;
            odn.CEP = this.CEP.Replace("-", null).Replace(".", null);
            odn.Logradouro = this.Logradouro;
            odn.Complemento = this.Complemento;
            odn.Numero = this.Numero;
            odn.CodEstado = this.CodEstado;
            odn.CodCidade = this.CodCidade;
            odn.CodBairro = this.CodBairro;
            odn.Telefone = this.Telefone;
            odn.Responsavel = this.Responsavel;
            odn.Contato = this.Contato;
            odn.Email = this.Email;
            odn.Observacao = this.Observacao;

            return odn;
        }
    }
}
