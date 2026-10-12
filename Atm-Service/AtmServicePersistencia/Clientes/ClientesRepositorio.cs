using AtmServiceInfra.EF;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmServiceInfra.Clientes
{
    public class ClientesRepositorio : Repositorio<ClienteODP>, IClientesRepositorio
    {
        public ClientesRepositorio(AtmDbContext context): base(context) { }

        public List<ClienteODP> ObterClientes(int? codCliente = 0, string nome = null, int? tipoDePessoa = null, int cnae = 0, string inscricaoEstadual = null,
            bool? microEmpresa = null, string telefone = null, string contato = null, string identidade = null, string cpfCnpj = null, 
            int[] situacaoInscricaoEstadual = null, DateTime? dataNascimento = null, int[] estadosCivis = null, int[] codUfs = null, int[] codCidades = null,
            string razaoSocial = null, string razaoSocialOuCnpj = null)
        {
            var odps = this.context.Clientes
                .Where(cli => (
                    (codCliente == 0 || cli.CodCliente == codCliente)) &&
                    (string.IsNullOrEmpty(nome) || cli.NomeCliente.ToUpper().Contains(nome.ToUpper())) &&
                    (string.IsNullOrEmpty(razaoSocial) || cli.RazaoSocial.ToUpper().Contains(razaoSocial.ToUpper())) &&
                    (string.IsNullOrEmpty(cpfCnpj) || cli.CPFCNPJ.ToUpper().Contains(cpfCnpj.ToUpper())) &&
                    (string.IsNullOrEmpty(identidade) || cli.Identidade.ToUpper().Contains(identidade.ToUpper())) &&
                    (string.IsNullOrEmpty(inscricaoEstadual) || cli.InscricaoEstadual.ToUpper().Contains(inscricaoEstadual.ToUpper())) &&
                    (tipoDePessoa == null || cli.TipoPessoa == tipoDePessoa) &&
                    (cnae == 0 || cli.CnaePrincipal == cnae) &&
                    (microEmpresa == null || cli.MicroEmpresa == microEmpresa) &&
                    (string.IsNullOrEmpty(telefone) || cli.Telefone.ToUpper().Contains(telefone.ToUpper())) &&
                    (string.IsNullOrEmpty(contato) || cli.Contato.ToUpper().Contains(contato.ToUpper())) &&
                    (situacaoInscricaoEstadual == null || situacaoInscricaoEstadual.Contains(cli.SituacaoIE)) &&
                    (dataNascimento == null || cli.DataNascimento == dataNascimento) &&
                    (estadosCivis == null || estadosCivis.Contains(cli.EstadoCivil.Value)) &&
                    (codUfs == null || codUfs.Contains(cli.CodEstado)) &&
                    (string.IsNullOrEmpty(razaoSocialOuCnpj) || 
                        cli.RazaoSocial.ToUpper().Contains(razaoSocialOuCnpj.ToUpper()) || cli.CPFCNPJ.ToUpper().Contains(razaoSocialOuCnpj.ToUpper())) &&
                    (codCidades == null || codCidades.Contains(cli.CodCidade))

                ).ToList();

            if(odps == null || odps.Count == 0)
            {
                return null;
            };

            return odps;
        }
    }
}
