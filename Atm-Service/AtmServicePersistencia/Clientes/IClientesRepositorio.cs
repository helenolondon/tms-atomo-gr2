using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceInfra.Clientes
{
    public interface IClientesRepositorio: IRepositorio<ClienteODP>
    {
        List<ClienteODP> ObterClientes(int? codCliente = 0, string nome = null, int? tiposDePessoa = null, int cnae = 0, string inscricaoEstadual = null,
            bool? microEmpresa = null, string telefone = null, string contato = null, string identidade = null, string cpfCnpj = null,
            int[] situacaoInscricaoEstadual = null, DateTime? dataNascimento = null, int[] estadosCivis = null, int[] codUfs = null, int[] codCidades = null,
            string razaoSocial = null, string razaoSocialOuCnpj = null);
    }
}
