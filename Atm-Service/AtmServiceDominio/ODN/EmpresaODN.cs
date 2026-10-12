using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceDominio.ODN.Attributes;
using AtmServiceDominio.ODN.Tipos.Fiscal;
using AtmServiceInfra.ODP;

namespace AtmServiceDominio.ODN
{
    public class EmpresaODN : ObjetoDeNegocio
    {
        public int CodEmpresa { get; set; }
        [Obrigatorio("Nome fantasia é obrigatório")]
        public string NomeFantasia { get; set; }
        [Obrigatorio("Razão social é obrigatório")]
        public string RazaoSocial { get; set; }
        [Obrigatorio("CNPJ é obrigatório")]
        public string Cnpj { get; set; }
        [Obrigatorio("Inscrição estadual é obrigatório")]
        public string InscricaoEstadual { get; set; }
        [Obrigatorio("CNAE principal é obrigatório")]
        public int CnaePrincipal { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string InscricaoSuframa { get; set; }
        public bool MicroEmpresa { get; set; }
        [Obrigatorio("CEP é obrigatório")]
        public string CEP { get; set; }
        [Obrigatorio("Logradouro é obrigatório")]
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        [Obrigatorio("Número é obrigatório")]
        public string Numero { get; set; }
        [Obrigatorio("Estado é obrigatório")]
        public int CodEstado { get; set; }
        [Obrigatorio("Cidade é obrigatório")]
        public int CodCidade { get; set; }
        [Obrigatorio("Bairro é obrigatório")]
        public int CodBairro { get; set; }
        [Obrigatorio("Telefone é obrigatório")]
        public string Telefone { get; set; }
        [Obrigatorio("Responsável é obrigatório")]
        public string Responsavel { get; set; }
        public string Contato { get; set; }
        public string Email { get; set; }
        public string Observacao { get; set; }

        public EmpresaODN()
        {

        }

        public EmpresaODN(EmpresaODP odp)
        {
            this.FromODP(odp);
        }

        internal override void FromODP(ObjetoDePersistencia obj)
        {
            base.FromODP(obj);

            if(obj == null)
            {
                return;
            }

            var odp = obj as EmpresaODP;

            if (odp != null)
            {
                this.CodEmpresa = odp.CodEmpresa;
                this.NomeFantasia = odp.NomeFantasia;
                this.RazaoSocial = odp.RazaoSocial;
                this.Cnpj = odp.Cnpj;
                this.InscricaoEstadual = odp.InscricaoEstadual;
                this.CnaePrincipal = odp.CnaePrincipal;
                this.InscricaoMunicipal = odp.InscricaoMunicipal;
                this.InscricaoSuframa = odp.InscricaoSuframa;
                this.MicroEmpresa = odp.MicroEmpresa == 1;
                this.CEP = odp.CEP;
                this.Logradouro = odp.Logradouro;
                this.Complemento = odp.Complemento;
                this.Numero = odp.Numero;
                this.CodEstado = odp.CodEstado;
                this.CodCidade = odp.CodCidade;
                this.CodBairro = odp.CodBairro;
                this.Telefone = odp.Telefone;
                this.Responsavel = odp.Responsavel;
                this.Contato = odp.Contato;
                this.Email = odp.Email;
                this.Observacao = odp.Observacao;
            }
        }

        internal override ObjetoDePersistencia ToODP(ObjetoDePersistencia destino = null)
        {
            if(destino == null)
            {
                destino = new EmpresaODP();
            }

            destino = base.ToODP(destino);
            
            var odp = destino as EmpresaODP;

            odp.CodEmpresa = this.CodEmpresa;
            odp.NomeFantasia = this.NomeFantasia;
            odp.RazaoSocial = this.RazaoSocial;
            odp.Cnpj = this.Cnpj;
            odp.InscricaoEstadual = this.InscricaoEstadual;
            odp.CnaePrincipal = this.CnaePrincipal;
            odp.InscricaoMunicipal = this.InscricaoMunicipal;
            odp.InscricaoSuframa = this.InscricaoSuframa;
            odp.MicroEmpresa = this.MicroEmpresa? 1: 0;
            odp.CEP = this.CEP;
            odp.Logradouro = this.Logradouro;
            odp.Complemento = this.Complemento;
            odp.Numero = this.Numero;
            odp.CodEstado = this.CodEstado;
            odp.CodCidade = this.CodCidade;
            odp.CodBairro = this.CodBairro;
            odp.Telefone = this.Telefone;
            odp.Responsavel = this.Responsavel;
            odp.Contato = this.Contato;
            odp.Email = this.Email;
            odp.Observacao = this.Observacao;

            return odp;
        }
    }
}
